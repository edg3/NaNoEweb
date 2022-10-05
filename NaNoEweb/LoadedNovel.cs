namespace NaNoEweb;

public static class LoadedNovels
{
    public static MNovelInstance? LoadedNovel { get; set; } = null;
    public static List<MNovelInstance> NovelInstances { get; set; } = new List<MNovelInstance>();
}