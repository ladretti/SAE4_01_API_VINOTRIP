using Microsoft.VisualStudio.TestTools.UnitTesting;
using WS_VINOTRIP.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WS_VINOTRIP.Models.DataManager;
using WS_VINOTRIP.Models.EntityFramework;
using WS_VINOTRIP.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WS_VINOTRIP.Controllers.Tests
{
    [TestClass()]
    public class RouteDesVinsControllerTests
    {
        private readonly VinotripDBContext _context;
        private readonly RouteDesVinsController _controller;
        private readonly IDataRepository<RouteDesVins> _dataRepository;
        private readonly IDataRepository<Lien> _dataRepositoryLien;
        private readonly IDataRepository<LienRouteDesVins> _dataRepositoryLienRouteDesVins;

        public RouteDesVinsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<VinotripDBContext>().UseNpgsql("Server=vinotrip.postgres.database.azure.com;port=5432;Database=vinotrique; uid=vinotrip_admin; password=Prout18#"); // Chaine de connexion à mettre dans les ( )
            _context = new VinotripDBContext(builder.Options);
            _dataRepository = new RouteDesVinsManager(_context);
            _dataRepositoryLien = new LienManager(_context);
            _dataRepositoryLienRouteDesVins = new LienRouteDesVinsManager(_context);
            _controller = new RouteDesVinsController(_dataRepository, _dataRepositoryLien, _dataRepositoryLienRouteDesVins);
        }
        //public async Task<ActionResult<IEnumerable<RouteDesVins>>> GetRoutesDesVins()
        [TestMethod()]
        public async Task GetRoutesDesVinsTestAsync()
        {
            ActionResult<IEnumerable<RouteDesVins>> routesDesVins = await _controller.GetRoutesDesVins();
            CollectionAssert.AreEqual(_context.RoutesDesVins.ToList(), routesDesVins.Value.ToList(), "La liste renvoyée n'est pas la bonne.");
        }
        // public async Task<ActionResult<RouteDesVins>> GetRouteDesVins(int id)
        [TestMethod()]
        public async Task GetRouteDesVinsByIdTestAsync()
        {
            ActionResult<RouteDesVins> routeDesVins = await _controller.GetRouteDesVinsById(1);
            Assert.AreEqual(_context.RoutesDesVins.Where(c => c.RouteDesVinsId == 1).FirstOrDefault(), routeDesVins.Value, "RouteDesVinss différent");
        }

        [TestMethod()]
        public async Task GetRouteDesVinsByIdTestAsyncFalse()
        {
            ActionResult<RouteDesVins> routeDesVins = await _controller.GetRouteDesVinsById(1);
            Assert.AreNotEqual(_context.RoutesDesVins.Where(c => c.RouteDesVinsId == 2).FirstOrDefault(), routeDesVins.Value, "RouteDesVinss différent");
        }

        [TestMethod]
        public void GetRouteDesVinsById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            RouteDesVins routeDesVins = new RouteDesVins
            {
                RouteDesVinsId = 1,
                VignobleId = 2,
                Titre = "ROUTE DES VINS D'ALSACE",
                Description = "Sans doute la plus connue des routes des vins en France, et assurément la plus ancienne ! Que cela soit à vélo ou en voiture, la routes des vins d'Alsace est parcourue par des millions de touristes chaque année, en quête de ses grands crus, ses fabuleux cépages, ses spécificités viticoles (citons les vendanges tardives), ses paysages vallonnés et villages pittoresques comme Colmar ou Riquewihr. Les vins d'Alsace, principalement blancs, sont réputés pour leur élégance, fraicheur et finesse.",
            };
            var mockRepository = new Mock<IDataRepository<RouteDesVins>>();
            var mockRepository1 = new Mock<IDataRepository<Lien>> ();
            var mockRepository2 = new Mock<IDataRepository<LienRouteDesVins>>();

            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(routeDesVins);
            var routeDesVinsController = new RouteDesVinsController(mockRepository.Object, mockRepository1.Object, mockRepository2.Object);
            // Act
            var actionResult = routeDesVinsController.GetRouteDesVinsById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(routeDesVins, actionResult.Value as RouteDesVins);
        }

        [TestMethod]
        public void GetRouteDesVinsById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<RouteDesVins>>();
            var mockRepository1 = new Mock<IDataRepository<Lien>>();
            var mockRepository2 = new Mock<IDataRepository<LienRouteDesVins>>();
            var routeDesVinsController = new RouteDesVinsController(mockRepository.Object, mockRepository1.Object, mockRepository2.Object);
            // Act
            var actionResult = routeDesVinsController.GetRouteDesVinsById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }
    }
}