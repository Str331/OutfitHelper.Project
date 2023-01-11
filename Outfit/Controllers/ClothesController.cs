using Microsoft.AspNetCore.Mvc;
using Outfit.Domain.Response;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Outfit.DAL.Interfaces;
using Outfit.Service.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Outfit.Domain.ViewModels.Clothes;
using System.Linq;
using Outfit.Domain.Extensions;

namespace Outfit.Controllers
{
    public class ClothesController : Controller
    {
        private readonly IClothesService _clothesService;
        public ClothesController(IClothesService clothesService)
        {
            _clothesService = clothesService;
        }

        [HttpGet ]
        //получение всех образов
        public IActionResult GetClothes()
        {
            var response =  _clothesService.GetClothes();
            if(response.StatusCode == Domain.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"response.Description");
        }

        //удаление обьекта
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _clothesService.DeleteClothes(id);
            if (response.StatusCode == Domain.StatusCode.OK)
            {
                return RedirectToAction("GetClothes");
            }
            return View("Error",$"{response.Description}");
        }

        public IActionResult Compare() => PartialView();

        //сохранение обьекта
        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
                return PartialView();
            var response = await _clothesService.GetOneClothes(id);
            if(response.StatusCode == Domain.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ClothesViewModel model)
        {
            ModelState.Remove("DateOfAdd");
            if (ModelState.IsValid)
            {
                if(model.Id == 0)
                {
                    byte[] imageData;
                    using(var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                    }
                    await _clothesService.CreateClothes(model, imageData);
                }
                else
                {
                    await _clothesService.Edit(model.Id,model);
                }
                return RedirectToAction("GetClothes");
            }
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().Join();
            return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
        }

        [HttpGet]
        //получение одного образа по id
        public async Task<IActionResult> GetOneClothes(int id, bool isJson)
        {
            var response = await _clothesService.GetOneClothes(id);
            if (isJson)
            {
                return Json(response.Data);
            }
            return PartialView("GetOneClothes", response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetOneClothes(string term,int page=1,int pageSize = 5)
        {
            var response = await _clothesService.GetOneClothes(term);
            return Json(response.Data);
        }

        //получение типа образа
        [HttpPost]
        public JsonResult GetTypes()
        {
            var types = _clothesService.GetTypes();
            return Json(types.Data);
        }
    }
}
