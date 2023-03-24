using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class ConcerneManager : IDataRepository<Concerne>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public ConcerneManager()
        { }

        public ConcerneManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Concerne>>> GetAllAsync()
        {
            return await vinotripDbContext.Concernes.ToListAsync();
        }

        public async Task<ActionResult<Concerne>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Concerne entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Concerne Concerne, Concerne entity)
        {
        }

        public async Task DeleteAsync(Concerne Concerne)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Concerne>> GetByStringAsync(string numen)
        {
            throw new NotImplementedException();
        }
    }
}
