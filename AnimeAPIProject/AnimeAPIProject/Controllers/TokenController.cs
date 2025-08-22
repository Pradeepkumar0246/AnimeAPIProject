using AnimeAPIProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AnimeAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly AnimeContext _context;
        private readonly IConfiguration _configuration;

        public TokenController(AnimeContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Email and password are required.");

            // Validate user
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.User_Email == request.Email && u.User_Password == request.Password);

            if (user == null)
                return Unauthorized("Invalid credentials.");

            // Create claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.User_Email),
                new Claim("role", user.Role ?? "User"),
                new Claim("userId", user.User_Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Create signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Generate token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                role = user.Role,
                username = user.User_Name
            });
        }
    }

    // DTO for login
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
