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
    public class CatSejoursController : ControllerBase
    {
        private readonly IDataRepository<CatSejour> dataRepository;
        private readonly IDataRepository<Lien> dataRepositoryLien;

        public CatSejoursController(IDataRepository<CatSejour> dataRepo, IDataRepository<Lien> dataRepoLien)
        {
            dataRepository = dataRepo;
            dataRepositoryLien = dataRepoLien;
        }

        /// <summary>
        /// Récupère toutes les catégories de séjour
        /// </summary>
        /// <returns>La liste de toutes les catégories de séjour</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<CatSejour>>> GetCatsSejour()
        {
            var catsSejour = await dataRepository.GetAllAsync();

            if (catsSejour == null)
            {
                return NotFound();
            }
            return catsSejour;
        }

        /// <summary>
        /// Récupère une catégorie de séjour par son identifiant
        /// </summary>
        /// <param name="id">L'identifiant de la catégorie de séjour</param>
        /// <returns>La catégorie de séjour correspondant à l'identifiant spécifié</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CatSejour>> GetCatSejourById(int id)
        {
            var catSejour = await dataRepository.GetByIdAsync(id);
            await dataRepositoryLien.GetAllAsync();

            if (catSejour == null)
            {
                return NotFound();
            }

            return catSejour;
        }
    }
}
