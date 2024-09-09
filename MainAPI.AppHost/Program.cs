var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MainAPI>("mainapi");
builder.AddProject<Projects.FrontEnd>("frontecn");

builder.AddProject<Projects.CustomerAPI>("customerapi");

builder.AddProject<Projects.OrdersAPI>("ordersapi");

builder.Build().Run();
