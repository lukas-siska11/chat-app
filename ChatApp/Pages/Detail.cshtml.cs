using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using ChatApp.Entities;
using ChatApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatApp.Pages;

public class DetailPageModel : PageModel
{
    private readonly IRepository<ChatRoom> _chatRoomRepository;
    private readonly IRepository<Message> _messageRepository;

    public DetailPageModel(IRepository<ChatRoom> chatRoomRepository, IRepository<Message> messageRepository)
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
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var message = new Message
        {
            Content = Input.Content,
            ChatRoomId = Input.ChatRoomId,
            UserId = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value)
        };
        await _messageRepository.CreateAsync(message);

        return RedirectToPage(new { chatRoomId = Input.ChatRoomId });
    }
}