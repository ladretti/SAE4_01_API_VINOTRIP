using Microsoft.VisualStudio.TestTools.UnitTesting;
using WS_VINOTRIP.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WS_VINOTRIP.Models.DataManager;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Controllers.Tests
{
    [TestClass()]
    public class CatVignoblesControllerTests
    {
        //public async Task<ActionResult<IEnumerable<CatVignoble>>> GetCatsVignoble()
        //public async Task<ActionResult<CatVignoble>> GetCatVignoble(int id)

        private readonly VinotripDBContext _context;
        private readonly CatVignoblesController _controller;
        private IDataRepository<CatVignoble> _dataRepository;

        public CatVignoblesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<VinotripDBContext>().UseNpgsql("Server=vinotrip.postgres.database.azure.com;port=5432;Database=vinotrique; uid=vinotrip_admin; password=Prout18#"); // Chaine de connexion à mettre dans les ( )
            _context = new VinotripDBContext(builder.Options);
            _dataRepository = new CatVignobleManager(_context);
            _controller = new CatVignoblesController(_dataRepository);
        }



        //public async Task<ActionResult<IEnumerable<CatVignoble>>> GetCatsSejour()
        //public async Task<ActionResult<CatVignoble>> GetCatVignoble(int id)
        [TestMethod()]
        public async Task GetCatVignoblesTestAsync()
        {
            ActionResult<IEnumerable<CatVignoble>> catVignobles = await _controller.GetCatsVignoble();
            CollectionAssert.AreEqual(_context.CatsVignoble.ToList(), catVignobles.Value.ToList(), "La liste renvoyée n'est pas la bonne.");
        }

        [TestMethod()]
        public async Task GetCatVignoblesByIdTestAsync()
        {
            ActionResult<CatVignoble> CatVignobles = await _controller.GetCatVignobleById(1);
            Assert.AreEqual(_context.CatsVignoble.Where(c => c.CatVignobleId == 1).FirstOrDefault(), CatVignobles.Value, "CatVignobles différent");
        }

        [TestMethod()]
        public async Task GetCatVignobleByIdTestAsyncFalse()
        {
            ActionResult<CatVignoble> CatVignoble = await _controller.GetCatVignobleById(1);
            Assert.AreNotEqual(_context.CatsVignoble.Where(c => c.CatVignobleId == 2).FirstOrDefault(), CatVignoble.Value, "CatVignobles différent");
        }

        [TestMethod]
        public void GetCatVignoblesById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            CatVignoble catVignoble = new CatVignoble
            {
                CatVignobleId= 1,
                Libelle= "Bourgogne",
                Description= "Le vignoble de bourgogne s’étend sur près de  240 km d’Auxerre à Mâcon. Ses célèbres domaines viticoles font résonner des noms de vins parmi les plus réputés au monde. Partez à la rencontre de ses vins, de sa gastronomie et de son héritage historique hors du commun.",
            };
            var mockRepository = new Mock<IDataRepository<CatVignoble>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(catVignoble);
            var CatVignoblesController = new CatVignoblesController(mockRepository.Object);
            // Act
            var actionResult = CatVignoblesController.GetCatVignobleById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(catVignoble, actionResult.Value as CatVignoble);
        }

        [TestMethod]
        public void GetCatVignoblesById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<CatVignoble>>();
            var CatVignoblesController = new CatVignoblesController(mockRepository.Object);
            // Act
            var actionResult = CatVignoblesController.GetCatVignobleById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }
    }
}