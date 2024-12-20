﻿static string ReadFileToString(string _sFilePath)
{
    string sReturnValue = "";

    foreach (string line in File.ReadLines(_sFilePath))
    {
        sReturnValue += line.Trim();
    }

    return sReturnValue;
}

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

static void print(string sText)
{
    Console.WriteLine(sText);
}

// string sFilePath = "data.txt";
string sFilePath = "testdata.txt";

List<string> sDataList = ReadFileToList(sFilePath);
foreach (string sLine in sDataList) print(sLine);