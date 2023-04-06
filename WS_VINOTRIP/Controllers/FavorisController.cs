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
    public class FavorisController : ControllerBase
    {
        private readonly IDataRepositoryFavori<Favori> dataRepository;
        private readonly IDataRepositorySejour<Sejour> dataRepositorySejour;
        private readonly IDataRepository<Lien> dataRepositoryLien;
        private readonly IDataRepository<LienSejour> dataRepositoryLienSejour;

        public FavorisController(IDataRepositoryFavori<Favori> dataRepo, IDataRepositorySejour<Sejour> dataRepoSejour, IDataRepository<Lien> dataRepoLien, IDataRepository<LienSejour> dataRepoLienSejour)
        {
            dataRepository = dataRepo;
            dataRepositorySejour = dataRepoSejour;
            dataRepositoryLien = dataRepoLien;
            dataRepositoryLienSejour = dataRepoLienSejour;
        }

        [HttpGet("{sejourid}/{userid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Favori>> GetFavoriByIds(int sejourid, int userid)
        {
            var favori = await dataRepository.GetBySejourIdUserIdAsync(sejourid, userid);

            if (favori == null)
                    return NotFound();


            return favori;
        }

        [HttpGet("{userid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Favori>>> GetFavorisByUserId(int userid)
        {
            var favori = await dataRepository.GetByUserIdAsync(userid);
            await dataRepositorySejour.GetAllAsync();
            await dataRepositoryLienSejour.GetAllAsync();
            await dataRepositoryLien.GetAllAsync();

            if (favori == null)
                    return NotFound();


            return favori;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Favori>> PostFavori(Favori favori)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var e = await dataRepository.GetBySejourIdUserIdAsync(favori.SejourId, favori.PersonneId);

            if (e != null)
                if (e.Value != null)
                    return BadRequest("Already exists");

            await dataRepository.AddAsync(favori);

            return CreatedAtAction("GetFavoriByIds", new { sejourid = favori.SejourId, userid = favori.SejourId }, favori); // GetById : nom de l’action
        }

        [HttpDelete("{sejourid}/{userid}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteFavori(int sejourid, int userid)
        {
            var favori = await dataRepository.GetBySejourIdUserIdAsync(sejourid, userid);

            if (favori == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(favori.Value);

            return NoContent();
        }
    }
}
