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
    public class LiensController : ControllerBase
    {
        private readonly IDataRepository<Lien> dataRepository;

        public LiensController(IDataRepository<Lien> dataRepo)
        {
            dataRepository = dataRepo;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Lien>> GetLienById(int id)
        {
            var lien = await dataRepository.GetByIdAsync(id);

            if (lien == null)
            {
                return NotFound();
            }

            return lien;
        }

        /// <summary>
        /// Ajoute un nouveau lien dans la base de données.
        /// </summary>
        /// <param name="lien">Le lien à ajouter.</param>
        /// <returns>Une action de création contenant le lien ajouté.</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Lien>> PostLien(Lien lien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(lien);

            return CreatedAtAction("GetLienById", new { id = lien.LienId }, lien); // GetById : nom de l’action
        }

        // <summary>
        /// Supprime le lien ayant l'identifiant spécifié de la base de données.
        /// </summary>
        /// <param name="id">L'identifiant du lien à supprimer.</param>
        /// <returns>Une action sans contenu si le lien a été supprimé avec succès, NotFound si aucun lien avec cet identifiant n'a été trouvé.</returns>

        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteLien(int id)
        {
            var lien = await dataRepository.GetByIdAsync(id);

            if (lien == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(lien.Value);

            return NoContent();
        }

    }
}
