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
    public class LiensController : ControllerBase
    {
        private readonly IDataRepository<Lien> dataRepository;

        public LiensController(IDataRepository<Lien> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Liens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lien>>> GetLiens()
        {
            return dataRepository.GetAllAsync().Result;
        }

        // GET: api/Liens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lien>> GetLienById(int id)
        {
            var lien = dataRepository.GetByIdAsync(id).Result;

            if (lien == null)
            {
                return NotFound();
            }

            return lien;
        }

        // POST: api/Liens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lien>> PostLien(Lien lien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dataRepository.AddAsync(lien);

            return CreatedAtAction("GetById", new { id = lien.LienId }, lien); // GetById : nom de l’action
        }

        // DELETE: api/Liens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLien(int id)
        {
            var lien = dataRepository.GetByIdAsync(id);

            if (lien == null)
            {
                return NotFound();
            }

            dataRepository.DeleteAsync(lien.Result.Value);

            return NoContent();
        }

    }
}
