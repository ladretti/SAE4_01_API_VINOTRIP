using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class CatSejourManager : IDataRepository<CatSejour>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public CatSejourManager()
        { }

        public CatSejourManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public Task AddAsync(CatSejour entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(CatSejour entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Microsoft.AspNetCore.Mvc.ActionResult<IEnumerable<CatSejour>>> GetAllAsync()
        {
            return await vinotripDbContext.CatsSejour.ToListAsync();
        }

        public async Task<Microsoft.AspNetCore.Mvc.ActionResult<CatSejour>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.CatsSejour.FirstOrDefaultAsync(e => e.CatSejourId == id);
        }

        public Task<Microsoft.AspNetCore.Mvc.ActionResult<CatSejour>> GetByStringAsync(string numen)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CatSejour entity1, CatSejour entity2)
        {
            throw new NotImplementedException();
        }
    }
}
