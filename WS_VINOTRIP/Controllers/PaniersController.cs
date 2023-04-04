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
    public class PaniersController : ControllerBase
    {
        private readonly IDataRepositoryPanier<Panier> dataRepository;
        private readonly IDataRepositorySejour<Sejour> dataRepositorySejour;

        public PaniersController(IDataRepositoryPanier<Panier> dataRepo, IDataRepositorySejour<Sejour> dataRepoSejour)
        {
            dataRepository = dataRepo;
            dataRepositorySejour = dataRepoSejour;
        }

        /// <summary>
        /// Obtient un panier par l'identifiant de l'utilisateur.
        /// </summary>
        /// <param name="id">L'identifiant de l'utilisateur.</param>
        /// <returns>Une liste des paniers de l'utilisateur correspondant à l'identifiant fourni.</returns>

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Panier>>> GetPanierByUserId(int id)
        {
            var panier = await dataRepository.GetByUserIdAsync(id);

            if (panier == null)
            {
                return NotFound();
            }

            return panier;
        }

        /// <summary>
        /// Met à jour un panier.
        /// </summary>
        /// <param name="userId">L'identifiant de l'utilisateur.</param>
        /// <param name="sejourId">L'identifiant du séjour.</param>
        /// <param name="panier">Les données à mettre à jour.</param>
        /// <returns>Un code 204 No Content si la mise à jour a réussi.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Authorize(Policy = Policies.User)]
        public async Task<IActionResult> PutPanier(int userId, int sejourId, Panier panier)
        {
            if (userId != panier.PersonneId || sejourId != panier.SejourId)
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

        /// <summary>
        /// Ajoute un nouveau panier.
        /// </summary>
        /// <param name="panier">Le panier à ajouter.</param>
        /// <returns>Le nouveau panier créé.</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Authorize(Policy = Policies.User)]
        public async Task<ActionResult<Panier>> PostPanier(Panier panier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(panier);
            return CreatedAtAction("GetPanierById", new { userId = panier.PersonneId, sejourId = panier.SejourId, offert = panier.Offert }, panier); // GetById : nom de l’action
        }

        /// <summary>
        /// Supprime un élément de panier pour un utilisateur spécifié, pour un séjour et un statut "offert" donnés.
        /// </summary>
        /// <param name="userid">Identifiant de l'utilisateur</param>
        /// <param name="sejid">Identifiant du séjour</param>
        /// <param name="offert">Statut "offert" de l'élément de panier</param>
        /// <returns>Renvoie un IActionResult indiquant le résultat de l'opération</returns>

        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Authorize(Policy = Policies.User)]
        public async Task<IActionResult> DeletePanier(int userid, int sejid, bool offert)
        {
            var panier = await dataRepository.GetByIdsAsync(userid, sejid, offert);

            if (panier == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(panier.Value);

            return NoContent();
        }


    }
}
