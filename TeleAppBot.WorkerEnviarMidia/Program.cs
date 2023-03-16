using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeleAppBot.CrossCutting.Extensions.DependencyInjection;
using TeleAppBot.WorkerEnviarMidia;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

        var configuration = builder.Build();

        services.AdicionarServicos(configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();