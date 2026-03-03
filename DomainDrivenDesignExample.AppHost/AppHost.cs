var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.DomainDrivenDesignExample_API>("domaindrivendesignexample");

builder.Build().Run();
