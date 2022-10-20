using System;

namespace NaNoEweb.Data
{
    public class TTracking
    {
        public uint ID { get; set; }
        public int NovelInstance_ID { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionStop { get; set; }
        public int WordCount_Start { get; set; }
        public int WordCount_End { get; set; }
        public int Chapters_Start { get; set; }
        public int Chapters_End { get; set; }
        public int Paragraphs_Start { get; set; }
        public int Paragraphs_End { get; set; }
        public int Bookmarks_Start { get; set; }
        public int Bookmarks_End { get; set; }
        public int Notes_Start { get; set; }
        public int Notes_End { get; set; }
    }
}
