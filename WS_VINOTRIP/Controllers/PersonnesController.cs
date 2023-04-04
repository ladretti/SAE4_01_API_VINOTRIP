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
    public class PersonnesController : ControllerBase
    {
        private readonly IDataRepository<Personne> dataRepository;

        public PersonnesController(IDataRepository<Personne> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère toutes les personnes
        /// </summary>
        /// <returns>La liste de toutes les personnes</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Personne>>> GetPersonnes()
        {
            var personnes = await dataRepository.GetAllAsync();

            if (personnes == null)
            {
                return NotFound();
            }
            return personnes;
        }

        /// <summary>
        /// Récupère une personne par son identifiant
        /// </summary>
        /// <param name="id">L'identifiant de la personne</param>
        /// <returns>La personne correspondante</returns>
        [HttpGet("{id}")]
        [ActionName("GetPersonneById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Personne>> GetPersonneById(int id)
        {
            var personne = await dataRepository.GetByIdAsync(id);

            if (personne == null)
            {
                return NotFound();
            }

            return personne;
        }

        /// <summary>
        /// Récupère une personne par son adresse mail
        /// </summary>
        /// <param name="mail">L'adresse mail de la personne</param>
        /// <returns>La personne correspondante</returns>
        [HttpGet("{mail}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Personne>> GetPersonneByMail(string mail)
        {
            var personne = await dataRepository.GetByStringAsync(mail);

            if (personne == null)
            {
                return NotFound();
            }

            return personne;
        }

        /// <summary>
        /// Récupère l'identifiant maximum des personnes
        /// </summary>
        /// <returns>L'identifiant maximum</returns>
        [HttpGet]
        [ActionName("GetMaxPersonneId")]
        public async Task<ActionResult<int>> GetMaxPersonneId()
        {
            var maxId = (await dataRepository.GetAllAsync()).Value.Max(e => e.PersonneId);

            return maxId;
        }

        /// <summary>
        /// Met à jour une personne
        /// </summary>
        /// <param name="id">L'identifiant de la personne à mettre à jour</param>
        /// <param name="personne">La personne avec les nouvelles données</param>
        /// <returns>Une réponse HTTP</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutPersonne(int id, Personne personne)
        {
            if (id != personne.PersonneId)
            {
                return BadRequest();
            }

            var personneToUpdate = await dataRepository.GetByIdAsync(id);

            if (personneToUpdate == null)
            {
                return NotFound();
            }

            else
            {
                await dataRepository.UpdateAsync(personneToUpdate.Value, personne);
                return NoContent();
            }
        }

        /// <summary>
        /// Ajoute une nouvelle personne
        /// </summary>
        /// <param name="personne">La personne à ajouter</param>
        /// <returns>La personne ajoutée</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Personne>> PostPersonne(Personne personne)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(personne);

            return CreatedAtAction("GetPersonneById", new { id = personne.PersonneId }, personne); // GetPersonneById : nom de l’action
        }

        /// <summary>
        /// Supprime une personne
        /// </summary>
        /// <param name="id">L'identifiant de la personne à supprimer</param>
        /// <returns>Une réponse HTTP</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeletePersonne(int id)
        {
            var personne = await dataRepository.GetByIdAsync(id);

            if (personne == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(personne.Value);

            return NoContent();
        }

       
    }
}
