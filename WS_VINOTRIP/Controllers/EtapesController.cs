using Microsoft.AspNetCore.Mvc;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EtapeController : ControllerBase
    {
        private readonly IDataRepositoryEtape<Etape> dataRepository;
        private readonly IDataRepository<Concerne> dataRepositoryConcerne;
        private readonly IDataRepositoryElementEtape<ElementEtape> dataRepositoryElementEtape;
        private readonly IDataRepository<LienEtape> dataRepositoryLienEtape;
        private readonly IDataRepository<Lien> dataRepositoryLien;

        public EtapeController(IDataRepositoryEtape<Etape> dataRepo, IDataRepositoryElementEtape<ElementEtape> dataRepoElementEtape, IDataRepository<Concerne> dataRepoConcerne, IDataRepository<LienEtape> dataRepoLienEtape, IDataRepository<Lien> dataRepoLien)
        {
            dataRepository = dataRepo;
            dataRepositoryConcerne = dataRepoConcerne;
            dataRepositoryElementEtape = dataRepoElementEtape;
            dataRepositoryLienEtape = dataRepoLienEtape;
            dataRepositoryLien = dataRepoLien;
        }


        // GET: api/Etape
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Etape>>> GetEtape()
        {
            var etape = await dataRepository.GetAllAsync();
            var liensejour = dataRepositoryLienEtape.GetAllAsync().Result;
            var lien = dataRepositoryLien.GetAllAsync().Result;

            if (etape == null)
            {
                return NotFound();
            }
            return etape;
        }

        // GET: api/Concerne
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Concerne>>> GetConcerne()
        {
            var concerne = await dataRepositoryConcerne.GetAllAsync();

            if (concerne == null)
            {
                return NotFound();
            }
            return concerne;
        }

        // GET: api/Etape/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Etape>> GetEtapeById(int id)
        {
            var etape = await dataRepository.GetByIdAsync(id);
            var concerne = await dataRepositoryConcerne.GetAllAsync();
            var elementEtape = await dataRepositoryElementEtape.GetAllAsync();

            if (etape == null)
            {
                return NotFound();
            }

            return etape;
        }
        // GET: api/Etape/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Etape>>> GetEtapeBySejourId(int id)
        {
            var etapes = await dataRepository.GetBySejourIdAsync(id);

            if (etapes == null)
            {
                return NotFound();
            }

            return etapes;
        }

        // PUT: api/Etapes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutEtape(int id, Etape etape)
        {
            if (id != etape.EtapeId)
            {
                return BadRequest();
            }

            var etapeToUpdate = await dataRepository.GetByIdAsync(id);

            if (etapeToUpdate == null)
            {
                return NotFound();
            }

            else
            {
                await dataRepository.UpdateAsync(etapeToUpdate.Value, etape);
                return NoContent();
            }
        }

        // POST: api/Etape
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Etape>> PostEtape(Etape etape)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(etape);

            return CreatedAtAction("GetEtapeById", new { id = etape.SejourId }, etape); // GetById : nom de l’action
        }

        // DELETE: api/Etape/5
        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteEtape(int id)
        {
            var etape = await dataRepository.GetByIdAsync(id);

            if (etape == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(etape.Value);

            return NoContent();
        }
    }
}
