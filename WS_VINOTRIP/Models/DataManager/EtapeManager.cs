using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class EtapeManager : IDataRepositoryEtape<Etape>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public EtapeManager()
        { }

        public EtapeManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Etape>>> GetAllAsync()
        {
            return await vinotripDbContext.Etapes.ToListAsync();
        }

        public async Task<ActionResult<Etape>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.Etapes.FirstOrDefaultAsync(e => e.EtapeId == id);
        }
        
        public async Task<ActionResult<IEnumerable<Etape>>> GetBySejourIdAsync(int id)
        {
            var etapes = vinotripDbContext.Etapes.Where(e => e.SejourId == id);

            List<Etape> listEtapes = new List<Etape>();

            foreach (Etape e in etapes)
            {
                listEtapes.Add(e);
            }

            return listEtapes;
        }
        

        public async Task AddAsync(Etape entity)
        {
            await vinotripDbContext.Etapes.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Etape etape, Etape entity)
        {
            vinotripDbContext.Entry(etape).State = EntityState.Modified;
            etape.Titre = entity.Titre;
            etape.Description = entity.Titre;
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Etape etape)
        {
            vinotripDbContext.Etapes.Remove(etape);
            await vinotripDbContext.SaveChangesAsync();
        }

        public Task<ActionResult<Etape>> GetByStringAsync(string numen)
        {
            throw new NotImplementedException();
        }

        
    }
}
