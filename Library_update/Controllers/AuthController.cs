using Library_update.CoreConfig;
using Library_update.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Library_update.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UsersStore _users;
        private readonly JwtToken _jwt;

        public AuthController(UsersStore users, IConfiguration config)
        {
            _users = users;
            _users.Create("admin@gmail.com", "admin", Roles.Admin);
            _jwt = new JwtToken(config);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password are required.");
            }


            var ok = _users.Create(request.Email, request.Password, Roles.User);

            return ok
                ? Ok()
                : Conflict("User already exist");

        }

        [HttpPost("login")]
        [AllowAnonymous]

        public IActionResult Login(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var user = _users.Find(request.Email);

            if (_users.CheckPassword(user, request.Password))
            {
                return Unauthorized();
            }

            return Ok(new AuthResponse { AccessToken = _jwt.Create(user) });
        }

        [HttpPost("createAdmin")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult CreateAdmin(RegisterRequest request)
        {
            var ok = _users.Create(request.Email, request.Password, Roles.Admin);

            return ok
             ? Ok()
             : Conflict("User already exist");
        }

        public sealed record UserDTO(string Id, string Email, string Role);

        [HttpGet("getall")]
        public async Task<ActionResult<List<UserDTO>>> GetAll()
        {
           
            var users = _users.GetAll()
                              .Select(u => new UserDTO(u.Id, u.Email, u.Role))
                              .ToList();

            return Ok(await Task.FromResult(users));
        }
    }
}
