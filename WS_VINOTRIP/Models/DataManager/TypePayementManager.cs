using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class TypePayementManager : IDataRepository<TypePayement>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public TypePayementManager()
        { }

        public TypePayementManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public Task AddAsync(TypePayement entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TypePayement entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<TypePayement>>> GetAllAsync()
        {
            return await vinotripDbContext.TypePayements.ToListAsync();
        }

        public async Task<ActionResult<TypePayement>> GetByIdAsync(int id)
        {

            return await vinotripDbContext.TypePayements.FirstOrDefaultAsync(e => e.TypePayementId == id);
        }

        public Task<ActionResult<TypePayement>> GetByStringAsync(string numen)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TypePayement entity1, TypePayement entity2)
        {
            throw new NotImplementedException();
        }
    }
}
