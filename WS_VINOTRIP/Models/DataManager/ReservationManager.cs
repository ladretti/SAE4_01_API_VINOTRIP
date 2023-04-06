using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class ReservationManager : IDataRepository<Reservation>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public ReservationManager()
        { }

        public ReservationManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Reservation>>> GetAllAsync()
        {
            return await vinotripDbContext.Reservations.ToListAsync();
        }

        public async Task<ActionResult<Reservation>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.Reservations.FirstOrDefaultAsync(e => e.ReservationId == id);
        }

        public async Task AddAsync(Reservation entity)
        {
            await vinotripDbContext.Reservations.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation Reservation, Reservation entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Reservation Reservation)
        {
            vinotripDbContext.Reservations.Remove(Reservation);
            await vinotripDbContext.SaveChangesAsync();
        }

        public Task<ActionResult<Reservation>> GetByStringAsync(string numen)
        {
            throw new NotImplementedException();
        }
    }
}
