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

        public FavorisController(IDataRepositoryFavori<Favori> dataRepo)
        {
            dataRepository = dataRepo;
        }

        [HttpGet("{sejourid}/{userid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Favori>> GetFavoriByIds(int sejourid, int userid)
        {
            var favori = await dataRepository.GetBySejourIdUserIdAsync(sejourid, userid);

            if (favori.Value == null)
            {
                return NotFound();
            }

            return favori;
        }

        [HttpGet("{userid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Favori>>> GetFavorisByUserId(int userid)
        {
            var favori = await dataRepository.GetByUserIdAsync(userid);

            if (favori != null)
                if (favori.Value != null)
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
