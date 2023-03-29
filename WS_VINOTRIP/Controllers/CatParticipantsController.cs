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
    public class CatParticipantsController : ControllerBase
    {
        private readonly IDataRepository<CatParticipant> dataRepository;

        public CatParticipantsController(IDataRepository<CatParticipant> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/CatParticipants
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<CatParticipant>>> GetCatsParticipant()
        {

            var catsParticipant = await dataRepository.GetAllAsync();

            if (catsParticipant == null)
            {
                return NotFound();
            }
            return catsParticipant;
        }

        // GET: api/CatParticipants/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CatParticipant>> GetCatParticipant(int id)
        {
            var catParticipant = await dataRepository.GetByIdAsync(id);

            if (catParticipant == null)
            {
                return NotFound();
            }

            return catParticipant;
        }
        /*// POST: api/CatParticipants
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
        }*/
    }
}
