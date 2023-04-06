using Microsoft.VisualStudio.TestTools.UnitTesting;
using WS_VINOTRIP.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.DataManager;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WS_VINOTRIP.Controllers.Tests
{
    [TestClass()]
    public class FavorisControllerTests
    {
        private readonly VinotripDBContext _context;
        private readonly FavorisController _controller;
        private readonly IDataRepositoryFavori<Favori> _dataRepository;
        private readonly IDataRepositorySejour<Sejour> _dataRepositorySejour;
        private readonly IDataRepository<Lien> _dataRepositoryLien;
        private readonly IDataRepository<LienSejour> _dataRepositoryLienSejour;

        public FavorisControllerTests()
        {
            var builder = new DbContextOptionsBuilder<VinotripDBContext>().UseNpgsql("Server=vinotrip.postgres.database.azure.com;port=5432;Database=vinotrique; uid=vinotrip_admin; password=Prout18#"); // Chaine de connexion à mettre dans les ( )
            _context = new VinotripDBContext(builder.Options);
            _dataRepository = new FavorisManager(_context);
            _dataRepositorySejour = new SejourManager(_context);
            _dataRepositoryLien = new LienManager(_context);
            _dataRepositoryLienSejour = new LienSejourManager(_context);
            _controller = new FavorisController(_dataRepository, _dataRepositorySejour, _dataRepositoryLien, _dataRepositoryLienSejour);
        }

        //public async Task<ActionResult<Favori>> GetFavoriByIds(int sejourid, int userid)
        [TestMethod()]
        public async Task GetFavoriByIdsTestAsync()
        {
            ActionResult<Favori> favori = await _controller.GetFavoriByIds(130, 1);
            Assert.AreEqual(_context.Favoris.FirstOrDefault(c => c.PersonneId == 130 && c.SejourId == 1), favori.Value, "Favori différent");
        }

        [TestMethod()]
        public async Task GetFavoriByIdsTestAsyncFalse()
        {
            ActionResult<Favori> favori = await _controller.GetFavoriByIds(130, 1);
            Assert.AreNotEqual(_context.Favoris.FirstOrDefault(c => c.PersonneId == 142 && c.SejourId == 1), favori.Value, "Favori différent");
        }

        [TestMethod]
        public void GetFavoriByIds_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Favori favori = new Favori
            {
                PersonneId = 142,
                SejourId = 1,
            };

            var mockRepository = new Mock<IDataRepositoryFavori<Favori>>();
            var mockRepository1 = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<LienSejour>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();
            mockRepository.Setup(x => x.GetBySejourIdUserIdAsync(130,1).Result).Returns(favori);
            var FavoriController = new FavorisController(mockRepository.Object, mockRepository1.Object, mockRepository3.Object, mockRepository2.Object);
            // Act
            var actionResult = FavoriController.GetFavoriByIds(130,1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(favori, actionResult.Value as Favori);
        }

        [TestMethod]
        public void GetFavoriByIds_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepositoryFavori<Favori>>();
            var mockRepository1 = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<LienSejour>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();

            var FavoriController = new FavorisController(mockRepository.Object, mockRepository1.Object, mockRepository3.Object, mockRepository2.Object);
            // Act
            var actionResult = FavoriController.GetFavoriByIds(0, 96).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }
        //public async Task<ActionResult<IEnumerable<Favori>>> GetFavorisByUserId(int userid)
        [TestMethod()]
        public async Task GetFavorisByUserIdTestAsync()
        {
            ActionResult<IEnumerable<Favori>> favori = await _controller.GetFavorisByUserId(142);
            Assert.AreEqual(_context.Favoris.Where(c => c.PersonneId == 142).ToList()[0], favori.Value.ToList()[0], "Favori différent");
        }

        [TestMethod()]
        public async Task GetFavorisByUserIdTestAsyncFalse()
        {
            ActionResult<IEnumerable<Favori>> favori = await _controller.GetFavorisByUserId(130);
            Assert.AreNotEqual(_context.Favoris.Where(c => c.PersonneId == 142).ToList()[0], favori.Value.ToList()[0], "Favori différent");
        }

        [TestMethod]
        public void GetFavorisByUserId_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Favori favori = new Favori
            {
                PersonneId = 142,
                SejourId = 1,
            };
            Favori favori2 = new Favori
            {
                PersonneId = 142,
                SejourId = 2,
            };

            List<Favori> listFavori = new List<Favori> { favori, favori2 };
            var mockRepository = new Mock<IDataRepositoryFavori<Favori>>();
            var mockRepository1 = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<LienSejour>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();
            mockRepository.Setup(x => x.GetByUserIdAsync(130).Result).Returns(listFavori);
            var FavoriController = new FavorisController(mockRepository.Object, mockRepository1.Object, mockRepository3.Object, mockRepository2.Object);
            // Act
            var actionResult = FavoriController.GetFavorisByUserId(130).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(listFavori[0], actionResult.Value.FirstOrDefault() as Favori);
        }

        [TestMethod]
        public void GetFavorisByUserId_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepositoryFavori<Favori>>();
            var mockRepository1 = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<LienSejour>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();

            var FavoriController = new FavorisController(mockRepository.Object, mockRepository1.Object, mockRepository3.Object, mockRepository2.Object);
            // Act
            var actionResult = FavoriController.GetFavorisByUserId(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }
        //public async Task<ActionResult<Favori>> PostFavori(Favori favori)
        [TestMethod]
        public void PostFavori_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepositoryFavori<Favori>>();
            var mockRepository1 = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<LienSejour>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();
            var FavoriController = new FavorisController(mockRepository.Object, mockRepository1.Object, mockRepository3.Object, mockRepository2.Object);

            Favori favori = new Favori
            {
                PersonneId = 130,
                SejourId = 97,
            };


            // Act
            var actionResult = FavoriController.PostFavori(favori).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Favori>), "Pas un ActionResult<Favori>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Favori), "Pas un Favori");
            Assert.AreEqual(favori, (Favori)result.Value, "Favori pas identiques");
        }
        //public async Task<IActionResult> DeleteFavori(int sejourid, int userid)
        [TestMethod]
        public void DeleteFavoriTest_AvecMoq()
        {
            // Arrange
            Favori favori = new Favori
            {
                PersonneId = 142,
                SejourId = 1,
            };

            var mockRepository = new Mock<IDataRepositoryFavori<Favori>>();
            var mockRepository1 = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<LienSejour>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();
            mockRepository.Setup(x => x.GetBySejourIdUserIdAsync(142, 2).Result).Returns(favori);
            var FavoriController = new FavorisController(mockRepository.Object, mockRepository1.Object, mockRepository3.Object, mockRepository2.Object);

            // Act
            var actionResult = FavoriController.DeleteFavori(142, 2).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}