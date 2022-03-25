using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatApp.Pages.Account;

[AllowAnonymous]
public class LogoutPageModel : PageModel
{
    public void OnGet()
    {
    }
 
    public async Task<IActionResult> OnPost(string? returnUrl = null)
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (returnUrl != null)
        {
            return LocalRedirect(returnUrl);
        }
        
        return RedirectToPage();
    }
}