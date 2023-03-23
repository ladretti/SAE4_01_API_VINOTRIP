using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class LienRouteDesVinsManager : IDataRepository<LienRouteDesVins>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public LienRouteDesVinsManager()
        { }

        public LienRouteDesVinsManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<LienRouteDesVins>>> GetAllAsync()
        {
            return await vinotripDbContext.LienRouteDesVinss.ToListAsync();
        }

        public async Task<ActionResult<LienRouteDesVins>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.LienRouteDesVinss.FirstOrDefaultAsync(e => e.RouteDesVinsId == id);
        }

        public async Task<ActionResult<LienRouteDesVins>> GetByStringAsync(string titre)
        {
            return null;
        }

        public async Task AddAsync(LienRouteDesVins entity)
        {
            await vinotripDbContext.LienRouteDesVinss.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(LienRouteDesVins LienRouteDesVins, LienRouteDesVins entity)
        {
        }

        public async Task DeleteAsync(LienRouteDesVins LienRouteDesVins)
        {
            vinotripDbContext.LienRouteDesVinss.Remove(LienRouteDesVins);
            await vinotripDbContext.SaveChangesAsync();
        }
    }
}
