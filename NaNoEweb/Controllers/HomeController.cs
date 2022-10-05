using Microsoft.AspNetCore.Mvc;
using NaNoEweb.Data;
using NaNoEweb.DBConnection;
using NaNoEweb.Models;
using System.Diagnostics;

namespace NaNoEweb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (LoadedNovels.LoadedNovel != null) return Redirect("~/Write/");

        return View();
    }

    public IActionResult CreateNewNovel()
    {
        if (LoadedNovels.LoadedNovel != null) return Redirect("~/Write/");

        return View();
    }

     [HttpPost]
    public JsonResult Act_NewNovel(string name, string info)
    {
        if (null == name) return Json(new { status = "error", message = "Please type in a name for your novel..." });
        if (name.Length < 2) return Json(new { status = "error", message = "Please make a name 2 characters or longer... I mean, sure, you could write the novel 'At' because 'It' already exists?" });
        if (name.Length > 256) return Json(new { status = "error", message = "Can only have up to the excessive 256 characters in the name... Sorry." });
        if (null == info) info = "";

        // TODO: Verify name doesnt already exist in DB - make it put both to lower and clean out '  ' to ' ' as per usual
        DB.I?.Insert(new MNovelInstance()
        {
            Title = name,
            Info = info,
            LastEditTime = DateTime.Now,
        });

        return Json(new {status = "success", message = "Novel Added" });
    }

    public IActionResult OpenExistingNovel()
    {
        if (LoadedNovels.LoadedNovel != null) return Redirect("~/Write/");

        return View();
    }

    public IActionResult ViewHistory()
    {
        if (LoadedNovels.LoadedNovel != null) return Redirect("~/Write/");

        return View();
    }

    public IActionResult _GetHistory()
    {
        // TODO: VM to retrieve data fast to show nicely
        return View();
    }

    [HttpPost]
    public JsonResult Act_LoadListing(string id)
    {
        try
        {
            var selected = (from item in LoadedNovels.NovelInstances
                            where item.ID == int.Parse(id)
                            select item).First();
            LoadedNovels.LoadedNovel = selected;
        }
        catch (Exception e)
        {
            return Json(new { status = "error", message = "Couldn't load that novel bound, oops..." });
        }
        return Json(new { status = "success", message = "Loaded novel!" });
    }

    [HttpPost]
    public JsonResult Act_UnloadListing()
    {
        LoadedNovels.LoadedNovel = null;
        return Json(new { status = "success", message = "Unloaded novel!" });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}