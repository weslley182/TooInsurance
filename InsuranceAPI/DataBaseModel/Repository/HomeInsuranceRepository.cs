using DataBaseModel.Data;
using DataBaseModel.Model;
using DataBaseModel.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataBaseModel.Repository;

public class HomeInsuranceRepository : IHomeInsuranceRepository
{
    private readonly AppDbContext _context;
    public HomeInsuranceRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task Add(HomeParcelModel home)
    {
        try
        {
            var returnModel = await _context.HomeParcels.AddAsync(home);
            await _context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw new Exception("Error on create home policy: " + e.Message);
        }
    }

    public async Task<List<HomeParcelModel>> GetAllAsync()
    {
        return await _context.HomeParcels
            .AsNoTracking()
            .ToListAsync();
    }
}
