using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LoginAPI.Models;

namespace LoginAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly DataContext _context;

        public LoginController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(user user)
        {
            try
            {
                var existingUser = _context.users.FirstOrDefault(u => u.username == user.username && u.password == user.password);

                if (existingUser != null)
                {
                    // User is authenticated
                    return Ok("Login successful!");
                }
                else
                {
                    // User not found or invalid credentials
                    return NotFound("Incorrect username or password");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
