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
    public class RefCarteBancairesControllerTests
    {
        private readonly VinotripDBContext _context;
        private readonly RefCarteBancairesController _controller;
        private readonly IDataRepositoryRefCarteBancaire<RefCarteBancaire> _dataRepository;
        private readonly IDataRepository<CompteCarte> _dataRepositoryCompteCarte;

        public RefCarteBancairesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<VinotripDBContext>().UseNpgsql("Server=vinotrip.postgres.database.azure.com;port=5432;Database=vinotrique; uid=vinotrip_admin; password=Prout18#"); // Chaine de connexion à mettre dans les ( )
            _context = new VinotripDBContext(builder.Options);
            _dataRepository = new RefCarteBancaireManager(_context);
            _dataRepositoryCompteCarte = new CompteCarteManager(_context);
            _controller = new RefCarteBancairesController(_dataRepository, _dataRepositoryCompteCarte);
        }

        //public async Task<ActionResult<RefCarteBancaire>> GetRefCarteBancaire(int id)
        [TestMethod()]
        public async Task GetRefCarteBancaireByIdTestAsync()
        {
            ActionResult<RefCarteBancaire> RefCarteBancaire = await _controller.GetRefCarteBancaireById(1);
            Assert.AreEqual(_context.RefCarteBancaires.Where(c => c.CarteId == 1).FirstOrDefault(), RefCarteBancaire.Value, "RefCarteBancaire différent");
        }

        [TestMethod()]
        public async Task GetRefCarteBancaireByIdTestAsyncFalse()
        {
            ActionResult<RefCarteBancaire> RefCarteBancaire = await _controller.GetRefCarteBancaireById(1);
            Assert.AreNotEqual(_context.RefCarteBancaires.Where(c => c.CarteId == 2).FirstOrDefault(), RefCarteBancaire.Value, "RefCarteBancaire différent");
        }

        [TestMethod]
        public void GetRefCarteBancaireById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            RefCarteBancaire RefCarteBancaire = new RefCarteBancaire
            {
                CarteId= 1,
                NumCarte= "10000010101",
                DateExpirationCarte= new DateTime(2023,04,04),
                NomCarte= "Connard",
            };
            var mockRepository = new Mock<IDataRepositoryRefCarteBancaire<RefCarteBancaire>>();
            var mockRepository1 = new Mock<IDataRepository<CompteCarte>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(RefCarteBancaire);
            var RefCarteBancaireController = new RefCarteBancairesController(mockRepository.Object, mockRepository1.Object);
            // Act
            var actionResult = RefCarteBancaireController.GetRefCarteBancaireById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(RefCarteBancaire, actionResult.Value as RefCarteBancaire);
        }

        [TestMethod]
        public void GetRefCarteBancaireById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepositoryRefCarteBancaire<RefCarteBancaire>>();
            var mockRepository1 = new Mock<IDataRepository<CompteCarte>>();
            var RefCarteBancaireController = new RefCarteBancairesController(mockRepository.Object, mockRepository1.Object);
            // Act
            var actionResult = RefCarteBancaireController.GetRefCarteBancaireById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }
        //public async Task<ActionResult<IEnumerable<RefCarteBancaire>>> GetCarteByUserId(int userid)
        [TestMethod()]
        public async Task GetCarteByUserIdTestAsync()
        {
            ActionResult<IEnumerable<RefCarteBancaire>> RefCarteBancaire = await _controller.GetCarteByUserId(130);
            var e = _context.CompteCartes.FirstOrDefault(e => e.PersonneId == 130);
            Assert.AreEqual(_context.RefCarteBancaires.Where(c => c.CarteId == e.CarteId).FirstOrDefault(), RefCarteBancaire.Value.ToList()[0], "RefCarteBancaire différent");
        }

        [TestMethod()]
        public async Task GetCarteByUserIdTestAsyncFalse()
        {
            ActionResult<IEnumerable<RefCarteBancaire>> RefCarteBancaire = await _controller.GetCarteByUserId(130);
            var e = _context.CompteCartes.FirstOrDefault(e => e.PersonneId == 142);
            Assert.AreNotEqual(_context.RefCarteBancaires.Where(c => c.CarteId == e.CarteId).FirstOrDefault(), RefCarteBancaire.Value.ToList()[0], "RefCarteBancaire différent");
        }

        [TestMethod]
        public void GetCarteByUserId_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            RefCarteBancaire RefCarteBancaire = new RefCarteBancaire
            {
                CarteId = 1,
                NumCarte = "10000010101",
                DateExpirationCarte = new DateTime(2023, 04, 04),
                NomCarte = "Connard",
            };
            List<RefCarteBancaire> listRefCarteBancaire = new List<RefCarteBancaire>() { RefCarteBancaire };
            var mockRepository = new Mock<IDataRepositoryRefCarteBancaire<RefCarteBancaire>>();
            var mockRepository1 = new Mock<IDataRepository<CompteCarte>>();
            mockRepository.Setup(x => x.GetByUserIdAsync(130).Result).Returns(listRefCarteBancaire);
            var RefCarteBancaireController = new RefCarteBancairesController(mockRepository.Object, mockRepository1.Object);
            // Act
            var actionResult = RefCarteBancaireController.GetCarteByUserId(130);
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Result.Value);
            Assert.AreEqual(listRefCarteBancaire, actionResult.Result.Value as List<RefCarteBancaire>);
        }

        [TestMethod]
        public void GetCarteByUserId_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepositoryRefCarteBancaire<RefCarteBancaire>>();
            var mockRepository1 = new Mock<IDataRepository<CompteCarte>>();
            var RefCarteBancaireController = new RefCarteBancairesController(mockRepository.Object, mockRepository1.Object);
            // Act
            var actionResult = RefCarteBancaireController.GetCarteByUserId(140).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }
        //public async Task<ActionResult<RefCarteBancaire>> PostRefCarteBancaire(RefCarteBancaire refCarteBancaire, int userId)
        [TestMethod]
        public void PostRefCarteBancaire_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepositoryRefCarteBancaire<RefCarteBancaire>>();
            var mockRepository1 = new Mock<IDataRepository<CompteCarte>>();
            var RefCarteBancaireController = new RefCarteBancairesController(mockRepository.Object, mockRepository1.Object);

            RefCarteBancaire refCarteBancaire = new RefCarteBancaire
            {
                NumCarte = "98874562310456",
                DateExpirationCarte = new DateTime(2026, 05, 13),
                NomCarte = "Roger"
            };

            // Act
            var actionResult = RefCarteBancaireController.PostRefCarteBancaire(refCarteBancaire, 142).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<RefCarteBancaire>), "Pas un ActionResult<RefCarteBancaire>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(RefCarteBancaire), "Pas un RefCarteBancaire");
            refCarteBancaire.CarteId = ((RefCarteBancaire)result.Value).CarteId;
            Assert.AreEqual(refCarteBancaire, (RefCarteBancaire)result.Value, "RefCarteBancaire pas identiques");
        }
        //public async Task<IActionResult> DeleteRefCarteBancaire(int id)
        [TestMethod]
        public void DeleteRefCarteBancaireTest_AvecMoq()
        {
            // Arrange
            RefCarteBancaire refCarteBancaire = new RefCarteBancaire
            {
                CarteId = 1,
                NumCarte = "10000010101",
                DateExpirationCarte = new DateTime(2023, 04, 04),
                NomCarte = "Connard",
            };

            var mockRepository = new Mock<IDataRepositoryRefCarteBancaire<RefCarteBancaire>>();
            var mockRepository1 = new Mock<IDataRepository<CompteCarte>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(refCarteBancaire);
            var RefCarteBancaireController = new RefCarteBancairesController(mockRepository.Object, mockRepository1.Object);
            // Act
            var actionResult = RefCarteBancaireController.DeleteRefCarteBancaire(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}