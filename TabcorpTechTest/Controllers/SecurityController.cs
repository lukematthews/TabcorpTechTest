using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TabcorpTechTest.Data;
using TabcorpTechTest.Models.Db;

namespace TabcorpTechTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private ApiContext _context;
        private readonly IConfiguration Configuration;

        public SecurityController(ApiContext context, IConfiguration configuration)
        {
            _context = context;
            this.Configuration = configuration;
        }
        
        // POST api/<CustomerController>
        [HttpPost("createToken"), AllowAnonymous]
        public IResult Post([FromBody] User user)
        {
            user = (from u in _context.Users 
                    where u.UserName == user.UserName && u.Password == user.Password 
                    select u)
                    .FirstOrDefault();
            if (user != null)
            {
                var issuer = Configuration["Jwt:Issuer"];
                var audience = Configuration["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
            }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return Results.Ok(stringToken);
            }
            return Results.Unauthorized();
        }

    }
}
