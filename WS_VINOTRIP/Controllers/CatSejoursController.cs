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
    public class CatSejoursController : ControllerBase
    {
        private readonly IDataRepository<CatSejour> dataRepository;

        public CatSejoursController(IDataRepository<CatSejour> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/CatSejours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatSejour>>> GetCatsSejour()
        {
            var response = HttpContext.Response;
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            var catsSejour = dataRepository.GetAllAsync().Result;

            if (catsSejour == null)
            {
                return NotFound();
            }
            return catsSejour;
        }

        // GET: api/CatSejours/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<CatSejour>> GetCatSejour(int id)
        {
            var response = HttpContext.Response;
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            var catSejour = dataRepository.GetByIdAsync(id).Result;

            if (catSejour == null)
            {
                return NotFound();
            }

            return catSejour;
        }

        // PUT: api/CatSejours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatSejour(int id, CatSejour catSejour)
        {
            throw new NotImplementedException();
        }

        // POST: api/CatSejours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CatSejour>> PostCatSejour(CatSejour catSejour)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/CatSejours/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatSejour(int id)
        {
            throw new NotImplementedException();
        }

        private bool CatSejourExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
