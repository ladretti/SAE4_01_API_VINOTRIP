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
    public class ElementEtapeController : ControllerBase
    {
        private readonly IDataRepositoryElementEtape<ElementEtape> dataRepository;
        private readonly IDataRepository<Contient> dataRepositoryContient;
        private readonly IDataRepository<Lien> dataRepositoryLien;

        public ElementEtapeController(IDataRepositoryElementEtape<ElementEtape> dataRepo, IDataRepository<Contient> dataRepoContient, IDataRepository<Lien> dataRepoLien)
        {
            dataRepository = dataRepo;
            dataRepositoryContient= dataRepoContient;
            dataRepositoryLien = dataRepoLien;
        }

        // GET: api/ElementEtape/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ElementEtape>> GetElementEtapeById(int id)
        {
            var elementEtape = await dataRepository.GetByIdAsync(id);

            if (elementEtape == null)
            {
                return NotFound();
            }

            return elementEtape;
        }
        // GET: api/ElementEtape/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ElementEtape>>> GetByEtapeId(int id)
        {
            var elementEtape = await dataRepository.GetByEtapeIdAsync(id);
           

            if (elementEtape == null)
            {
                return NotFound();
            }

            return elementEtape;
        }

        // PUT: api/ElementEtape/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutElementEtape(int id, ElementEtape elementEtape)
        {
            if (id != elementEtape.ElementId)
            {
                
                return BadRequest();
            }

            var elementEtapeToUpdate = await dataRepository.GetByIdAsync(id);

            if (elementEtapeToUpdate == null)
            {
                return NotFound();
            }
            
            else
            {
                await dataRepository.UpdateAsync(elementEtapeToUpdate.Value, elementEtape);
                return NoContent();
            }
        }

        // POST: api/ElementEtape
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ElementEtape>> PostElementEtape(ElementEtape elementEtape)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(elementEtape);

            return CreatedAtAction("GetById", new { id = elementEtape.ElementId }, elementEtape); // GetById : nom de l’action
        }

        // DELETE: api/ElementEtape/5
        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteElementEtape(int id)
        {
            var elementEtape = await dataRepository.GetByIdAsync(id);

            if (elementEtape == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(elementEtape.Value);

            return NoContent();
        }
    }
}
