using Microsoft.AspNetCore.Mvc;
using Outfit.Domain;
using Outfit.Domain.ViewModels.Profile;
using Outfit.Service.Service_Interfaces;

namespace Outfit.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProfileViewModel model)
        {
            ModelState.Remove("Id");
            ModelState.Remove("UserName");
            if (ModelState.IsValid)
            {
                var response = await _profileService.Save(model);
                if(response.StatusCode == Domain.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        public async Task<IActionResult> Detail()
        {
            var UserName = User.Identity.Name;
            var response = await _profileService.GetProfile(UserName);
            if(response.StatusCode== Domain.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View();
        }
    }
}
