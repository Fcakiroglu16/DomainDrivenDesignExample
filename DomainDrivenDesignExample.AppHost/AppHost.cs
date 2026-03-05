using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<DomainDrivenDesignExample_API>("domaindrivendesignexample");

builder.Build().Run();