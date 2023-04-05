using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class ResideManager : IDataRepositoryReside<Reside>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public ResideManager()
        { }

        public ResideManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Reside>>> GetAllAsync()
        {
            return await vinotripDbContext.Resides.ToListAsync();
        }

        public async Task<ActionResult<Reside>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.Resides.FirstOrDefaultAsync(e => e.AdresseId == id);
        }
        public async Task<ActionResult<IEnumerable<Reside>>> GetByAdresseIdAsync(int id)
        {
            return vinotripDbContext.Resides.Where(e => e.AdresseId == id).ToList();
        }

        public async Task<ActionResult<Reside>> GetByStringAsync(string titre)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Reside entity)
        {
            await vinotripDbContext.Resides.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reside Reside, Reside entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Reside Reside)
        {
            vinotripDbContext.Resides.Remove(Reside);
            await vinotripDbContext.SaveChangesAsync();
        }
        public async Task DeleteByDoubleIdAsync(int userId, int id)
        {
            vinotripDbContext.Resides.Remove(vinotripDbContext.Resides.FirstOrDefaultAsync(e => e.AdresseId == id && e.PersonneId == userId).Result);
            await vinotripDbContext.SaveChangesAsync();
        }

    }
}
