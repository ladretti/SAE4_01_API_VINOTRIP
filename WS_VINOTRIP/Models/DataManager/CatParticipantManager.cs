using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class CatParticipantManager : IDataRepository<CatParticipant>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public CatParticipantManager()
        { }

        public CatParticipantManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<CatParticipant>>> GetAllAsync()
        {
            return await vinotripDbContext.CatsParticipant.ToListAsync();
        }

        public async Task<ActionResult<CatParticipant>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.CatsParticipant.FirstOrDefaultAsync(e => e.CatParticipantId == id);
        }

        public async Task<ActionResult<CatParticipant>> GetByStringAsync(string titre)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(CatParticipant entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(CatParticipant sejour, CatParticipant entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(CatParticipant sejour)
        {
            throw new NotImplementedException();
        }
    }
}
