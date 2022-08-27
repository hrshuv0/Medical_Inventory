using Medical_Inventory.Data;
using Medical_Inventory.Models.ViewModel.AuthViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Inventory.Controllers;

    public class AuthController : Controller
    {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is not null)
            {
                TempData["Error"] = "User name already exists";
                return View(model);
            }

            var newUser = new IdentityUser()
            {
                UserName = model.UserName
            };
            var response = await _userManager.CreateAsync(newUser, model.Password);

            if (!response.Succeeded)
            {
                TempData["Error"] = "User registration failed";
                return View(model);
            }
            await _userManager.AddToRoleAsync(newUser, StaticData.RoleUser);

            var result = await _signInManager.PasswordSignInAsync(newUser, model.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["Error"] = "Something wrong, trying again later";
            return View(model);
        }



    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVm model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user is not null)
        {
            var checkCredentials = await _userManager.CheckPasswordAsync(user, model.Password);
            if (checkCredentials)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index), "Home");
            }
        }

        TempData["Error"] = "wrong credentials, please try again";
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Index), "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }


}

