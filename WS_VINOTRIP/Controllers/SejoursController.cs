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
    public class SejoursController : ControllerBase
    {
        private readonly IDataRepository<Sejour> dataRepository;
        private readonly IDataRepository<Comporte> dataRepositoryComporte;
        private readonly IDataRepository<Lien> dataRepositoryLien;
        private readonly IDataRepository<LienSejour> dataRepositoryLienSejour;
        /*private readonly IDataRepository<CatParticipant> dataRepository3;*/
        public SejoursController(IDataRepository<Sejour> dataRepo, IDataRepository<Comporte> dataRepoComporte, IDataRepository<LienSejour> dataRepoLienSejour, IDataRepository<Lien> dataRepoLien)
        {
            dataRepository = dataRepo;
            dataRepositoryComporte = dataRepoComporte;
            dataRepositoryLien = dataRepoLien;
            dataRepositoryLienSejour = dataRepoLienSejour;
            /*dataRepository3 = dataRepo3;*/
        }

        // GET: api/Sejours
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Sejour>>> GetSejours()
        {
            var catparticipant = dataRepositoryComporte.GetAllAsync().Result;
            var liensejour = dataRepositoryLienSejour.GetAllAsync().Result;
            var lien = dataRepositoryLien.GetAllAsync().Result;

            var sejours = await dataRepository.GetAllAsync();

            if (sejours == null)
            {
                return NotFound();
            }
            return sejours;
        }

        // GET: api/Sejours/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Sejour>> GetSejourById(int id)
        {
            var sejour = await dataRepository.GetByIdAsync(id);
            var catparticipant = await dataRepositoryComporte.GetAllAsync();
            var liensejour = await dataRepositoryLienSejour.GetAllAsync();
            var lien = await dataRepositoryLien.GetAllAsync();

            if (sejour == null)
            {
                return NotFound();
            }

            return sejour;
        }

        /*//idcatvignoble idcatsejour, idcatparticipant
        [HttpGet]
        [Route("[action]/{catsejour}/{catvignoble}/{catparticipant}")]
        [ActionName("GetWithFilter")]
        public async Task<ActionResult<IEnumerable<Sejour>>> GetSejourFilter(int catsejour, int catvignoble, int catparticipant)
        {
            List<Sejour> filterList = new List<Sejour>();

            if (catsejour == null && catvignoble == null && catparticipant == null)
                return dataRepository.GetAllAsync().Result;
            
            var truc = dataRepositoryComporte.GetAllAsync().Result.Value.Where(e => e.CatParticipantId == catparticipant);

            foreach (var item in truc)
            {
                var t = dataRepository.GetAllAsync().Result.Value.Where(e => e.CatSejourId == catsejour && e.CatVignobleId == catvignoble && e.SejourId == item.SejourId).FirstOrDefault();
                if (t != null)
                    filterList.Add(t);
            }


            if (filterList == null)
            {
                return NotFound();
            }

            return filterList;
        }*/

        // GET: api/Sejours/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetByRoute")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Sejour>>> GetSejoursByRouteDesVins(int id)
        {
            var catparticipant = dataRepositoryComporte.GetAllAsync().Result;
            var liensejour = dataRepositoryLienSejour.GetAllAsync().Result;
            var lien = dataRepositoryLien.GetAllAsync().Result;

            var sejours = dataRepository.GetAllAsync().Result.Value.Where(e => e.RouteVinId == id);
            List<Sejour> sejoursRdvList = new List<Sejour>();
            foreach(var item in sejours)
            {
                sejoursRdvList.Add(item);
            }


            if (sejours == null)
            {
                return NotFound();
            }
            return sejoursRdvList;
        }

        // PUT: api/Sejours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutSejour(int id, Sejour sejour)
        {
            if (id != sejour.SejourId)
            {
                return BadRequest();
            }

            var sejourToUpdate = await dataRepository.GetByIdAsync(id);

            if (sejourToUpdate == null)
            {
                return NotFound();
            }

            else
            {
                await dataRepository.UpdateAsync(sejourToUpdate.Value, sejour);
                return NoContent();
            }
        }

        // POST: api/Sejours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Sejour>> PostSejour(Sejour sejour)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(sejour);

            return CreatedAtAction("GetSejourById", new { id = sejour.SejourId }, sejour); // GetById : nom de l’action
        }

        // DELETE: api/Sejours/5
        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteSejour(int id)
        {
            var sejour = await dataRepository.GetByIdAsync(id);

            if (sejour == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(sejour.Value);

            return NoContent();
        }

        /*private bool SejourExists(int id)
        {
            return _context.Sejours.Any(e => e.SejourId == id);
        }*/
    }
}
