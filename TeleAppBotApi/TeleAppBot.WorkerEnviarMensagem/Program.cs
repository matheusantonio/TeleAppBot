using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeleAppBot.CrossCutting.Extensions.DependencyInjection;
using TeleAppBot.WorkerEnviarMensagem;

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

        services.AdicionarServicos(configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();