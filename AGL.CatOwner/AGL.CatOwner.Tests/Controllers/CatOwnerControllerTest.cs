using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AGL.CatOwner.Web.Controllers;
using AGL.CatOwner.Models;
using AGL.CatOwner.Tests.MockData;
using AGL.CatOwner.Service.PetOwner;

namespace AGL.CatOwner.Tests.Controllers
{
    [TestClass]
    public class CatOwnerControllerTest
    {
        private readonly MockProvider _mockProvider;
        private readonly Mock<IPetOwnerService> _mockService;
        private readonly CatOwnerController _controller;

        public CatOwnerControllerTest()
        {
            _mockProvider = new MockProvider();
            _mockService = new Mock<IPetOwnerService>();
            _controller = new CatOwnerController(_mockService.Object);
        }

        [TestMethod]
        public void IndexBasic()
        {
            // Arrange
            string _petType = "Cat";
            _mockService.Setup(p => p.GetPetsByOwnerGender(_petType)).Returns(_mockProvider.GetMockPetGroup());

            // Act
            ViewResult result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexModelNotNull()
        {
            // Arrange
            string _petType = "Cat";
            _mockService.Setup(p => p.GetPetsByOwnerGender(_petType)).Returns(_mockProvider.GetMockPetGroup());

            // Act
            ViewResult result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void IndexReturnsViewModel()
        {
            // Arrange
            string _petType = "Cat";
            _mockService.Setup(p => p.GetPetsByOwnerGender(_petType)).Returns(_mockProvider.GetMockPetGroup());

            // Act
            ViewResult result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void IndexViewModelGroupCount()
        {
            // Arrange
            string _petType = "Cat";
            _mockService.Setup(p => p.GetPetsByOwnerGender(_petType)).Returns(_mockProvider.GetMockPetGroup());

            // Act
            ViewResult result = _controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual(((List<PetGroup>)result.Model).Count, 2);
        }

        [TestMethod]
        public void IndexViewModelNullModel()
        {
            // Arrange
            string _petType = "Cat";
            _mockService.Setup(p => p.GetPetsByOwnerGender(_petType)).Returns(_mockProvider.GetMockPetGroupWithNull());

            // Act
            ViewResult result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNull(result.Model);
        }
    }
}
