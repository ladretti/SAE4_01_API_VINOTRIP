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
    public class ElementEtapeControllerTests
    {
        //public async Task<ActionResult<IEnumerable<ElementEtape>>> GetElementEtape()
        //public async Task<ActionResult<ElementEtape>> GetElementEtapeById(int id)
        //public async Task<IActionResult> PutElementEtape(int id, ElementEtape elementEtape)
        //public async Task<ActionResult<ElementEtape>> PostElementEtape(ElementEtape elementEtape)
        //public async Task<IActionResult> DeleteElementEtape(int id)

        private readonly VinotripDBContext _context;
        private readonly ElementEtapeController _controller;
        private readonly IDataRepositoryElementEtape<ElementEtape> _dataRepository;
        private readonly IDataRepository<Contient> _dataRepositoryContient;
        private readonly IDataRepository<Lien> _dataRepositoryLien;

        public ElementEtapeControllerTests()
        {
            var builder = new DbContextOptionsBuilder<VinotripDBContext>().UseNpgsql("Server=vinotrip.postgres.database.azure.com;port=5432;Database=vinotrique; uid=vinotrip_admin; password=Prout18#"); // Chaine de connexion à mettre dans les ( )
            _context = new VinotripDBContext(builder.Options);
            _dataRepository = new ElementEtapeManager(_context);
            _dataRepositoryContient = new ContientManager(_context);
            _dataRepositoryLien = new LienManager(_context);
            _controller = new ElementEtapeController(_dataRepository, _dataRepositoryContient, _dataRepositoryLien);
        }

        [TestMethod()]
        public async Task GetElementEtapeByIdTestAsync()
        {
            ActionResult<ElementEtape> ElementEtape = await _controller.GetElementEtapeById(25);
            Assert.AreEqual(_context.ElementEtapes.Where(c => c.ElementId == 25).FirstOrDefault(), ElementEtape.Value, "ElementEtape différent");
        }

        [TestMethod()]
        public async Task GetElementEtapeByIdTestAsyncFalse()
        {
            ActionResult<ElementEtape> ElementEtape = await _controller.GetElementEtapeById(25);
            Assert.AreNotEqual(_context.ElementEtapes.Where(c => c.ElementId == 26).FirstOrDefault(), ElementEtape.Value, "ElementEtape différent");
        }

        [TestMethod]
        public void GetElementEtapeById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            ElementEtape elementEtape = new ElementEtape
            {

            };
            var mockRepository = new Mock<IDataRepositoryElementEtape<ElementEtape>>();
            var mockRepository1 = new Mock<IDataRepository<Contient>>();
            var mockRepository2 = new Mock<IDataRepository<Lien>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(elementEtape);
            var ElementEtapeController = new ElementEtapeController(mockRepository.Object, mockRepository1.Object, mockRepository2.Object);
            // Act
            var actionResult = ElementEtapeController.GetElementEtapeById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(elementEtape, actionResult.Value as ElementEtape);
        }

        [TestMethod]
        public void GetElementEtapeById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepositoryElementEtape<ElementEtape>>();
            var mockRepository1 = new Mock<IDataRepository<Contient>>();
            var mockRepository2 = new Mock<IDataRepository<Lien>>();
            var ElementEtapeController = new ElementEtapeController(mockRepository.Object, mockRepository1.Object, mockRepository2.Object);
            // Act
            var actionResult = ElementEtapeController.GetElementEtapeById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }
        [TestMethod]
        public void PutElementEtapeTest_AvecMoq()
        {
            ElementEtape elementEtape = new ElementEtape
            {
                ElementId = 1,
                PersonneId = 46,
                TypeElementId = 2,
                Libelle = "Visite des caves, du musée et de la tour du Domaine de Castellane",
                Description = "Le Champagne De Castellane est une maison de négoce créée en 1895 par le Vicomte Florens de Castellane. Depuis plus d'un siècle, De Castellane assemble des vins jeunes, frais et légers avec des vins de réserve plus amples, ...",
            };

            ElementEtape elementEtapeUpdated = new ElementEtape()
            {
                ElementId = 1,
                PersonneId = 46,
                TypeElementId = 4,
                Libelle = "Test post elementEtape",
                Description = "HMMMMMMMMMM"
            };
            // Act
            var mockRepository = new Mock<IDataRepositoryElementEtape<ElementEtape>>();
            var mockRepository1 = new Mock<IDataRepository<Contient>>();
            var mockRepository2 = new Mock<IDataRepository<Lien>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(elementEtape);
            var elementEtapeController = new ElementEtapeController(mockRepository.Object, mockRepository1.Object, mockRepository2.Object);

            // Act
            var actionResult = elementEtapeController.PutElementEtape(1, elementEtapeUpdated).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }


        [TestMethod]
        public void PostElementEtape_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepositoryElementEtape<ElementEtape>>();
            var mockRepository1 = new Mock<IDataRepository<Contient>>();
            var mockRepository2 = new Mock<IDataRepository<Lien>>();
            var ElementEtapeController = new ElementEtapeController(mockRepository.Object, mockRepository1.Object, mockRepository2.Object);

            ElementEtape elementEtape = new ElementEtape
            {
                ElementId = 1,
                PersonneId = 46,
                TypeElementId = 2,
                Libelle = "Visite des caves, du musée et de la tour du Domaine de Castellane",
                Description = "Le Champagne De Castellane est une maison de négoce créée en 1895 par le Vicomte Florens de Castellane. Depuis plus d'un siècle, De Castellane assemble des vins jeunes, frais et légers avec des vins de réserve plus amples, ...",
            };

            // Act
            var actionResult = ElementEtapeController.PostElementEtape(elementEtape).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<ElementEtape>), "Pas un ActionResult<ElementEtape>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(ElementEtape), "Pas un ElementEtape");
            elementEtape.ElementId = ((ElementEtape)result.Value).ElementId;
            Assert.AreEqual(elementEtape, (ElementEtape)result.Value, "ElementEtape pas identiques");
        }
        //public async Task<IActionResult> DeleteElementEtape(int id)
        [TestMethod]
        public void DeleteElementEtapeTest_AvecMoq()
        {
            // Arrange
            ElementEtape elementEtape = new ElementEtape
            {
                ElementId = 1,
                PersonneId = 46,
                TypeElementId = 2,
                Libelle = "Visite des caves, du musée et de la tour du Domaine de Castellane",
                Description = "Le Champagne De Castellane est une maison de négoce créée en 1895 par le Vicomte Florens de Castellane. Depuis plus d'un siècle, De Castellane assemble des vins jeunes, frais et légers avec des vins de réserve plus amples, ...",
            };

            var mockRepository = new Mock<IDataRepositoryElementEtape<ElementEtape>>();
            var mockRepository1 = new Mock<IDataRepository<Contient>>();
            var mockRepository2 = new Mock<IDataRepository<Lien>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(elementEtape);
            var ElementEtapeController = new ElementEtapeController(mockRepository.Object, mockRepository1.Object, mockRepository2.Object);
            // Act
            var actionResult = ElementEtapeController.DeleteElementEtape(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

    }
}