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
    public class AvisController : ControllerBase
    {
        private readonly IDataRepositoryAvis<Avis> dataRepository;

        public AvisController(IDataRepositoryAvis<Avis> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Avis/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Avis>> GetAvisById(int id)
        {
            var avis =  await dataRepository.GetByIdAsync(id);

            if (avis == null)
            {
                return NotFound();
            }

            return avis;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Avis>>> GetAvisBySejourId(int id)
        {
            var avis =  await dataRepository.GetBySejourIdAsync(id);

            if (avis == null)
            {
                return NotFound();
            }

            return avis;
        }

        // POST: api/Avis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Avis>> PostAvis(Avis avis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(avis);

            return CreatedAtAction("GetAvisById", new { id = avis.SejourId }, avis); // GetById : nom de l’action
        }

        // DELETE: api/Avis/5
        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAvis(int id)
        {
            var avis = await dataRepository.GetByIdAsync(id);

            if (avis == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(avis.Value);

            return NoContent();
        }
    }
}
