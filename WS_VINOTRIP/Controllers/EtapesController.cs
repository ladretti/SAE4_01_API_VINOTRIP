using Microsoft.AspNetCore.Mvc;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EtapeController : ControllerBase
    {
        private readonly IDataRepository<Etape> dataRepository;
        private readonly IDataRepository<Concerne> dataRepositoryConcerne;
        private readonly IDataRepository<ElementEtape> dataRepositoryElementEtape;

        public EtapeController(IDataRepository<Etape> dataRepo, IDataRepository<Concerne> dataRepoConcerne, IDataRepository<ElementEtape> dataRepoElementEtape)
        {
            dataRepository = dataRepo;
            dataRepositoryConcerne = dataRepoConcerne;
            dataRepositoryElementEtape = dataRepoElementEtape;
        }


        // GET: api/Etape
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Etape>>> GetEtape()
        {
            var etape = dataRepository.GetAllAsync().Result;

            if (etape == null)
            {
                return NotFound();
            }
            return etape;
        }

        // GET: api/Concerne
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Concerne>>> GetConcerne()
        {
            var concerne = dataRepositoryConcerne.GetAllAsync().Result;

            if (concerne == null)
            {
                return NotFound();
            }
            return concerne;
        }

        // GET: api/ElementEtape
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElementEtape>>> GetElementEtape()
        {
            var elementEtape = dataRepositoryElementEtape.GetAllAsync().Result;

            if (elementEtape == null)
            {
                return NotFound();
            }
            return elementEtape;
        }

        // GET: api/Etape/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<Etape>> GetEtapeById(int id)
        {
            var etape = dataRepository.GetByIdAsync(id).Result;
            var concerne = dataRepositoryConcerne.GetAllAsync().Result;
            var elementEtape = dataRepositoryElementEtape.GetAllAsync().Result;

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
