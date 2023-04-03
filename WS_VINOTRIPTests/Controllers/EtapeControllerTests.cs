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
    public class EtapeControllerTests
    {
        //public async Task<ActionResult<IEnumerable<Etape>>> GetEtape()
        //public async Task<ActionResult<IEnumerable<Concerne>>> GetConcerne()
        //public async Task<ActionResult<Etape>> GetEtapeById(int id)
        //public async Task<IActionResult> PutEtape(int id, Etape etape)
        //public async Task<ActionResult<Etape>> PostEtape(Etape etape)
        //public async Task<IActionResult> DeleteEtape(int id)

        private readonly VinotripDBContext _context;
        private readonly EtapeController _controller;
        private readonly IDataRepositoryEtape<Etape> _dataRepository;
        private readonly IDataRepository<Concerne> _dataRepositoryConcerne;
        private readonly IDataRepository<ElementEtape> _dataRepositoryElementEtape;
        private readonly IDataRepository<LienEtape> _dataRepositoryLienEtape;
        private readonly IDataRepository<Lien> _dataRepositoryLien;

        public EtapeControllerTests()
        {
            var builder = new DbContextOptionsBuilder<VinotripDBContext>().UseNpgsql("Server=localhost;port=5432;Database=FilmsDBOff; uid=postgres;\npassword=postgres;"); // Chaine de connexion à mettre dans les ( )
            _context = new VinotripDBContext(builder.Options);
            _dataRepository = new EtapeManager(_context);
            _dataRepositoryConcerne = new ConcerneManager(_context);
            _dataRepositoryElementEtape = new ElementEtapeManager(_context);
            _dataRepositoryLienEtape = new LienEtapeManager(_context);
            _dataRepositoryLien = new LienManager(_context);
            _controller = new EtapeController(_dataRepository, _dataRepositoryElementEtape, _dataRepositoryConcerne, _dataRepositoryLienEtape, _dataRepositoryLien);
        }

        [TestMethod()]
        public async Task GetEtapeTestAsync()
        {
            ActionResult<IEnumerable<Etape>> etape = await _controller.GetEtape();
            CollectionAssert.AreEqual(_context.Etapes.ToList(), etape.Value.ToList(), "La liste renvoyée n'est pas la bonne.");
        }
        [TestMethod()]
        public async Task GetConcerneTestAsync()
        {
            ActionResult<IEnumerable<Concerne>> concerne = await _controller.GetConcerne();
            CollectionAssert.AreEqual(_context.Concernes.ToList(), concerne.Value.ToList(), "La liste renvoyée n'est pas la bonne.");
        }

        [TestMethod()]
        public async Task GetEtapeByIdTestAsync()
        {
            ActionResult<Etape> Etape = await _controller.GetEtapeById(25);
            Assert.AreEqual(_context.Etapes.Where(c => c.EtapeId == 25).FirstOrDefault(), Etape.Value, "Etape différent");
        }

        [TestMethod()]
        public async Task GetEtapeByIdTestAsyncFalse()
        {
            ActionResult<Etape> Etape = await _controller.GetEtapeById(25);
            Assert.AreNotEqual(_context.Etapes.Where(c => c.EtapeId == 26).FirstOrDefault(), Etape.Value, "Etape différent");
        }

        [TestMethod]
        public void GetEtapeById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Etape etape = new Etape
            {
                EtapeId = 25,
                SejourId = 14,
                Titre = "Jour 2 - Escapade en 2CV dans les vignes de Champagne",
                Description = "\n            Petit-déjeuner à votre hébergement\n            Vous participez au Domaine Philippe Martin, propriété familiale depuis 1892, à une balade inédite en 2 CV avec les propriétaires du domaine. Une expérience inoubliable qui vous offrira des vues imprenables sur la Vallée de la Marne et vous permettra de partager, le temps d’une escapade, la passion des professionnels de la vigne et du vin. En point d’orgue de la balade, vous bénéficierez d’une dégustation de leurs meilleures cuvées\n            En option - Déjeuner dans un restaurant en centre ville d'Epernay autour d'une cuisine généreuse et de terroir – Menu entrée, plat, dessert, hors boisson\n            Vous prenez ensuite la direction du Domaine de Castellane pour une visite guidée du domaine, du musée et de sa célèbre tour panoramique de 60m de haut qui vous offrira une vue imprenable sur la ville. La visite sera suivie d’une dégustation des champagnes du domaine",
            };
            var mockRepository = new Mock<IDataRepositoryEtape<Etape>>();
            var mockRepository1 = new Mock<IDataRepository<ElementEtape>>();
            var mockRepository2 = new Mock<IDataRepository<Concerne>>();
            var mockRepository3 = new Mock<IDataRepository<LienEtape>>();
            var mockRepository4 = new Mock<IDataRepository<Lien>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(etape);
            var EtapeController = new EtapeController(mockRepository.Object, mockRepository1.Object, mockRepository2.Object, mockRepository3.Object, mockRepository4.Object);
            // Act
            var actionResult = EtapeController.GetEtapeById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(etape, actionResult.Value as Etape);
        }

        [TestMethod]
        public void GetEtapeById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepositoryEtape<Etape>>();
            var mockRepository1 = new Mock<IDataRepository<ElementEtape>>();
            var mockRepository2 = new Mock<IDataRepository<Concerne>>();
            var mockRepository3 = new Mock<IDataRepository<LienEtape>>();
            var mockRepository4 = new Mock<IDataRepository<Lien>>();


            var EtapeController = new EtapeController(mockRepository.Object, mockRepository1.Object, mockRepository2.Object, mockRepository3.Object, mockRepository4.Object);
            // Act
            var actionResult = EtapeController.GetEtapeById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }



        [TestMethod()]
        public async Task GetEtapeBySejourIdTestAsync()
        {
            ActionResult<IEnumerable<Etape>> etapes =   _controller.GetEtapeBySejourId(14).Result;
            Assert.AreEqual(_context.Etapes.Where(c => c.SejourId == 14).ToList()[0], etapes.Value.First(), "Etapes différent");
        }

        [TestMethod()]
        public void GetEtapeBySejourIdTestAsyncFalse()
        {
            ActionResult<IEnumerable<Etape>> etapes =  _controller.GetEtapeBySejourId(1).Result;
            Assert.AreNotEqual(_context.Etapes.Where(c => c.SejourId == 26).ToList(), etapes.Value, "Etapes différent");
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [TestMethod]
        public void GetEtapeBySejourId_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Etape etape = new Etape
            {
                EtapeId = 25,
                SejourId = 14,
                Titre = "Jour 2 - Escapade en 2CV dans les vignes de Champagne",
                Description = "\n            Petit-déjeuner à votre hébergement\n            Vous participez au Domaine Philippe Martin, propriété familiale depuis 1892, à une balade inédite en 2 CV avec les propriétaires du domaine. Une expérience inoubliable qui vous offrira des vues imprenables sur la Vallée de la Marne et vous permettra de partager, le temps d’une escapade, la passion des professionnels de la vigne et du vin. En point d’orgue de la balade, vous bénéficierez d’une dégustation de leurs meilleures cuvées\n            En option - Déjeuner dans un restaurant en centre ville d'Epernay autour d'une cuisine généreuse et de terroir – Menu entrée, plat, dessert, hors boisson\n            Vous prenez ensuite la direction du Domaine de Castellane pour une visite guidée du domaine, du musée et de sa célèbre tour panoramique de 60m de haut qui vous offrira une vue imprenable sur la ville. La visite sera suivie d’une dégustation des champagnes du domaine",
            };
            List<Etape> etapes = new List<Etape> { etape };
            var mockRepository = new Mock<IDataRepositoryEtape<Etape>>();
            var mockRepository1 = new Mock<IDataRepository<ElementEtape>>();
            var mockRepository2 = new Mock<IDataRepository<Concerne>>();
            var mockRepository3 = new Mock<IDataRepository<LienEtape>>();
            var mockRepository4 = new Mock<IDataRepository<Lien>>();
            mockRepository.Setup(x => x.GetBySejourIdAsync(14).Result).Returns(etapes);
            var EtapeController = new EtapeController(mockRepository.Object, mockRepository1.Object, mockRepository2.Object, mockRepository3.Object, mockRepository4.Object);
            // Act
            var actionResult = EtapeController.GetEtapeBySejourId(14).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(etapes[0], actionResult.Value.First());
        }

        [TestMethod]
        public void GetEtapeBySejourId_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepositoryEtape<Etape>>();
            var mockRepository1 = new Mock<IDataRepository<ElementEtape>>();
            var mockRepository2 = new Mock<IDataRepository<Concerne>>();
            var mockRepository3 = new Mock<IDataRepository<LienEtape>>();
            var mockRepository4 = new Mock<IDataRepository<Lien>>();


            var EtapeController = new EtapeController(mockRepository.Object, mockRepository1.Object, mockRepository2.Object, mockRepository3.Object, mockRepository4.Object);
            // Act
            var actionResult = EtapeController.GetEtapeBySejourId(9999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void PutEtapeTest_AvecMoq()
        {
            Etape etape = new Etape
            {
                EtapeId= 1,
                SejourId= 3,
                Titre= "Jour 1 - Arrivée en plein coeur du vignoble",
                Description= "\n            A votre arrivée en Bourgogne, vous vous installez à la Maison Rouge, chambre d’hôtes de charme située à quelques kilomètres de Beaune, à deux pas des vignobles de la Côte de Nuits, point de départ idéal pour une découverte de l'oenotourisme en Bourgogne\n            Dîner au restaurant La Gremelle proposant une cuisine gourmande du terroir - hors boisson, apéritif inclus (en option)\n            Nuitée à la Maison Rouge",
            };

            Etape etapeUpdated = new Etape
            { 
                EtapeId = 1,
                SejourId = 789,
                Titre = "Feur",
                Description = "Feur",
            };
            // Act
            var mockRepository = new Mock<IDataRepositoryEtape<Etape>>();
            var mockRepository1 = new Mock<IDataRepository<ElementEtape>>();
            var mockRepository2 = new Mock<IDataRepository<Concerne>>();
            var mockRepository3 = new Mock<IDataRepository<LienEtape>>();
            var mockRepository4 = new Mock<IDataRepository<Lien>>();

            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(etape);
            var etapeController = new EtapeController(mockRepository.Object, mockRepository1.Object, mockRepository2.Object, mockRepository3.Object, mockRepository4.Object);

            // Act
            var actionResult = etapeController.PutEtape(etapeUpdated.EtapeId, etapeUpdated).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public void PostEtape_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepositoryEtape<Etape>>();
            var mockRepository1 = new Mock<IDataRepository<ElementEtape>>();
            var mockRepository2 = new Mock<IDataRepository<Concerne>>();
            var mockRepository3 = new Mock<IDataRepository<LienEtape>>();
            var mockRepository4 = new Mock<IDataRepository<Lien>>();

            var etapeController = new EtapeController(mockRepository.Object, mockRepository1.Object, mockRepository2.Object, mockRepository3.Object, mockRepository4.Object);

            Etape etape = new Etape
            {
                EtapeId = 1,
                SejourId = 3,
                Titre = "Jour 1 - Arrivée en plein coeur du vignoble",
                Description = "\n            A votre arrivée en Bourgogne, vous vous installez à la Maison Rouge, chambre d’hôtes de charme située à quelques kilomètres de Beaune, à deux pas des vignobles de la Côte de Nuits, point de départ idéal pour une découverte de l'oenotourisme en Bourgogne\n            Dîner au restaurant La Gremelle proposant une cuisine gourmande du terroir - hors boisson, apéritif inclus (en option)\n            Nuitée à la Maison Rouge",
            };

            // Act
            var actionResult = etapeController.PostEtape(etape).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Etape>), "Pas un ActionResult<Etape>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Etape), "Pas un Etape");
            etape.EtapeId = ((Etape)result.Value).EtapeId;
            Assert.AreEqual(etape, (Etape)result.Value, "Etape pas identiques");
        }
        //public async Task<IActionResult> DeleteElementEtape(int id)
        [TestMethod]
        public void DeleteEtapeTest_AvecMoq()
        {
            // Arrange
            Etape etape = new Etape
            {
                EtapeId = 1,
                SejourId = 3,
                Titre = "Jour 1 - Arrivée en plein coeur du vignoble",
                Description = "\n            A votre arrivée en Bourgogne, vous vous installez à la Maison Rouge, chambre d’hôtes de charme située à quelques kilomètres de Beaune, à deux pas des vignobles de la Côte de Nuits, point de départ idéal pour une découverte de l'oenotourisme en Bourgogne\n            Dîner au restaurant La Gremelle proposant une cuisine gourmande du terroir - hors boisson, apéritif inclus (en option)\n            Nuitée à la Maison Rouge",
            };
            var mockRepository = new Mock<IDataRepositoryEtape<Etape>>();
            var mockRepository1 = new Mock<IDataRepository<ElementEtape>>();
            var mockRepository2 = new Mock<IDataRepository<Concerne>>();
            var mockRepository3 = new Mock<IDataRepository<LienEtape>>();
            var mockRepository4 = new Mock<IDataRepository<Lien>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(etape);
            var etapeController = new EtapeController(mockRepository.Object, mockRepository1.Object, mockRepository2.Object, mockRepository3.Object, mockRepository4.Object);

            // Act
            var actionResult = etapeController.DeleteEtape(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}