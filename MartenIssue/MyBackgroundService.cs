using Marten;

namespace MartenIssue;

public class MyBackgroundService(IServiceProvider sp) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // simply start a new stream
        using var scope = sp.CreateScope();
        var documentSession = scope.ServiceProvider.GetRequiredService<IDocumentSession>();
        var id = Guid.NewGuid();
        documentSession.Events.StartStream<MyStream>(id, new MyCreated(id));
        await documentSession.SaveChangesAsync(stoppingToken);
    }
}
