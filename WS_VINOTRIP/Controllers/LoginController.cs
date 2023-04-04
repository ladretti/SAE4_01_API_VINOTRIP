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
        private readonly IDataRepository<Personne> dataRepositoryPersonne;

        public LoginController(IConfiguration config, IDataRepository<User> dataRepo, IDataRepository<Personne> dataRepoPersonne)
        {
            _config = config;
            dataRepository = dataRepo;
            dataRepositoryPersonne = dataRepoPersonne;
        }

        /// <summary>
        /// Fonction HTTP POST qui permet de s'authentifier avec un pseudo et un mot de passe
        /// </summary>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        /// <param name="mdp">Mot de passe de l'utilisateur</param>
        /// <returns>Renvoie un token JWT pour l'utilisateur si l'authentification réussit, sinon renvoie une réponse Unauthorized</returns>

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

        /// <summary>
        /// Fonction qui permet d'authentifier un utilisateur avec son pseudo et son mot de passe
        /// </summary>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        /// <param name="mdp">Mot de passe de l'utilisateur</param>
        /// <returns>Retourne l'utilisateur correspondant au pseudo et mot de passe fournis</returns>

        private User AuthenticateUser(String pseudo, String mdp)
        {
            var listUsers = dataRepository.GetAllAsync().Result;
            dataRepositoryPersonne.GetAllAsync();
            return listUsers.Value.FirstOrDefault(x => x.Pseudo.ToUpper() == pseudo.ToUpper() && x.Mdp == mdp);
        }

        /// <summary>
        /// Fonction qui permet de générer un token JWT pour un utilisateur authentifié
        /// </summary>
        /// <param name="userInfo">Informations de l'utilisateur</param>
        /// <returns>Renvoie le token JWT pour l'utilisateur authentifié</returns>

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
