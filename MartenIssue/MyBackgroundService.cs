using Marten;

namespace MartenIssue;

public class MyBackgroundService(IServiceProvider sp) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = sp.CreateScope();
        var documentSession = scope.ServiceProvider.GetRequiredService<IDocumentSession>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<MyBackgroundService>>();

        // simply start a new stream
        var id = Guid.NewGuid();
        documentSession.Events.StartStream<MyStream>(id, new MyCreated(id));
        await documentSession.SaveChangesAsync(stoppingToken);

        // polling for the async projection
        while (!stoppingToken.IsCancellationRequested)
        {
            var mydoc = await documentSession.LoadAsync<MyDoc>(id, stoppingToken);

            if (mydoc != null)
            {
                logger.LogInformation("Projection completed!");
                break;
            }

            logger.LogInformation("Waiting for projection...");
            await Task.Delay(1000, stoppingToken);
        }
    }
}
