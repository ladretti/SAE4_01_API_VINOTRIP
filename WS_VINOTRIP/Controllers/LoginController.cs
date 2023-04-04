using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDataRepository<User> dataRepository;

        public LoginController(IConfiguration config, IDataRepository<User> dataRepo)
        {
            _config = config;
            dataRepository = dataRepo;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(String pseudo, String mdp)
        {
            IActionResult response = Unauthorized();
            User user = AuthenticateUser(pseudo, mdp);
            if (user != null)
            {
                var tokenString = GenerateJwtToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }


        private User AuthenticateUser(String pseudo, String mdp)
        {
            var listUsers = dataRepository.GetAllAsync().Result;
            return listUsers.Value.FirstOrDefault(x => x.Pseudo.ToUpper() == pseudo.ToUpper() && x.Mdp == mdp);
        }


        private string GenerateJwtToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Pseudo),
                new Claim("pseudo", userInfo.Pseudo.ToString()),
                new Claim("role",userInfo.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
