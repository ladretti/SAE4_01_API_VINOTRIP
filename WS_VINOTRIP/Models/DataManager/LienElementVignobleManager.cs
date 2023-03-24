using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class LienElementVignobleManager : IDataRepository<LienElementVignoble>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public LienElementVignobleManager()
        { }

        public LienElementVignobleManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<LienElementVignoble>>> GetAllAsync()
        {
            return await vinotripDbContext.LiensElementVignoble.ToListAsync();
        }

        public async Task<ActionResult<LienElementVignoble>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.LiensElementVignoble.FirstOrDefaultAsync(e => e.ElementVignobleId == id);
        }

        public async Task<ActionResult<LienElementVignoble>> GetByStringAsync(string titre)
        {
            return null;
        }

        public async Task AddAsync(LienElementVignoble entity)
        {
            await vinotripDbContext.LiensElementVignoble.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(LienElementVignoble LienElementVignoble, LienElementVignoble entity)
        {
        }

        public async Task DeleteAsync(LienElementVignoble LienElementVignoble)
        {
            vinotripDbContext.LiensElementVignoble.Remove(LienElementVignoble);
            await vinotripDbContext.SaveChangesAsync();
        }
    }
}
