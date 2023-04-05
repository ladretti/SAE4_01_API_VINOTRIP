using Microsoft.VisualStudio.TestTools.UnitTesting;
using WS_VINOTRIP.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WS_VINOTRIP.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.DataManager;
using WS_VINOTRIP.Models.Repository;
using Moq;

namespace WS_VINOTRIP.Controllers.Tests
{
    [TestClass()]
    public class AdressesControllerTests
    {
        private readonly VinotripDBContext _context;
        private readonly AdressesController _controller;
        private IDataRepositoryAdresse<Adresse> _dataRepository;
        private readonly IDataRepositoryReside<Reside> dataRepositoryReside;

        public AdressesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<VinotripDBContext>().UseNpgsql("Server=vinotrip.postgres.database.azure.com;port=5432;Database=vinotrique; uid=vinotrip_admin; password=Prout18#"); // Chaine de connexion à mettre dans les ( )
            _context = new VinotripDBContext(builder.Options);
            _dataRepository = new AdresseManager(_context);
            dataRepositoryReside = new ResideManager(_context);
            _controller = new AdressesController(_dataRepository, dataRepositoryReside);
        }

        //public async Task<ActionResult<Adresse>> GetAdresseById(int id)

        [TestMethod()]
        public async Task GetAdresseByIdTestAsync()
        {
            ActionResult<Adresse> Adresse = await _controller.GetAdresseById(1);
            Assert.AreEqual(_context.Adresses.Where(c => c.AdresseId == 1).FirstOrDefault(), Adresse.Value, "Adresse différent");
        }

        [TestMethod()]
        public async Task GetAdresseByIdTestAsyncFalse()
        {
            ActionResult<Adresse> Adresse = await _controller.GetAdresseById(1);
            Assert.AreNotEqual(_context.Adresses.Where(c => c.AdresseId == 2).FirstOrDefault(), Adresse.Value, "Adresse différent");
        }

        [TestMethod]
        public void GetAdresseById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Adresse adresse = new Adresse
            {
                AdresseId = 1,
                Rue1 = "160 avenue René Cassin",
                Rue2 = null,
                Cp = "84110",
                Ville = "Vaison la Romaine",
                Pays = "France"
            };
            var mockRepository = new Mock<IDataRepositoryAdresse<Adresse>>();
            var mockRepository1 = new Mock<IDataRepositoryReside<Reside>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(adresse);
            var adresseController = new AdressesController(mockRepository.Object, mockRepository1.Object);
            // Act
            var actionResult = adresseController.GetAdresseById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(adresse, actionResult.Value as Adresse);
        }

        [TestMethod]
        public void GetAdresseById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepositoryAdresse<Adresse>>();
            var mockRepository1 = new Mock<IDataRepositoryReside<Reside>>();
            var adresseController = new AdressesController(mockRepository.Object, mockRepository1.Object);
            // Act
            var actionResult = adresseController.GetAdresseById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }

        //public async Task<ActionResult<IEnumerable<Adresse>>> GetAdresseByUserId(int userid)
        [TestMethod()]
        public async Task GetAdresseByUserIdTestAsync()
        {
            ActionResult<IEnumerable<Adresse>> Adresse = await _controller.GetAdresseByUserId(142);
            var e = _context.Resides.FirstOrDefault(e => e.PersonneId == 142);
            Assert.AreEqual(_context.Adresses.Where(c => c.AdresseId == e.AdresseId).FirstOrDefault(), Adresse.Value.ToList()[0], "Adresse différent");
        }

        [TestMethod()]
        public async Task GetAdresseByUserIdTestAsyncFalse()
        {
            ActionResult<IEnumerable<Adresse>> Adresse = await _controller.GetAdresseByUserId(142);
            var e = _context.Resides.FirstOrDefault(e => e.PersonneId == 130);
            Assert.AreNotEqual(_context.Adresses.Where(c => c.AdresseId == e.AdresseId).FirstOrDefault(), Adresse.Value.ToList()[0], "Adresse différent");
        }

        [TestMethod]
        public void GetAdresseByUserId_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Adresse adresse = new Adresse
            {
                AdresseId = 320,
                Rue1 = "OUI",
                Rue2 = "OUsI",
                Cp = "18974",
                Ville = "JSP",
                Pays = "JSPASNONPLUS"
            };
            List<Adresse> listAdresse = new List<Adresse>() { adresse };
            var mockRepository = new Mock<IDataRepositoryAdresse<Adresse>>();
            var mockRepository1 = new Mock<IDataRepositoryReside<Reside>>();
            mockRepository.Setup(x => x.GetByUserId(130).Result).Returns(listAdresse);
            var adresseController = new AdressesController(mockRepository.Object, mockRepository1.Object);
            // Act
            var actionResult = adresseController.GetAdresseByUserId(130);
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Result.Value);
            Assert.AreEqual(listAdresse, actionResult.Result.Value as List<Adresse>);
        }

        [TestMethod]
        public void GetAdresseByUserId_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepositoryAdresse<Adresse>>();
            var mockRepository1 = new Mock<IDataRepositoryReside<Reside>>();
            var adresseController = new AdressesController(mockRepository.Object, mockRepository1.Object);
            // Act
            var actionResult = adresseController.GetAdresseByUserId(140).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }

        //public async Task<ActionResult<Adresse>> PostAdresse(Adresse adresse, int userId)
        [TestMethod]
        public void PostAdresse_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepositoryAdresse<Adresse>>();
            var mockRepository1 = new Mock<IDataRepositoryReside<Reside>>();
            var adresseController = new AdressesController(mockRepository.Object, mockRepository1.Object);

            Adresse adresse = new Adresse
            {
                Rue1 = "145 rue du test",
                Rue2 = null,
                Cp = "83000",
                Ville = "Toulon",
                Pays = "France"
            };

            // Act
            var actionResult = adresseController.PostAdresse(adresse, 142).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Adresse>), "Pas un ActionResult<Adresse>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Adresse), "Pas un Adresse");
            adresse.AdresseId = ((Adresse)result.Value).AdresseId;
            Assert.AreEqual(adresse, (Adresse)result.Value, "Adresse pas identiques");
        }
        //public async Task<IActionResult> DeleteAdresse(int id)
        [TestMethod]
        public void DeleteAdresseTest_AvecMoq()
        {
            // Arrange
            Adresse Adresse = new Adresse
            {
                AdresseId = 318,
                Rue1 = "OUI",
                Rue2 = "OUsI",
                Cp = "18974",
                Ville = "JSP",
                Pays = "JSPASNONPLUS"
            };

            var mockRepository = new Mock<IDataRepositoryAdresse<Adresse>>();
            var mockRepository1 = new Mock<IDataRepositoryReside<Reside>>();
            mockRepository.Setup(x => x.GetByIdAsync(318).Result).Returns(Adresse);
            var adresseController = new AdressesController(mockRepository.Object, mockRepository1.Object);
            // Act
            var actionResult = adresseController.DeleteAdresse(318, 130).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}