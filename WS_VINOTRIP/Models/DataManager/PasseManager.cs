using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class PasseManager : IDataRepositoryPasse<Passe>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public PasseManager()
        { }

        public PasseManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Passe>>> GetAllAsync()
        {
            return await vinotripDbContext.Passes.ToListAsync();
        }

        public async Task<ActionResult<Passe>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Passe entity)
        {
            await vinotripDbContext.Passes.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Passe Passe, Passe entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Passe Passe)
        {
            vinotripDbContext.Passes.Remove(Passe);
            await vinotripDbContext.SaveChangesAsync();
        }

        public Task<ActionResult<Passe>> GetByStringAsync(string numen)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Passe>>> GetByReservationsId(int id)
        {
            var passes = vinotripDbContext.Passes.Where(e => e.ReservationId == id).ToList();

            List<Passe> listPasses = new List<Passe>();

            foreach (Passe p in passes)
            {
                listPasses.Add(p);
            }

            return listPasses;
        }
    }
}
