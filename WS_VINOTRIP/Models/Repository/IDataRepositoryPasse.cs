using Microsoft.AspNetCore.Mvc;
using WS_VINOTRIP.Models.EntityFramework;

namespace WS_VINOTRIP.Models.Repository
{
    public interface IDataRepositoryPasse<TEntity> : IDataRepository<TEntity>
    {
        public Task<ActionResult<IEnumerable<TEntity>>> GetByReservationsId(int id);
    }
}
