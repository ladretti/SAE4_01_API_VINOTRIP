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
using Moq;
using WS_VINOTRIP.Models.DataManager;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Controllers.Tests
{
    [TestClass()]
    public class CatSejoursControllerTests
    {
        //public async Task<ActionResult<IEnumerable<CatSejour>>> GetCatsSejour()
        //public async Task<ActionResult<CatSejour>> GetCatSejour(int id)

        private readonly VinotripDBContext _context;
        private readonly CatSejoursController _controller;
        private IDataRepository<CatSejour> _dataRepository;

        public CatSejoursControllerTests()
        {
            var builder = new DbContextOptionsBuilder<VinotripDBContext>().UseNpgsql("Server=vinotrip.postgres.database.azure.com;port=5432;Database=vinotrique; uid=vinotrip_admin; password=Prout18#"); // Chaine de connexion à mettre dans les ( )
            _context = new VinotripDBContext(builder.Options);
            _dataRepository = new CatSejourManager(_context);
            _controller = new CatSejoursController(_dataRepository);
        }



        //public async Task<ActionResult<IEnumerable<CatSejour>>> GetCatsSejour()
        //public async Task<ActionResult<CatSejour>> GetCatSejour(int id)
        [TestMethod()]
        public async Task GetCatSejoursTestAsync()
        {
            ActionResult<IEnumerable<CatSejour>> catSejours = await _controller.GetCatsSejour();
            CollectionAssert.AreEqual(_context.CatsSejour.ToList(), catSejours.Value.ToList(), "La liste renvoyée n'est pas la bonne.");
        }

        [TestMethod()]
        public async Task GetCatSejoursByIdTestAsync()
        {
            ActionResult<CatSejour> CatSejours = await _controller.GetCatSejourById(1);
            Assert.AreEqual(_context.CatsSejour.Where(c => c.CatSejourId == 1).FirstOrDefault(), CatSejours.Value, "CatSejours différent");
        }

        [TestMethod()]
        public async Task GetCatSejourByIdTestAsyncFalse()
        {
            ActionResult<CatSejour> CatSejour = await _controller.GetCatSejourById(1);
            Assert.AreNotEqual(_context.CatsSejour.Where(c => c.CatSejourId == 2).FirstOrDefault(), CatSejour.Value, "CatSejours différent");
        }

        [TestMethod]
        public void GetCatSejoursById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            CatSejour catSejour = new CatSejour
            {
                CatSejourId= 1,
                Libelle= "Vin & Gastronomie",
                LienId=331,
            };
            var mockRepository = new Mock<IDataRepository<CatSejour>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(catSejour);
            var CatSejoursController = new CatSejoursController(mockRepository.Object);
            // Act
            var actionResult = CatSejoursController.GetCatSejourById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(catSejour, actionResult.Value as CatSejour);
        }

        [TestMethod]
        public void GetCatSejoursById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<CatSejour>>();
            var CatSejoursController = new CatSejoursController(mockRepository.Object);
            // Act
            var actionResult = CatSejoursController.GetCatSejourById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }
    }
}