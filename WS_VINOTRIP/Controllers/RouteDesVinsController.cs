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
    public class RouteDesVinsController : ControllerBase
    {
        private readonly IDataRepository<RouteDesVins> dataRepository;
        private readonly IDataRepository<Lien> dataRepositoryLien;
        private readonly IDataRepository<LienRouteDesVins> dataRepositoryLienRouteDesVins;

        public RouteDesVinsController(IDataRepository<RouteDesVins> dataRepo, IDataRepository<Lien> dataRepoLien, IDataRepository<LienRouteDesVins> dataRepoLienRouteDesVins)
        {
            dataRepository = dataRepo;
            dataRepositoryLien = dataRepoLien;
            dataRepositoryLienRouteDesVins = dataRepoLienRouteDesVins;
        }

        /// <summary>
        /// Obtient tous les vignobles dans la collection "RouteDesVins" de la base de données.
        /// </summary>
        /// <returns>Une liste de tous les vignobles dans la collection "RouteDesVins".</returns>
        /// <response code="200">Retourne la liste des vignobles.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<RouteDesVins>>> GetRoutesDesVins()
        {
            var routeDesVins = await dataRepository.GetAllAsync();
            await dataRepositoryLien.GetAllAsync();
            await dataRepositoryLienRouteDesVins.GetAllAsync();

            if (routeDesVins == null)
            {
                return NotFound();
            }
            return routeDesVins;
        }

        /// <summary>
        /// Obtient un vignoble spécifique dans la collection "RouteDesVins" de la base de données en fonction de son identifiant.
        /// </summary>
        /// <param name="id">Identifiant du vignoble.</param>
        /// <returns>Le vignoble ayant l'identifiant spécifié.</returns>
        /// <response code="200">Retourne le vignoble ayant l'identifiant spécifié.</response>
        /// <response code="404">Si aucun vignoble n'est trouvé avec l'identifiant spécifié.</response>

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RouteDesVins>> GetRouteDesVinsById(int id)
        {
            var vignoble = await dataRepository.GetByIdAsync(id);
            await dataRepositoryLien.GetAllAsync();
            await dataRepositoryLienRouteDesVins.GetAllAsync();

            if (vignoble == null)
            {
                return NotFound();
            }

            return vignoble;
        }


    }
}
