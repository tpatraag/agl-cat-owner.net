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
    public class PetOwnerServicesTest
    {
        private readonly MockProvider _mockProvider;
        private readonly Mock<IPetOwnerRepo> _mockRepo;
        private readonly PetOwnerService _petOwnerService;

        public PetOwnerServicesTest()
        {
            _mockProvider = new MockProvider();
            _mockRepo = new Mock<IPetOwnerRepo>();
            _petOwnerService = new PetOwnerService(_mockRepo.Object);
        }

        [TestMethod]
        public void GetPetsByOwnerGenderNotNull()
        {
            //Arrange
            string _petType = "Cat";
            _mockRepo.Setup(p => p.GetAllPetOwner()).Returns(_mockProvider.GetMockPetOwnerResult());

            //Act
            var result = _petOwnerService.GetPetsByOwnerGender(_petType);
            
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetPetsByOwnerGenderGroupCount()
        {
            //Arrange
            string _petType = "Cat";
            _mockRepo.Setup(p => p.GetAllPetOwner()).Returns(_mockProvider.GetMockPetOwnerResult());

            //Act
            var result = _petOwnerService.GetPetsByOwnerGender(_petType) as List<PetGroup>;

            //Assert
            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void GetPetsByOwnerGenderEachGenderGroupAvailable()
        {
            //Arrange
            string _petType = "Cat";
            _mockRepo.Setup(p => p.GetAllPetOwner()).Returns(_mockProvider.GetMockPetOwnerResult());

            //Act
            var result = _petOwnerService.GetPetsByOwnerGender(_petType) as List<PetGroup>;

            //Assert
            Assert.IsTrue(result.Any(r => "Male" == r.GroupName));
            Assert.IsTrue(result.Any(r => "Female" == r.GroupName));
        }

        [TestMethod]
        public void GetPetsByOwnerGenderGenderGroupCount()
        {
            //Arrange
            string _petType = "Cat";
            _mockRepo.Setup(p => p.GetAllPetOwner()).Returns(_mockProvider.GetMockPetOwnerResult());

            //Act
            var result = _petOwnerService.GetPetsByOwnerGender(_petType) as List<PetGroup>;

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
            _mockRepo.Setup(p => p.GetAllPetOwner()).Returns(_mockProvider.GetMockPetOwnerResult());

            //Act
            var result = _petOwnerService.GetPetsByOwnerGender(_petType) as List<PetGroup>;
            var petNames = result.Where(r => "Male" == r.GroupName).SelectMany(p => p.PetNames);
            var expectedResult = _mockProvider.GetMockPetGroup().Where(r => "Male" == r.GroupName).SelectMany(m => m.PetNames);

            //Assert
            Assert.AreEqual(petNames.ElementAt(index), expectedResult.ElementAt(index));
        }

        [TestMethod]
        public void TestNoGenderAvailableForGrouping()
        {
            //Arrange
            string _petType = "Cat";
            _mockRepo.Setup(p => p.GetAllPetOwner()).Returns(_mockProvider.GetMockPetOwnerSingleGenderNull());

            //Act
            var result = _petOwnerService.GetPetsByOwnerGender(_petType) as List<PetGroup>;
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
            _mockRepo.Setup(p => p.GetAllPetOwner()).Returns(_mockProvider.GetMockPetOwnerSingleNullPetArrayResult());

            //Act
            var result = _petOwnerService.GetPetsByOwnerGender(_petType) as List<PetGroup>;
            var maleGroupPetNames = result.Where(r => "Male" == r.GroupName).SelectMany(m => m.PetNames);

            //Assert
            Assert.AreEqual(maleGroupPetNames.Count(), 0);
        }


        [TestMethod]
        public void TestEmptyPetListExistsForAllGroup()
        {
            //Arrange
            string _petType = "Cat";
            _mockRepo.Setup(p => p.GetAllPetOwner()).Returns(_mockProvider.GetMockPetOwnerBothNullPetArrayResult());

            //Act
            var result = _petOwnerService.GetPetsByOwnerGender(_petType) as List<PetGroup>;
            var maleGroupPetNames = result.Where(r => "Male" == r.GroupName).SelectMany(m => m.PetNames);
            var femaleGroupPetNames = result.Where(r => "Female" == r.GroupName).SelectMany(m => m.PetNames);

            //Assert
            Assert.AreEqual(maleGroupPetNames.Count(), 0);
            Assert.AreEqual(femaleGroupPetNames.Count(), 0);
        }
    }
}
