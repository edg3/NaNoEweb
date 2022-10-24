namespace NaNoEweb.Data
{
    public class MNovelChapter : IModel
    {
        public int NovelInstance_Id { get; set; } = 0;
        public string Title { get; set; } = "";
        public int OrderPosition { get; set; } = 0;
    }
}
