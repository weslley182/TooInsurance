using DataBaseModel.Model;

namespace DataBaseModel.Repository.Interface;

public interface ICarInsuranceRepository
{
    Task<List<CarParcelModel>> GetAllAsync();
    Task Add(CarParcelModel car);
}
