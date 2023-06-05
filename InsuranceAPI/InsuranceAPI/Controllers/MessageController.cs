using DataBaseModel.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers;

[ApiController]
[Route(template: "v1/Message")]
public class MessageController : ControllerBase
{
    private readonly IMessageRepository _repo;
    public MessageController(IMessageRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var messages = await _repo.GetAllAsync();
        return !messages.Any() ? NotFound() : Ok(messages);
    }
}
