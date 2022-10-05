namespace NaNoEweb.Data
{
    public class MNovelContent : IModel
    {
        public int NoverlInstance_ID { get; set; }
        // Is the ID Just before this
        public int ID_Before { get; set; }
        // Is the ID after this
        public int ID_After { get; set; }
        // Chapter / Note / Bookmark
        public string Flag { get; set; }
        public string Text { get; set; }
    }
}