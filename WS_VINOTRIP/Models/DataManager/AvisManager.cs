using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class AvisManager : IDataRepositoryAvis<Avis>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public AvisManager()
        { }

        public AvisManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Avis>>> GetAllAsync()
        {
            return await vinotripDbContext.Aviss.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Avis>>> GetBySejourIdAsync(int id)
        {
            var avis = vinotripDbContext.Aviss.Where(e => e.SejourId == id);

            List<Avis> listAvis = new List<Avis>();

            foreach (Avis e in avis)
            {
                listAvis.Add(e);
            }

            return listAvis;
        }

        public async Task<ActionResult<Avis>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.Aviss.FirstOrDefaultAsync(e => e.AvisId == id);
        }

        public async Task<ActionResult<Avis>> GetByStringAsync(string titre)
        {
            return await vinotripDbContext.Aviss.FirstOrDefaultAsync(u => u.Titre.ToUpper() == titre.ToUpper());
        }

        public async Task AddAsync(Avis entity)
        {
            await vinotripDbContext.Aviss.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Avis avis, Avis entity)
        {
        }

        public async Task DeleteAsync(Avis avis)
        {
            vinotripDbContext.Aviss.Remove(avis);
            await vinotripDbContext.SaveChangesAsync();
        }
    }
}
