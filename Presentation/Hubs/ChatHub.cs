using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.Hubs;

public class ChatHub : Hub
{
    private readonly IMessageService _messageService;

    public ChatHub(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task SendMessage(string user, string content)
    {
        var dto = new CreateMessageDto
        {
            User = user,
            Content = content
        };

        var message = await _messageService.CreateMessageAsync(dto);

        // Broadcast to all connected clients
        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    public async Task GetAllMessages()
    {
        var messages = await _messageService.GetAllMessagesAsync();
        await Clients.Caller.SendAsync("LoadMessages", messages);
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        Console.WriteLine($"Client connected: {Context.ConnectionId}");
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
        Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
    }
}
