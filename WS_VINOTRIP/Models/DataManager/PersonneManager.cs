using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class PersonneManager : IDataRepository<Personne>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public PersonneManager()
        { }

        public PersonneManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }
        public async Task<ActionResult<IEnumerable<Personne>>> GetAllAsync()
        {
            return await vinotripDbContext.Personnes.ToListAsync();
        }

        public Task<ActionResult<Personne>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Personne>> GetByStringAsync(string numen)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Personne entity)
        {
            await vinotripDbContext.Personnes.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(Personne entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Personne entity1, Personne entity2)
        {
            throw new NotImplementedException();
        }
    }
}
