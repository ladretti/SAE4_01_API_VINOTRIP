using Microsoft.AspNetCore.Mvc;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EtapeController : ControllerBase
    {
        private readonly IDataRepositoryEtape<Etape> dataRepository;
        private readonly IDataRepository<Concerne> dataRepositoryConcerne;
        private readonly IDataRepositoryElementEtape<ElementEtape> dataRepositoryElementEtape;
        private readonly IDataRepository<LienEtape> dataRepositoryLienEtape;
        private readonly IDataRepository<Lien> dataRepositoryLien;

        public EtapeController(IDataRepositoryEtape<Etape> dataRepo, IDataRepositoryElementEtape<ElementEtape> dataRepoElementEtape, IDataRepository<Concerne> dataRepoConcerne, IDataRepository<LienEtape> dataRepoLienEtape, IDataRepository<Lien> dataRepoLien)
        {
            dataRepository = dataRepo;
            dataRepositoryConcerne = dataRepoConcerne;
            dataRepositoryElementEtape = dataRepoElementEtape;
            dataRepositoryLienEtape = dataRepoLienEtape;
            dataRepositoryLien = dataRepoLien;
        }


        /// <summary>
        /// Obtient toutes les étapes existantes.
        /// </summary>
        /// <returns>Une action résultant en une liste d'étapes existantes.</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Etape>>> GetEtape()
        {
            var etape = await dataRepository.GetAllAsync();
            var liensejour = dataRepositoryLienEtape.GetAllAsync().Result;
            var lien = dataRepositoryLien.GetAllAsync().Result;

            if (etape == null)
            {
                return NotFound();
            }
            return etape;
        }

        /// <summary>
        /// Obtient tous les concerne existants.
        /// </summary>
        /// <returns>Une action résultant en une liste de concerne existants.</returns>

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Concerne>>> GetConcerne()
        {
            var concerne = await dataRepositoryConcerne.GetAllAsync();

            if (concerne == null)
            {
                return NotFound();
            }
            return concerne;
        }

        /// <summary>
        /// Obtient une étape spécifique par son ID.
        /// </summary>
        /// <param name="id">L'ID de l'étape à récupérer.</param>
        /// <returns>Une action résultant en l'étape correspondante ou NotFound si elle n'existe pas.</returns>

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Etape>> GetEtapeById(int id)
        {
            var etape = await dataRepository.GetByIdAsync(id);
            var concerne = await dataRepositoryConcerne.GetAllAsync();
            var elementEtape = await dataRepositoryElementEtape.GetAllAsync();

            if (etape == null)
            {
                return NotFound();
            }

            return etape;
        }

        /// <summary>
        /// Obtient toutes les étapes correspondant à un séjour spécifique.
        /// </summary>
        /// <param name="id">L'ID du séjour dont les étapes doivent être récupérées.</param>
        /// <returns>Une action résultant en une liste d'étapes correspondantes ou NotFound si aucune étape n'est trouvée.</returns>

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Etape>>> GetEtapeBySejourId(int id)
        {
            var etapes = await dataRepository.GetBySejourIdAsync(id);

            if (etapes == null)
            {
                return NotFound();
            }

            return etapes;
        }

        /// <summary>
        /// Met à jour une étape en fonction de son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de l'étape à mettre à jour.</param>
        /// <param name="etape">L'objet étape avec les données à mettre à jour.</param>
        /// <returns>Un code de réponse HTTP 204 No Content si la mise à jour est réussie, ou un code de réponse HTTP 400 Bad Request si l'identifiant fourni ne correspond pas à celui de l'étape fournie, ou un code de réponse HTTP 404 Not Found si l'étape n'est pas trouvée.</returns>

        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutEtape(int id, Etape etape)
        {
            if (id != etape.EtapeId)
            {
                return BadRequest();
            }

            var etapeToUpdate = await dataRepository.GetByIdAsync(id);

            if (etapeToUpdate == null)
            {
                return NotFound();
            }

            else
            {
                await dataRepository.UpdateAsync(etapeToUpdate.Value, etape);
                return NoContent();
            }
        }

        /// <summary>
        /// Ajoute une nouvelle étape.
        /// </summary>
        /// <param name="etape">L'objet étape à ajouter.</param>
        /// <returns>Un code de réponse HTTP 201 Created si l'ajout est réussi, avec l'objet étape créé dans le corps de la réponse, ou un code de réponse HTTP 400 Bad Request si les données fournies ne sont pas valides.</returns>

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Etape>> PostEtape(Etape etape)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(etape);

            return CreatedAtAction("GetEtapeById", new { id = etape.SejourId }, etape); // GetById : nom de l’action
        }

        /// <summary>
        /// Supprime une étape en fonction de son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de l'étape à supprimer.</param>
        /// <returns>Un code de réponse HTTP 204 No Content si la suppression est réussie, ou un code de réponse HTTP 404 Not Found si l'étape n'est pas trouvée.</returns>

        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteEtape(int id)
        {
            var etape = await dataRepository.GetByIdAsync(id);

            if (etape == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(etape.Value);

            return NoContent();
        }
    }
}
