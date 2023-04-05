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
    public class ElementEtapeController : ControllerBase
    {
        private readonly IDataRepositoryElementEtape<ElementEtape> dataRepository;
        private readonly IDataRepository<Contient> dataRepositoryContient;
        private readonly IDataRepository<Lien> dataRepositoryLien;

        public ElementEtapeController(IDataRepositoryElementEtape<ElementEtape> dataRepo, IDataRepository<Contient> dataRepoContient, IDataRepository<Lien> dataRepoLien)
        {
            dataRepository = dataRepo;
            dataRepositoryContient= dataRepoContient;
            dataRepositoryLien = dataRepoLien;
        }

        /// <summary>
        /// Récupère un élément étape par son ID
        /// </summary>
        /// <param name="id">L'ID de l'élément étape à récupérer</param>
        /// <returns>L'élément étape correspondant à l'ID fourni</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ElementEtape>> GetElementEtapeById(int id)
        {
            var elementEtape = await dataRepository.GetByIdAsync(id);
            await dataRepositoryContient.GetAllAsync();
            await dataRepositoryLien.GetAllAsync();

            if (elementEtape.Result == null)
            {
                return NotFound();
            }

            return elementEtape;
        }

        /// <summary>
        /// Récupère tous les éléments étapes d'une étape
        /// </summary>
        /// <param name="id">L'ID de l'étape pour laquelle récupérer les éléments étapes</param>
        /// <returns>La liste des éléments étapes de l'étape correspondante à l'ID fourni</returns>

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ElementEtape>>> GetByEtapeId(int id)
        {
            var elementEtape = await dataRepository.GetByEtapeIdAsync(id);
            await dataRepositoryContient.GetAllAsync();
            await dataRepositoryLien.GetAllAsync();

            if (elementEtape == null)
            {
                return NotFound();
            }

            return elementEtape;
        }

        /// <summary>
        /// Met à jour un élément étape existant
        /// </summary>
        /// <param name="id">L'ID de l'élément étape à mettre à jour</param>
        /// <param name="elementEtape">L'élément étape mis à jour</param>
        /// <returns>Une réponse HTTP 201 si la mise à jour a réussi, une réponse HTTP 400 si la requête est invalide, une réponse HTTP 404 si l'élément étape n'a pas été trouvé</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutElementEtape(int id, ElementEtape elementEtape)
        {
            if (id != elementEtape.ElementId)
            {
                
                return BadRequest();
            }

            var elementEtapeToUpdate = await dataRepository.GetByIdAsync(id);

            if (elementEtapeToUpdate == null)
            {
                return NotFound();
            }
            
            else
            {
                await dataRepository.UpdateAsync(elementEtapeToUpdate.Value, elementEtape);
                return NoContent();
            }
        }

        /// <summary>
        /// Ajoute un nouvel élément étape
        /// </summary>
        /// <param name="elementEtape">L'élément étape à ajouter</param>
        /// <returns>Une réponse HTTP 201 avec l'élément étape ajouté si la création a réussi, une réponse HTTP 400 si la requête est invalide</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ElementEtape>> PostElementEtape(ElementEtape elementEtape)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(elementEtape);

            return CreatedAtAction("GetElementEtapeById", new { id = elementEtape.ElementId }, elementEtape); // GetById : nom de l’action
        }

        /// <summary>
        /// Supprime un élément étape existant
        /// </summary>
        /// <param name="id">L'ID de l'élément étape à supprimer</param>
        /// <returns>Une réponse HTTP 204 si la suppression a réussi, une réponse HTTP 404 si l
        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteElementEtape(int id)
        {
            var elementEtape = await dataRepository.GetByIdAsync(id);

            if (elementEtape == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(elementEtape.Value);

            return NoContent();
        }
    }
}
