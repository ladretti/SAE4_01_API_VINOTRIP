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
    public class PersonnesControllerTests
    {
        private readonly VinotripDBContext _context;
        private readonly PersonnesController _controller;
        private readonly IDataRepository<Personne> _dataRepository;

        public PersonnesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<VinotripDBContext>().UseNpgsql("Server=vinotrip.postgres.database.azure.com;port=5432;Database=vinotrique; uid=vinotrip_admin; password=Prout18#"); // Chaine de connexion à mettre dans les ( )
            _context = new VinotripDBContext(builder.Options);
            _dataRepository = new PersonneManager(_context);
            _dataRepository = new PersonneManager(_context);
            _controller = new PersonnesController(_dataRepository);
        }

        //public async Task<ActionResult<IEnumerable<Personne>>> GetPersonnes()
        [TestMethod()]
        public async Task GetEtapeTestAsync()
        {
            ActionResult<IEnumerable<Personne>> personne = await _controller.GetPersonnes();
            CollectionAssert.AreEqual(_context.Personnes.ToList(), personne.Value.ToList(), "La liste renvoyée n'est pas la bonne.");
        }

        //public async Task<ActionResult<Personne>> GetPersonneById(int id)
        [TestMethod()]
        public async Task GetPersonneByIdTestAsync()
        {
            ActionResult<Personne> personne = await _controller.GetPersonneById(1);
            Assert.AreEqual(_context.Personnes.Where(c => c.PersonneId == 1).FirstOrDefault(), personne.Value, "Personne différent");
        }

        [TestMethod()]
        public async Task GetPersonneByIdTestAsyncFalse()
        {
            ActionResult<Personne> personne = await _controller.GetPersonneById(1);
            Assert.AreNotEqual(_context.Personnes.Where(c => c.PersonneId == 2).FirstOrDefault(), personne.Value, "Personne différent");
        }

        [TestMethod]
        public void GetPersonneById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Personne personne = new Personne
            {
                PersonneId = 1,
                Nom = "Sun-E-Bike",
                Mail = "sunebike@yahoo.fr",
            };

            var mockRepository = new Mock<IDataRepository<Personne>>();
            mockRepository.Setup(x => x.GetByIdAsync(130).Result).Returns(personne);
            var panierController = new PersonnesController(mockRepository.Object);
            // Act
            var actionResult = panierController.GetPersonneById(130).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(personne, actionResult.Value as Personne);
        }
        [TestMethod]
        public void GetPersonneById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Personne>>();
            var panierController = new PersonnesController(mockRepository.Object);
            // Act
            var actionResult = panierController.GetPersonneById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }
        //public async Task<ActionResult<Personne>> GetPersonneByMail(string mail)
        [TestMethod()]
        public async Task GetPersonneByMailTestAsync()
        {
            ActionResult<Personne> personne = await _controller.GetPersonneByMail("sunebike@yahoo.fr");
            Assert.AreEqual(_context.Personnes.Where(c => c.Mail == "sunebike@yahoo.fr").FirstOrDefault(), personne.Value, "Personne différent");
        }

        [TestMethod()]
        public async Task GetPersonneByMailTestAsyncFalse()
        {
            ActionResult<Personne> personne = await _controller.GetPersonneByMail("sunebike@yahoo.fr");
            Assert.AreNotEqual(_context.Personnes.Where(c => c.Mail == "test").FirstOrDefault(), personne.Value, "Personne différent");
        }

        [TestMethod]
        public void GetPersonneByMail_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Personne personne = new Personne
            {
                PersonneId = 1,
                Nom = "Sun-E-Bike",
                Mail = "sunebike@yahoo.fr",
            };

            var mockRepository = new Mock<IDataRepository<Personne>>();
            mockRepository.Setup(x => x.GetByStringAsync("sunebike@yahoo.fr").Result).Returns(personne);
            var panierController = new PersonnesController(mockRepository.Object);
            // Act
            var actionResult = panierController.GetPersonneByMail("sunebike@yahoo.fr").Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(personne, actionResult.Value as Personne);
        }
        [TestMethod]
        public void GetPersonneByMail_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Personne>>();
            var panierController = new PersonnesController(mockRepository.Object);
            // Act
            var actionResult = panierController.GetPersonneByMail("e@e.eeeeee").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }

        //public async Task<IActionResult> PutPersonne(int id, Personne personne)
        [TestMethod]
        public void PutPersonneTest_AvecMoq()
        {

            Personne personne = new Personne
            {
                PersonneId = 1,
                Nom = "Sun-E-Bike",
                Mail = "sunebike@yahoo.fr",
            };


            Personne personneUpdated = new Personne()
            {
                PersonneId = 1,
                Nom = "FEUR",
                Mail = "FEUR",
            };
            // Act
            var mockRepository = new Mock<IDataRepository<Personne>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(personne);
            var personneController = new PersonnesController(mockRepository.Object);

            // Act
            var actionResult = personneController.PutPersonne(1,personneUpdated).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        //public async Task<ActionResult<Personne>> PostPersonne(Personne personne)

        [TestMethod]
        public void PostPersonne_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Personne>>();
            var personneController = new PersonnesController(mockRepository.Object);

            Personne personne = new Personne
            {
                Nom = "FEUR",
                Mail = "FEUR",
            };


            // Act
            var actionResult = personneController.PostPersonne(personne).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Personne>), "Pas un ActionResult<Personne>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Personne), "Pas un Personne");
            Assert.AreEqual(personne, (Personne)result.Value, "Personne pas identiques");
        }
        //public async Task<IActionResult> DeletePersonne(int id)
        [TestMethod]
        public void DeletePersonneTest_AvecMoq()
        {
            // Arrange
            Personne personne = new Personne
            {
                PersonneId = 1,
                Nom = "Sun-E-Bike",
                Mail = "sunebike@yahoo.fr",
            };

            var mockRepository = new Mock<IDataRepository<Personne>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(personne);
            var personneController = new PersonnesController(mockRepository.Object);

            // Act
            var actionResult = personneController.DeletePersonne(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }



    }
}