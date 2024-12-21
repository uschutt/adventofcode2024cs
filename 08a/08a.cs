class Frequency
{
    public char Character;
    public int x;
    public int y;

    public Frequency(char _cCharacter, int _x, int _y)
    {
        Character = _cCharacter;
        x = _x;
        y = _y;
    }

    public static Frequency operator -(Frequency obj1, Frequency obj2)
    {
        int x, y;

        x = obj1.x - obj2.x;
        y = obj1.y - obj2.y;

        return new Frequency(obj1.Character, x, y);
    }

    public static Frequency operator +(Frequency obj1, Frequency obj2)
    {
        int x, y;

        x = obj1.x + obj2.x;
        y = obj1.y + obj2.y;

        return new Frequency(obj1.Character, x, y);
    }

    public static bool operator ==(Frequency obj1, Frequency obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true; // Both references point to the same object
        if (obj1 is null || obj2 is null)
            return false; // One is null, and the other is not

        // Compare property values
        return obj1.Character == obj2.Character &&
               obj1.x == obj2.x &&
               obj1.y == obj2.y;
    }

    public static bool operator !=(Frequency obj1, Frequency obj2)
    {
        return !(obj1 == obj2);
    }

    // frågetecknet(?) efter object markerar att obj kan vara null
    public override bool Equals(object? obj)
    {
        if (obj is Frequency other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Character, x, y);
    }

    public void print(string _prefix = "")
    {
        string sText = $"Frequency: {Character} | Position (x,y): ({x},{y})";
        if (_prefix != "") sText = $"{_prefix} | {sText}";
        Console.WriteLine(sText);
    }

}

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
        Frequency newObj, oDistance;
        List<Frequency> oAntiNodeList = new List<Frequency>();
        Frequency[] oAntiNodeArray;
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

                        oAntiNodeArray = [oFrom - oDistance, oTo + oDistance];

                        foreach (Frequency oAntiNode in oAntiNodeArray)
                        {
                            oAntiNode.Character = '#';
                            if (!IsOutOfBounds(oAntiNode, x_max, y_max) && !oAntiNodeList.Contains(oAntiNode))
                            {
                                oAntiNodeList.Add(oAntiNode);
                                if (bDebug) oAntiNode.print("Added antinode: ");
                            }
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