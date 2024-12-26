using System;

class Program12a
{
    static string __sFilePath = "testdata.txt";

    static void Main(string[] args)
    {
        int iResult;

        List<string> sDataList = ReadFileToList(__sFilePath);
        foreach (string sLine in sDataList) print(sLine);

        DateTime dtStartTime = DateTime.Now;

        iResult = Solution(sDataList);

        DateTime dtEndTime = DateTime.Now;

        TimeSpan tsDifference = dtEndTime - dtStartTime;

        print("------------------------------------------------------------------------------------------------------------------------------------------");
        print($"| Result: {iResult} | calculated in {tsDifference.TotalSeconds} seconds");
        print("------------------------------------------------------------------------------------------------------------------------------------------");

    }

    static int Solution(List<string> _sDataList)
    {
        int iResult = 0;

        Map theMap = new Map(_sDataList);
        // Regions RegionList = new Regions(theMap);
        Plots PlotsList = new Plots();

        char cPreviousPlotType = '-';
        char cCurrentPlotType;

        for (int x = 0; x <= theMap.xMax; x++)
        {
            for (int y = 0; y <= theMap.yMax; y++)
            {
                cCurrentPlotType = theMap.TypeByXY(x, y);
                PlotsList.AddPlot(cCurrentPlotType, new Position(x, y));
            }

        }

        foreach (Plot p in PlotsList.Items)
        {
            print(p.ToString());
        }

        return iResult;
    }

    static string ReadFileToString(string _sFilePath)
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
}