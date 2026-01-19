using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Presentation.Hubs;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly IFirebaseService _firebaseService;
    private readonly IHubContext<ChatHub> _hubContext;

    public MessagesController(
        IMessageService messageService,
        IFirebaseService firebaseService,
        IHubContext<ChatHub> hubContext)
    {
        _messageService = messageService;
        _firebaseService = firebaseService;
        _hubContext = hubContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<MessageDto>>> GetAll()
    {
        var messages = await _messageService.GetAllMessagesAsync();
        return Ok(messages);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MessageDto>> GetById(int id)
    {
        var message = await _messageService.GetMessageByIdAsync(id);
        if (message == null)
            return NotFound();

        return Ok(message);
    }

    [HttpPost]
    public async Task<ActionResult<MessageDto>> Create([FromBody] CreateMessageDto dto)
    {
        var message = await _messageService.CreateMessageAsync(dto);

        // Broadcast via SignalR
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);

        // Send Firebase notification (optional - to topic "messages")
        try
        {
            await _firebaseService.SendNotificationToTopicAsync(
                "messages",
                $"New message from {message.User}",
                message.Content);
        }
        catch (Exception ex)
        {
            // Log but don't fail the request if Firebase notification fails
            Console.WriteLine($"Firebase notification failed: {ex.Message}");
        }

        return CreatedAtAction(nameof(GetById), new { id = message.Id }, message);
    }
}
