using Microsoft.VisualStudio.TestTools.UnitTesting;
using WS_VINOTRIP.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WS_VINOTRIP.Models.EntityFramework;
using Moq;
using WS_VINOTRIP.Models.Repository;

namespace WS_VINOTRIP.Controllers.Tests
{
    [TestClass()]
    public class LiensControllerTests
    {
        //public async Task<ActionResult<Lien>> PostLien(Lien lien)
        [TestMethod]
        public void PostLien_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Lien>>();

            var lienController = new LiensController(mockRepository.Object);

            Lien lien = new Lien
            {
                LienId = 1,
                Url = "https://medias1.vinotrip.com/310-product_big/decouverte-art-tonnellerie-bourgogne.jpg",
                Type = "Photo Séjour",
            };

            // Act
            var actionResult = lienController.PostLien(lien).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Lien>), "Pas un ActionResult<Lien>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Lien), "Pas un Lien");
            lien.LienId = ((Lien)result.Value).LienId;
            Assert.AreEqual(lien, (Lien)result.Value, "Lien pas identiques");
        }

        //public async Task<IActionResult> DeleteLien(int id)

        [TestMethod]
        public void DeleteLienTest_AvecMoq()
        {
            // Arrange
            Lien lien = new Lien
            {
                LienId = 1,
                Url = "https://medias1.vinotrip.com/310-product_big/decouverte-art-tonnellerie-bourgogne.jpg",
                Type = "Photo Séjour",
            };
            var mockRepository = new Mock<IDataRepository<Lien>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(lien);
            var lienController = new LiensController(mockRepository.Object);

           
            // Act
            var actionResult = lienController.DeleteLien(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}