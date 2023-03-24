using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.DataManager;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VignoblesController : ControllerBase
    {
        private readonly IDataRepository<Vignoble> dataRepository;
        private readonly IDataRepository<Lien> dataRepositoryLien;
        private readonly IDataRepository<ElementVignoble> dataRepositoryElementVignoble;
        private readonly IDataRepository<LienElementVignoble> dataRepositoryLienElementVignoble;

        public VignoblesController(IDataRepository<Vignoble> dataRepo, IDataRepository<Lien> dataRepoLien, IDataRepository<ElementVignoble> dataRepoElementVignoble, IDataRepository<LienElementVignoble> dataRepoLienElementVignoble)
        {
            dataRepository = dataRepo;
            dataRepositoryLien = dataRepoLien;
            dataRepositoryElementVignoble = dataRepoElementVignoble;
            dataRepositoryLienElementVignoble = dataRepoLienElementVignoble;
        }

        // GET: api/Vignobles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vignoble>>> GetVignobles()
        {
            var vignobles = dataRepository.GetAllAsync().Result;
            var liens = dataRepositoryLien.GetAllAsync().Result;

            if (vignobles == null)
            {
                return NotFound();
            }
            return vignobles;
        }

        // GET: api/Vignobles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vignoble>> GetVignobleById(int id)
        {
            var vignoble = dataRepository.GetByIdAsync(id).Result;
            var lien = dataRepositoryLien.GetAllAsync().Result;
            var elementsVignobles = dataRepositoryElementVignoble.GetAllAsync().Result;
            var lienElementsVignobles = dataRepositoryLienElementVignoble.GetAllAsync().Result;

            if (vignoble == null)
            {
                return NotFound();
            }

            return vignoble;
        }
    }
}
