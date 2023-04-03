using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Models.DataManager
{
    public class ElementEtapeManager : IDataRepositoryElementEtape<ElementEtape>
    {
        readonly VinotripDBContext? vinotripDbContext;
        public ElementEtapeManager()
        { }

        public ElementEtapeManager(VinotripDBContext context)
        {
            vinotripDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<ElementEtape>>> GetAllAsync()
        {
            return await vinotripDbContext.ElementEtapes.ToListAsync();
        }

        public async Task<ActionResult<ElementEtape>> GetByIdAsync(int id)
        {
            return await vinotripDbContext.ElementEtapes.FirstOrDefaultAsync(e => e.ElementId == id);
        }

        public async Task AddAsync(ElementEtape entity)
        {
            await vinotripDbContext.ElementEtapes.AddAsync(entity);
            await vinotripDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ElementEtape elementEtape, ElementEtape entity)
        {
        }

        public async Task DeleteAsync(ElementEtape elementEtape)
        {
            vinotripDbContext.ElementEtapes.Remove(elementEtape);
            await vinotripDbContext.SaveChangesAsync();
        }

        public Task<ActionResult<ElementEtape>> GetByStringAsync(string numen)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<ElementEtape>>> GetByEtapeIdAsync(int etapeId)
        {
            var concernes =  vinotripDbContext.Concernes.Where(e => e.EtapeId == etapeId).ToList();

            List<ElementEtape> listElementEtapes = new List<ElementEtape>();

            foreach (Concerne c in concernes)
            {
                listElementEtapes.Add(GetByIdAsync(c.EtapeId).Result.Value);
            }
            return listElementEtapes;

        }
    }
}
