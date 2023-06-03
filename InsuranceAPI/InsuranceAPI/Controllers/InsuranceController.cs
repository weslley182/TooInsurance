using DataBaseModel.Repository.Interface;
using InsuranceAPI.Dto;
using InsuranceAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using DataBaseModel.Model;
using System;
using System.Text.Json.Serialization;

namespace InsuranceAPI.Controllers;

[ApiController]
[Route(template: "v1/Insurance")]
public class InsuranceController: ControllerBase
{
    private readonly IPolicyService _policyService;
    private readonly IMessageRepository _repo;
    public InsuranceController(IPolicyService policyService, IMessageRepository repo)
    {
        _policyService = policyService;
        _repo = repo;
    }    

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

        var newMessage = new MessageModel()
        {
            Model = json
        };
        
        var messageId = await _repo.Add(newMessage);
        policy.MessageId = messageId;
        await _policyService.SendPolicy(policy);

        return Ok();
    }
}
