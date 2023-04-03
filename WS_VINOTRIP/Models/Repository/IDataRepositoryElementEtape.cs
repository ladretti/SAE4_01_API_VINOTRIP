using Microsoft.AspNetCore.Mvc;
using WS_VINOTRIP.Models.EntityFramework;

namespace WS_VINOTRIP.Models.Repository
{
    public interface IDataRepositoryElementEtape<TEntity> : IDataRepository<TEntity>
    {
        public Task<ActionResult<IEnumerable<ElementEtape>>> GetByEtapeIdAsync(int etapeId);

    }
}
