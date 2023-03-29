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
    public class LiensController : ControllerBase
    {
        private readonly IDataRepository<Lien> dataRepository;

        public LiensController(IDataRepository<Lien> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Liens
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Lien>>> GetLiens()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Liens/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Lien>> GetLienById(int id)
        {
            var lien = await dataRepository.GetByIdAsync(id);

            if (lien == null)
            {
                return NotFound();
            }

            return lien;
        }

        // PUT: api/liens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Putlien(int id, Lien lien)
        {
            if (id != lien.LienId)
            {
                return BadRequest();
            }

            var lienToUpdate = await dataRepository.GetByIdAsync(id);

            if (lienToUpdate == null)
            {
                return NotFound();
            }

            else
            {
                await dataRepository.UpdateAsync(lienToUpdate.Value, lien);
                return NoContent();
            }
        }

        // POST: api/Liens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Lien>> PostLien(Lien lien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(lien);

            return CreatedAtAction("GetLienById", new { id = lien.LienId }, lien); // GetById : nom de l’action
        }

        // DELETE: api/Liens/5
        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteLien(int id)
        {
            var lien = await dataRepository.GetByIdAsync(id);

            if (lien == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(lien.Value);

            return NoContent();
        }

    }
}
