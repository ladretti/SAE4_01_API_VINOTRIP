using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{

    public class PanierManager : IDataRepositoryPanier<Panier>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public PanierManager()
        { }

        public PanierManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Panier>>> GetAllAsync()
        {
            return await vinotripDbContext.Paniers.ToListAsync();
        }

        public async Task<ActionResult<Panier>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.Paniers.FirstOrDefaultAsync(e => e.PersonneId == id);
        }

        public async Task<ActionResult<Panier>> GetByStringAsync(string titre)
        {
            return null;
        }
        public async Task<ActionResult<Panier>> GetByIdsAsync(int userId, int sejourId, bool offert)
        {
            return await vinotripDbContext.Paniers.FirstOrDefaultAsync(e => e.PersonneId == userId && e.SejourId == sejourId && e.Offert == offert);

        }


        public async Task AddAsync(Panier entity)
        {
            await vinotripDbContext.Paniers.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Panier Panier, Panier entity)
        {
        }

        public async Task DeleteAsync(Panier Panier)
        {
            vinotripDbContext.Paniers.Remove(Panier);
            await vinotripDbContext.SaveChangesAsync();
        }
    }
}
