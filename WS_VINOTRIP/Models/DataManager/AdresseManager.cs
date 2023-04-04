using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class AdresseManager : IDataRepositoryAdresse<Adresse>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public AdresseManager()
        { }

        public AdresseManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Adresse>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Adresse>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.Adresses.FirstOrDefaultAsync(e => e.AdresseId == id);
        }

        public async Task AddAsync(Adresse entity)
        {
            await vinotripDbContext.Adresses.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Adresse Adresse, Adresse entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Adresse Adresse)
        {
            vinotripDbContext.Adresses.Remove(Adresse);
            await vinotripDbContext.SaveChangesAsync();
        }

        public Task<ActionResult<Adresse>> GetByStringAsync(string numen)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Adresse>>> GetByUserId(int id)
        {
            var resides = vinotripDbContext.Resides.Where(e => e.PersonneId == id).ToList();

            List<Adresse> listAdresses = new List<Adresse>();

            foreach (Reside c in resides)
            {
                listAdresses.Add(GetByIdAsync(c.AdresseId).Result.Value);
            }
            return listAdresses;
        }
    }
}
