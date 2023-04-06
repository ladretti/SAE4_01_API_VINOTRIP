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
    public class CommandesController : ControllerBase
    {
        private readonly IDataRepositoryCommande<Commande> dataRepository;
        private readonly IDataRepositoryPasse<Passe> dataRepositoryPasse;
        private readonly IDataRepository<Reservation> dataRepositoryReservation;

        public CommandesController(IDataRepositoryCommande<Commande> dataRepo, IDataRepositoryPasse<Passe> dataRepoPasse, IDataRepository<Reservation> dataRepoReservation)
        {
            dataRepository = dataRepo;
            dataRepositoryPasse = dataRepoPasse;
            dataRepositoryReservation = dataRepoReservation;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Commande>> GetCommandeById(int id)
        {
            var commande = await dataRepository.GetByIdAsync(id);

            if (commande.Value == null)
            {
                return NotFound();
            }

            return commande;
        }

        [HttpGet("{userid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Commande>>> GetAdresseByUserId(int userid)
        {
            var commandes = await dataRepository.GetByUserIdAsync(userid);
            await dataRepositoryPasse.GetAllAsync();
            await dataRepositoryReservation.GetAllAsync();
            if (commandes == null)
            {
                return NotFound();
            }

            return commandes;
        }

        [HttpPost]
        public async Task<ActionResult<Commande>> PostCommande(Commande commande)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(commande);

            return CreatedAtAction("GetCommandeById", new { id = commande.CommandeId }, commande); // GetById : nom de l’action
        }

        // DELETE: api/Commandes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommande(int id)
        {
            var commande = await dataRepository.GetByIdAsync(id);

            if (commande == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(commande.Value);

            return NoContent();
        }

    }
}
