using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeleAppBot.CrossCutting.Extensions.DependencyInjection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AdicionarServicos(context.Configuration);
    })
    .Build();

await host.RunAsync();