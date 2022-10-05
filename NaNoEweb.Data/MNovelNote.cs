namespace NaNoEweb.Data
{
    public enum MNovelNoteType
    {
        Note = 0,
        Chapter = 1
    }

    /// <summary>
    /// This will be how we organise our list of 'Notes', can set it up for orderby Title
    /// e.g. '001.Chapter Name' at start with '999.Chapter Name' at end
    /// Makes ordering simple. Need a search bar to 'filter', so like on Chapters I can type '12' which shows '012.Chapter Name'; notes makes it easy to find them - see interaction with .hidden in OE
    /// 
    /// Make these 'editable' and 'deletable'; perhaps TODO: make it like OE delete state where we dont remove the row, we can just make a 'Junk' pile of rows that aren't there anymore and can get them back one day if needed? Not sure about that though
    /// This way can so something like
    /// c:Sidd Fiddlehorn w/ description
    /// c:Kathy Alice ... w/ description
    /// can edit it as needed to add/change notes for the story
    ///  - Perhaps TODO: 'log changes' for absolutely everything so can go 'last week tuesday I changed 'this fab paragraph' to 'this unfab paragraph' and can view 'all actions in past' to get something I deleted by accident back? Maybe have a way to 'clean logs' 
    ///    when I dont care about 'something' in history anymore?
    /// 
    /// Instead of 'multiple rows bound to this' style, make it 'easy to see/edit' HTML vibes for usability and cleanliness
    /// This way can have like 40 to 60 rows here for info and can have 'chapters' used like sections, as in '001.Intro to Storyline' with info needed there, '009.The Aliens Attack', not specific to 'chapters', but rather 'what happens in the storyline'? TODO consider rename
    /// Fast interactions and simple side; perhaps keep searches in each uniquely so can jump back 'chapters' on '001' and 'notes' on 'sidd' search makes it back-and-forth-able somewhat
    /// 
    /// The goal here is to make it super simple to manage all data in our 'novel' with ease
    ///  - Perhaps TODO: Make this into a more complex data type with 'Category' which can be proper formatted text (e.g. 'Characters', or in chapters 'Attack of XYZ', which will order by name but be easy to search as well, can see Chapter 7 and 12 both have 
    ///    'Attack of XYZ' kinda vibes
    /// </summary>
    public class MNovelNote : IModel
    {
        public uint NovelInstance_ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}