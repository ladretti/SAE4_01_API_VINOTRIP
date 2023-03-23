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
    [Route("api/[controller]")]
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
        public async Task<ActionResult<IEnumerable<RouteDesVins>>> GetRoutesDesVins()
        {
            var routeDesVins = dataRepository.GetAllAsync().Result;
            var liens = dataRepositoryLien.GetAllAsync().Result;
            var liensRdv = dataRepositoryLienRouteDesVins.GetAllAsync().Result;

            if (routeDesVins == null)
            {
                return NotFound();
            }
            return routeDesVins;
        }

        // GET: api/RouteDesVins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RouteDesVins>> GetRouteDesVins(int id)
        {
            var vignoble = dataRepository.GetByIdAsync(id).Result;
            var lien = dataRepositoryLien.GetAllAsync().Result;
            var liensRdv = dataRepositoryLienRouteDesVins.GetAllAsync().Result;

            if (vignoble == null)
            {
                return NotFound();
            }

            return vignoble;
        }

       
    }
}
