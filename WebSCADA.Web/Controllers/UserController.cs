using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebSCADA.BLL.Interfaces;
using WebSCADA.Domain.Models;
using WebSCADA.Web.Authorization;

namespace WebSCADA.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenFactory tokenFactory;
        private readonly IUserService userService;

        public UserController(ITokenFactory tokenFactory, IUserService userService)
        {
            this.tokenFactory = tokenFactory;
            this.userService = userService;
        }

        [HttpPost("token")]
        public IActionResult Token([FromBody]UserDomain credentials)
        {
            var user = userService.Get(credentials.Login, credentials.Password);
            if (user == null)
            {
                return BadRequest("Invalid credentials.");
            }

            return Ok(GenerateTokenForUser(user));
        }

        [HttpPost("token")]
        public IActionResult Add([FromBody]UserDomain newUser)
        {
            if (newUser != null)
            {
                return BadRequest("Invalid data.");
            }

            userService.Add(newUser);

            return Ok(GenerateTokenForUser(newUser));
        }

        private string GenerateTokenForUser(UserDomain user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            claims.Add(new Claim(ClaimTypes.Role, user.Role));

            return tokenFactory.Create(claims);
        }
    }
}