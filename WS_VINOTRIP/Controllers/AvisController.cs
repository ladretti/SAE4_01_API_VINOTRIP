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

        /// <summary>
        /// Récupère un avis par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de l'avis.</param>
        /// <returns>L'avis correspondant à l'identifiant donné.</returns>
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

        /// <summary>
        /// Récupère tous les avis associés à un séjour.
        /// </summary>
        /// <param name="id">Identifiant du séjour.</param>
        /// <returns>La liste des avis associés au séjour donné.</returns>
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

        /// <summary>
        /// Ajoute un nouvel avis.
        /// </summary>
        /// <param name="avis">L'avis à ajouter.</param>
        /// <returns>Le nouvel avis ajouté.</returns>
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

        /// <summary>
        /// Supprime un avis.
        /// </summary>
        /// <param name="id">Identifiant de l'avis à supprimer.</param>
        /// <returns>204 si la suppression s'est bien déroulée, 404 si l'avis n'existe pas.</returns>

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
