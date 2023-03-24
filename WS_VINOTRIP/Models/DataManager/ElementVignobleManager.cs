using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class ElementVignobleManager : IDataRepository<ElementVignoble>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public ElementVignobleManager()
        { }

        public ElementVignobleManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<ElementVignoble>>> GetAllAsync()
        {
            return await vinotripDbContext.ElementsVignoble.ToListAsync();
        }

        public async Task<ActionResult<ElementVignoble>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.ElementsVignoble.FirstOrDefaultAsync(e => e.ElementVignobleId == id);
        }

        public async Task<ActionResult<ElementVignoble>> GetByStringAsync(string titre)
        {
            return await vinotripDbContext.ElementsVignoble.FirstOrDefaultAsync(u => u.Titre.ToUpper() == titre.ToUpper());
        }

        public async Task AddAsync(ElementVignoble entity)
        {
            await vinotripDbContext.ElementsVignoble.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ElementVignoble ElementVignoble, ElementVignoble entity)
        {
        }

        public async Task DeleteAsync(ElementVignoble ElementVignoble)
        {
            vinotripDbContext.ElementsVignoble.Remove(ElementVignoble);
            await vinotripDbContext.SaveChangesAsync();
        }
    }
}
