using AGL.CatOwner.Models;
using System.Collections.Generic;

namespace AGL.CatOwner.Service.PetOwner
{
    public interface IPetOwnerService
    {
        IEnumerable<PetOwnerPerson> GetAllPetOwner();
    }
}
