namespace NaNoEweb.Data
{
    public class MNovelChapterNote : IModel
    {
        public int NovelChapter_Id { get; set; } = 0;
        public string Note { get; set; } = "";
    }
}
