using DataBaseModel.Repository.Interface;
using InsuranceAPI.Dto;
using InsuranceAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using DataBaseModel.Model;

namespace InsuranceAPI.Controllers;

[ApiController]
[Route(template: "v1/Isurance")]
public class InsuranceController: ControllerBase
{
    private readonly IPolicyService _policyService;
    private readonly IMessageRepository _repo;
    public InsuranceController(IPolicyService policyService, IMessageRepository repo)
    {
        _policyService = policyService;
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var movies = await _repo.GetAllAsync();
        return !movies.Any() ? NotFound() : Ok(movies);
    }

    [HttpPost]
    public async Task<ActionResult> SendInsuranceAsync([FromBody] Policy policy)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var newMessage = new MessageModel()
        {
            Model = JsonSerializer.Serialize(policy)
        };
        

        //var returnId = await _repo.Add(newMessage);
        await _repo.Add(newMessage);
        await _policyService.SendPolicy(policy);

        return Ok();
    }
}
