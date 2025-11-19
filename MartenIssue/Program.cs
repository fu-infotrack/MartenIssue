using JasperFx.Events.Projections;
using Marten;
using MartenIssue;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDataSource("DB");

builder.Services.AddMarten(options =>
{
    options.AutoCreateSchemaObjects = JasperFx.AutoCreate.All;
    options.Projections.Add<MyDocProjection>(ProjectionLifecycle.Async);
})
.UseLightweightSessions()
.UseNpgsqlDataSource()
.AddAsyncDaemon(JasperFx.Events.Daemon.DaemonMode.HotCold);

builder.Services.AddHostedService<MyBackgroundService>();

var app = builder.Build();

app.MapDefaultEndpoints();

app.Run();