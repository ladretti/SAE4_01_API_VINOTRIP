using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class LienSejourManager : IDataRepository<LienSejour>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public LienSejourManager()
        { }

        public LienSejourManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<LienSejour>>> GetAllAsync()
        {
            return await vinotripDbContext.LienSejours.ToListAsync();
        }

        public async Task<ActionResult<LienSejour>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.LienSejours.FirstOrDefaultAsync(e => e.SejourId == id);
        }

        public async Task<ActionResult<LienSejour>> GetByStringAsync(string titre)
        {
            return null;
        }

        public async Task AddAsync(LienSejour entity)
        {
            await vinotripDbContext.LienSejours.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(LienSejour LienSejour, LienSejour entity)
        {
        }

        public async Task DeleteAsync(LienSejour LienSejour)
        {
            vinotripDbContext.LienSejours.Remove(LienSejour);
            await vinotripDbContext.SaveChangesAsync();
        }

    }
}
