using DataBaseModel.Data;
using DataBaseModel.Repository;
using DataBaseModel.Repository.Interface;
using InsuranceAPI.Data;
using InsuranceAPI.Services;
using InsuranceAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPolicyService, PolicyService>();

var libraryConfig = new ConfigurationBuilder()
        .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
        .AddJsonFile("appsettings.json")
        .Build();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("DataSource=../DataBaseModel/app.db; Cache=Shared");
    options.UseSqlite(libraryConfig.GetConnectionString("InsuranceAPIConnectionRelativePath"));
});

builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<ICarInsuranceRepository, CarInsuranceRepository>();
builder.Services.AddScoped<IHomeInsuranceRepository, HomeInsuranceRepository>();
builder.Services.AddTransient<DbInitializer>();

builder.Services.AddSwaggerGen(opt =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    opt.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

var services = app.Services.CreateScope().ServiceProvider;
var initialiser = services.GetRequiredService<DbInitializer>();
initialiser.Run();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }