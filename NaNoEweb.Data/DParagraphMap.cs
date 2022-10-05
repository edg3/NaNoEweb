namespace NaNoEweb.Data
{
    public class DParagraphMap
    {
        // Paragraph ID
        public int ID { get; set; }
        // ID of Paragraph Before this ID
        public int ID_Before { get; set; }
        // ID of Paragraph After this ID
        public int ID_After { get; set; }

        // e.g. The paragraph before '21' is '27', and the paragraph after '21' is '22' => we put 27 in that spot afterwards
        public string Flag { get; set; }
    }
}
