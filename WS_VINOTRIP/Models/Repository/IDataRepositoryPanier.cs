using Microsoft.AspNetCore.Mvc;
using WS_VINOTRIP.Models.EntityFramework;

namespace WS_VINOTRIP.Models.Repository
{
    public interface IDataRepositoryPanier<TEntity> : IDataRepository<TEntity>
    {
        public Task<ActionResult<TEntity>> GetByIdsAsync(int userId, int sejourId, bool offert);
        public Task<ActionResult<IEnumerable<Panier>>> GetByUserIdAsync(int userId);
    }
}
