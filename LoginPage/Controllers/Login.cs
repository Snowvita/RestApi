using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginPageController : ControllerBase
    {
        private const string Username = "Snowvita";
        private const string Password = "password";

        [HttpPost]
        public IActionResult Loginpage(User user)
        {
            if (user.Username == Username && user.Password == Password)
            {
                return StatusCode(200,"Ok");
            }

            return StatusCode(404,"Not Found");
        }
    }
}