using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using ChatApp.Entities;
using ChatApp.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatApp.Pages.Account;

[AllowAnonymous]
public class LoginPageModel : PageModel
{
    private readonly IUserRepository _userRepository;

    public LoginPageModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        Input = new InputModel();
    }

    [BindProperty] 
    public InputModel Input { get; init; }

    public class InputModel
    {
        [Required] 
        [EmailAddress] 
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }

    public async Task OnGetAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var user = await _userRepository.GetAsync(user => user.Email == Input.Email);
        if (user == null)
        {
            ModelState.AddModelError("Input.Email", "User with this Email does not exist.");

            return Page();
        }
        
        if (!BCrypt.Net.BCrypt.Verify(Input.Password, user.Password))
        {
            ModelState.AddModelError("Input.Password", "Incorrect Password");

            return Page();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Email),
            new("Name", user.Name),
            new("Surname", user.Surname),
            new("FullName", user.Name + " " + user.Surname),
            new("AvatarColor", user.AvatarColor)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties {IsPersistent = true}
        );

        return LocalRedirect(Url.Content("~/"));
    }
}