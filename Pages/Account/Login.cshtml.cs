using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

public class LoginModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public LoginModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [BindProperty]
    public LoginInputModel Input { get; set; }

    public string ErrorMessage { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Check if the user exists
        var user = await _userManager.FindByNameAsync(Input.Username);

        if (user == null)
        {
            ErrorMessage = "نام کاربری یا کلمه عبور اشتباه است.";
            return Page();
        }

        // Log in the user
        var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, true, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return RedirectToPage("/Index");
        }

        if (result.IsLockedOut)
        {
            ErrorMessage = "حساب کاربری شما قفل شده است.";
            return Page();
        }

        ErrorMessage = "نام کاربری یا کلمه عبور اشتباه است.";
        return Page();
    }

    public class LoginInputModel
    {
        [Required(ErrorMessage = "نام کاربری الزامی است.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "کلمه عبور الزامی است.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}