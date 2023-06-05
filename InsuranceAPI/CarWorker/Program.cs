using CarWorker;
using CarWorker.Services;
using CarWorker.Services.Interface;
using DataBaseModel.Data;
using DataBaseModel.Repository;
using DataBaseModel.Repository.Interface;
using MassTransit;
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
        
        services.AddTransient<ICarInsuranceRepository, CarInsuranceRepository>();

        //services.AddMassTransit(config => {
        //    config.AddConsumer<ConsumerService>();
            
        //    config.UsingRabbitMq((ctx, cfg) => {
        //        cfg.Host(new Uri("rabbitmq://guest:guest@localhost:5672"));
        //        cfg.ReceiveEndpoint("car-insurance-queue", e => {
        //            e.ConfigureConsumeTopology = false;
        //            e.Consumer<ConsumerService>(ctx);
        //        });                
        //    });
        //});
    })
    .Build();

await host.RunAsync();
