using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AGL.CatOwner.Web.Controllers;
using AGL.CatOwner.Models;
using AGL.CatOwner.Repository.PetOwner;
using AGL.CatOwner.Tests.MockData;

namespace AGL.CatOwner.Tests.Controllers
{
    [TestClass]
    public class CatOwnerControllerTest
    {
        [TestMethod]
        public void IndexBasic()
        {
            // Arrange
            string _petType = "Cat";
            MockProvider _mockp = new MockProvider();
            var mock = new Mock<IPetOwnerRepo>();
            mock.Setup(p => p.GetPetsByOwnerGender(_petType)).Returns(_mockp.GetMockPetGroup());
            CatOwnerController controller = new CatOwnerController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexModelNotNull()
        {
            // Arrange
            string _petType = "Cat";
            MockProvider _mockp = new MockProvider();
            var mock = new Mock<IPetOwnerRepo>();
            mock.Setup(p => p.GetPetsByOwnerGender(_petType)).Returns(_mockp.GetMockPetGroup());
            CatOwnerController controller = new CatOwnerController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void IndexReturnsViewModel()
        {
            // Arrange
            string _petType = "Cat";
            MockProvider _mockp = new MockProvider();
            var mock = new Mock<IPetOwnerRepo>();
            mock.Setup(p => p.GetPetsByOwnerGender(_petType)).Returns(_mockp.GetMockPetGroup());
            CatOwnerController controller = new CatOwnerController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void IndexViewModelGroupCount()
        {
            // Arrange
            string _petType = "Cat";
            MockProvider _mockp = new MockProvider();
            var mock = new Mock<IPetOwnerRepo>();
            mock.Setup(p => p.GetPetsByOwnerGender(_petType)).Returns(_mockp.GetMockPetGroup());
            CatOwnerController controller = new CatOwnerController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual(((List<PetGroup>)result.Model).Count, 2);
        }

        [TestMethod]
        public void IndexViewModelNullModel()
        {
            // Arrange
            string _petType = "Cat";
            MockProvider _mockp = new MockProvider();
            var mock = new Mock<IPetOwnerRepo>();
            mock.Setup(p => p.GetPetsByOwnerGender(_petType)).Returns(_mockp.GetMockPetGroupWithNull());
            CatOwnerController controller = new CatOwnerController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNull(result.Model);
        }
    }
}
