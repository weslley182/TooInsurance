using DataBaseModel.Data;
using HomeWorker;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var libraryConfig = new ConfigurationBuilder()
        .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
        .AddJsonFile("appsettings.json")
        .Build();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(libraryConfig.GetConnectionString("InsuranceAPIConnectionRelativePath"));
        });
    })
    .Build();

await host.RunAsync();