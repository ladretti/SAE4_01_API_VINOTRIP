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

        // GET: api/CatVignobles
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

        // GET: api/CatVignobles/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CatVignoble>> GetCatVignoble(int id)
        {
            var catVignoble = await dataRepository.GetByIdAsync(id);

            if (catVignoble == null)
            {
                return NotFound();
            }

            return catVignoble;
        }

        /*// PUT: api/CatVignobles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatVignoble(int id, CatVignoble catVignoble)
        {
            throw new NotImplementedException();
        }

        // POST: api/CatVignobles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CatVignoble>> PostCatVignoble(CatVignoble catVignoble)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/CatVignobles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatVignoble(int id)
        {
            throw new NotImplementedException();
        }

        private bool CatVignobleExists(int id)
        {
            throw new NotImplementedException();
        }*/
    }
}
