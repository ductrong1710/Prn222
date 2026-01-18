using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[ApiController]
[Route("api/messages")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _service;
    private readonly IHubContext<ChatHub> _hub;
    private readonly FirebaseService _firebase;

    public MessageController(
        IMessageService service,
        IHubContext<ChatHub> hub,
        FirebaseService firebase)
    {
        _service = service;
        _hub = hub;
        _firebase = firebase;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_service.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> Post(string content)
    {
        _service.Create(content);

        await _hub.Clients.All.SendAsync("ReceiveMessage", content);
        _firebase.Send(content);

        return Ok();
    }
}
