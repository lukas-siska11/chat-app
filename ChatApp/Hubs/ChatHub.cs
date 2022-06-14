using System.Security.Claims;
using ChatApp.Entities;
using ChatApp.Repositories;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace ChatApp.Hubs;

public class ChatHub : Hub
{
    private readonly IMessageRepository _messageRepository;

    public ChatHub(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task SendMessage(Guid chatRoomId, string message)
    {
        if (message == "")
        {
            return;
        }

        var messageEntity = new Message
        {
            ChatRoomId = chatRoomId,
            Content = message,
            UserId = Guid.Parse(Context.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value)
        };
        
        await _messageRepository.CreateAsync(messageEntity);

        await Clients.All.SendAsync("NewMessage", messageEntity.Id);
    }

    public async Task SendIsTypingMessage(Guid chatRoomId)
    {
        var userId = Context.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
        var userFullName = Context.User.Claims.First(claim => claim.Type == "FullName").Value;

        await Clients.All.SendAsync("IsTypingMessage", chatRoomId, JsonConvert.SerializeObject(new
        {
            UserId = userId,
            UserFullName = userFullName
        }));
    }
}