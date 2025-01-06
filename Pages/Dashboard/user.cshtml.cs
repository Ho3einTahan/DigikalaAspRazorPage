using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize(Roles = "Admin")]
public class UserModel : PageModel
{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public List<UserWithRoles> Users { get; set; }
    public List<string> AllRoles { get; set; }

    public async Task OnGetAsync()
    {
        var users = _userManager.Users.ToList();

        Users = new List<UserWithRoles>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);

            Users.Add(new UserWithRoles
            {
                Id = user.Id,
                Username = user.UserName,
                Roles = roles.ToList()
            });
        }

        AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
    }

    public async Task<IActionResult> OnPostAsync(string UserId, string SelectedRole)
    {
        var user = await _userManager.FindByIdAsync(UserId);

        if (user != null)
        {
            await _userManager.AddToRoleAsync(user, SelectedRole);
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostRemoveRoleAsync(string UserId, string RoleToRemove)
    {
        var user = await _userManager.FindByIdAsync(UserId);


        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "کاربر یافت نشد.");
            return Page();
        }

        if (!string.IsNullOrEmpty(RoleToRemove))
        {
            await _userManager.RemoveFromRoleAsync(user, RoleToRemove);
        }
        return RedirectToPage();
    }

    public class UserWithRoles
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
    }

}