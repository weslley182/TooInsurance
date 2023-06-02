using DataBaseModel.Data;
using DataBaseModel.Repository;
using DataBaseModel.Repository.Interface;

using InsuranceAPI.Services;
using InsuranceAPI.Services.Interface;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPolicyService, PolicyService>();

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
