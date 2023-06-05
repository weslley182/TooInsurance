
using DataBaseModel.Data;
using InsuranceAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Moq;

namespace InsuranceAPI.Tests;
public class InsuranceApiApplication : WebApplicationFactory<Program>
{
    private readonly Mock<IPolicyService> mock = new Mock<IPolicyService>();
    protected override IHost CreateHost(IHostBuilder builder)
    {

        var root = new InMemoryDatabaseRoot();

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            services.RemoveAll(typeof(IPolicyService));

            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("InsuranceAPIDatabase", root));
            services.AddScoped(serv => mock.Object);
        });

        return base.CreateHost(builder);
    }
}


