using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
using ClinicManagementSys.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicManagementSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILoginRepository _loginRepository;

        public LoginsController(IConfiguration config, ILoginRepository loginRepository)
        {
            _config = config;
            _loginRepository = loginRepository;
        }

        [AllowAnonymous]
        [HttpGet("{username}/{userpass}")]
        public async Task<IActionResult> LoginCredential(string username, string userpass)
        {
            IActionResult response = Unauthorized();
            var validUser = await _loginRepository.ValidateUsers(username, userpass);

            if (validUser != null)
            {
                var tokenString = GenerateJWTToken(validUser);
                response = Ok(new
                {
                    Username = validUser.Username,
                    RoleId = validUser.RoleId,
                    Token = tokenString,
                    DoctorId = validUser.DoctorId // Include DoctorId in response
                });
            }
            return response;
        }

        private string GenerateJWTToken(LoginRegistrationViewModel validUser)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
