using AGL.CatOwner.Models;
using AGL.CatOwner.Service.PetOwner;
using AGL.CatOwner.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AGL.CatOwner.Repository.PetOwner
{
    public class PetOwnerRepo : IPetOwnerRepo
    {
        private IPetOwnerService petOwnerService;

        public PetOwnerRepo(IPetOwnerService petOwnerService)
        {
            this.petOwnerService = petOwnerService;
        }

        public IEnumerable<PetGroup> GetPetsByOwnerGender(string petType)
        {
            List<PetGroup> _result = null;
            List<PetOwnerPerson> petOwners = this.petOwnerService.GetAllPetOwner().ToList();
            if (!string.IsNullOrWhiteSpace(petType) && petOwners.Count > 0)
            {
                _result = new List<PetGroup>();

                _result = petOwners.GroupBy(p => p.Gender)
                    .Select(p => new PetGroup
                    {
                        GroupName = p.Key,
                        PetNames = p.SelectManyExceptNull(po => po.Pets)
                        .Where(c => petType == c.Type)
                        .Select(c => c.Name)
                        .Distinct()
                        .OrderBy(c => c)
                        .ToList()
                    }).ToList<PetGroup>();
            }
            return _result;
        }
    }
}
