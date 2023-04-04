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
    public class SejoursControllerTests
    {
        private readonly VinotripDBContext _context;
        private readonly SejoursController _controller;
        private readonly IDataRepositorySejour<Sejour> _dataRepository;
        private readonly IDataRepository<Comporte> _dataRepositoryComporte;
        private readonly IDataRepository<Lien> _dataRepositoryLien;
        private readonly IDataRepository<LienSejour> _dataRepositoryLienSejour;

        public SejoursControllerTests()
        {
            var builder = new DbContextOptionsBuilder<VinotripDBContext>().UseNpgsql("Server=vinotrip.postgres.database.azure.com;port=5432;Database=vinotrique; uid=vinotrip_admin; password=Prout18#"); // Chaine de connexion à mettre dans les ( )
            _context = new VinotripDBContext(builder.Options);
            _dataRepository = new SejourManager(_context);
            _dataRepositoryComporte = new ComporteManager(_context);
            _dataRepositoryLien = new LienManager(_context);
            _dataRepositoryLienSejour = new LienSejourManager(_context);
            _controller = new SejoursController(_dataRepository, _dataRepositoryComporte, _dataRepositoryLienSejour, _dataRepositoryLien);
        }

        //public async Task<ActionResult<IEnumerable<Sejour>>> GetSejours()
        [TestMethod()]
        public async Task GetSejoursTestAsync()
        {
            ActionResult<IEnumerable<Sejour>> sejours = await _controller.GetSejours();
            CollectionAssert.AreEqual(_context.Sejours.ToList(), sejours.Value.ToList(), "La liste renvoyée n'est pas la bonne.");
        }

        //public async Task<ActionResult<Sejour>> GetSejourById(int id)

        [TestMethod()]
        public async Task GetSejourByIdTestAsync()
        {
            ActionResult<Sejour> sejour = await _controller.GetSejourById(1);
            Assert.AreEqual(_context.Sejours.Where(c => c.SejourId == 1).FirstOrDefault(), sejour.Value, "Sejour différent");
        }

        [TestMethod()]
        public async Task GetSejourByIdTestAsyncFalse()
        {
            ActionResult<Sejour> sejour = await _controller.GetSejourById(1);
            Assert.AreNotEqual(_context.Sejours.Where(c => c.SejourId == 2).FirstOrDefault(), sejour.Value, "Sejour différent");
        }

        [TestMethod]
        public void GetSejourById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Sejour sejour = new Sejour
            {
                SejourId = 2,
                RouteVinId = 5,
                CatSejourId = 3,
                CatVignobleId = 4,
                Titre = "Week-end vin et golf en Champagne",
                Description = "Combinez le plaisir du golf à des dégustations et des expériences gastronomiques lors d’un séjour oenologique en Champagne, au cœur d'un site historique à couper le souffle.",
                Prix = 455,
                NbJour = 2,
                NbNuit = 1,
            };
            var mockRepository = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<Comporte>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();
            var mockRepository4 = new Mock<IDataRepository<LienSejour>>();
            mockRepository.Setup(x => x.GetByIdAsync(2).Result).Returns(sejour);
            var sejourController = new SejoursController(mockRepository.Object, mockRepository2.Object, mockRepository4.Object, mockRepository3.Object);
            // Act
            var actionResult = sejourController.GetSejourById(2).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(sejour, actionResult.Value as Sejour);
        }

        [TestMethod]
        public void GetSejourById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<Comporte>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();
            var mockRepository4 = new Mock<IDataRepository<LienSejour>>();

            var sejourController = new SejoursController(mockRepository.Object, mockRepository2.Object, mockRepository4.Object, mockRepository3.Object);
            // Act
            var actionResult = sejourController.GetSejourById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }

        //public async Task<ActionResult<IEnumerable<Sejour>>> GetSejoursByRouteDesVins(int id)
        [TestMethod()]
        public async Task GetSejoursByRouteDesVinsTestAsync()
        {
            ActionResult<IEnumerable<Sejour>> sejours = await _controller.GetSejoursByRouteDesVins(1);
            Assert.AreEqual(_context.Sejours.Where(c => c.RouteVinId == 1).ToList()[0], sejours.Value.ToList()[0], "Liste Sejour différent");
            Assert.AreEqual(_context.Sejours.Where(c => c.RouteVinId == 1).ToList().Count(), sejours.Value.ToList().Count(), "Taille liste Sejour différente");
        }

        [TestMethod()]
        public async Task GetSejoursByRouteDesVinsTestAsyncFalse()
        {
            ActionResult<IEnumerable<Sejour>> sejours = await _controller.GetSejoursByRouteDesVins(1);
            Assert.AreNotEqual(_context.Sejours.Where(c => c.RouteVinId == 2).ToList()[0], sejours.Value.ToList()[0], "Liste Sejour différent");
            Assert.AreNotEqual(_context.Sejours.Where(c => c.RouteVinId == 2).ToList().Count(), sejours.Value.ToList().Count(), "Taille liste Sejour différente");
        }

        [TestMethod]
        public void GetSejoursByRouteDesVins_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Sejour sejour = new Sejour
            {
                SejourId = 15,
                RouteVinId = 1,
                CatSejourId = 1,
                CatVignobleId = null,
                Titre = "Séjour",
                Description = "séjour",
                Prix = 100,
                NbJour = 2,
                NbNuit = 1,
            };
            List<Sejour> listSejours = new List<Sejour>() { sejour };
            var mockRepository = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<Comporte>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();
            var mockRepository4 = new Mock<IDataRepository<LienSejour>>();
            mockRepository.Setup(x => x.GetByRouteDesVinsIdAsync(1).Result).Returns(listSejours);
            var sejourController = new SejoursController(mockRepository.Object, mockRepository2.Object, mockRepository4.Object, mockRepository3.Object);
            // Act
            var actionResult = sejourController.GetSejoursByRouteDesVins(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(listSejours[0], actionResult.Value.ToList()[0] as Sejour);
        }

        [TestMethod]
        public void GetSejoursByRouteDesVins_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<Comporte>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();
            var mockRepository4 = new Mock<IDataRepository<LienSejour>>();

            var sejourController = new SejoursController(mockRepository.Object, mockRepository2.Object, mockRepository4.Object, mockRepository3.Object);
            // Act
            var actionResult = sejourController.GetSejoursByRouteDesVins(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }

        //public async Task<IActionResult> PutSejour(int id, Sejour sejour)
        [TestMethod]
        public void PutSejourTest_AvecMoq()
        {
            Sejour sejour = new Sejour
            {
                 SejourId= 1,
                 RouteVinId= 2,
                 CatSejourId= 1,
                 CatVignobleId= 1,
                 Titre= "Week-end découverte de l'œnologie",
                 Description= "Faites plaisir à vos proches et offrez-leur un séjour découverte des vins. Un coffret cadeau au coeur des vignobles bourguignon et bordelais !",
                 Prix= 134.5,
                 NbJour= 2,
                 NbNuit= 1,   
            };

            Sejour sejourUpdated = new Sejour
            {
                SejourId = 1,
                RouteVinId = 2,
                CatSejourId = 1,
                CatVignobleId = 1,
                Titre = "e",
                Description = "e",
                Prix = 180,
                NbJour = 2,
                NbNuit = 1,
            };
            // Act

            var mockRepository = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<Comporte>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();
            var mockRepository4 = new Mock<IDataRepository<LienSejour>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(sejour);
            var sejourController = new SejoursController(mockRepository.Object, mockRepository2.Object, mockRepository4.Object, mockRepository3.Object);

            // Act
            var actionResult = sejourController.PutSejour(sejourUpdated.SejourId, sejourUpdated).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        //public async Task<ActionResult<Sejour>> PostSejour(Sejour sejour)
        [TestMethod]
        public void PostSejour_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<Comporte>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();
            var mockRepository4 = new Mock<IDataRepository<LienSejour>>();

            var sejourController = new SejoursController(mockRepository.Object, mockRepository2.Object, mockRepository4.Object, mockRepository3.Object);

            Sejour sejour = new Sejour
            {
                RouteVinId = 2,
                CatSejourId = 1,
                CatVignobleId = 1,
                Titre = "e",
                Description = "e",
                Prix = 180,
                NbJour = 2,
                NbNuit = 1,
            };

            // Act
            var actionResult = sejourController.PostSejour(sejour).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Sejour>), "Pas un ActionResult<Sejour>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Sejour), "Pas un Sejour");
            sejour.SejourId = ((Sejour)result.Value).SejourId;
            Assert.AreEqual(sejour, (Sejour)result.Value, "Sejour pas identiques");
        }
        //public async Task<IActionResult> DeleteSejour(int id)
        [TestMethod]
        public void DeleteEtapeTest_AvecMoq()
        {
            // Arrange
            Sejour sejour = new Sejour
            {
                SejourId = 1,
                RouteVinId = 2,
                CatSejourId = 1,
                CatVignobleId = 1,
                Titre = "Week-end découverte de l'œnologie",
                Description = "Faites plaisir à vos proches et offrez-leur un séjour découverte des vins. Un coffret cadeau au coeur des vignobles bourguignon et bordelais !",
                Prix = 134.5,
                NbJour = 2,
                NbNuit = 1,
            };
            var mockRepository = new Mock<IDataRepositorySejour<Sejour>>();
            var mockRepository2 = new Mock<IDataRepository<Comporte>>();
            var mockRepository3 = new Mock<IDataRepository<Lien>>();
            var mockRepository4 = new Mock<IDataRepository<LienSejour>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(sejour);
            var sejourController = new SejoursController(mockRepository.Object, mockRepository2.Object, mockRepository4.Object, mockRepository3.Object);
            
            // Act
            var actionResult = sejourController.DeleteSejour(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}