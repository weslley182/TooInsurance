using DataBaseModel.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers;

[ApiController]
[Route(template: "v1/HomeParcels")]
public class HomeParcelsController : ControllerBase
{
    private readonly IHomeInsuranceRepository _repo;
    public HomeParcelsController(IHomeInsuranceRepository repo)
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
        var homes = await _repo.GetAllAsync();
        var home = homes.Where(h => h.MessageId == id);        
        return home == null ? NotFound() : Ok(home);
    }
}
