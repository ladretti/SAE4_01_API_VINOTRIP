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
    public class UsersControllerTests
    {
        private readonly VinotripDBContext _context;
        private readonly UsersController _controller;
        private readonly IDataRepository<User> _dataRepository;

        public UsersControllerTests()
        {
            var builder = new DbContextOptionsBuilder<VinotripDBContext>().UseNpgsql("Server=localhost;port=5432;Database=FilmsDBOff; uid=postgres;\npassword=postgres;"); // Chaine de connexion à mettre dans les ( )
            _context = new VinotripDBContext(builder.Options);
            _dataRepository = new UserManager(_context);
            _controller = new UsersController(_dataRepository);
        }

        //public async Task<ActionResult<User>> GetUserById(int id)
        [TestMethod()]
        public async Task GetUserByIdTestAsync()
        {
            ActionResult<User> User = await _controller.GetUserById(130);
            Assert.AreEqual(_context.Users.Where(c => c.PersonneId == 130).FirstOrDefault(), User.Value, "User différent");
        }

        [TestMethod()]
        public async Task GetUserByIdTestAsyncFalse()
        {
            ActionResult<User> User = await _controller.GetUserById(130);
            Assert.AreNotEqual(_context.Users.Where(c => c.PersonneId == 26).FirstOrDefault(), User.Value, "User différent");
        }

        [TestMethod]
        public void GetUserById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            User user = new User
            {
                PersonneId= 130,
                Titre= "M.",
                Prenom= "Yanis",
                Pseudo= "Cubeman74",
                DateNaissance= new DateTime(2003,08,07),
                Tel= "0707070707",
                Mdp= "oui",
                Newsletter= true,
                DateConnexion= new DateTime(2023,03,28),
                Role= "user",    
            };

            var mockRepository = new Mock<IDataRepository<User>>();
            mockRepository.Setup(x => x.GetByIdAsync(130).Result).Returns(user);
            var userController = new UsersController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUserById(130).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(user, actionResult.Value as User);
        }

        [TestMethod]
        public void GetUserById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<User>>();

            var UserController = new UsersController(mockRepository.Object);
            // Act
            var actionResult = UserController.GetUserById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }
        //public async Task<ActionResult<User>> GetUserByPseudo(string pseudo)
        [TestMethod()]
        public async Task GetUserByPseudoTestAsync()
        {
            ActionResult<User> User = await _controller.GetUserByPseudo("degabaia");
            Assert.AreEqual(_context.Users.Where(c => c.Pseudo == "degabaia").FirstOrDefault(), User.Value, "User différent");
        }

        [TestMethod()]
        public async Task GetUserByPseudoTestAsyncFalse()
        {
            ActionResult<User> User = await _controller.GetUserByPseudo("Cubeman74");
            Assert.AreNotEqual(_context.Users.Where(c => c.Pseudo == "Cessouille").FirstOrDefault(), User.Value, "User différent");
        }

        [TestMethod]
        public void GetUserByPseudo_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            User user = new User
            {
                PersonneId = 130,
                Titre = "M.",
                Prenom = "Yanis",
                Pseudo = "Cubeman74",
                DateNaissance = new DateTime(2003, 08, 07),
                Tel = "0707070707",
                Mdp = "oui",
                Newsletter = true,
                DateConnexion = new DateTime(2023, 03, 28),
                Role = "user",
            };

            var mockRepository = new Mock<IDataRepository<User>>();
            mockRepository.Setup(x => x.GetByStringAsync("Cubeman74").Result).Returns(user);
            var userController = new UsersController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUserByPseudo("Cubeman74").Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(user, actionResult.Value as User);
        }

        [TestMethod]
        public void GetUserByPseudo_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<User>>();

            var UserController = new UsersController(mockRepository.Object);
            // Act
            var actionResult = UserController.GetUserByPseudo("PseudoCool").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));

        }

        //public async Task<IActionResult> PutUser(int id, User user)
        [TestMethod]
        public void PutUserTest_AvecMoq()
        {
            User user = new User
            {
                PersonneId = 130,
                Titre = "M.",
                Prenom = "Yanis",
                Pseudo = "Cubeman74",
                DateNaissance = new DateTime(2003, 08, 07),
                Tel = "0707070707",
                Mdp = "oui",
                Newsletter = true,
                DateConnexion = new DateTime(2023, 03, 28),
                Role = "user",
            };

            User userUpdated = new User
            {
                PersonneId = 130,
                Titre = "M.",
                Prenom = "Yanou",
                Pseudo = "Zoro Masqué",
                DateNaissance = new DateTime(2003, 08, 07),
                Tel = "0707070707",
                Mdp = "oui",
                Newsletter = true,
                DateConnexion = new DateTime(2023, 03, 28),
                Role = "user",
            };
            // Act
            var mockRepository = new Mock<IDataRepository<User>>();

            mockRepository.Setup(x => x.GetByIdAsync(130).Result).Returns(user);
            var userController = new UsersController(mockRepository.Object);

            // Act
            var actionResult = userController.PutUser(userUpdated.PersonneId, userUpdated).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        //public async Task<ActionResult<User>> PostUser(User user)
        [TestMethod]
        public void PostUser_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<User>>();


            var userController = new UsersController(mockRepository.Object);

            User user = new User
            {
                Titre = "M.",
                Prenom = "Jérémy",
                Pseudo = "Jerem",
                DateNaissance = new DateTime(2003, 06, 10),
                Tel = "0707070707",
                Mdp = "Super MDP",
                Newsletter = true,
                DateConnexion = new DateTime(2023, 03, 28),
                Role = "user",
            };

            // Act
            var actionResult = userController.PostUser(user).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<User>), "Pas un ActionResult<User>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(User), "Pas un User");
            user.PersonneId = ((User)result.Value).PersonneId;
            Assert.AreEqual(user, (User)result.Value, "User pas identiques");
        }
        //public async Task<IActionResult> DeleteUser(int id)
        [TestMethod]
        public void DeleteUserTest_AvecMoq()
        {
            // Arrange
            User user = new User
            {
                Titre = "M.",
                Prenom = "Jérémy",
                Pseudo = "Jerem",
                DateNaissance = new DateTime(2003, 06, 10),
                Tel = "0707070707",
                Mdp = "Super MDP",
                Newsletter = true,
                DateConnexion = new DateTime(2023, 03, 28),
                Role = "user",
            };
            var mockRepository = new Mock<IDataRepository<User>>();

            mockRepository.Setup(x => x.GetByIdAsync(130).Result).Returns(user);
            var userController = new UsersController(mockRepository.Object);

            // Act
            var actionResult = userController.DeleteUser(130).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

    }
}