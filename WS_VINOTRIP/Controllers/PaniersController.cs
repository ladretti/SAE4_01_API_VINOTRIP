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
        private readonly IDataRepository<Panier> dataRepository;
        private readonly IDataRepository<Sejour> dataRepositorySejour;

        public PaniersController(IDataRepository<Panier> dataRepo, IDataRepository<Sejour> dataRepoSejour)
        {
            dataRepository = dataRepo;
            dataRepositorySejour = dataRepoSejour;
        }

        // GET: api/Paniers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Panier>>> GetPaniers()
        {
            var paniers = dataRepository.GetAllAsync().Result;
            var sejours = dataRepositorySejour.GetAllAsync().Result;
            return paniers;
        }

        // GET: api/Paniers/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<Panier>> GetPanier(int id)
        {
            var panier = dataRepository.GetByIdAsync(id).Result;
            var sejour = dataRepository.GetAllAsync().Result;

            if (panier == null)
            {
                return NotFound();
            }

            return panier;
        }

        // GET: api/Paniers/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetByUserId")]
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

        // PUT: api/Paniers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPanier(int userId, int sejourId, Panier panier)
        {
            if (userId != panier.PersonneId)
            {
                return BadRequest();
            }

            var panierToUpdate = dataRepository.GetAllAsync().Result.Value.Where(e => e.PersonneId == userId && e.SejourId == sejourId && e.Offert == panier.Offert).FirstOrDefault();

            if (panierToUpdate == null)
            {
                return NotFound();
            }

            else
            {
                dataRepository.UpdateAsync(panierToUpdate, panier);
                return NoContent();
            }
        }

        // POST: api/Paniers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Panier>> PostPanier(Panier panier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dataRepository.AddAsync(panier);

            return CreatedAtAction("GetById", new { id = panier }, panier); // GetById : nom de l’action
        }

        // DELETE: api/Paniers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePanier(int id)
        {
            var panier = dataRepository.GetByIdAsync(id);

            if (panier == null)
            {
                return NotFound();
            }

            dataRepository.DeleteAsync(panier.Result.Value);

            return NoContent();
        }


    }
}
