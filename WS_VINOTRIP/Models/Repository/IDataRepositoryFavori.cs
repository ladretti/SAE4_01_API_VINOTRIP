using Microsoft.AspNetCore.Mvc;
using WS_VINOTRIP.Models.EntityFramework;

namespace WS_VINOTRIP.Models.Repository
{
    public interface IDataRepositoryFavori<TEntity> : IDataRepository<TEntity>
    {
        public Task<ActionResult<TEntity>> GetBySejourIdUserIdAsync(int sejourid, int userid);
        public Task<ActionResult<IEnumerable<TEntity>>> GetByUserIdAsync(int userid);
    }
}
