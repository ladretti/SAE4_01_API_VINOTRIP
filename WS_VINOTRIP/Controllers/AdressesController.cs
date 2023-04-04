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

        public AdressesController(IDataRepositoryAdresse<Adresse> dataRepo, IDataRepository<Reside> dataRepoReside)
        {
            dataRepository = dataRepo;
            dataRepositoryReside = dataRepoReside;
        }



        /// <summary>
        /// Récupère une adresse par son ID.
        /// </summary>
        /// <param name="id">ID de l'adresse à récupérer.</param>
        /// <returns>
        /// Une adresse correspondant à l'ID donné, ou un code de réponse HTTP 404 (Non trouvé) si l'adresse n'est pas trouvée.
        /// </returns>
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

        /// <summary>
        /// Récupère une liste d'adresses par ID utilisateur.
        /// </summary>
        /// <param name="userid">ID de l'utilisateur pour lequel récupérer les adresses.</param>
        /// <returns>
        /// Une liste d'adresses correspondant à l'ID utilisateur donné, ou un code de réponse HTTP 404 (Non trouvé) si aucune adresse n'est trouvée.
        /// </returns>
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

        /// <summary>
        /// Récupère une liste d'adresses par ID utilisateur.
        /// </summary>
        /// <param name="userid">ID de l'utilisateur pour lequel récupérer les adresses.</param>
        /// <returns>
        /// Une liste d'adresses correspondant à l'ID utilisateur donné, ou un code de réponse HTTP 404 (Non trouvé) si aucune adresse n'est trouvée.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<Adresse>> PostAdresse(Adresse adresse, int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(adresse);
            var reside = new Reside()
            {
                AdresseId = adresse.AdresseId,
                PersonneId = userId
            };
            await dataRepositoryReside.AddAsync(reside);

            return CreatedAtAction("GetAdresseById", new { id = adresse.AdresseId }, adresse); // GetById : nom de l’action
        }

        /// <summary>
        /// Supprime une adresse par son ID.
        /// </summary>
        /// <param name="id">ID de l'adresse à supprimer.</param>
        /// <returns>
        /// Un code de réponse HTTP 204 (Pas de contenu) si l'adresse est supprimée avec succès, ou un code de réponse HTTP 404 (Non trouvé) si l'adresse n'est pas trouvée.
        /// </returns>
        /// <remarks>
        /// Cette méthode est une action d'API web ASP.NET Core qui est déclenchée lorsqu'une requête HTTP DELETE est reçue avec un ID en tant que paramètre d'URL.
        /// </remarks>
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
