using AGL.CatOwner.Models;
using AGL.CatOwner.Repository.PetOwner;
using AGL.CatOwner.Service.PetOwner;
using AGL.CatOwner.Tests.MockData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace AGL.CatOwner.Tests.Repositories
{
    [TestClass]
    public class PetOwnerRepoTest
    {

        [TestMethod]
        public void GetPetsByOwnerGenderNotNull()
        {
            //Arrange
            string _petType = "Cat";
            MockProvider _mockp = new MockProvider();
            var mock = new Mock<IPetOwnerService>();
            mock.Setup(p => p.GetAllPetOwner()).Returns(_mockp.GetMockPetOwnerResult());
            PetOwnerRepo petRepository = new PetOwnerRepo(mock.Object);
            //Act
            var result = petRepository.GetPetsByOwnerGender(_petType);
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetPetsByOwnerGenderGroupCount()
        {
            //Arrange
            string _petType = "Cat";
            MockProvider _mockp = new MockProvider();
            var mock = new Mock<IPetOwnerService>();
            mock.Setup(p => p.GetAllPetOwner()).Returns(_mockp.GetMockPetOwnerResult());
            PetOwnerRepo petRepository = new PetOwnerRepo(mock.Object);
            //Act
            var result = petRepository.GetPetsByOwnerGender(_petType) as List<PetGroup>;
            //Assert
            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void GetPetsByOwnerGenderEachGenderGroupAvailable()
        {
            //Arrange
            string _petType = "Cat";
            MockProvider _mockp = new MockProvider();
            var mock = new Mock<IPetOwnerService>();
            mock.Setup(p => p.GetAllPetOwner()).Returns(_mockp.GetMockPetOwnerResult());
            PetOwnerRepo petRepository = new PetOwnerRepo(mock.Object);
            //Act
            var result = petRepository.GetPetsByOwnerGender(_petType) as List<PetGroup>;
            //Assert
            Assert.IsTrue(result.Any(r => "Male" == r.GroupName));
            Assert.IsTrue(result.Any(r => "Female" == r.GroupName));
        }

        [TestMethod]
        public void GetPetsByOwnerGenderGenderGroupCount()
        {
            //Arrange
            string _petType = "Cat";
            MockProvider _mockp = new MockProvider();
            var mock = new Mock<IPetOwnerService>();
            mock.Setup(p => p.GetAllPetOwner()).Returns(_mockp.GetMockPetOwnerResult());
            PetOwnerRepo petRepository = new PetOwnerRepo(mock.Object);
            //Act
            var result = petRepository.GetPetsByOwnerGender(_petType) as List<PetGroup>;
            //Assert
            Assert.AreEqual(result.Count(r => "Male" == r.GroupName), 1);
            Assert.AreEqual(result.Count(r => "Female" == r.GroupName), 1);
        }

        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataTestMethod]
        public void TestOrderSequenceOfPetsForMaleOwner(int index)
        {
            //Arrange
            string _petType = "Cat";
            MockProvider _mockp = new MockProvider();
            var mock = new Mock<IPetOwnerService>();
            mock.Setup(p => p.GetAllPetOwner()).Returns(_mockp.GetMockPetOwnerResult());
            PetOwnerRepo petRepository = new PetOwnerRepo(mock.Object);

            //Act
            var result = petRepository.GetPetsByOwnerGender(_petType) as List<PetGroup>;
            var petNames = result.Where(r => "Male" == r.GroupName).SelectMany(p => p.PetNames);
            var expectedResult = _mockp.GetMockPetGroup().Where(r => "Male" == r.GroupName).SelectMany(m => m.PetNames);
            //Assert
            Assert.AreEqual(petNames.ElementAt(index), expectedResult.ElementAt(index));
        }

        [TestMethod]
        public void TestNoGenderAvailableForGrouping()
        {
            //Arrange
            string _petType = "Cat";
            MockProvider _mockp = new MockProvider();
            var mock = new Mock<IPetOwnerService>();
            mock.Setup(p => p.GetAllPetOwner()).Returns(_mockp.GetMockPetOwnerSingleGenderNull());
            PetOwnerRepo petRepository = new PetOwnerRepo(mock.Object);
            //Act
            var result = petRepository.GetPetsByOwnerGender(_petType) as List<PetGroup>;
            var genderGroupKey = result.Select(r => r.GroupName).First();
            //Assert
            Assert.AreEqual(result.Count, 1);
            Assert.IsNull(genderGroupKey);
        }

        [TestMethod]
        public void TestEmptyPetListExistsForSingleGroup()
        {
            //Arrange
            string _petType = "Cat";
            MockProvider _mockp = new MockProvider();
            var mock = new Mock<IPetOwnerService>();
            mock.Setup(p => p.GetAllPetOwner()).Returns(_mockp.GetMockPetOwnerSingleNullPetArrayResult());
            PetOwnerRepo petRepository = new PetOwnerRepo(mock.Object);
            //Act
            var result = petRepository.GetPetsByOwnerGender(_petType) as List<PetGroup>;
            var maleGroupPetNames = result.Where(r => "Male" == r.GroupName).SelectMany(m => m.PetNames);
            //Assert
            Assert.AreEqual(maleGroupPetNames.Count(), 0);
        }


        [TestMethod]
        public void TestEmptyPetListExistsForAllGroup()
        {
            //Arrange
            string _petType = "Cat";
            MockProvider _mockp = new MockProvider();
            var mock = new Mock<IPetOwnerService>();
            mock.Setup(p => p.GetAllPetOwner()).Returns(_mockp.GetMockPetOwnerBothNullPetArrayResult());
            PetOwnerRepo petRepository = new PetOwnerRepo(mock.Object);
            //Act
            var result = petRepository.GetPetsByOwnerGender(_petType) as List<PetGroup>;
            var maleGroupPetNames = result.Where(r => "Male" == r.GroupName).SelectMany(m => m.PetNames);
            var femaleGroupPetNames = result.Where(r => "Female" == r.GroupName).SelectMany(m => m.PetNames);
            //Assert
            Assert.AreEqual(maleGroupPetNames.Count(), 0);
            Assert.AreEqual(femaleGroupPetNames.Count(), 0);
        }
    }
}
