static List<string> ReadFileToList(string _sFilePath)
{
    // Läs alla rader från filen
    var sLinesList = new List<string>();

    foreach (string sLine in File.ReadLines(_sFilePath))
    {
        sLinesList.Add(sLine.Trim());
    }

    return sLinesList;
}

// string sFilePath = "data.txt";
string sFilePath = "testdata.txt";

List<string> sDataList = ReadFileToList(sFilePath);

foreach (string sLine in sDataList)
{
    Console.WriteLine(sLine);
}