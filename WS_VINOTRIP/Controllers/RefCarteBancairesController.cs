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
    public class RefCarteBancairesController : ControllerBase
    {
        private readonly IDataRepositoryRefCarteBancaire<RefCarteBancaire> dataRepository;
        private readonly IDataRepository<CompteCarte> dataRepositoryCompteCarte;

        public RefCarteBancairesController(IDataRepositoryRefCarteBancaire<RefCarteBancaire> dataRepo, IDataRepository<CompteCarte> dataRepoCompteCarte)
        {
            dataRepository = dataRepo;
            dataRepositoryCompteCarte = dataRepoCompteCarte;
        }

        // GET: api/RefCarteBancaires/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RefCarteBancaire>> GetRefCarteBancaireById(int id)
        {
            var refCarteBancaire = await dataRepository.GetByIdAsync(id);

            if (refCarteBancaire == null)
            {
                return NotFound();
            }

            return refCarteBancaire;
        }
        [HttpGet("{userid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<RefCarteBancaire>>> GetCarteByUserId(int userid)
        {
            var refCarteBancaire = await dataRepository.GetByUserIdAsync(userid);

            if (refCarteBancaire == null)
            {
                return NotFound();
            }

            return refCarteBancaire;
        }

        // POST: api/RefCarteBancaires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RefCarteBancaire>> PostRefCarteBancaire(RefCarteBancaire refCarteBancaire, int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(refCarteBancaire);
            await dataRepositoryCompteCarte.AddAsync(new CompteCarte() { CarteId = refCarteBancaire.CarteId, PersonneId = userId });

            return CreatedAtAction("GetRefCarteBancaireById", new { id = refCarteBancaire.CarteId }, refCarteBancaire); // GetById : nom de l’action
        }

        // DELETE: api/RefCarteBancaires/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefCarteBancaire(int id)
        {
            var refCarteBancaire = await dataRepository.GetByIdAsync(id);

            if (refCarteBancaire == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(refCarteBancaire.Value);

            return NoContent();
        }

    }
}
