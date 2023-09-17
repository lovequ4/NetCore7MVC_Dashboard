using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dashboard.Context;
using Dashboard.Service;
using Dashboard.Models;
using Dashboard.DTO;

namespace Dashboard.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDBContext _dbContext;
        private readonly PasswordService _passwordService;

        public AuthController(AppDBContext context, PasswordService passwordService)
        {
            _dbContext = context;
            _passwordService = passwordService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user =  await _dbContext.users.FirstOrDefaultAsync(a => a.Email == loginDTO.EmailOrName || a.Name == loginDTO.EmailOrName);
            if (user == null || !_passwordService.VerifyPassword(user.Password, loginDTO.Password))
            {
                ViewBag.notice = "Invalid name or password";
            }
            else
            {
                return Redirect("/Admin/Index");
            }
            return View();
        }


        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<User>>Signup(User user) 
        {
            if (await _dbContext.users.AnyAsync(a => a.Email == user.Email || a.Name == user.Name))
            {
                return BadRequest("user already exists");
            }

            user.Password = _passwordService.HashPassword(user.Password);

            _dbContext.users.Add(user);
            await _dbContext.SaveChangesAsync();

            return View();
        }

    }
}