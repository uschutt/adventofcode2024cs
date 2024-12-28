using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading;

class Region
{
    private char __Type;
    private Plots __Plots;
    private Plot __StartPosition;
    private int __TotalArea;
    private int __TotalPerimiter;
    private List<Side> __Sides;

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

    public int PriceWithDiscount
    {
        get { return __TotalArea * __Sides.Count; }
    }

    public List<Side> Sides
    {
        get { return __Sides; }
    }

    public Region(char _Type, Plot _StartPosition)
    {
        __Type = _Type;
        __StartPosition = _StartPosition;
        __Plots = new Plots();
        __Plots.AddPlot(_StartPosition);
        __TotalArea += _StartPosition.Area;
        __TotalPerimiter += _StartPosition.PerimiterCount;
        __Sides = new List<Side>();
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
            __TotalPerimiter += _p.PerimiterCount;
            return true;
        }
        return false;

    }



    public void CreateSides()
    {
        // for each plot in the region
        foreach (Plot p in __Plots.Items)
        {
            // only Plots with at least one perimiter can be part of a side
            if (p.PerimiterCount > 0)
            {
                foreach (Side side in GetSides(p))
                {
                    side.Plots.AddPlot(p);
                }
            }
        }
    }

    private List<Side> GetSides(Plot _p)
    {
        List<Side> FoundSides = new List<Side>();
        foreach (Side side in __Sides)
        {
            foreach (Plot p in side.Plots.Items)
            {
                if (AdjectantPlots(_p, p, side.Direction))
                {
                    FoundSides.Add(side);
                    break; // a plot can only be added once to each side, so why keep looking
                }
            }
        }

        // if the _p does not fit in any existing side, 
        // a new side for each of the plots perimiter is created
        if (FoundSides.Count < _p.PerimiterCount)
        {
            foreach (Position perimiter in _p.Perimiters)
            {
                Side newSide = new Side(perimiter, _p);
                if (!FoundSides.Contains(newSide))
                {
                    __Sides.Add(newSide);
                    FoundSides.Add(newSide);
                }

            }
        }

        return FoundSides;
    }

    private bool AdjectantPlots(Plot _p1, Plot _p2, Position _Direction)
    {

        if (_p1.PerimiterCount == 0) return false;
        if (_p2.PerimiterCount == 0) return false;

        if (_p1.Perimiters.Contains(_Direction) && _p2.Perimiters.Contains(_Direction))
        {
            if (_p1.Position.x == _p2.Position.x && (_p1.Position.y == _p2.Position.y - 1 || _p1.Position.y == _p2.Position.y + 1)) return true;
            if (_p1.Position.y == _p2.Position.y && (_p1.Position.x == _p2.Position.x - 1 || _p1.Position.x == _p2.Position.x + 1)) return true;
        }

        return false;
    }

    public string Description()
    {
        string sReturnValue = "";

        sReturnValue += $"Region type: {Type} ";
        sReturnValue += $"| Start position: {__StartPosition.Position.Description()} ";
        sReturnValue += $"| Area: {__TotalArea,4} | Perimiter: {__TotalPerimiter,3} ";
        sReturnValue += $"| Sides: {__Sides.Count,3}";
        sReturnValue += $"| Price: {Price,5}";
        sReturnValue += $"| Price with discount: {PriceWithDiscount,5}";

        return sReturnValue;
    }

    public string Export(bool _bHeader = false)
    {
        string sReturnValue = "";

        if (_bHeader)
        {
            sReturnValue += "RegionType;StartPositionX;StartPositionY;Area;PerimiterCount;SideCount;Price1;Price2";
        }
        else
        {
            sReturnValue += $"{Type};{__StartPosition.Position.x};{__StartPosition.Position.y};{__TotalArea};{__TotalPerimiter};{__Sides.Count};{Price};{PriceWithDiscount}";
        }

        return sReturnValue;
    }
}

class Regions
{
    private List<Region> __RegionList;
    private Map __Map;
    private int __TotalPrice;
    private int __TotalPriceWithDiscount;
    private int __TotalArea;
    private Position[] __Directions;

    public Regions(Map _Map)
    {
        __RegionList = new List<Region>();
        __Map = _Map;
        __Directions = GetDirections();
        __TotalPrice = 0;
        __TotalPriceWithDiscount = 0;
        __TotalArea = 0;
    }

    public int TotalPrice
    {
        get { return __TotalPrice; }
    }

    public int TotalPriceWithDiscount
    {
        get { return __TotalPriceWithDiscount; }
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
        _StartPosition.PerimiterCount = PerimiterCount(_StartPosition);
        Region oNewRegion = new Region(_Type, _StartPosition);

        GetPositions(_Type, _StartPosition, oNewRegion);
        oNewRegion.CreateSides();
        __RegionList.Add(oNewRegion);
        __TotalPrice += oNewRegion.Price;
        __TotalPriceWithDiscount += oNewRegion.PriceWithDiscount;
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
                p.PerimiterCount = PerimiterCount(p);
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
            if (__Map.TypeByPosition(NextPosition) == _p.PlotType)
            {
                iAdjectantCount++;
            }
            else if (!_p.Perimiters.Contains(Direction))
            {
                _p.Perimiters.Add(Direction);
            }
        }
        return 4 - iAdjectantCount;
    }

    static Position[] GetDirections()
    {
        Position pUp = new Position(-1, 0, "up");
        Position pDown = new Position(1, 0, "down");
        Position pLeft = new Position(0, -1, "left");
        Position pRight = new Position(0, 1, "right");
        return [pUp, pDown, pLeft, pRight];
    }

    public string Description()
    {
        string sReturnValue = "";
        sReturnValue += $"Total number of regions: {__RegionList.Count} | Total area: {__TotalArea} | Total price: {__TotalPrice}";
        sReturnValue += $" | Total price with discount: {__TotalPriceWithDiscount}";
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

    public List<string> Export()
    {
        List<string> ReturnList = new List<string>();

        string sHeader = "x\\y;";
        for (int y = 0; y <= yMax; y++)
        {
            sHeader += $"{y};";
        }

        ReturnList.Add(sHeader);

        int iX = 0;
        foreach (string x in __map)
        {
            string sLine = string.Join(";", x.ToArray());
            sLine = $"{iX};" + sLine;
            ReturnList.Add(sLine);
            iX++;
        }

        return ReturnList;
    }

}

class Side
{
    private Position __Direction;
    private Plots __Plots;

    #region Properties
    public Position Direction
    {
        get { return __Direction; }
    }

    public Plots Plots
    {
        get { return __Plots; }
    }

    #endregion

    #region Constructors

    public Side(Position _Direction)
    {
        __Direction = _Direction;
        __Plots = new Plots();
    }

    public Side(Position _Direction, Plot _Plot)
    {
        __Direction = _Direction;
        __Plots = new Plots();
        AddPlot(_Plot);
    }

    #endregion

    #region Operators

    public static bool operator ==(Side obj1, Side obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true; // Both references point to the same object
        if (obj1 is null || obj2 is null)
            return false; // One is null, and the other is not

        // Compare property values
        return obj1.Direction == obj2.Direction;
    }

    public static bool operator !=(Side obj1, Side obj2)
    {
        return !(obj1 == obj2);
    }

    // frågetecknet(?) efter object markerar att obj kan vara null
    public override bool Equals(object? obj)
    {
        if (obj is Side other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Direction);
    }

    #endregion

    public void AddPlot(Plot _Plot)
    {
        __Plots.AddPlot(_Plot);
    }

    public string Description(string _sPrefix = "")
    {
        string sReturnValue = "";
        if (_sPrefix != "") sReturnValue += _sPrefix + " | ";
        sReturnValue += $"Direction: {__Direction.Description()} | PlotCount: {__Plots.Items.Count} ";
        return sReturnValue;
    }
}