class Frequency
{
    public char Character;
    private int x;
    private int y;

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

    public void print()
    {
        Console.WriteLine($"Frequency: {Character} | Position (x,y): ({x},{y})");
    }

}

class Program08a
{

    static void Main(string[] args)
    {
        // string sFilePath = "data.txt";
        string sFilePath = "testdata.txt";

        List<char> cFrequencyList = new List<char>();
        List<Frequency> oFrequencyList = new List<Frequency>();
        List<Frequency> oFilteredFrequencyList;
        char cFrequency;
        int x_max, y_max;
        Frequency newObj;

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
        foreach (char f in cFrequencyList) print($"{f}");

        // sortera listan med objekt, sortera på frekvens
        oFrequencyList.Sort((f1, f2) => f1.Character.CompareTo(f2.Character));

        // skriv ut en lista över alla frekvenser med sina koordinater
        // foreach (Frequency f in oFrequencyList) f.print();

        // coming up
        // för varje unik frekvens
        //    - hitta alla förekomster av samma frekvens
        //         - räkna ut riktning och avstånd mellan utgångspunkten och förekomsten, använd avstånd och riktning för att 
        //              - hitta antinoden hos kompisen -> lägg till i lista med antinoder (av typen Frequency) om den inte redan finns
        //              - hitta antinoden hos den egna -> lägg till i lista med antinoder (av typen Frequency) om den inte redan finns

        if (1 == 2)
        {
            foreach (char f in cFrequencyList)
            {
                oFilteredFrequencyList = FilterFrequencyList(oFrequencyList, f);
                print($"--------------------------------------------------------------------------------------------------------{f}");

                foreach (Frequency oFrom in oFilteredFrequencyList)
                {

                    oFrom.print();
                    print("------------------------------------");
                    foreach (Frequency oTo in oFilteredFrequencyList)
                    {
                        if (oTo != oFrom)
                        {
                            // int iDistance = GetDistance(oFrom, oTo);
                            // int iDirection = GetDirection(oFrom, oTo);
                            oTo.print();
                        }
                    }
                    print("------------------------------------");
                }
            }
        }

        Frequency obj1 = new Frequency('a', 4, 4);
        Frequency obj2 = new Frequency('a', 5, 2);

        Frequency obj3 = obj2 - obj1;

        obj3.print();

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