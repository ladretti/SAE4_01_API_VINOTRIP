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
    public class PaniersController : ControllerBase
    {
        private readonly IDataRepositoryPanier<Panier> dataRepository;
        private readonly IDataRepository<Sejour> dataRepositorySejour;

        public PaniersController(IDataRepositoryPanier<Panier> dataRepo, IDataRepository<Sejour> dataRepoSejour)
        {
            dataRepository = dataRepo;
            dataRepositorySejour = dataRepoSejour;
        }

        // GET: api/Paniers
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Panier>>> GetPaniers()
        {
            var paniers = await dataRepository.GetAllAsync();
            //await dataRepositorySejour.GetAllAsync();
            return paniers;
        }

        // GET: api/Paniers/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Panier>>> GetPanierByUserId(int id)
        {
            var panier = dataRepository.GetAllAsync().Result.Value.Where(e => e.PersonneId == id);
            var sejour = dataRepository.GetAllAsync().Result;
            List<Panier> panierList = new List<Panier>();

            foreach (var e in panier)
            {
                panierList.Add(e);
            }

            if (panier == null)
            {
                return NotFound();
            }

            return panierList;
        }
        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Panier>> GetPanierByIds(int userId, int sejourId, bool offert)
        {
            var panier = await dataRepository.GetByIdsAsync(userId, sejourId, offert);

            if (panier == null)
            {
                return NotFound();
            }

            return panier;
        }


        // PUT: api/Paniers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutPanier(int userId, int sejourId, Panier panier)
        {
            if (userId != userId)
            {
                return BadRequest();
            }

            var panierToUpdate = await dataRepository.GetByIdsAsync(userId, sejourId, panier.Offert);

            if (panierToUpdate == null)
            {
                return NotFound();
            }

            else
            {
                await dataRepository.UpdateAsync(panierToUpdate.Value, panier);
                return NoContent();
            }
        }

        // POST: api/Paniers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Panier>> PostPanier(Panier panier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(panier);
            return CreatedAtAction("GetPanierById", new { userId = panier.PersonneId, sejourId = panier.SejourId, offert = panier.Offert }, panier); // GetById : nom de l’action
        }

        // DELETE: api/Paniers/5
        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeletePanier(int id)
        {
            var panier = await dataRepository.GetByIdAsync(id);

            if (panier == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(panier.Value);

            return NoContent();
        }


    }
}
