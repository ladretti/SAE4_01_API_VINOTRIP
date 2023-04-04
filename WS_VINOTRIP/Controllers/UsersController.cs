using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDataRepository<User> dataRepository;
        private readonly IDataRepository<Personne> dataRepositoryPersonne;

        public UsersController(IDataRepository<User> dataRepo, IDataRepository<Personne> dataRepoPersonne)
        {
            dataRepository = dataRepo;
            dataRepositoryPersonne = dataRepoPersonne;
        }

        [HttpGet]
        [Route("GetUserData")]
        [Authorize(Policy = Policies.User)]
        public IActionResult GetUserData()
        {
            return Ok("This is a response from user method");
        }
        [HttpGet]
        [Route("GetAdminData")]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult GetAdminData()
        {
            return Ok("This is a response from Admin method");
        }

        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await dataRepository.GetAllAsync();

            if (users == null)
            {
                return NotFound();
            }
            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await dataRepository.GetByIdAsync(id);
            await dataRepositoryPersonne.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        // GET: api/Users/5
        [HttpGet("{pseudo}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> GetUserByPseudo(string pseudo)
        {
            var user = await dataRepository.GetByStringAsync(pseudo);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.PersonneId)
            {
                return BadRequest();
            }

            var userToUpdate = await dataRepository.GetByIdAsync(id);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, user);
                return NoContent();
            }
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(user);

            return CreatedAtAction("GetUserById", new { id = user.PersonneId }, user); // GetUserById : nom de l’action
            /*{
                "pseudo": "Cessouille",
                "tel": "0782602628",
                "newsletter": true,
                "estVerifie": true,
                "role": "user",
                "dateConnexion": "2023-03-24T08:37:36.337Z",
                "titre": "M.",
                "prenom": "Célian",
                "dateNaissance": "2003-08-07",
                "mdp": "oui",
                "resideUser": [],
                "personneUser": null
            }*/
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await dataRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(user.Value);

            return NoContent();
        }
    }
}
