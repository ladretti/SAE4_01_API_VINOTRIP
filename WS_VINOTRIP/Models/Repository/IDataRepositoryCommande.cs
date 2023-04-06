using Microsoft.AspNetCore.Mvc;
using WS_VINOTRIP.Models.EntityFramework;

namespace WS_VINOTRIP.Models.Repository
{
    public interface IDataRepositoryCommande<TEntity> : IDataRepository<TEntity>
    {
        public Task<ActionResult<IEnumerable<TEntity>>> GetByUserIdAsync(int userid);
    }
}
