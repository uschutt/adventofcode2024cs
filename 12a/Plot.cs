class Plot
{
    public char PlotType;
    public int Perimiter;
    public int Area;
    public Position Position;

    public Plot(char _cPlotType, Position _oPosition, int _iPerimiter = 0, int _iArea = 1)
    {
        PlotType = _cPlotType;
        Area = _iArea;
        Perimiter = _iPerimiter;
        Position = _oPosition;
    }

    public static bool operator ==(Plot obj1, Plot obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true; // Both references point to the same object
        if (obj1 is null || obj2 is null)
            return false; // One is null, and the other is not

        // Compare property values
        return obj1.PlotType == obj2.PlotType && obj1.Position == obj2.Position;
    }

    public static bool operator !=(Plot obj1, Plot obj2)
    {
        return !(obj1 == obj2);
    }

    // frågetecknet(?) efter object markerar att obj kan vara null
    public override bool Equals(object? obj)
    {
        if (obj is Plot other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PlotType);
    }

    public override string ToString() => Description();

    public string Description()
    {
        return $"Type: {PlotType} | Area: {Area,3} | Perimiter: {Perimiter,3} | Position: {Position.ToString()}";
    }

}

class Plots
{
    private List<Plot> __Plots;

    public Plots()
    {
        __Plots = new List<Plot>();
    }

    public List<Plot> Items
    {
        get { return __Plots; }
    }

    public bool AddPlot(char _PlotType, Position _oPosition, int _iPerimiter = 0, int _iArea = 1)
    {
        Plot oNewPlot = new Plot(_PlotType, _oPosition, _iPerimiter, _iArea);
        return AddPlot(oNewPlot);
    }

    public bool AddPlot(Plot _p)
    {
        if (!__Plots.Contains(_p))
        {
            // Console.WriteLine($"New Plot:     {oNewPlot.ToString()}");
            __Plots.Add(_p);
            return true;
        }
        else
        {
            return false;
        }
    }

}