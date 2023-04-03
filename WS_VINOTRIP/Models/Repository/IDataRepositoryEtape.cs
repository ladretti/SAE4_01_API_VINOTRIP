using Microsoft.AspNetCore.Mvc;
using WS_VINOTRIP.Models.EntityFramework;

namespace WS_VINOTRIP.Models.Repository
{
    public interface IDataRepositoryEtape<TEntity> : IDataRepository<TEntity>
    {
        public Task<ActionResult<IEnumerable<Etape>>> GetBySejourIdAsync(int userId);
    }
}
