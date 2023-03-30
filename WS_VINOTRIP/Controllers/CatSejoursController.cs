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

        public CatSejoursController(IDataRepository<CatSejour> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/CatSejours
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

        // GET: api/CatSejours/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CatSejour>> GetCatSejourById(int id)
        {
            var catSejour = await dataRepository.GetByIdAsync(id);

            if (catSejour == null)
            {
                return NotFound();
            }

            return catSejour;
        }
    }
}
