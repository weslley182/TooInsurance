using DataBaseModel.Data;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Data;

public class DbInitializer
{
    private readonly AppDbContext _context;

    public DbInitializer(AppDbContext context)
    {
        _context = context;
    }

    public void Run()
    {
        _context.Database.Migrate();
    }
}
