using DataBaseModel.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers;

/// <summary>
/// Controller responsable for first messages of API
/// </summary>
[ApiController]
[Route(template: "v1/HomeParcels")]
public class HomeParcelsController : ControllerBase
{
    private readonly IHomeInsuranceRepository _repo;
    public HomeParcelsController(IHomeInsuranceRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Get all home parcels
    /// </summary>
    /// <returns>list of home parcels</returns>
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var parcels = await _repo.GetAllAsync();
            return !parcels.Any() ? NotFound() : Ok(parcels);
        }
        catch (Exception ex)
        {
            return StatusCode(503, ex.Message);
        }
    }

    /// <summary>
    /// Get home parcels
    /// </summary>
    /// <param name="id">id from messageId</param>
    /// <returns>List of home parcels from message</returns>
    [HttpGet]
    [Route("GetByMessageId/{id}")]
    public async Task<ActionResult> GetByMessageId([FromRoute] int id)
    {
        try
        {
            var homes = await _repo.GetAllAsync();
            var home = homes.Where(h => h.MessageId == id);
            return home == null ? NotFound() : Ok(home);
        }
        catch (Exception ex)
        {
            return StatusCode(503, ex.Message);
        }
    }
}
