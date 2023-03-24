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
        private readonly IDataRepository<ElementEtape> dataRepository;

        public ElementEtapeController(IDataRepository<ElementEtape> dataRepo)
        {
            dataRepository = dataRepo;
        }


        // GET: api/ElementEtape
       [HttpGet]
        public async Task<ActionResult<IEnumerable<ElementEtape>>> GetElementEtape()
        {
            return dataRepository.GetAllAsync().Result;
        }

        // GET: api/ElementEtape/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ElementEtape>> GetElementEtapeById(int id)
        {
            var elementEtape = dataRepository.GetByIdAsync(id).Result;

            if (elementEtape == null)
            {
                return NotFound();
            }

            return elementEtape;
        }

        // POST: api/ElementEtape
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ElementEtape>> PostElementEtape(ElementEtape elementEtape)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dataRepository.AddAsync(elementEtape);

            return CreatedAtAction("GetById", new { id = elementEtape.ElementId }, elementEtape); // GetById : nom de l’action
        }

        // DELETE: api/ElementEtape/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElementEtape(int id)
        {
            var elementEtape = dataRepository.GetByIdAsync(id);

            if (elementEtape == null)
            {
                return NotFound();
            }

            dataRepository.DeleteAsync(elementEtape.Result.Value);

            return NoContent();
        }
    }
}
