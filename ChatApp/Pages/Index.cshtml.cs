﻿using ChatApp.Entities;
using ChatApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatApp.Pages;

[Authorize]
public class IndexPageModel : PageModel
{
    private readonly IRepository<ChatRoom> _chatRoomRepository;

    public IndexPageModel(IRepository<ChatRoom> chatRoomRepository)
    {
        _chatRoomRepository = chatRoomRepository;
    }
    
    public IReadOnlyCollection<ChatRoom> ChatRooms { set; get; }

    public async Task<IActionResult> OnGet()
    {
        ChatRooms = await _chatRoomRepository.GetAllAsync();

        return Page();
    }
}