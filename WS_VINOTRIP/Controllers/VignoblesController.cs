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
    [Route("api/[controller]/[action]")]
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
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Vignoble>>> GetVignobles()
        {
            var vignobles = await dataRepository.GetAllAsync();
            await dataRepositoryLien.GetAllAsync();

            if (vignobles == null)
            {
                return NotFound();
            }
            return vignobles;
        }

        // GET: api/Vignobles/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Vignoble>> GetVignobleById(int id)
        {
            var vignoble = await dataRepository.GetByIdAsync(id);
            await dataRepositoryLien.GetAllAsync();
            await dataRepositoryElementVignoble.GetAllAsync();
            await dataRepositoryLienElementVignoble.GetAllAsync();

            if (vignoble == null)
            {
                return NotFound();
            }

            return vignoble;
        }
    }
}
