using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class CommandeManager : IDataRepositoryCommande<Commande>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public CommandeManager()
        { }

        public CommandeManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Commande>>> GetAllAsync()
        {
            return await vinotripDbContext.Commandes.ToListAsync();
        }

        public async Task<ActionResult<Commande>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.Commandes.FirstOrDefaultAsync(e => e.CommandeId == id);
        }

        public async Task AddAsync(Commande entity)
        {
            await vinotripDbContext.Commandes.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Commande Commande, Commande entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Commande Commande)
        {
            vinotripDbContext.Commandes.Remove(Commande);
            await vinotripDbContext.SaveChangesAsync();
        }

        public Task<ActionResult<Commande>> GetByStringAsync(string numen)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Commande>>> GetByUserIdAsync(int userid)
        {
            var commandes = vinotripDbContext.Commandes.Where(e => e.PersonneId == userid).ToList();

            List<Commande> listCommandes = new List<Commande>();

            foreach (Commande c in commandes)
            {
                listCommandes.Add(c);
            }

            return listCommandes;
        }
    }
}
