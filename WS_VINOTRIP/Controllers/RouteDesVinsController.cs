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

        // GET: api/RouteDesVins
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

        // GET: api/RouteDesVins/5
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
