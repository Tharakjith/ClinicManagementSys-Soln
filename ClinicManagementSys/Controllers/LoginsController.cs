using ClinicManagementSys.Model;
using ClinicManagementSys.Repository;
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
    { //Get Configurations from appsettings-secretKey
        private readonly IConfiguration _config;
        private readonly ILoginRepository _loginRepository;
        //DI
        public LoginsController(IConfiguration config, ILoginRepository loginRepository)
        {
            _config = config;

            _loginRepository = loginRepository;

        }
        #region validate username and password


        
        [AllowAnonymous] // this refers to while login donot check token  number check only username and password for authorization
        [HttpGet("{username}/{userpass}")]
        public async Task<IActionResult> LoginCredential(string username, string userpass)
        {
            IActionResult response = Unauthorized(); //401 error
            LoginRegistration validUser = null;

            // 1  -Authenticate the user by passing username and password
            validUser = await _loginRepository.ValidateUsers(username, userpass);

            //2 generate jwt token
            if (validUser != null)
            {
                var tokenString = GenerateJWTToken(validUser);
                response = Ok(new
                {
                    Uname = validUser.Username,
                    roleId = validUser.RoleId,
                    token = tokenString
                });
            }
            return response;
        }

        #endregion
        #region Generate JWT token
        private string GenerateJWTToken(LoginRegistration validUser)
        {
            //1-Secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //2-algorithm
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            //JWT
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], null, expires: DateTime.Now.AddMinutes(20), signingCredentials: credentials);
            ///4-writing token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

    }
}
