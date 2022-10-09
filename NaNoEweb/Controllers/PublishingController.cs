using Microsoft.AspNetCore.Mvc;
using NaNoEweb.Data;

namespace NaNoEweb.Controllers
{
    public class PublishingController : Controller
    {
        public IActionResult Index()
        {
            if (LoadedNovels.LoadedNovel == null) return Redirect("~/");

            return View();
        }
    }
}
