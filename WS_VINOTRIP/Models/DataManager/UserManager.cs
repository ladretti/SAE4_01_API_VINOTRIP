using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class UserManager : IDataRepository<User>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public UserManager()
        { }

        public UserManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
        {
            return await vinotripDbContext.Users.ToListAsync();
        }

        public async Task<ActionResult<User>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.Users.FirstOrDefaultAsync(e => e.PersonneId == id);
        }

        public async Task<ActionResult<User>> GetByStringAsync(string chaine)
        {
            var user = await vinotripDbContext.Users.FirstOrDefaultAsync(u => u.Pseudo.ToUpper() == chaine.ToUpper());
            if (user == null)
            {
                var personne = await vinotripDbContext.Personnes.FirstOrDefaultAsync(u => u.Mail.ToUpper() == chaine.ToUpper());
                user = await vinotripDbContext.Users.FirstOrDefaultAsync(u => u.PersonneId == personne.PersonneId);
            }
            return user;
        }

        public async Task AddAsync(User entity)
        {
            await vinotripDbContext.Users.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user, User entity)
        {
            vinotripDbContext.Entry(entity).State = EntityState.Modified;
            user.PersonneId = entity.PersonneId;
            user.Titre = entity.Titre;
            user.Prenom = entity.Prenom;
            user.Pseudo = entity.Pseudo;
            user.DateNaissance = entity.DateNaissance;
            user.Tel = entity.Tel;
            user.Mdp = entity.Mdp;
            user.Newsletter = entity.Newsletter;
            user.DateConnexion = entity.DateConnexion;
            user.Role = entity.Role;
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            vinotripDbContext.Users.Remove(user);
            await vinotripDbContext.SaveChangesAsync();
        }
    }
}
