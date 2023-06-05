using DataBaseModel.Model;

namespace DataBaseModel.Repository.Interface;

public interface IHomeInsuranceRepository
{
    Task<List<HomeParcelModel>> GetAllAsync();
    Task Add(HomeParcelModel car);
}
