var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.DomainDrivenDesignExample>("domaindrivendesignexample");

builder.Build().Run();
