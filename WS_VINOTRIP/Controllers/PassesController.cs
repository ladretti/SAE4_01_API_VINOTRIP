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
    public class PassesController : ControllerBase
    {
        private readonly IDataRepositoryPasse<Passe> dataRepository;
        public PassesController(IDataRepositoryPasse<Passe> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Passes/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Passe>>> GetByReservationsId(int id)
        {
            var resa = await dataRepository.GetByReservationsId(id);

            if (resa.Value == null)
            {
                return NotFound();
            }

            return resa;
        }

        [HttpPost]
        public async Task<ActionResult<Passe>> PostPasse(Passe passe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(passe);

            return CreatedAtAction("GetByReservationsId", new { id = passe.ReservationId }, passe); // GetById : nom de l’action
        }

    }
}
