using ChatApp.Entities;
using ChatApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatApp.Pages.Account;

public class RegisterConfirmationPageModel : PageModel
{
    private readonly IRepository<User> _userRepository;

    public RegisterConfirmationPageModel(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public string Email { get; set; } = string.Empty;
    
    public async Task<IActionResult> OnGetAsync(string email)
    {
        var user = await _userRepository.GetAsync(user => user.Email == email);
        if (user == null)
        {
            return NotFound($"Unable to load user with email '{email}'.");
        }
 
        Email = email;
 
        return Page();
    }
}