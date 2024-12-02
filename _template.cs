class day02
{
    static void Main(string[] args)
    {
        // string filePath = "data.txt";
        string filePath = "testdata.txt";

        var dataList = ReadFileToList(filePath);

        foreach (string d in dataList)
        {
            Console.WriteLine(d);
        }
    }
    static List<string> ReadFileToList(string filePath)
    {
        // Läs alla rader från filen
        var lines = new List<string>();

        foreach (var line in File.ReadLines(filePath))
        {
            lines.Add(line.Trim());
        }

        return lines;
    }
}