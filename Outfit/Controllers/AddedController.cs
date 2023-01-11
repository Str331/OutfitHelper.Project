using Microsoft.AspNetCore.Mvc;
using Outfit.Domain;
using Outfit.Domain.ViewModels.Added;
using Outfit.Service.Service_Interfaces;

namespace Outfit.Controllers
{
    public class AddedController : Controller
    {
        private readonly IAddedService _addedService;

        public AddedController(IAddedService addedService) => _addedService = addedService;

        [HttpGet]
        public IActionResult CreateAdded(long id)
        {
            var addedModel = new CreateAddedViewModel()
            {
                ClothesId = id,
                Login = User.Identity.Name
            };
            return View(addedModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdded(CreateAddedViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _addedService.Create(model);
                if(response.StatusCode == Domain.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _addedService.Delete(id);
            if(response.StatusCode == Domain.StatusCode.OK)
            {
                return RedirectToAction("Detail", "Favorite");
            }
            return View("Error", $"{response.Description}");
        }
    }
}
