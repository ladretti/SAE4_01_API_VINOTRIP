using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class PersonneManager : IDataRepository<Personne>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public PersonneManager()
        { }

        public PersonneManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }
        public async Task<ActionResult<IEnumerable<Personne>>> GetAllAsync()
        {
            return await vinotripDbContext.Personnes.ToListAsync();
        }

        public async Task<ActionResult<Personne>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.Personnes.FirstOrDefaultAsync(e => e.PersonneId == id);
        }

        public async Task<ActionResult<Personne>> GetByStringAsync(string mail)
        {
            return await vinotripDbContext.Personnes.FirstOrDefaultAsync(u => u.Mail.ToUpper() == mail.ToUpper());
        }

        public async Task AddAsync(Personne entity)
        {
            await vinotripDbContext.Personnes.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Personne personne, Personne entity)
        {
            vinotripDbContext.Entry(personne).State = EntityState.Modified;
            personne.PersonneId = entity.PersonneId;
            personne.Nom = entity.Nom;
            personne.Mail = entity.Mail;
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Personne entity)
        {
            vinotripDbContext.Personnes.Remove(entity);
            await vinotripDbContext.SaveChangesAsync();
        }
    }
}
