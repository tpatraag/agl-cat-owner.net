using AGL.CatOwner.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AGL.CatOwner.Tests.MockData
{
    public class MockProvider
    {
        #region Gender Pets
        [Ignore]
        public List<Pet> GetMaleMockPetResult()
        {
            return new List<Pet>
            {
                new Pet {Name="Garfield",Type="Cat"},
                new Pet {Name="Fido", Type="Dog" },
                new Pet {Name="Tom", Type="Cat" },
                new Pet {Name="Simba", Type="Cat" },
                new Pet {Name="Nemo", Type="Fish" },
                new Pet {Name="Sam", Type="Dog" },
                new Pet {Name="Garfield", Type="Cat" }
            };
        }
        [Ignore]
        public List<Pet> GetFemaleMockPetResult()
        {
            return new List<Pet>
            {
                new Pet {Name="Rosy",Type="Cat"},
                new Pet {Name="Fido", Type="Dog" },
                new Pet {Name="Lucy", Type="Cat" },
                new Pet {Name="Sweetie", Type="Cat" },
                new Pet {Name="Nemo", Type="Fish" },
                new Pet {Name="Sam", Type="Dog" },
                new Pet {Name="Rosy", Type="Cat" }
            };
        }
        #endregion

        #region PetOwner Setup
        [Ignore]
        public List<PetOwnerPerson> GetMockPetOwnerResult()
        {
            return new List<PetOwnerPerson>
            {
                new PetOwnerPerson { Name ="Bob",Gender = "Male", Age=23, Pets = GetMaleMockPetResult() },
                new PetOwnerPerson { Name ="Jennifer",Gender = "Female", Age=18, Pets = GetFemaleMockPetResult()}
             };
        }
        [Ignore]
        public List<PetOwnerPerson> GetMockPetOwnerSingleNullPetArrayResult()
        {
            return new List<PetOwnerPerson>
            {
                new PetOwnerPerson { Name ="Bob",Gender = "Male", Age=23, Pets = null },
                new PetOwnerPerson { Name ="Jennifer",Gender = "Female", Age=18, Pets = GetFemaleMockPetResult()}
             };
        }
        [Ignore]
        public List<PetOwnerPerson> GetMockPetOwnerBothNullPetArrayResult()
        {
            return new List<PetOwnerPerson>
            {
                new PetOwnerPerson { Name ="Bob",Gender = "Male", Age=23, Pets = null },
                new PetOwnerPerson { Name ="Jennifer",Gender = "Female", Age=18, Pets =null}
             };
        }
        [Ignore]
        public List<PetOwnerPerson> GetMockPetOwnerSingleGenderNull()
        {
            return new List<PetOwnerPerson>
            {
                new PetOwnerPerson { Name ="Bob",Gender = null, Age=23, Pets = GetMaleMockPetResult() },
                new PetOwnerPerson { Name ="Jennifer",Gender = null, Age=18, Pets = GetFemaleMockPetResult()}
             };
        }
        #endregion

        #region Mock PetGroup Setup
        [Ignore]
        public List<PetGroup> GetMockPetGroup()
        {
            return new List<PetGroup>
            {
                new PetGroup { GroupName = "Male", PetNames = new List<string> { "Garfield", "Simba", "Tom" }},
                new PetGroup { GroupName = "Female", PetNames = new List<string> { "Lucy", "Rosy", "Sweetie" }},
            };
        }
        //Mock Error Data For Test
        [Ignore]
        public List<PetGroup> GetMockPetGroupWithError()
        {
            return new List<PetGroup>
            {
                new PetGroup { GroupName = "Male", PetNames = null},
                new PetGroup { GroupName = "Female", PetNames = null},
            };
        }
        //Mock Error Data For Test
        [Ignore]
        public List<PetGroup> GetMockPetGroupWithNull()
        {
            return null;
        }
        #endregion
    }
}
