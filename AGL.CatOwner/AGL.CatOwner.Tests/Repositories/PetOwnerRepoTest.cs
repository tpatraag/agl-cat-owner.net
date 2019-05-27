using AGL.CatOwner.Repository.PetOwner;
using AGL.CatOwner.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AGL.CatOwner.Tests.Repositories
{
    [TestClass]
    public class PetOwnerRepoTest
    {
        private IPetOwnerRepo _petOwnerRepo;

        [TestCleanup]
        public void TestClean()
        {
            _petOwnerRepo = null;
        }

        [TestInitialize]
        public void TestInit()
        {
            var mock = new Mock<ICaching>();
            _petOwnerRepo = new PetOwnerRepo(mock.Object);
        }

        [TestMethod]
        public void PetOwnerServiceResultTest()
        {
            //Act
            var result = _petOwnerRepo.GetAllPetOwner();

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
