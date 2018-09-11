using AGL.CatOwner.Repository.PetOwner;
using System.Web.Mvc;

namespace AGL.CatOwner.Web.Controllers
{
    public class CatOwnerController : BaseController
    {
        private IPetOwnerRepo petOwnerRepo;

        public CatOwnerController(IPetOwnerRepo petOwnerRepo) {
            this.petOwnerRepo = petOwnerRepo;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "AGL Cat Owner";
            return View(this.petOwnerRepo.GetPetsByOwnerGender("Cat"));
        }
    }
}