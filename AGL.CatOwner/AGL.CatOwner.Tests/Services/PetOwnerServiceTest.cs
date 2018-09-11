using AGL.CatOwner.Service.PetOwner;
using AGL.CatOwner.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AGL.CatOwner.Tests.Services
{
    [TestClass]
    public class PetOwnerServiceTest
    {
        private IPetOwnerService _petOwnerService;

        [TestCleanup]
        public void TestClean()
        {
            _petOwnerService = null;
        }

        [TestInitialize]
        public void TestInit()
        {
            var mock = new Mock<ICaching>();
            _petOwnerService = new PetOwnerService(mock.Object);
        }

        [TestMethod]
        public void PetOwnerServiceResultTest()
        {
            //Act
            var result = _petOwnerService.GetAllPetOwner();
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
