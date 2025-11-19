namespace MartenIssue;

public record MyStream(Guid Id, bool Updated)
{
    public static MyStream Create(MyCreated @event) => new(@event.Id, false);
    public static MyStream Apply(MyStream a, MyUpdated @event) => a with { Updated = true };
}

public record MyCreated(Guid Id);
public record MyUpdated(Guid Id);

