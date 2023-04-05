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
    public class CatParticipantsController : ControllerBase
    {
        private readonly IDataRepository<CatParticipant> dataRepository;
        private readonly IDataRepository<Lien> dataRepositoryLien;

        public CatParticipantsController(IDataRepository<CatParticipant> dataRepo, IDataRepository<Lien> dataRepoLien)
        {
            dataRepository = dataRepo;
            dataRepositoryLien = dataRepoLien;
        }

        /// <summary>
        /// Récupère tous les CatParticipants.
        /// </summary>
        /// <returns>Une liste de tous les CatParticipants.</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<CatParticipant>>> GetCatsParticipant()
        {

            var catsParticipant = await dataRepository.GetAllAsync();
            await dataRepositoryLien.GetAllAsync();

            if (catsParticipant == null)
            {
                return NotFound();
            }
            return catsParticipant;
        }

        /// <summary>
        /// Récupère un CatParticipant en fonction de son identifiant.
        /// </summary>
        /// <param name="id">Identifiant du CatParticipant à récupérer.</param>
        /// <returns>Le CatParticipant correspondant à l'identifiant spécifié.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CatParticipant>> GetCatParticipantById(int id)
        {
            var catParticipant = await dataRepository.GetByIdAsync(id);
            await dataRepositoryLien.GetAllAsync();

            if (catParticipant == null)
            {
                return NotFound();
            }

            return catParticipant;
        }
        
    }
}
