using DataBaseModel.Model;
using DataBaseModel.Repository.Interface;
using InsuranceAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLib.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InsuranceAPI.Controllers;

/// <summary>
/// Controller responsable for sent messages for exchange and data
/// </summary>
[ApiController]
[Route(template: "v1/Insurance")]
public class InsuranceController : ControllerBase
{
    private readonly IPolicyService _policyService;
    private readonly IMessageRepository _repo;
    public InsuranceController(IPolicyService policyService, IMessageRepository repo)
    {
        _policyService = policyService;
        _repo = repo;
    }

    /// <summary>
    /// Post Policy on exchange
    /// </summary>
    /// <param name="policy">policy data with product 222 or 111</param>
    /// <returns>action result</returns>
    [HttpPost]
    public async Task<ActionResult> SendInsuranceAsync([FromBody] PolicyDto policy)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var json = JsonSerializer.Serialize(policy,
            new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

        var newMessage = new MessageModel() { Model = json };

        try
        {
            var messageId = await _repo.Add(newMessage);
            policy.MessageId = messageId;
            await _policyService.SendPolicy(policy);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

        return Ok();
    }
}
