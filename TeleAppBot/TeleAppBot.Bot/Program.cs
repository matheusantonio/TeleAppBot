using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeleAppBot.Bot;
using TeleAppBot.Bot.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(context.HostingEnvironment.ContentRootPath)
            .AddEnvironmentVariables();

        if (context.HostingEnvironment.IsDevelopment())
            builder = builder.AddJsonFile("appsettings.Development.json");

        else
            builder = builder.AddJsonFile("appsettings.json");

        var configuration = builder.Build();

        services.ConfigureServices(configuration);

        services.AddHostedService<Bot>();
    })
    .Build();

await host.RunAsync();

