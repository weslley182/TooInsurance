using DataBaseModel.Data;
using DataBaseModel.Model;
using DataBaseModel.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataBaseModel.Repository;

public class CarInsuranceRepository : ICarInsuranceRepository
{
    private readonly AppDbContext _context;
    public CarInsuranceRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task Add(CarParcelModel car)
    {
        try
        {
            var returnModel = await _context.CarParcels.AddAsync(car);
            await _context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw new Exception("Error on create car policy: " + e.Message);
        }
    }

    public async Task<List<CarParcelModel>> GetAllAsync()
    {
        return await _context.CarParcels
            .AsNoTracking()
            .ToListAsync();
    }
}
