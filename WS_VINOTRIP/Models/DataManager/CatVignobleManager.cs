using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class CatVignobleManager : IDataRepository<CatVignoble>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public CatVignobleManager()
        { }

        public CatVignobleManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public Task AddAsync(CatVignoble entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(CatVignoble entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<CatVignoble>>> GetAllAsync()
        {
            return await vinotripDbContext.CatsVignoble.ToListAsync();
        }

        public async Task<ActionResult<CatVignoble>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.CatsVignoble.FirstOrDefaultAsync(e => e.CatVignobleId == id);
        }

        public Task<ActionResult<CatVignoble>> GetByStringAsync(string numen)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CatVignoble entity1, CatVignoble entity2)
        {
            throw new NotImplementedException();
        }
    }
}
