using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TypePayementsController : ControllerBase
    {
        private readonly IDataRepository<TypePayement> dataRepository;

        public TypePayementsController(IDataRepository<TypePayement> dataRepo)
        {
            dataRepository = dataRepo;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<TypePayement>>> GetAllTypePayement()
        {
            var typeDePayments = await dataRepository.GetAllAsync();

            if (typeDePayments == null)
            {
                return NotFound();

            }
            return typeDePayments.Value.OrderBy(etape => etape.TypePayementId).ToList();
        }

        // GET: api/TypePayements/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TypePayement>> GetTypePayement(int id)
        {
            var typeDePayment = await dataRepository.GetByIdAsync(id);

            if (typeDePayment == null)
            {
                return NotFound();
            }

            return typeDePayment;
        }

    }
}
