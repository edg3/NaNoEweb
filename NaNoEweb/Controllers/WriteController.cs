using Microsoft.AspNetCore.Mvc;
using NaNoEweb.Data;
using NaNoEweb.DBConnection;

namespace NaNoEweb.Controllers;

public class WriteController : Controller
{
    public IActionResult Index()
    {
        if (LoadedNovels.LoadedNovel == null) return Redirect("~/");

        return View();
    }

    public IActionResult _UpdateCounts()
    {
        if (LoadedNovels.LoadedNovel == null) return Redirect("~/");

        return View();
    }


    [HttpPost]
    public JsonResult Act_AddParagraph(string temp_id, string id_before, string id_after, string flag, string text)
    {
        if (string.IsNullOrEmpty(temp_id) || string.IsNullOrEmpty(id_before) || string.IsNullOrEmpty(id_after) || string.IsNullOrEmpty(flag) || string.IsNullOrEmpty(text)) return Json(new { status = "failure", message = $"Something was invisible." });

        var answer = new MNovelContent()
        {
            NoverlInstance_ID = (int)LoadedNovels.LoadedNovel.ID,
            ID_Before = int.Parse(id_before),
            ID_After = int.Parse(id_after),
            Flag = flag,
            Text = text,
            LastEditTime = DateTime.Now
        };
        DB.I.Insert(answer);

        // when success ajax call can update all ids before and after of temp IDs with DB id
        return Json(new { status = "success", message = $"{temp_id}:{answer.ID}" });
    }

    [HttpPost]
    public JsonResult Act_UpdateParagraphNextID(string para_id, string newId)
    {
        if (int.Parse(para_id) > 0)
        {
            var which = DB.I.Get<MNovelContent>($"WHERE id={para_id}").First();
            which.ID_After = int.Parse(newId);
            DB.I.Update(which);
        }
        return Json(new { status = "success" });
    }

    [HttpPost]
    public JsonResult Act_UpdateParagraphPreviousID(string para_id, string newId)
    {
        if (int.Parse(para_id) > 0)
        {
            var which = DB.I.Get<MNovelContent>($"WHERE id={para_id}").First();
            which.ID_Before = int.Parse(newId);
            DB.I.Update(which);
        }
        return Json(new { status = "success" });
    }

    [HttpPost]
    public JsonResult Act_UpdateTimeTrack()
    {
        var lastTrack = DB.I.GetLatestTracking((int)LoadedNovels.LoadedNovel.ID);
        if (null == lastTrack)
        {
            var insert = new TTracking()
            {
                NovelInstance_ID = (int)LoadedNovels.LoadedNovel.ID,
                SessionStart = DateTime.Now,
                SessionStop = DateTime.Now
            };
            DB.I.Insert(insert);
        }
        else
        {
            if (lastTrack.SessionStop.AddMinutes(5) > DateTime.Now)
            {
                // Update it
                lastTrack.SessionStop = DateTime.Now;
                DB.I.Update(lastTrack);
            }
            else
            {
                var insert = new TTracking()
                {
                    NovelInstance_ID = (int)LoadedNovels.LoadedNovel.ID,
                    SessionStart = DateTime.Now,
                    SessionStop = DateTime.Now
                };
                DB.I.Insert(insert);
            }
        }

        return Json(new { status = "success" });
    }

    public JsonResult Act_UpdateContentText(string content_id, string new_text)
    {
        var content = DB.I.Get<MNovelContent>($"WHERE [id]={content_id}").First();
        content.Text = new_text;
        DB.I.Update(content);

        return Json(new { status = "success" });
    }
}
