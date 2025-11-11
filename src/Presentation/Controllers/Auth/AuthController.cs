using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SaaS.src.Application.Interfaces.Repositories;
using SaaS.src.Domain.Entities;
using SaaS.src.Application.DTOs.Auth;


namespace SaaS.src.Presentation.Controllers.Auth
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {


        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        

        public AuthController(IUserRepository repository, IConfiguration configuration)
        {

            _repository = repository;
            _configuration = configuration;


        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var existing = await _repository.GetByIdentificationAsync(req.IdentificationNumber);
            if (existing != null) return Conflict("El número de identificación ya está registrado.");


            var user = new User
            {
                //Id = Guid.NewGuid(),
                Email = req.Email,
                IdentificationNumber = req.IdentificationNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
                RoleId = 1
            };


            await _repository.CreateAsync(user);
            var token = GenerateJwt(user);
            return Ok(new { token });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto req)
        {
            var user = await _repository.GetByIdentificationAsync(req.IdentificationNumber);
            if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return Unauthorized("Credenciales inválidas");


            var token = GenerateJwt(user);
            return Ok(new { token });
        }


        private string GenerateJwt(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("identification", user.IdentificationNumber),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty)
            };
            var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
