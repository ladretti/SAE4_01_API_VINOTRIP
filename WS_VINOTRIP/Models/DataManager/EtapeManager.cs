using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class EtapeManager : IDataRepository<Etape>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public EtapeManager()
        { }

        public EtapeManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Etape>>> GetAllAsync()
        {
            return await vinotripDbContext.Etapes.ToListAsync();
        }

        public async Task<ActionResult<Etape>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.Etapes.FirstOrDefaultAsync(e => e.EtapeId == id);
        }

        public async Task AddAsync(Etape entity)
        {
            await vinotripDbContext.Etapes.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Etape etape, Etape entity)
        {
        }

        public async Task DeleteAsync(Etape etape)
        {
            vinotripDbContext.Etapes.Remove(etape);
            await vinotripDbContext.SaveChangesAsync();
        }

        public Task<ActionResult<Etape>> GetByStringAsync(string numen)
        {
            throw new NotImplementedException();
        }
    }
}
