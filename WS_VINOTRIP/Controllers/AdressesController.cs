using System;
using System.Collections;
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
    public class AdressesController : ControllerBase
    {
        private readonly IDataRepositoryAdresse<Adresse> dataRepository;
        private readonly IDataRepository<Reside> dataRepositoryReside;

        public AdressesController(IDataRepositoryAdresse<Adresse> dataRepo)
        {
            dataRepository = dataRepo;
        }



        // GET: api/Avis/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Adresse>> GetAdresseById(int id)
        {
            var adresse = await dataRepository.GetByIdAsync(id);

            if (adresse == null)
            {
                return NotFound();
            }

            return adresse;
        }

        // GET: api/Adresses/5
        [HttpGet("{userid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAdresseByUserId(int userid)
        {
            var adresse = await dataRepository.GetByUserId(userid);

            if (adresse == null)
            {
                return NotFound();
            }

            return adresse;
        }

        // POST: api/Adresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Adresse>> PostAdresse(Adresse adresse, int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(adresse);
            await dataRepositoryReside.AddAsync(new Reside() { AdresseId = adresse.AdresseId, PersonneId = userId });

            return CreatedAtAction("GetAdresseById", new { id = adresse.AdresseId }, adresse); // GetById : nom de l’action
        }

        // DELETE: api/Adresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdresse(int id)
        {
            var adresse = await dataRepository.GetByIdAsync(id);

            if (adresse == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(adresse.Value);

            return NoContent();
        }


    }
}
