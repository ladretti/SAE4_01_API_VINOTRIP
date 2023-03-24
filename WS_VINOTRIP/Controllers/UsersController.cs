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
    [Route("api/[controller]")]
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

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var sejour = dataRepository.GetByIdAsync(id).Result;

            if (sejour == null)
            {
                return NotFound();
            }

            return sejour;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.PersonneId)
            {
                return BadRequest();
            }

            var sejourToUpdate = dataRepository.GetByIdAsync(id);

            if (sejourToUpdate == null)
            {
                return NotFound();
            }

            else
            {
                dataRepository.UpdateAsync(sejourToUpdate.Result.Value, user);
                return NoContent();
            }
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dataRepository.AddAsync(user);

            return CreatedAtAction("PostUser", new { id = user.PersonneId }, user); // GetById : nom de l’action
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
        public async Task<IActionResult> DeleteUser(int id)
        {
            var sejour = dataRepository.GetByIdAsync(id);

            if (sejour == null)
            {
                return NotFound();
            }

            dataRepository.DeleteAsync(sejour.Result.Value);

            return NoContent();
        }

        /* private bool UserExists(int id)
         {
             return dataRepository.Any(e => e.PersonneId == id);
         }*/
        }
    }
