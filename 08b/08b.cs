class Program08a
{

    static void Main(string[] args)
    {
        string sFilePath = "data.txt";
        // string sFilePath = "testdata.txt";

        List<char> cFrequencyList = new List<char>();
        List<Frequency> oFrequencyList = new List<Frequency>();
        List<Frequency> oFilteredFrequencyList;
        char cFrequency;
        int x_max, y_max;
        Frequency newObj, oDistance, oAntiNode;
        List<Frequency> oAntiNodeList = new List<Frequency>();
        bool bDebug = false;

        List<string> sDataList = ReadFileToList(sFilePath);
        foreach (string sLine in sDataList) print(sLine);

        x_max = sDataList[0].Length - 1;
        y_max = sDataList.Count - 1;

        print($"x_max = {x_max} | y_max: {y_max}");

        for (int x = 0; x <= x_max; x++)
        {
            for (int y = 0; y <= y_max; y++)
            {
                cFrequency = sDataList[y][x];
                if (cFrequency != '.')
                {
                    if (!cFrequencyList.Contains(cFrequency)) cFrequencyList.Add(cFrequency);
                    newObj = new Frequency(cFrequency, x, y);
                    if (!oFrequencyList.Contains(newObj)) oFrequencyList.Add(newObj);
                }
            }
        }

        // lista de olika frekvenserna
        if (bDebug) foreach (char f in cFrequencyList) print($"{f}");

        // sortera listan med objekt, sortera på frekvens
        oFrequencyList.Sort((f1, f2) => f1.Character.CompareTo(f2.Character));

        foreach (char f in cFrequencyList)
        {
            oFilteredFrequencyList = FilterFrequencyList(oFrequencyList, f);
            if (bDebug) print($"--------------------------------------------------------------------------------------------------------{f}");

            foreach (Frequency oFrom in oFilteredFrequencyList)
            {

                if (bDebug) oFrom.print();
                if (bDebug) print("------------------------------------");
                foreach (Frequency oTo in oFilteredFrequencyList)
                {
                    if (oTo != oFrom)
                    {
                        if (bDebug) oTo.print();

                        oDistance = oTo - oFrom;

                        oAntiNode = oFrom;

                        while (!IsOutOfBounds(oAntiNode, x_max, y_max))
                        {
                            oAntiNode.Character = '#';

                            if (!oAntiNodeList.Contains(oAntiNode))
                            {
                                oAntiNodeList.Add(oAntiNode);
                                if (bDebug) oAntiNode.print("Added antinode: ");
                            }

                            oAntiNode = oAntiNode - oDistance;
                        }

                        oAntiNode = oTo;

                        while (!IsOutOfBounds(oAntiNode, x_max, y_max))
                        {
                            oAntiNode.Character = '#';

                            if (!oAntiNodeList.Contains(oAntiNode))
                            {
                                oAntiNodeList.Add(oAntiNode);
                                if (bDebug) oAntiNode.print("Added antinode: ");
                            }

                            oAntiNode = oAntiNode + oDistance;
                        }





                    }
                }
                if (bDebug) print("------------------------------------");
            }
        }

        print($"Antinode count: {oAntiNodeList.Count}");
        if (bDebug) foreach (Frequency an in oAntiNodeList) an.print();
    }

    static bool IsOutOfBounds(Frequency _node, int _x_max, int _y_max)
    {
        return !((_node.x >= 0 && _node.x <= _x_max) && _node.y >= 0 && _node.y <= _y_max);
    }

    static void TestFrequencyObject()
    {
        Frequency oLocal = new Frequency('0', 4, 4);
        Frequency oRemote = new Frequency('0', 5, 2);

        Frequency oDistance = oRemote - oLocal;
        Frequency oAntiNode1 = oLocal - oDistance;
        Frequency oAntiNode2 = oRemote + oDistance;

        oDistance.print("Distance: ");
        oAntiNode1.print("AntiNode 1: ");
        oAntiNode2.print("AntiNode 2: ");
    }

    static List<Frequency> FilterFrequencyList(List<Frequency> _oInputFrequencyList, char _cFrequency)
    {
        List<Frequency> oOutputFrequencyList = new List<Frequency>();

        foreach (Frequency f in _oInputFrequencyList)
        {
            if (f.Character == _cFrequency) oOutputFrequencyList.Add(f);
        }

        return oOutputFrequencyList;
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