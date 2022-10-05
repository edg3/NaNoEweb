using PlatformSpellCheck;

string line = "";
var spellChecker = new SpellChecker();
string characters = "0123456789qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM' ";
while (line != "r")
{
    Console.WriteLine("Give the line; 'r' alone ends...");
    line = Console.ReadLine();
    var innerLine = "";
    foreach (char c in line)
    {
        if (characters.Contains(c))
        {
            innerLine += c;
        }
    }

    Console.WriteLine("Checking: " + innerLine);
    var words = innerLine.Split(' ');
    foreach (var word in words)
    {
        Console.WriteLine(" - Word: " + word);
        var errors = spellChecker.Check(word).ToList();
        foreach (var e in errors)
            Console.WriteLine("\t" + e.RecommendedReplacement.ToString() + "\n\t" + e.RecommendedReplacement.ToString() + "\n\t- - -");

    }
    Console.WriteLine("\n\n");
}