using Microsoft.AspNetCore.Mvc;
using WS_VINOTRIP.Models.EntityFramework;

namespace WS_VINOTRIP.Models.Repository
{
    public interface IDataRepositoryReside<TEntity> : IDataRepository<TEntity>
    {
        public Task<ActionResult<IEnumerable<Reside>>> GetByAdresseIdAsync(int id);
        public Task DeleteByDoubleIdAsync(int userId, int id);
    }
}
