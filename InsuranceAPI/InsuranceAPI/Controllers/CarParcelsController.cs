using DataBaseModel.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers;

[ApiController]
[Route(template: "v1/CarParcels")]
public class CarParcelsController : ControllerBase
{
    private readonly ICarInsuranceRepository _repo;
    public CarParcelsController(ICarInsuranceRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var parcels = await _repo.GetAllAsync();
        return !parcels.Any() ? NotFound() : Ok(parcels);
    }

    [HttpGet]
    [Route("GetByMessageId/{id}")]
    public async Task<ActionResult> GetByMessageId([FromRoute] int id)
    {
        var cars = await _repo.GetAllAsync();
        var car = cars.Where(h => h.MessageId == id);
        return car == null ? NotFound() : Ok(car);
    }
}
