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
    [Route("api/[controller]")]
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
        public async Task<ActionResult<IEnumerable<Personne>>> GetPersonnes()
        {
            var personnes = dataRepository.GetAllAsync().Result;

            if (personnes == null)
            {
                return NotFound();
            }
            return personnes;
        }

        // GET: api/Personnes/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<Personne>> GetPersonneById(int id)
        {
            var personne = dataRepository.GetByIdAsync(id).Result;

            if (personne == null)
            {
                return NotFound();
            }

            return personne;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<int>> GetMaxPersonneId()
        {
            var maxId = dataRepository.GetAllAsync().Result.Value.Max(e => e.PersonneId);

            return maxId;
        }

        // PUT: api/Personnes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonne(int id, Personne personne)
        {
            if (id != personne.PersonneId)
            {
                return BadRequest();
            }

            var personneToUpdate = dataRepository.GetByIdAsync(id);

            if (personneToUpdate == null)
            {
                return NotFound();
            }

            else
            {
                dataRepository.UpdateAsync(personneToUpdate.Result.Value, personne);
                return NoContent();
            }
        }

        // POST: api/Personnes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Personne>> PostPersonne(Personne personne)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dataRepository.AddAsync(personne);

            return CreatedAtAction("PostPersonne", new { id = personne.PersonneId }, personne); // GetById : nom de l’action
        }

        // DELETE: api/Personnes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonne(int id)
        {
            var personne = dataRepository.GetByIdAsync(id);

            if (personne == null)
            {
                return NotFound();
            }

            dataRepository.DeleteAsync(personne.Result.Value);

            return NoContent();
        }

        /*private bool PersonneExists(int id)
        {
            return (_context.Personnes?.Any(e => e.PersonneId == id)).GetValueOrDefault();
        }*/
    }
}
