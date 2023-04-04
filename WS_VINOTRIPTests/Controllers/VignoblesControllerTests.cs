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
    public class VignoblesControllerTests
    {
        private readonly VinotripDBContext _context;
        private readonly VignoblesController _controller;
        private readonly IDataRepository<Vignoble> _dataRepository;
        private readonly IDataRepository<Lien> _dataRepositoryLien;
        private readonly IDataRepository<ElementVignoble> _dataRepositoryElementVignoble;
        private readonly IDataRepository<LienElementVignoble> _dataRepositoryLienElementVignoble;


        public VignoblesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<VinotripDBContext>().UseNpgsql("Server=vinotrip.postgres.database.azure.com;port=5432;Database=vinotrique; uid=vinotrip_admin; password=Prout18#"); // Chaine de connexion à mettre dans les ( )
            _context = new VinotripDBContext(builder.Options);
            _dataRepository = new VignobleManager(_context);
            _dataRepositoryLien = new LienManager(_context);
            _dataRepositoryElementVignoble = new ElementVignobleManager(_context);
            _dataRepositoryLienElementVignoble = new LienElementVignobleManager(_context);
            _controller = new VignoblesController(_dataRepository, _dataRepositoryLien, _dataRepositoryElementVignoble, _dataRepositoryLienElementVignoble);
        }

        //public async Task<ActionResult<IEnumerable<Vignoble>>> GetVignobles()
        [TestMethod()]
        public async Task GetVignoblesTestAsync()
        {
            ActionResult<IEnumerable<Vignoble>> vignobles = await _controller.GetVignobles();
            CollectionAssert.AreEqual(_context.Vignobles.ToList(), vignobles.Value.ToList(), "La liste renvoyée n'est pas la bonne.");
        }
        //public async Task<ActionResult<Vignoble>> GetVignobleById(int id)
        [TestMethod()]
        public async Task GetVignobleByIdTestAsync()
        {
            ActionResult<Vignoble> vignoble = await _controller.GetVignobleById(1);
            Assert.AreEqual(_context.Vignobles.Where(c => c.VignobleId == 1).FirstOrDefault(), vignoble.Value, "Vignoble différent");
        }

        [TestMethod()]
        public async Task GetVignobleByIdTestAsyncFalse()
        {
            ActionResult<Vignoble> vignoble = await _controller.GetVignobleById(1);
            Assert.AreNotEqual(_context.Vignobles.Where(c => c.VignobleId == 2).FirstOrDefault(), vignoble.Value, "Vignoble différent");
        }

        [TestMethod]
        public void GetVignobleById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Vignoble vignoble = new Vignoble
            {
                VignobleId = 1,
                Titre = "Vignoble de Bourgogne",
                Description = "Le vignoble de bourgogne s’étend sur près de  240 km d’Auxerre à Mâcon. Ses célèbres domaines viticoles font résonner des noms de vins parmi les plus réputés au monde. Partez à la rencontre de ses vins, de sa gastronomie et de son héritage historique hors du commun.",
                LienId = 291,
            };
            var mockRepository1 = new Mock<IDataRepository<Vignoble>>();
            var mockRepository2 = new Mock<IDataRepository<Lien>>();
            var mockRepository3 = new Mock<IDataRepository<ElementVignoble>>();
            var mockRepository4 = new Mock<IDataRepository<LienElementVignoble>>();

            mockRepository1.Setup(x => x.GetByIdAsync(1).Result).Returns(vignoble);
            var vignobleController = new VignoblesController(mockRepository1.Object, mockRepository2.Object, mockRepository3.Object, mockRepository4.Object);
            // Act
            var actionResult = vignobleController.GetVignobleById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(vignoble, actionResult.Value as Vignoble);
        }

        [TestMethod]
        public void GetVignobleById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository1 = new Mock<IDataRepository<Vignoble>>();
            var mockRepository2 = new Mock<IDataRepository<Lien>>();
            var mockRepository3 = new Mock<IDataRepository<ElementVignoble>>();
            var mockRepository4 = new Mock<IDataRepository<LienElementVignoble>>();

            var VignobleController = new VignoblesController(mockRepository1.Object, mockRepository2.Object, mockRepository3.Object, mockRepository4.Object);
            // Act
            var actionResult = VignobleController.GetVignobleById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }
    }
}