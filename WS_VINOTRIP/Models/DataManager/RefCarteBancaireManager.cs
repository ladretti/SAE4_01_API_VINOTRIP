using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class RefCarteBancaireManager : IDataRepositoryRefCarteBancaire<RefCarteBancaire>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public RefCarteBancaireManager()
        { }

        public RefCarteBancaireManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<RefCarteBancaire>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<RefCarteBancaire>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.RefCarteBancaires.FirstOrDefaultAsync(e => e.CarteId == id);
        }

        public async Task AddAsync(RefCarteBancaire entity)
        {
            await vinotripDbContext.RefCarteBancaires.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(RefCarteBancaire RefCarteBancaire, RefCarteBancaire entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(RefCarteBancaire RefCarteBancaire)
        {
            vinotripDbContext.RefCarteBancaires.Remove(RefCarteBancaire);
            await vinotripDbContext.SaveChangesAsync();
        }

        public Task<ActionResult<RefCarteBancaire>> GetByStringAsync(string numen)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<RefCarteBancaire>>> GetByUserIdAsync(int id)
        {
            var comptesCartes = vinotripDbContext.CompteCartes.Where(e => e.PersonneId == id).ToList();

            List<RefCarteBancaire> listCartes = new List<RefCarteBancaire>();

            foreach (CompteCarte c in comptesCartes)
            {
                listCartes.Add(GetByIdAsync(c.CarteId).Result.Value);
            }
            return listCartes;
        }
    }
}
