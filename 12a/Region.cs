using System.Diagnostics;
using System.Threading;

class Region
{
    public char __Type;
    private Plots __Plots;
    private Plot __StartPosition;
    private int __TotalArea;
    private int __TotalPerimiter;

    public char Type
    {
        get { return __Type; }
    }

    public Plot StartPosition
    {
        get { return __StartPosition; }
    }

    public Plots Plots
    {
        get { return __Plots; }
    }

    public int TotalArea
    {
        get { return __TotalArea; }
    }

    public int TotalPerimiter
    {
        get { return __TotalPerimiter; }
    }

    public int Price
    {
        get { return __TotalArea * __TotalPerimiter; }
    }

    public Region(char _Type, Plot _StartPosition)
    {
        __Type = _Type;
        __StartPosition = _StartPosition;
        __Plots = new Plots();
        __Plots.AddPlot(_StartPosition);
        __TotalArea += _StartPosition.Area;
        __TotalPerimiter += _StartPosition.Perimiter;
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
        return HashCode.Combine(__Type, __StartPosition.Position.x, __StartPosition.Position.y);
    }

    public bool AddPlot(Plot _p)
    {
        if (__Plots.AddPlot(_p))
        {
            __TotalArea += _p.Area;
            __TotalPerimiter += _p.Perimiter;
            return true;
        }
        return false;

    }

    public string Description()
    {
        string sReturnValue = "";

        sReturnValue = $"Region type: {Type} ";
        sReturnValue += $"| Start position: {__StartPosition.Position.Description()} ";
        sReturnValue += $"| Area: {__TotalArea,4} | Perimiter: {__TotalPerimiter,3} ";
        sReturnValue += $"| Price: {Price,5}";

        return sReturnValue;
    }

}

class Regions
{
    private List<Region> __RegionList;
    private Map __Map;
    private int __TotalPrice;
    private int __TotalArea;
    private Position[] __Directions;

    public Regions(Map _Map)
    {
        __RegionList = new List<Region>();
        __Map = _Map;
        __Directions = GetDirections();
        __TotalPrice = 0;
        __TotalArea = 0;
    }

    public int TotalPrice
    {
        get { return __TotalPrice; }
    }

    public int TotalArea
    {
        get { return __TotalArea; }
    }

    public List<Region> Items
    {
        get { return __RegionList; }
    }

    public bool RegionExists(char _Type, Plot _StartPosition)
    {

        foreach (Region r in __RegionList.FindAll(p => p.Type == _Type))
        {
            int index = r.Plots.Items.FindIndex(p => p.Position == _StartPosition.Position);
            if (index != -1) return true;
        }

        return false;

    }

    public Region AddRegion(char _Type, Plot _StartPosition)
    {
        _StartPosition.Perimiter = PerimiterCount(_StartPosition);
        Region oNewRegion = new Region(_Type, _StartPosition);

        GetPositions(_Type, _StartPosition, oNewRegion);
        __RegionList.Add(oNewRegion);
        __TotalPrice += oNewRegion.Price;
        __TotalArea += oNewRegion.TotalArea;

        return oNewRegion;

    }

    public void GetPositions(char _Type, Plot _StartPosition, Region _Region)
    {

        Plots NewPlots = GetAdjectantPlots(_Region, _StartPosition);
        Plots TempPlots = new Plots();

        while (NewPlots.Items.Count > 0)
        {
            foreach (Plot p in NewPlots.Items)
            {
                p.Perimiter = PerimiterCount(p);
                if (_Region.AddPlot(p))
                {
                    TempPlots.AddPlot(p);
                }
            }

            NewPlots.Items.Clear();

            foreach (Plot p in TempPlots.Items)
            {

                Plots adjectants = GetAdjectantPlots(_Region, p);

                foreach (Plot a in adjectants.Items)
                {
                    NewPlots.AddPlot(a);
                }

            }

            TempPlots.Items.Clear();

        }

    }

    private Plots GetAdjectantPlots(Region _Region, Plot _oPlot)
    {
        Plots NewPlots = new Plots();

        foreach (Position Direction in __Directions)
        {
            Position NextPosition = _oPlot.Position + Direction;
            if (__Map.TypeByPosition(NextPosition) == _oPlot.PlotType)
            {
                Plot NextPlot = new Plot(_oPlot.PlotType, NextPosition);
                if (!_Region.Plots.Items.Contains(NextPlot))
                {
                    NewPlots.AddPlot(NextPlot);
                }
            }
        }

        return NewPlots;
    }

    private int PerimiterCount(Plot _p)
    {
        int iAdjectantCount = 0;
        foreach (Position Direction in __Directions)
        {
            Position NextPosition = _p.Position + Direction;
            if (__Map.TypeByPosition(NextPosition) == _p.PlotType) iAdjectantCount++;
        }
        return 4 - iAdjectantCount;
    }

    static Position[] GetDirections()
    {
        Position pUp = new Position(0, -1);
        Position pDown = new Position(0, 1);
        Position pLeft = new Position(-1, 0);
        Position pRight = new Position(1, 0);
        return [pUp, pDown, pLeft, pRight];
    }

    public string Description()
    {
        string sReturnValue;
        sReturnValue = $"Total number of regions: {__RegionList.Count} | Total area: {__TotalArea} | Total price: {__TotalPrice}";
        return sReturnValue;
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
        return TypeByPosition(new Position(x, y));
    }

    public char TypeByPosition(Position _oPosition)
    {
        if (ValidPosition(_oPosition))
        {
            return __map[_oPosition.x][_oPosition.y];
        }
        return '-';
    }

    public bool ValidPosition(Position _p)
    {
        return _p.x >= 0 && _p.y >= 0 && _p.x <= xMax && _p.y <= yMax;
    }

    public string Description()
    {
        string sReturnValue = $"Map dimensions: {xMax + 1} x {yMax + 1} | Area: {(xMax + 1) * (yMax + 1)} | Max index (x,y): ({xMax},{yMax}) ";
        return sReturnValue;
    }

}