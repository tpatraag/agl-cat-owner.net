using AGL.CatOwner.Service.PetOwner;
using System.Web.Mvc;

namespace AGL.CatOwner.Web.Controllers
{
    public class CatOwnerController : BaseController
    {
        private readonly IPetOwnerService _petOwnerService;

        public CatOwnerController(IPetOwnerService petOwnerService) {
            this._petOwnerService = petOwnerService;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "AGL Cat Owner";
            return View(this._petOwnerService.GetPetsByOwnerGender("Cat"));
        }
    }
}