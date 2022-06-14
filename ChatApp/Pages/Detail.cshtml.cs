using System.ComponentModel.DataAnnotations;
using ChatApp.Entities;
using ChatApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatApp.Pages;

public class DetailPageModel : PageModel
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IMessageRepository _messageRepository;

    public DetailPageModel(IChatRoomRepository chatRoomRepository, IMessageRepository messageRepository)
    {
        _chatRoomRepository = chatRoomRepository;
        _messageRepository = messageRepository;

        Input = new InputModel();
    }
    
    [BindProperty]
    public InputModel Input { get; set; }

    public ChatRoom ChatRoom { set; get; }
    
    public IReadOnlyCollection<Message> Messages { set; get; }
    
    public class InputModel
    {
        [Required]
        public Guid ChatRoomId { get; set; }

        [Required] 
        [Display(Name = "Content")] 
        public string Content { get; set; } = string.Empty;
    }

    public async Task<IActionResult> OnGet(Guid chatRoomId)
    {
        var chatRoom = await _chatRoomRepository.GetAsync(chatRoom => chatRoom.Id == chatRoomId);
        if (chatRoom == null)
        {
            return RedirectToPage("Error");
        }

        ChatRoom = chatRoom;
        Messages = await _messageRepository.GetAllAsync(message => message.ChatRoomId == chatRoomId);

        return Page();
    }

    public async Task<IActionResult> OnGetMessage(Guid messageId)
    {
        var message = await _messageRepository.GetAsync(messageId);
        return Partial("_Message", message);
    }
}