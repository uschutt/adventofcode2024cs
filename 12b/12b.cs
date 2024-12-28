using System;

class Program12b
{
    static string __sFilePath = "data.txt";

    static void Main(string[] args)
    {
        int iResult;

        List<string> sDataList = ReadFileToList(__sFilePath);
        // foreach (string sLine in sDataList) print(sLine);

        DateTime dtStartTime = DateTime.Now;

        iResult = Solution(sDataList);

        DateTime dtEndTime = DateTime.Now;

        TimeSpan tsDifference = dtEndTime - dtStartTime;

        print("------------------------------------------------------------------------------------------------------------------------------------------");
        print($"| Result: {iResult} | calculated in {tsDifference.TotalSeconds} seconds");
        print("------------------------------------------------------------------------------------------------------------------------------------------");

        // 821737 wrong answer - too high

    }

    static int Solution(List<string> _sDataList)
    {
        Map theMap = new Map(_sDataList);
        Regions RegionList = new Regions(theMap);

        print("");
        print(theMap.Description());
        print("");

        char cCurrentPlotType;

        for (int x = 0; x <= theMap.xMax; x++)
        {
            for (int y = 0; y <= theMap.yMax; y++)
            {
                cCurrentPlotType = theMap.TypeByXY(x, y);

                // crate plot
                Plot newPlot = new Plot(cCurrentPlotType, new Position(x, y));

                // create region 
                if (!RegionList.RegionExists(cCurrentPlotType, newPlot))
                {
                    Region newRegion = RegionList.AddRegion(cCurrentPlotType, newPlot);
                    print(newRegion.Description());
                }

            }

        }

        // DebugPrint(RegionList);
        ExportRegions(RegionList);
        ExportMap(theMap);

        print();
        print(RegionList.Description());

        return RegionList.TotalPriceWithDiscount;
    }

    static void ExportRegions(Regions _RegionList, string _filePath = "results.txt")
    {
        using (StreamWriter writer = new StreamWriter(_filePath))
        {
            writer.WriteLine(_RegionList.Items[0].Export(true));
            foreach (Region r in _RegionList.Items)
            {
                writer.WriteLine(r.Export());
            }
        }
    }

    static void ExportMap(Map _map, string _filePath = "map.txt")
    {
        using (StreamWriter writer = new StreamWriter(_filePath))
        {
            foreach (string s in _map.Export())
            {
                writer.WriteLine(s);
            }
        }
    }

    static void DebugPrint(Regions _RegionList)
    {
        foreach (Region r in _RegionList.Items)
        {
            // foreach (Plot p in r.Plots.Items) print(p.ToString());
            // print(r.Description());
            // print($"{r.Type} | {r.Plots.Items.Count}");
            if (r.Type == 'M')
            {
                int iSide = 0;
                foreach (Side ps in r.Sides)
                {
                    iSide++;
                    print();
                    print(ps.Description($"{r.Type} side {iSide}"));
                    foreach (Plot p in ps.Plots.Items)
                    {
                        print($"{p.Description()}");
                    }
                }
                // foreach (Plot p in r.Plots.Items)
                // {
                //     print($"Plot {p.Description()} sides:");
                //     foreach (Position side in p.Perimiters)
                //     {
                //         print($" - {side.Description()}");
                //     }
                // }
            }
        }
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

    static void print(string sText = "")
    {
        Console.WriteLine(sText);
    }
}