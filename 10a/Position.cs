class Position
{
    public int x;
    public int y;

    public Position(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public static Position operator -(Position obj1, Position obj2)
    {
        int x, y;

        x = obj1.x - obj2.x;
        y = obj1.y - obj2.y;

        return new Position(x, y);
    }

    public static Position operator +(Position obj1, Position obj2)
    {
        int x, y;

        x = obj1.x + obj2.x;
        y = obj1.y + obj2.y;

        return new Position(x, y);
    }

    public static bool operator ==(Position obj1, Position obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true; // Both references point to the same object
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
        string sText = Description();
        if (_prefix != "") sText = $"{_prefix} | {sText}";
        Console.WriteLine(sText);
    }

    public string Description()
    {
        return $"({x,2},{y,2})";
    }

}

class Trail
{
    public Position StartPosition;
    public List<Position> TargetPositions = new List<Position>();

    public Trail(Position _oStartPosition, List<Position> _oTargetPositionsList)
    {
        StartPosition = _oStartPosition;
        TargetPositions = _oTargetPositionsList;
    }

    public static bool operator ==(Trail obj1, Trail obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true; // Both references point to the same object
        if (obj1 is null || obj2 is null)
            return false; // One is null, and the other is not

        // Compare property values
        return obj1.StartPosition == obj2.StartPosition;
    }

    public static bool operator !=(Trail obj1, Trail obj2)
    {
        return !(obj1 == obj2);
    }

    // frågetecknet(?) efter object markerar att obj kan vara null
    public override bool Equals(object? obj)
    {
        if (obj is Trail other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(StartPosition);
    }

}