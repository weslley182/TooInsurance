using DataBaseModel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DataBaseModel.Data;

public class AppDbContext: DbContext
{    
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
        
    }
    
    public DbSet<MessageModel> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("InsuranceAPIConnection");
            optionsBuilder.UseSqlite(connectionString);
        }
        //optionsBuilder.UseSqlite("DataSource=app.db; Cache=Shared");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
