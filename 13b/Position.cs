class Position
{
    public long x;
    public long y;
    public string name;

    public Position(long _x, long _y)
    {
        x = _x; // horisontal / col / string
        y = _y; // vertical   / row / item
        name = "";
    }

    public Position(long _x, long _y, string _name)
    {
        x = _x; // horisontal / col / string
        y = _y; // vertical   / row / item
        name = _name;
    }

    public string Description
    {
        get
        {
            if (name != "") return $"({x,4},{y,4}) {name}";
            return $"({x,4},{y,4})";
        }
    }

    public static Position operator -(Position obj1, Position obj2)
    {
        long x, y;

        x = obj1.x - obj2.x;
        y = obj1.y - obj2.y;

        return new Position(x, y);
    }

    public static Position operator +(Position obj1, Position obj2)
    {
        long x, y;

        x = obj1.x + obj2.x;
        y = obj1.y + obj2.y;

        return new Position(x, y);
    }

    public static Position operator *(Position obj1, long Factor)
    {
        long x, y;

        x = obj1.x * Factor;
        y = obj1.y * Factor;

        return new Position(x, y);
    }

    public static Position operator *(long Factor, Position obj1)
    {
        long x, y;

        x = obj1.x * Factor;
        y = obj1.y * Factor;

        return new Position(x, y);
    }

    public static long operator /(Position obj1, Position obj2)
    {
        long x, y;

        x = obj1.x / obj2.x;
        y = obj1.y / obj2.y;

        if (x <= y) return x;
        else return y;

    }

    public static bool operator <=(Position obj1, Position obj2)
    {
        return obj1.x <= obj2.x && obj1.y <= obj2.y;
    }

    public static bool operator >=(Position obj1, Position obj2)
    {
        return obj1.x >= obj2.x && obj1.y >= obj2.y;
    }

    public static bool operator <(Position obj1, Position obj2)
    {
        return obj1.x < obj2.x && obj1.y < obj2.y;
    }

    public static bool operator >(Position obj1, Position obj2)
    {
        return obj1.x > obj2.x && obj1.y > obj2.y;
    }

    public static bool operator ==(Position obj1, Position obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true; // Both references poulong to the same object
        if (obj1 is null || obj2 is null)
            return false; // One is null, and the other is not

        // Compare property values
        return obj1.x == obj2.x && obj1.y == obj2.y;
    }

    public static bool operator !=(Position obj1, Position obj2)
    {
        return !(obj1 == obj2);
    }

    // frågetecknet(?) efter object markerar att obj kan vara null
    public override bool Equals(object? obj)
    {
        if (obj is Position other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }

    public void print(string _prefix = "")
    {
        string sText = Description;
        if (_prefix != "") sText = $"{_prefix}{sText}";
        Console.WriteLine(sText);
    }

    public override string ToString() => Description;

}