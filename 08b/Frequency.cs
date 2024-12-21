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