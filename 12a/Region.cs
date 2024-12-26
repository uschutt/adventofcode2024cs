using System.Threading;

class Region
{
    public char __Type;
    private List<Position> __Plots = new List<Position>();
    private Position __StartPosition;


    public char Type
    {
        get { return __Type; }
    }

    public Position StartPosition
    {
        get { return __StartPosition; }
    }

    public Region(char _Type, Position _StartPosition)
    {
        __Type = _Type;
        __StartPosition = _StartPosition;
        AddPlot(_StartPosition);
    }

    public static bool operator ==(Region obj1, Region obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true; // Both references point to the same object
        if (obj1 is null || obj2 is null)
            return false; // One is null, and the other is not

        // Compare property values
        return obj1.Type == obj2.Type && obj1.StartPosition == obj2.StartPosition;
    }

    public static bool operator !=(Region obj1, Region obj2)
    {
        return !(obj1 == obj2);
    }

    // frågetecknet(?) efter object markerar att obj kan vara null
    public override bool Equals(object? obj)
    {
        if (obj is Region other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(__Type, __StartPosition.x, __StartPosition.y);
    }

    public bool AddPlot(Position _p)
    {
        if (__Plots.Contains(_p)) return false;
        __Plots.Add(_p);
        return true;
    }

    public string GetDescription()
    {
        string sReturnValue = "";

        sReturnValue = $"Type: {Type} | Start position: {__StartPosition.Description()}";

        return sReturnValue;
    }

}

class Regions
{
    List<Region> __RegionList;
    Map __Map;

    public Regions(Map _Map)
    {
        __RegionList = new List<Region>();
        __Map = _Map;
    }

    public Region TypeExists(Char _Type)
    {
        foreach (Region r in __RegionList)
        {
            if (r.Type == _Type) return r;
        }
        return null;
    }

    public void AddRegion(char _Type, Position _StartPosition)
    {
        Region oNewRegion = new Region(_Type, _StartPosition);
        _StartPosition.print($"New {_Type} region starting at ");
        GetPositions(_Type, _StartPosition, oNewRegion);
        __RegionList.Add(oNewRegion);
    }

    public void GetPositions(char _Type, Position _StartPosition, Region _Region)
    {
        Position[] Directions = GetDirections();
        Position oCurrentPosition = _StartPosition;

        Console.WriteLine($"GetPositions  {_StartPosition.Description()}");
        Thread.Sleep(100);

        foreach (Position oDirection in Directions)
        {
            // oCurrentPosition = oCurrentPosition + oDirection;
            while (__Map.ValidPosition(oCurrentPosition))
            {
                if (__Map.TypeByPosition(oCurrentPosition) == _Type)
                {
                    if (_Region.AddPlot(oCurrentPosition)) oCurrentPosition.print($"Type {_Type} | added: ");

                    oCurrentPosition = oCurrentPosition + oDirection;
                }

                oCurrentPosition = oCurrentPosition + oDirection;
                if (!__Map.ValidPosition(oCurrentPosition)) break;
                GetPositions(_Type, oCurrentPosition, _Region);

            }

            oCurrentPosition = _StartPosition;


        }
    }

    static Position[] GetDirections()
    {
        Position pUp = new Position(0, -1);
        Position pDown = new Position(0, 1);
        Position pLeft = new Position(-1, 0);
        Position pRight = new Position(1, 0);
        return [pUp, pDown, pLeft, pRight];
    }

}



class Map
{
    private List<string> __map = new List<string>();

    public Map(List<string> _map)
    {
        __map = _map;
    }

    public int xMax
    {
        // horisontal / col - the length of the first string
        get { return __map[0].Length - 1; }
    }

    public int yMax
    {
        // vertical / row - the count of the objects i the list
        get { return __map.Count - 1; }
    }

    public char TypeByXY(int x, int y)
    {
        return __map[x][y];
    }

    public char TypeByPosition(Position _oPosition)
    {
        return __map[_oPosition.x][_oPosition.y];
    }

    public bool ValidPosition(Position _p)
    {
        return (_p.x >= 0 && _p.y >= 0 && _p.x <= xMax && _p.y <= yMax);
    }

}