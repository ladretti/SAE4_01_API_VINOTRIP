using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonnesController : ControllerBase
    {
        private readonly IDataRepository<Personne> dataRepository;

        public PersonnesController(IDataRepository<Personne> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Personnes
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Personne>>> GetPersonnes()
        {
            var personnes = await dataRepository.GetAllAsync();

            if (personnes == null)
            {
                return NotFound();
            }
            return personnes;
        }

        // GET: api/Personnes/5
        [HttpGet("{id}")]
        [ActionName("GetPersonneById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Personne>> GetPersonneById(int id)
        {
            var personne = await dataRepository.GetByIdAsync(id);

            if (personne == null)
            {
                return NotFound();
            }

            return personne;
        }

        [HttpGet("{mail}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Personne>> GetPersonneByMail(string mail)
        {
            var personne = await dataRepository.GetByStringAsync(mail);

            if (personne == null)
            {
                return NotFound();
            }

            return personne;
        }

        [HttpGet]
        [ActionName("GetMaxPersonneId")]
        public async Task<ActionResult<int>> GetMaxPersonneId()
        {
            var maxId = (await dataRepository.GetAllAsync()).Value.Max(e => e.PersonneId);

            return maxId;
        }

        // PUT: api/Personnes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutPersonne(int id, Personne personne)
        {
            if (id != personne.PersonneId)
            {
                return BadRequest();
            }

            var personneToUpdate = await dataRepository.GetByIdAsync(id);

            if (personneToUpdate == null)
            {
                return NotFound();
            }

            else
            {
                await dataRepository.UpdateAsync(personneToUpdate.Value, personne);
                return NoContent();
            }
        }

        // POST: api/Personnes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Personne>> PostPersonne(Personne personne)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(personne);

            return CreatedAtAction("GetPersonneById", new { id = personne.PersonneId }, personne); // GetPersonneById : nom de l’action
        }

        // DELETE: api/Personnes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeletePersonne(int id)
        {
            var personne = await dataRepository.GetByIdAsync(id);

            if (personne == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(personne.Value);

            return NoContent();
        }

       
    }
}
