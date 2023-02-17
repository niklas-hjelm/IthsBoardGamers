using IthsBoardGamers.DataAccess.Services;
using IthsBoardGamers.Shared.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace IthsBoardGamers.Server.Hubs;

public class ChatHub : Hub
{
    private readonly IRepository<ChatMessageDto> _messageRepository;

    public ChatHub(IRepository<ChatMessageDto> messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task BroadcastMessage(ChatMessageDto message)
    {
        await _messageRepository.AddAsync(message);
        await Clients.All.SendAsync("BroadcastMessage", message);
    }
}