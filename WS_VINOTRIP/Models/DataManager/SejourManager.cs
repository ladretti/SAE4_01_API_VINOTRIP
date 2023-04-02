using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class SejourManager : IDataRepositorySejour<Sejour>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public SejourManager() 
        { }

        public SejourManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Sejour>>> GetAllAsync()
        {
            return await vinotripDbContext.Sejours.ToListAsync();
        }

        public async Task<ActionResult<Sejour>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.Sejours.FirstOrDefaultAsync(e => e.SejourId == id);
        }

        public async Task<ActionResult<Sejour>> GetByStringAsync(string titre)
        {
            return await vinotripDbContext.Sejours.FirstOrDefaultAsync(u => u.Titre.ToUpper() == titre.ToUpper());
        }

        public async Task AddAsync(Sejour entity)
        {
            await vinotripDbContext.Sejours.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sejour sejour, Sejour entity)
        {
            vinotripDbContext.Entry(sejour).State = EntityState.Modified;
            sejour.SejourId = entity.SejourId;
            sejour.RouteVinId = entity.RouteVinId;
            sejour.CatSejourId = entity.CatSejourId;
            sejour.CatVignobleId = entity.CatVignobleId;
            sejour.Titre = entity.Titre;
            sejour.Description = entity.Description;
            sejour.Prix = entity.Prix;
            sejour.NbJour = entity.NbJour;
            sejour.NbNuit = entity.NbNuit;
            sejour.Promotion = entity.Promotion;
            sejour.Est_Valide = entity.Est_Valide;
            sejour.RouteDesVinsSejour = entity.RouteDesVinsSejour;
            sejour.SejourCatSejour = entity.SejourCatSejour;
            sejour.SejourCatVignoble = entity.SejourCatVignoble;
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Sejour sejour)
        {
            vinotripDbContext.Sejours.Remove(sejour);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Sejour>>> GetByRouteDesVinsIdAsync(int id)
        {
            var sejours = vinotripDbContext.Sejours.Where(e => e.RouteVinId == id);
            List<Sejour> sejoursRdvList = new List<Sejour>();
            foreach (var item in sejours)
            {
                sejoursRdvList.Add(item);
            }
          
            return sejoursRdvList;
        }
    }
}
