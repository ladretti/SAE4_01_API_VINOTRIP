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
    public class CatParticipantsController : ControllerBase
    {
        private readonly IDataRepository<CatParticipant> dataRepository;

        public CatParticipantsController(IDataRepository<CatParticipant> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/CatParticipants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatParticipant>>> GetCatsParticipant()
        {
            var response = HttpContext.Response;
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            var catsParticipant = dataRepository.GetAllAsync().Result;

            if (catsParticipant == null)
            {
                return NotFound();
            }
            return catsParticipant;
        }

        // GET: api/CatParticipants/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<CatParticipant>> GetCatParticipant(int id)
        {
            var response = HttpContext.Response;
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            var catParticipant = dataRepository.GetByIdAsync(id).Result;

            if (catParticipant == null)
            {
                return NotFound();
            }

            return catParticipant;
        }

        // PUT: api/CatParticipants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatParticipant(int id, CatParticipant catParticipant)
        {
            throw new NotImplementedException();
        }

        // POST: api/CatParticipants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CatParticipant>> PostCatParticipant(CatParticipant catParticipant)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/CatParticipants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatParticipant(int id)
        {
            throw new NotImplementedException();
        }

        private bool CatParticipantExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
