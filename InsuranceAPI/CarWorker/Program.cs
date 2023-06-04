using CarWorker;
using DataBaseModel.Data;
using DataBaseModel.Repository.Interface;
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
        services.AddScoped<ICarInsuranceRepository, ICarInsuranceRepository>();
    })
    .Build();

await host.RunAsync();
