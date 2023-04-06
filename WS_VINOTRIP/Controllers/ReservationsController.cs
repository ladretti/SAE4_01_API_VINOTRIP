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
    public class ReservationsController : ControllerBase
    {
        private readonly IDataRepository<Reservation> dataRepository;
        private readonly IDataRepositoryPasse<Passe> dataRepositoryPasse;

        public ReservationsController(IDataRepository<Reservation> dataRepo, IDataRepositoryPasse<Passe> dataRepoPasse)
        {
            dataRepository = dataRepo;
            dataRepositoryPasse = dataRepoPasse;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Reservation>> GetReservationById(int id)
        {
            var resa = await dataRepository.GetByIdAsync(id);

            if (resa.Value == null)
            {
                return NotFound();
            }

            return resa;
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation, int commandeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(reservation);

            return CreatedAtAction("GetReservationById", new { id = reservation.ReservationId }, reservation); // GetById : nom de l’action
        }

       


        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var user = await dataRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(user.Value);
            var e = await dataRepositoryPasse.GetByReservationsId(id);
            foreach (Passe p in e.Value)
                await dataRepositoryPasse.DeleteAsync(p);

            return NoContent();
        }

    }
}
