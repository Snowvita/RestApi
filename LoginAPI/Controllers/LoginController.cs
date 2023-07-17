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

        /*[HttpPost]
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
        }*/

        [HttpPost]
        public IActionResult Login(user user)
        {
            try
            {
                var existingUser = _context.users.FirstOrDefault(u => u.username == user.username);

                if (existingUser != null)
                {
                    // User already exists, check if the password is correct
                    if (existingUser.password == user.password)
                    {
                        // User is authenticated
                        return Ok("Login successful!");
                    }
                    else
                    {
                        // Incorrect password
                        return NotFound("Incorrect password");
                    }
                }
                else
                {
                    // User doesn't exist, create a new user
                    var newUser = new user
                    {
                        username = user.username,
                        password = user.password
                    };

                    _context.users.Add(newUser);
                    _context.SaveChanges();

                    return Ok("User created and logged in successfully!");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            try
            {
                var user = _context.users.FirstOrDefault(u => u.username == username);

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        public class UpdatePasswordRequest
        {
            public string ?NewPassword { get; set; }
        }

        /*[HttpPatch("{username}")]
        public IActionResult UpdatePassword(string username, [FromBody] UpdatePasswordRequest newPasswordRequest)
        {
            try
            {
                var existingUser = _context.users.FirstOrDefault(u => u.username == username);

                if (existingUser != null)
                {
                    // Update the password
                    existingUser.password = newPasswordRequest.NewPassword;
                    _context.SaveChanges();

                    return Ok("Password updated successfully");
                }
                else
                {
                    // User not found
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{username}")]
        public IActionResult DeleteUser(string username)
        {
            try
            {
                var existingUser = _context.users.FirstOrDefault(u => u.username == username);

                if (existingUser != null)
                {
                    // Remove the user from the context and save changes
                    _context.users.Remove(existingUser);
                    _context.SaveChanges();

                    return Ok("User deleted successfully");
                }
                else
                {
                    // User not found
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }*/
    }
}
