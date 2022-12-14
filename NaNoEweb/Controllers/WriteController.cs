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

    public IActionResult _GetChapterNotes()
    {
        if (LoadedNovels.LoadedNovel == null) return Redirect("~/");

        return View();
    }

    public IActionResult _GetNoteNotes()
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
        var counts = DB.I.GetNumbers((int)LoadedNovels.LoadedNovel.ID);
        /* Paragraphs, Chapters, Notes, Bookmarks, Wordcount*/
        if (null == lastTrack)
        {
            var insert = new TTracking()
            {
                NovelInstance_ID = (int)LoadedNovels.LoadedNovel.ID,
                SessionStart = DateTime.Now,
                SessionStop = DateTime.Now,
                Paragraphs_Start = counts[0],
                Paragraphs_End = counts[0],
                Chapters_Start = counts[1],
                Chapters_End = counts[1],
                Notes_Start = counts[2],
                Notes_End = counts[2],
                Bookmarks_Start = counts[3],
                Bookmarks_End = counts[3],
                WordCount_Start = counts[4],
                WordCount_End = counts[4]
            };
            DB.I.Insert(insert);
        }
        else
        {
            if (lastTrack.SessionStop.AddMinutes(5) > DateTime.Now)
            {
                // Update it
                lastTrack.SessionStop = DateTime.Now;
                lastTrack.Paragraphs_End = counts[0];
                lastTrack.Chapters_End = counts[1];
                lastTrack.Notes_End = counts[2];
                lastTrack.Bookmarks_End = counts[3];
                lastTrack.WordCount_End = counts[4];
                DB.I.Update(lastTrack);
            }
            else
            {
                var insert = new TTracking()
                {
                    NovelInstance_ID = (int)LoadedNovels.LoadedNovel.ID,
                    SessionStart = DateTime.Now,
                    SessionStop = DateTime.Now,
                    Paragraphs_Start = counts[0],
                    Paragraphs_End = counts[0],
                    Chapters_Start = counts[1],
                    Chapters_End = counts[1],
                    Notes_Start = counts[2],
                    Notes_End = counts[2],
                    Bookmarks_Start = counts[3],
                    Bookmarks_End = counts[3],
                    WordCount_Start = counts[4],
                    WordCount_End = counts[4]
                };
                DB.I.Insert(insert);
            }
        }

        return Json(new { status = "success" });
    }

    [HttpPost]
    public JsonResult Act_UpdateContentText(string content_id, string new_text)
    {
        var content = DB.I.Get<MNovelContent>($"WHERE [id]={content_id}").First();
        content.Text = new_text;
        DB.I.Update(content);

        return Json(new { status = "success" });
    }

    [HttpPost]
    public JsonResult Act_RemoveParagraph(string id, string prev, string next, string curContent, string prevContent, string nextContent, string dataType)
    {
        if (LoadedNovels.LoadedNovel == null) return Json(new { status = "failure" });

        int _id = int.Parse(id);
        int _prev = int.Parse(prev);
        int _next = int.Parse(next);
        if (curContent.Contains("\r\n")) curContent = curContent.Replace("\r\n", "");
        curContent = curContent.Trim();
        if (prevContent.Contains("\r\n")) prevContent = prevContent.Replace("\r\n", "");
        prevContent = prevContent.Trim();
        if (nextContent.Contains("\r\n")) nextContent = nextContent.Replace("\r\n", "");
        nextContent = nextContent.Trim();
        if (prevContent == "undefined") prevContent = "[Novel Start]";
        if (nextContent == "undefined") nextContent = "[Novel End]";
        var _dataType = dataType.Split(' ').Last();

        DB.I?.DeleteAndTrack((int)LoadedNovels.LoadedNovel.ID, _id, curContent, _dataType, _prev, prevContent, _next, nextContent);

        return Json(new { status = "success" });
    }

    [HttpPost]
    public JsonResult Act_AddChapterNote(string title, string firstNote)
    {
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(firstNote)) return Json(new { status = "failed" });

        var countChaptersInNotes = DB.I.Get<MNovelChapter>($"WHERE novelinstance_id={LoadedNovels.LoadedNovel.ID}").Count;
        var newChapterInNotes = new MNovelChapter()
        {
            Title = title,
            OrderPosition = countChaptersInNotes + 1,
            NovelInstance_Id = (int)LoadedNovels.LoadedNovel.ID
        };
        DB.I.Insert(newChapterInNotes);

        var chapterInNotesNote = new MNovelChapterNote()
        {
            Note = firstNote,
            NovelChapter_Id = (int)newChapterInNotes.ID
        };
        DB.I.Insert(chapterInNotesNote);

        return Json(new { status = "success", id = newChapterInNotes.ID });
    }

    [HttpPost]
    public JsonResult Act_AddAnotherChapterNote(string id, string note)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(note)) return Json(new { status = "failed" });

        var inserted = new MNovelChapterNote()
        {
            NovelChapter_Id = int.Parse(id),
            Note = note
        };
        DB.I.Insert(inserted);

        return Json(new { status = "success", id = inserted.ID });
    }

    [HttpPost]
    public JsonResult Act_AddNoteNote(string title, string firstNote)
    {
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(firstNote)) return Json(new { status = "failed" });

        var newNoteInNotes = new MNovelNote()
        {
            Title = title,
            NovelInstance_ID = LoadedNovels.LoadedNovel.ID
        };
        DB.I.Insert(newNoteInNotes);

        var noteInNotesNote = new MNovelNoteNote()
        {
            Note = firstNote,
            NovelNote_Id = (int)newNoteInNotes.ID
        };
        DB.I.Insert(noteInNotesNote);

        return Json(new { status = "success", id = newNoteInNotes.ID });
    }

    [HttpPost]
    public JsonResult Act_AddAnotherNoteNote(string id, string note)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(note)) return Json(new { status = "failed" });

        var inserted = new MNovelNoteNote()
        {
            NovelNote_Id = int.Parse(id),
            Note = note
        };
        DB.I.Insert(inserted);

        return Json(new { status = "success", id = inserted.ID });
    }
}
