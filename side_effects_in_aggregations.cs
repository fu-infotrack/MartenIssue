using JasperFx.Core;
using JasperFx.Events;
using JasperFx.Events.Projections;
using Marten;
using Marten.Events.Projections;
using Testcontainers.PostgreSql;

namespace MartenIssueTests;

public class side_effects_in_aggregations
{
    [Fact]
    public async Task add_events_multi_stream()
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;

        var postgreSqlContainer = new PostgreSqlBuilder().Build();
        await postgreSqlContainer.StartAsync(cancellationToken);
        var conStr = postgreSqlContainer.GetConnectionString();

        using var store = DocumentStore.For(opts =>
        {
            opts.Connection(conStr);
            opts.Projections.Add<MyDocProjection>(ProjectionLifecycle.Async);
        });

        await store.Advanced.Clean.DeleteAllEventDataAsync(cancellationToken);
        await store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(MyDoc), cancellationToken);

        var daemon = await store.BuildProjectionDaemonAsync();
        await daemon.StartAllAsync();

        var id = Guid.NewGuid();

        // Act
        using (var session = store.LightweightSession())
        {
            session.Events.StartStream<MyStream>(id, new MyCreated(id));
            await session.SaveChangesAsync(cancellationToken);
        }

        await daemon.WaitForNonStaleData(10.Seconds());

        await using var query = store.QuerySession();
        var doc = await query.LoadAsync<MyDoc>(id, cancellationToken);
        var myUpdated = await query.Events.QueryRawEventDataOnly<MyUpdated>().FirstOrDefaultAsync(cancellationToken);

        // Assert
        Assert.NotNull(doc);
        Assert.NotNull(myUpdated);
    }
}

public record MyStream(Guid Id, bool Updated)
{
    public static MyStream Create(MyCreated @event) => new(@event.Id, false);
    public static MyStream Apply(MyStream a, MyUpdated @event) => a with { Updated = true };
}

public record MyCreated(Guid Id);
public record MyUpdated(Guid Id);

public record MyDoc(Guid Id)
{
    public static MyDoc Create(MyCreated @event) => new(@event.Id);
}

public class MyDocProjection : MultiStreamProjection<MyDoc, Guid>
{
    public MyDocProjection()
    {
        Identity<MyCreated>(e => e.Id);
    }

    public override ValueTask RaiseSideEffects(IDocumentOperations operations, IEventSlice<MyDoc> slice)
    {
        slice.AppendEvent(new MyUpdated(slice.Snapshot!.Id));
        return ValueTask.CompletedTask;
    }
}
