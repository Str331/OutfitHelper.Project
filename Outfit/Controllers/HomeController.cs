using Microsoft.AspNetCore.Mvc;
using Outfit.Models;
using System.Diagnostics;
using Outfit.Domain.Entity;
using Outfit.DAL;

namespace Outfit.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}