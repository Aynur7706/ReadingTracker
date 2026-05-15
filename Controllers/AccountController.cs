using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ReadingTracker.Models;
using ReadingTracker.Services;
using System.Security.Claims;

namespace ReadingTracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly JsonService _jsonService;

        public AccountController(JsonService jsonService)
        {
            _jsonService = jsonService;
        }

        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("/register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var users = _jsonService.GetUsers();

            bool usernameExists = users.Any(u =>
                u.Username.ToLower() == model.Username.ToLower());

            if (usernameExists)
            {
                ModelState.AddModelError("Username", "Bu username artıq mövcuddur");
                return View(model);
            }

            var newUser = new AppUser
            {
                Id = users.Count == 0 ? 1 : users.Max(u => u.Id) + 1,
                FullName = model.FullName,
                Username = model.Username,
                Password = model.Password
            };

            users.Add(newUser);
            _jsonService.SaveUsers(users);

            return RedirectToAction("Login");
        }

        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/login")]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var users = _jsonService.GetUsers();

            var user = users.FirstOrDefault(u =>
                u.Username == model.Username &&
                u.Password == model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Username və ya şifrə yanlışdır");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("FullName", user.FullName)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return RedirectToAction("Index", "Books");
        }

        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}