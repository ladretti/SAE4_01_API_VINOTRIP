using Microsoft.VisualStudio.TestTools.UnitTesting;
using WS_VINOTRIP.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WS_VINOTRIP.Models.DataManager;

namespace WS_VINOTRIP.Controllers.Tests
{
    [TestClass()]
    public class PaniersControllerTests
    {
        private readonly VinotripDBContext _context;
        private readonly PaniersController _controller;
        private readonly IDataRepositoryPanier<Panier> _dataRepository;
        private readonly IDataRepositorySejour<Sejour> _dataRepositorySejour;
        private readonly IDataRepository<Lien> _dataRepositoryLien;
        private readonly IDataRepository<LienSejour> _dataRepositoryLienSejour;

        public PaniersControllerTests()
        {
            var builder = new DbContextOptionsBuilder<VinotripDBContext>().UseNpgsql("Server=vinotrip.postgres.database.azure.com;port=5432;Database=vinotrique; uid=vinotrip_admin; password=Prout18#"); // Chaine de connexion à mettre dans les ( )
            _context = new VinotripDBContext(builder.Options);
            _dataRepository = new PanierManager(_context);
            _dataRepositorySejour = new SejourManager(_context);
            _dataRepositoryLien = new LienManager(_context);
            _dataRepositoryLienSejour = new LienSejourManager(_context);
            _controller = new PaniersController(_dataRepository, _dataRepositorySejour, _dataRepositoryLien, _dataRepositoryLienSejour);
        }

        //public async Task<ActionResult<IEnumerable<Panier>>> GetPanierByUserId(int id)

        [TestMethod()]
        public async Task GetPanierByUserIdTestAsync()
        {
            ActionResult<IEnumerable<Panier>> panier = await _controller.GetPanierByUserId(130);
            Assert.AreEqual(_context.Paniers.Where(c => c.PersonneId == 130).ToList()[0], panier.Value.ToList()[0], "Panier différent");
        }

        [TestMethod()]
        public async Task GetPanierByIdTestAsyncFalse()
        {
            ActionResult<IEnumerable<Panier>> panier = await _controller.GetPanierByUserId(130);
            Assert.AreNotEqual(_context.Paniers.Where(c => c.PersonneId == 142).ToList()[0], panier.Value.ToList()[0], "Panier différent");
        }

        [TestMethod]
        public void GetPanierByUserId_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Panier panier = new Panier
            {
                PersonneId = 130,
                SejourId = 1,
                NbAdultes = 2,
                NbEnfants = 1,
                NbChambres = 1,
                Offert = false,
            };
            Panier panier2 = new Panier
            {
                PersonneId = 130,
                SejourId = 1,
                NbAdultes = 2,
                NbEnfants = 1,
                NbChambres = 1,
                Offert = true,
            };

            List<Panier> listPanier = new List<Panier> { panier, panier2 };
            var mockRepository = new Mock<IDataRepositoryPanier<Panier>>();
            var mockRepository1 = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<LienSejour>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();
            mockRepository.Setup(x => x.GetByUserIdAsync(130).Result).Returns(listPanier);
            var panierController = new PaniersController(mockRepository.Object, mockRepository1.Object, mockRepository3.Object, mockRepository2.Object);
            // Act
            var actionResult = panierController.GetPanierByUserId(130).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(listPanier, actionResult.Value as List<Panier>);
        }

        [TestMethod]
        public void GetPanierByUserId_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepositoryPanier<Panier>>();
            var mockRepository1 = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<LienSejour>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();

            var panierController = new PaniersController(mockRepository.Object, mockRepository1.Object, mockRepository3.Object, mockRepository2.Object);
            // Act
            var actionResult = panierController.GetPanierByUserId(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }
        //public async Task<IActionResult> PutPanier(int userId, int sejourId, Panier panier)
        [TestMethod]
        public void PutPanierTest_AvecMoq()
        {
            Panier panier = new Panier
            {
                PersonneId = 130,
                SejourId = 1,
                NbAdultes = 2,
                NbEnfants = 1,
                NbChambres = 1,
                Offert = false,
            };

            Panier panierUpdated = new Panier()
            {
                PersonneId = 130,
                SejourId = 1,
                NbAdultes = 4,
                NbEnfants = 1,
                NbChambres = 1,
                Offert = false,
            };
            // Act
            var mockRepository = new Mock<IDataRepositoryPanier<Panier>>();
            var mockRepository1 = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<LienSejour>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();
            mockRepository.Setup(x => x.GetByIdsAsync(130, 1, false).Result).Returns(panier);
            var panierController = new PaniersController(mockRepository.Object, mockRepository1.Object, mockRepository3.Object, mockRepository2.Object);

            // Act
            var actionResult = panierController.PutPanier(130, 1, panierUpdated).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        //public async Task<ActionResult<Panier>> PostPanier(Panier panier)
        [TestMethod]
        public void PostPanier_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepositoryPanier<Panier>>();
            var mockRepository1 = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<LienSejour>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();
            var panierController = new PaniersController(mockRepository.Object, mockRepository1.Object, mockRepository3.Object, mockRepository2.Object);

            Panier Panier = new Panier
            {
                PersonneId = 142,
                SejourId = 2,
                NbAdultes = 5,
                NbEnfants = 1,
                NbChambres = 1,
                Offert = false,
            };


            // Act
            var actionResult = panierController.PostPanier(Panier).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Panier>), "Pas un ActionResult<Panier>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Panier), "Pas un Panier");
            Assert.AreEqual(Panier, (Panier)result.Value, "Panier pas identiques");
        }

        //public async Task<IActionResult> DeletePanier(int id)
        [TestMethod]
        public void DeletePanierTest_AvecMoq()
        {
            // Arrange
            Panier panier = new Panier
            {
                PersonneId = 126,
                SejourId = 2,
                NbAdultes = 5,
                NbEnfants = 1,
                NbChambres = 1,
                Offert = false,
            };

            var mockRepository = new Mock<IDataRepositoryPanier<Panier>>();
            var mockRepository1 = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<LienSejour>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();
            mockRepository.Setup(x => x.GetByIdsAsync(126, 2, false).Result).Returns(panier);
            var panierController = new PaniersController(mockRepository.Object, mockRepository1.Object, mockRepository3.Object, mockRepository2.Object);

            // Act
            var actionResult = panierController.DeletePanier(126, 2, false).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}