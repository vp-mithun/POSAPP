using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;



namespace PosAPI.Controllers
{
    [Route("posapi/api/[controller]")]
    public class AuthController : Controller
    {
        private readonly posprojectContext _context;
        private readonly IConfigurationRoot _config;

        public AuthController(posprojectContext Context, IConfigurationRoot config)
        {
            _context = Context;
            _config = config;
        }

        [HttpPost]
        [Route("token")]
        public IActionResult Get([FromBody] UserInfo query)
        {
            if (query == null)
            {
                return BadRequest();
            }
            var userList = _context.Users.Where(e => e.UserName.Equals(query.username) && e.Password.Equals(query.password));
            if (userList.ToList().Count > 0)
            {
                var user = userList.FirstOrDefault();
                var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
              new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                  issuer: _config["Tokens:Issuer"],
                  audience: _config["Tokens:Audience"],
                  claims: claims,
                  expires: DateTime.UtcNow.AddHours(12),
                  signingCredentials: creds
                  );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    loggeduserid = user.Id,
                    expiration = token.ValidTo
                });
            }
            return BadRequest("Failed to generate token");
        }
    }
    public class UserInfo
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
