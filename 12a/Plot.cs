class Plot
{
    public char PlotType;
    public int Perimiter;
    public int Area;
    public List<Position> PositionList;
    public Position StartPosition;

    public Plot(char _cPlotType, int _iPerimiter = 0, int _iArea = 1, Position _oStartPosition)
    {
        PlotType = _cPlotType;
        Area = _iArea;
        Perimiter = _iPerimiter;
        StartPosition = _oStartPosition;
        PositionList.Add(_oStartPosition);

    }

    public static Plot operator +(Plot obj1, Plot obj2)
    {
        int iPerimiter = obj1.Perimiter + obj2.Perimiter;
        int iArea = obj1.Area + obj2.Area;
        return new Plot(obj1.PlotType, iPerimiter, iArea);
    }

    public static bool operator ==(Plot obj1, Plot obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true; // Both references point to the same object
        if (obj1 is null || obj2 is null)
            return false; // One is null, and the other is not

        // Compare property values
        return obj1.PlotType == obj2.PlotType;
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

    public override string ToString() => $"Type: {PlotType} | Area: {Area,3} | Perimiter: {Perimiter,3}";

    public string Description()
    {
        return $"Type: {PlotType} | Area: {Area} | Perimiter: {Perimiter}";
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

    public void AddPlot(char _PlotType, int _iPerimiter = 0, int _iArea = 1)
    {
        Plot oNewPlot = new Plot(_PlotType, _iPerimiter, _iArea);
        if (!__Plots.Contains(oNewPlot))
        {
            Console.WriteLine($"New Plot:     {oNewPlot.ToString()}");
            __Plots.Add(oNewPlot);
        }
        else
        {
            Plot FoundPlot = __Plots.Find(p => p.PlotType == _PlotType);
            FoundPlot.Area += oNewPlot.Area;
            FoundPlot.Perimiter += oNewPlot.Perimiter;
            Console.WriteLine($"Updated Plot: {FoundPlot.ToString()}");

        }

    }



}