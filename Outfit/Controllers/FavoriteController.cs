using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Outfit.Service.Service_Interfaces;

namespace Outfit.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService) => _favoriteService = favoriteService;

        public async Task<IActionResult> Detail()
        {
            var response = await _favoriteService.GetItems(User.Identity.Name);
            if(response.StatusCode== Domain.StatusCode.OK)
            {
                return View(response.Data.ToList());
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetOneItem(long id)
        {
            var response = await _favoriteService.GetOneItem(User.Identity.Name, id);
            if(response.StatusCode== Domain.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
