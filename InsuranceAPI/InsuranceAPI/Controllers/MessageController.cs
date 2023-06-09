﻿using DataBaseModel.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers;

/// <summary>
/// Controller responsable for first messages of API
/// </summary>
[ApiController]
[Route(template: "v1/Message")]
public class MessageController : ControllerBase
{
    private readonly IMessageRepository _repo;
    public MessageController(IMessageRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Get all sent messages
    /// </summary>
    /// <returns>list of messages</returns>
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var messages = await _repo.GetAllAsync();
            return !messages.Any() ? NotFound() : Ok(messages);
        }
        catch (Exception ex)
        {
            return StatusCode(503, ex.Message);
        }
    }
}
