using Microsoft.AspNetCore.Mvc;
using WS_VINOTRIP.Models.EntityFramework;

namespace WS_VINOTRIP.Models.Repository
{
    public interface IDataRepositoryAdresse<TEntity> : IDataRepository<TEntity>
    {
        public Task<ActionResult<IEnumerable<TEntity>>> GetByUserId(int id);
    }
}
