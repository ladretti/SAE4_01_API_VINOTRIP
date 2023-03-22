using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class LienManager : IDataRepository<Lien>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public LienManager()
        { }

        public LienManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Lien>>> GetAllAsync()
        {
            return await vinotripDbContext.Liens.ToListAsync();
        }

        public async Task<ActionResult<Lien>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.Liens.FirstOrDefaultAsync(e => e.LienId == id);
        }

        public async Task<ActionResult<Lien>> GetByStringAsync(string titre)
        {
            return await vinotripDbContext.Liens.FirstOrDefaultAsync(u => u.Url.ToUpper() == titre.ToUpper());
        }

        public async Task AddAsync(Lien entity)
        {
            await vinotripDbContext.Liens.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Lien Lien, Lien entity)
        {
        }

        public async Task DeleteAsync(Lien Lien)
        {
            vinotripDbContext.Liens.Remove(Lien);
            await vinotripDbContext.SaveChangesAsync();
        }
    }

}
