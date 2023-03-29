using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class LienEtapeManager : IDataRepository<LienEtape>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public LienEtapeManager()
        { }

        public LienEtapeManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<LienEtape>>> GetAllAsync()
        {
            return await vinotripDbContext.LienEtapes.ToListAsync();
        }

        public async Task<ActionResult<LienEtape>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.LienEtapes.FirstOrDefaultAsync(e => e.EtapeId == id);
        }

        public async Task<ActionResult<LienEtape>> GetByStringAsync(string titre)
        {
            return null;
        }

        public async Task AddAsync(LienEtape entity)
        {
            await vinotripDbContext.LienEtapes.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(LienEtape lienEtape, LienEtape entity)
        {
        }

        public async Task DeleteAsync(LienEtape lienEtape)
        {
            vinotripDbContext.LienEtapes.Remove(lienEtape);
            await vinotripDbContext.SaveChangesAsync();
        }

    }
}
