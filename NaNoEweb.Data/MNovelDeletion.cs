namespace NaNoEweb.Data
{
    public class MNovelDeletion : IModel
    {
        public int Novel_Id { get; set; } = 0;

        public int Id_Deleted { get; set; } = 0;
        public string Content_Deleted { get; set; } = string.Empty;
        public string Content_Type { get; set; } = string.Empty;

        public int Id_Before { get; set; } = 0;
        public string Content_Before { get; set; } = string.Empty;

        public int Id_After { get; set; } = 0;
        public string Content_After { get; set; } = string.Empty;
    }
}
