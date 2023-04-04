using Microsoft.VisualStudio.TestTools.UnitTesting;
using WS_VINOTRIP.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS_VINOTRIP.Models.Repository;
using WS_VINOTRIP.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.DataManager;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WS_VINOTRIP.Controllers.Tests
{
    [TestClass()]
    public class AvisControllerTests
    {
        private readonly VinotripDBContext _context;
        private readonly AvisController _controller;
        private IDataRepositoryAvis<Avis> _dataRepository;

        public AvisControllerTests()
        {
            var builder = new DbContextOptionsBuilder<VinotripDBContext>().UseNpgsql("Server=vinotrip.postgres.database.azure.com;port=5432;Database=vinotrique; uid=vinotrip_admin; password=Prout18#"); // Chaine de connexion à mettre dans les ( )
            _context = new VinotripDBContext(builder.Options);
            _dataRepository = new AvisManager(_context);
            _controller = new AvisController(_dataRepository);
        }


        //public async Task<ActionResult<Avis>> GetAvisById(int id)




        [TestMethod()]
        public async Task GetAvisByIdTestAsync()
        {
            ActionResult<Avis> avis = await _controller.GetAvisById(25);
            Assert.AreEqual(_context.Aviss.Where(c => c.AvisId == 25).FirstOrDefault(), avis.Value, "Avis différent");
        }

        [TestMethod()]
        public async Task GetAvisByIdTestAsyncFalse()
        {
            ActionResult<Avis> avis = await _controller.GetAvisById(25);
            Assert.AreNotEqual(_context.Aviss.Where(c => c.AvisId == 26).FirstOrDefault(), avis.Value, "Avis différent");
        }

        [TestMethod]
        public void GetAvisById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Avis avis = new Avis
            {
                AvisId = 1,
                PersonneId = 54,
                SejourId = 1,
                Titre = "J'adore",
                Note = 5,
                Description = "Simplement incroyable, autant les restaurants et les vins que les activités et les hébergements. Je recommande fortement..",
                DateAvis = new DateTime(2021, 04, 10),
                Reponse = null
            };
            var mockRepository = new Mock<IDataRepositoryAvis<Avis>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(avis);
            var avisController = new AvisController(mockRepository.Object);
            // Act
            var actionResult = avisController.GetAvisById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(avis, actionResult.Value as Avis);
        }

        [TestMethod]
        public void GetAvisById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepositoryAvis<Avis>>();
            var avisController = new AvisController(mockRepository.Object);
            // Act
            var actionResult = avisController.GetAvisById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }



        //public async Task<ActionResult<IEnumerable<Avis>>> GetAvisBySejourId(int id)
        [TestMethod()]
        public async Task GetAvisBySejourIdTestAsync()
        {
            ActionResult<IEnumerable<Avis>> avis = await _controller.GetAvisBySejourId(1);
            IEnumerable<Avis> listavis = _context.Aviss.Where(c => c.SejourId == 1);
            List<Avis> listAvis = new List<Avis>();

            foreach (Avis e in listavis)
            {
                listAvis.Add(e);
            }

            Assert.AreEqual(listAvis[0], avis.Value.First(), "List d'avis différent");
        }

        [TestMethod()]
        public async Task GetAvisBySejourIdTestAsyncFalse()
        {
            ActionResult<IEnumerable<Avis>> avis = await _controller.GetAvisBySejourId(1);
            IEnumerable<Avis> listavis = _context.Aviss.Where(c => c.SejourId == 6);
            List<Avis> listAvis = new List<Avis>();

            foreach (Avis e in listavis)
            {
                listAvis.Add(e);
            }

            Assert.AreNotEqual(listAvis[0], avis.Value.First(), "List d'avis différent");
        }

        [TestMethod]
        public void GetAvisBySejourId_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Avis avis = new Avis
            {
                AvisId = 1,
                PersonneId = 54,
                SejourId = 1,
                Titre = "J'adore",
                Note = 5,
                Description = "Simplement incroyable, autant les restaurants et les vins que les activités et les hébergements. Je recommande fortement..",
                DateAvis = new DateTime(2021, 04, 10),
                Reponse = null
            };
            Avis avis2 = new Avis
            {
                AvisId= 2,
                PersonneId= 41,
                SejourId= 1,
                Titre= "Décevant",
                Note= 1,
                Description= "Horrible, le restaurant m'a rendu malade et le vin pour la dégustation était bouchonné.",
                DateAvis= new DateTime(2021, 03, 20),
                Reponse= null
            };
            List<Avis> listavis = new List<Avis>() { avis, avis2 };

            var mockRepository = new Mock<IDataRepositoryAvis<Avis>>();
            mockRepository.Setup(x => x.GetBySejourIdAsync(1).Result).Returns(listavis);
            var avisController = new AvisController(mockRepository.Object);
            // Act
            var actionResult = avisController.GetAvisBySejourId(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(listavis[0], actionResult.Value.First()) ;
        }

        [TestMethod]
        public void GetAvisBySejourId_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepositoryAvis<Avis>>();
            var avisController = new AvisController(mockRepository.Object);
            // Act
            var actionResult = avisController.GetAvisBySejourId(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }

        //public async Task<ActionResult<Avis>> PostAvis(Avis avis)

        [TestMethod]
        public void Postavis_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepositoryAvis<Avis>>();
            var avisController = new AvisController(mockRepository.Object);

            Avis avis = new Avis
            {
                PersonneId = 41,
                SejourId = 4,
                Titre = "Mouais",
                Note = 1,
                Description = "Le mec de l'acceuil m'a dit quoicoubeh j'ai pas trouvé ça drole",
                DateAvis = new DateTime(2023, 03, 30),
                Reponse = null
            };

            // Act
            var actionResult = avisController.PostAvis(avis).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Avis>), "Pas un ActionResult<Avis>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Avis), "Pas un Avis");
            avis.AvisId = ((Avis)result.Value).AvisId;
            Assert.AreEqual(avis, (Avis)result.Value, "Avis pas identiques");
        }
        //public async Task<IActionResult> DeleteAvis(int id)
        [TestMethod]
        public void DeleteAvisTest_AvecMoq()
        {
            // Arrange
            Avis avis = new Avis
            {
                PersonneId = 41,
                SejourId = 4,
                Titre = "Mouais",
                Note = 1,
                Description = "Le mec de l'acceuil m'a dit quoicoubeh j'ai pas trouvé ça drole",
                DateAvis = new DateTime(2023, 03, 30),
                Reponse = null
            };

            var mockRepository = new Mock<IDataRepositoryAvis<Avis>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(avis);
            var avisController = new AvisController(mockRepository.Object);
            // Act
            var actionResult = avisController.DeleteAvis(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

    }
}