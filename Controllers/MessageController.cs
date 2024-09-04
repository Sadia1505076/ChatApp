using ChatApi.Context;
using ChatApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly ILogger<MessageController> _logger;
    private readonly ChatApiDbContext _context;

    public MessageController(ILogger<MessageController> logger, ChatApiDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("GetAllMessagesOfAChat")]
    public IEnumerable<Message> GetAllMessagesOfAChat(Conversation conversation)
    {
        return [.. _context.Messages.Where(m => m.Conversation == conversation)];
    }
}
