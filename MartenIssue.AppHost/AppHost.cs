var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("Postgres")
    .WithPgWeb();

var db = postgres.AddDatabase("DB", "compliance-mono");

var api = builder.AddProject<Projects.MartenIssue>("martenissue")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
