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
    private readonly IRepository<User> _userRepository;

    public LoginPageModel(IRepository<User> userRepository)
    {
        _userRepository = userRepository;

        Input = new InputModel();
    }

    [BindProperty] 
    public InputModel Input { get; init; }

    public string? ReturnUrl { get; set; }

    public class InputModel
    {
        [Required] 
        [EmailAddress] 
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }

    public async Task OnGetAsync(string? returnUrl = null)
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        returnUrl ??= Url.Content("~/");
        var user = await _userRepository.GetAsync(user => user.Email == Input.Email && user.Password == Input.Password);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid Email or Password");

            return Page();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Email),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties {IsPersistent = true}
        );

        return LocalRedirect(returnUrl);
    }
}