using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class ContientManager : IDataRepository<Contient>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public ContientManager()
        { }

        public ContientManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Contient>>> GetAllAsync()
        {
            return await vinotripDbContext.Contients.ToListAsync();
        }

        public async Task<ActionResult<Contient>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.Contients.FirstOrDefaultAsync(e => e.ElementId == id);
        }

        public async Task<ActionResult<Contient>> GetByStringAsync(string titre)
        {
            return null;
        }

        public async Task AddAsync(Contient entity)
        {
            await vinotripDbContext.Contients.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contient contient, Contient entity)
        {
        }

        public async Task DeleteAsync(Contient contient)
        {
            vinotripDbContext.LienEtapes.Remove(contient);
            await vinotripDbContext.SaveChangesAsync();
        }

    }
}
