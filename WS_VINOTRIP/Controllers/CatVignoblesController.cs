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
    public class CatVignoblesController : ControllerBase
    {
        private readonly IDataRepository<CatVignoble> dataRepository;

        public CatVignoblesController(IDataRepository<CatVignoble> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère tous les éléments de la catégorie "Vignoble".
        /// </summary>
        /// <returns>Une liste d'éléments de la catégorie "Vignoble".</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<CatVignoble>>> GetCatsVignoble()
        {
            var catsVignoble = await dataRepository.GetAllAsync();

            if (catsVignoble == null)
            {
                return NotFound();
            }
            return catsVignoble;
        }

        /// <summary>
        /// Récupère un élément de la catégorie "Vignoble" par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de l'élément de la catégorie "Vignoble" à récupérer.</param>
        /// <returns>L'élément de la catégorie "Vignoble" correspondant à l'identifiant donné.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CatVignoble>> GetCatVignobleById(int id)
        {
            var catVignoble = await dataRepository.GetByIdAsync(id);

            if (catVignoble == null)
            {
                return NotFound();
            }

            return catVignoble;
        }

       
    }
}
