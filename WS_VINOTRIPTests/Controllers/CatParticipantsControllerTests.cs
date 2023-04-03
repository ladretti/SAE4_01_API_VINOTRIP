using Microsoft.VisualStudio.TestTools.UnitTesting;
using WS_VINOTRIP.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.DataManager;
using WS_VINOTRIP.Models.Repository;
using Moq;

namespace WS_VINOTRIP.Controllers.Tests
{
    [TestClass()]
    public class CatParticipantsControllerTests
    {
        private readonly VinotripDBContext _context;
        private readonly CatParticipantsController _controller;
        private IDataRepository<CatParticipant> _dataRepository;

        public CatParticipantsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<VinotripDBContext>().UseNpgsql("Server=localhost;port=5432;Database=FilmsDBOff; uid=postgres;\npassword=postgres;"); // Chaine de connexion à mettre dans les ( )
            _context = new VinotripDBContext(builder.Options);
            _dataRepository = new CatParticipantManager(_context);
            _controller = new CatParticipantsController(_dataRepository);
        }



        //public async Task<ActionResult<IEnumerable<CatParticipant>>> GetCatsParticipant()
        //public async Task<ActionResult<CatParticipant>> GetCatParticipant(int id)
        [TestMethod()]
        public async Task GetCatParticipantsTestAsync()
        {
            ActionResult<IEnumerable<CatParticipant>> catParticipants = await _controller.GetCatsParticipant();
            CollectionAssert.AreEqual(_context.CatsParticipant.ToList(), catParticipants.Value.ToList(), "La liste renvoyée n'est pas la bonne.");
        }

        [TestMethod()]
        public async Task GetCatParticipantsByIdTestAsync()
        {
            ActionResult<CatParticipant> catParticipants = await _controller.GetCatParticipantById(1);
            Assert.AreEqual(_context.CatsParticipant.Where(c => c.CatParticipantId == 1).FirstOrDefault(), catParticipants.Value, "CatParticipants différent");
        }

        [TestMethod()]
        public async Task GetCatParticipantByIdTestAsyncFalse()
        {
            ActionResult<CatParticipant> catParticipant = await _controller.GetCatParticipantById(1);
            Assert.AreNotEqual(_context.CatsParticipant.Where(c => c.CatParticipantId == 2).FirstOrDefault(), catParticipant.Value, "CatParticipants différent");
        }

        [TestMethod]
        public void GetCatParticipantsById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            CatParticipant catParticipant = new CatParticipant
            {
                CatParticipantId= 1,
                Libelle= "En couple 💖",
                LienId= 328
            };
            var mockRepository = new Mock<IDataRepository<CatParticipant>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(catParticipant);
            var CatParticipantsController = new CatParticipantsController(mockRepository.Object);
            // Act
            var actionResult = CatParticipantsController.GetCatParticipantById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(catParticipant, actionResult.Value as CatParticipant);
        }

        [TestMethod]
        public void GetCatParticipantsById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<CatParticipant>>();
            var CatParticipantsController = new CatParticipantsController(mockRepository.Object);
            // Act
            var actionResult = CatParticipantsController.GetCatParticipantById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }
    }
}