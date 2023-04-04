using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.EntityFramework
{
    public class CompteCarteManager : IDataRepository<CompteCarte>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public CompteCarteManager()
        { }

        public CompteCarteManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<CompteCarte>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<CompteCarte>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(CompteCarte entity)
        {
            await vinotripDbContext.CompteCartes.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CompteCarte CompteCarte, CompteCarte entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(CompteCarte CompteCarte)
        {
            vinotripDbContext.CompteCartes.Remove(CompteCarte);
            await vinotripDbContext.SaveChangesAsync();
        }

        public Task<ActionResult<CompteCarte>> GetByStringAsync(string numen)
        {
            throw new NotImplementedException();
        }

        
    }
}
