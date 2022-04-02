using System.ComponentModel.DataAnnotations;
using ChatApp.Entities;
using ChatApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatApp.Pages.Account;

[AllowAnonymous]
public class RegisterPageModel : PageModel
{
    private readonly IRepository<User> _userRepository;

    public RegisterPageModel(IRepository<User> userRepository)
    {
        _userRepository = userRepository;

        Input = new InputModel();
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required] 
        [Display(Name = "Name")] 
        public string Name { get; set; } = string.Empty;

        [Required] 
        [Display(Name = "Surname")] 
        public string Surname { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var user = await _userRepository.GetAsync(user => user.Email == Input.Email);
        if (user != null)
        {
            ModelState.AddModelError("Input.Email", "Email " + Input.Email + " already exists");

            return Page();
        }

        user = new User
        {
            Name = Input.Name,
            Surname = Input.Surname,
            Email = Input.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(Input.Password)
        };
        await _userRepository.CreateAsync(user);

        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
    }
}