using DataBaseModel.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers;

/// <summary>
/// Controller responsable for search car parcels
/// </summary>
[ApiController]
[Route(template: "v1/CarParcels")]
public class CarParcelsController : ControllerBase
{
    private readonly ICarInsuranceRepository _repo;
    public CarParcelsController(ICarInsuranceRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Get all car parcels on database
    /// </summary>
    /// <returns>list of car parcels</returns>
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var parcels = await _repo.GetAllAsync();
        return !parcels.Any() ? NotFound() : Ok(parcels);
    }
    
    /// <summary>
    /// Get car parcels
    /// </summary>
    /// <param name="id">id from messageId</param>
    /// <returns>List of car parcels from message</returns>
    [HttpGet]
    [Route("GetByMessageId/{id}")]
    public async Task<ActionResult> GetByMessageId([FromRoute] int id)
    {
        var cars = await _repo.GetAllAsync();
        var car = cars.Where(h => h.MessageId == id);
        return car == null ? NotFound() : Ok(car);
    }
}
