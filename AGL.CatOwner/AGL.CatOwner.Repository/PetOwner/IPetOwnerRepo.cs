using AGL.CatOwner.Models;
using System.Collections.Generic;

namespace AGL.CatOwner.Repository.PetOwner
{
    public interface IPetOwnerRepo
    {
        IEnumerable<PetGroup> GetPetsByOwnerGender(string petType);
    }
}
