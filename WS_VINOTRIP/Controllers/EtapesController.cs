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
    public class EtapeController : ControllerBase
    {
        private readonly IDataRepository<Etape> dataRepository;

        public EtapeController(IDataRepository<Etape> dataRepo)
        {
            dataRepository = dataRepo;
        }


        // GET: api/Etape
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Etape>>> GetEtape()
        {
            return dataRepository.GetAllAsync().Result;
        }

        // GET: api/Etape/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<Etape>> GetEtapeById(int id)
        {
            var etape = dataRepository.GetByIdAsync(id).Result;

            if (etape == null)
            {
                return NotFound();
            }

            return etape;
        }

        // POST: api/Etape
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Etape>> PostEtape(Etape etape)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dataRepository.AddAsync(etape);

            return CreatedAtAction("GetById", new { id = etape.SejourId }, etape); // GetById : nom de l’action
        }

        // DELETE: api/Etape/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtape(int id)
        {
            var etape = dataRepository.GetByIdAsync(id);

            if (etape == null)
            {
                return NotFound();
            }

            dataRepository.DeleteAsync(etape.Result.Value);

            return NoContent();
        }
    }
}
