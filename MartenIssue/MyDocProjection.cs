using JasperFx.Events;
using Marten;
using Marten.Events.Projections;

namespace MartenIssue;

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
