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
    public class CatVignoblesController : ControllerBase
    {
        private readonly IDataRepository<CatVignoble> dataRepository;

        public CatVignoblesController(IDataRepository<CatVignoble> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/CatVignobles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatVignoble>>> GetCatsVignoble()
        {
            var response = HttpContext.Response;
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            var catsVignoble =  dataRepository.GetAllAsync().Result;

            if (catsVignoble== null)
            {
                return NotFound();
            }
            return catsVignoble;
        }

        // GET: api/CatVignobles/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<CatVignoble>> GetCatVignoble(int id)
        {

            var response = HttpContext.Response;
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            var catVignoble = dataRepository.GetByIdAsync(id).Result;

            if (catVignoble == null)
            {
                return NotFound();
            }

            return catVignoble;
        }

        // PUT: api/CatVignobles/5
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
        }
    }
}
