using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class FavorisManager : IDataRepositoryFavori<Favori>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public FavorisManager()
        { }

        public FavorisManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Favori>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Favori>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Favori entity)
        {
            await vinotripDbContext.Favoris.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Favori Favori, Favori entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Favori Favori)
        {
            vinotripDbContext.Favoris.Remove(Favori);
            await vinotripDbContext.SaveChangesAsync();
        }

        public Task<ActionResult<Favori>> GetByStringAsync(string numen)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Favori>> GetBySejourIdUserIdAsync(int sejourid, int userid)
        {
            return await vinotripDbContext.Favoris.FirstOrDefaultAsync(e => e.SejourId == sejourid && e.PersonneId == userid);
        }

        public async Task<ActionResult<IEnumerable<Favori>>> GetByUserIdAsync(int userid)
        {
            var favoris = vinotripDbContext.Favoris.Where(e => e.PersonneId == userid);

            List<Favori> listFavoris = new List<Favori>();

            foreach (Favori e in favoris)
            {
                listFavoris.Add(e);
            }

            return listFavoris;
        }
    }
}
