class Plot
{
    public char PlotType;
    public int Perimiter;
    public int Area;
    public List<Position> PositionList;
    public Position StartPosition;

    public Plot(char _cPlotType, Position _oStartPosition, int _iPerimiter = 0, int _iArea = 1)
    {
        PlotType = _cPlotType;
        Area = _iArea;
        Perimiter = _iPerimiter;
        StartPosition = _oStartPosition;
        PositionList = new List<Position>();
        PositionList.Add(_oStartPosition);

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

    public override string ToString() => $"Type: {PlotType} | Area: {Area,3} | Perimiter: {Perimiter,3} | Start position: {StartPosition.ToString()}";

    // public string Description()
    // {
    //     return $"Type: {PlotType} | Area: {Area} | Perimiter: {Perimiter}";
    // }

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

    public void AddPlot(char _PlotType, Position _oPosition, int _iPerimiter = 0, int _iArea = 1)
    {
        Plot oNewPlot = new Plot(_PlotType, _oPosition, _iPerimiter, _iArea);

        if (!__Plots.Contains(oNewPlot))
        {
            Console.WriteLine($"New Plot:     {oNewPlot.ToString()}");
            __Plots.Add(oNewPlot);
        }
        else
        {
            Plot FoundPlot = __Plots.Find(p => p.PlotType == _PlotType);

            // does the position in _oPosition have a path to FoundPlot.StartPosition
            // PositionsIsInSameRegion(_oPosition, FoundPlot.StartPosition, _PlotType)
            // if not add new with oPosition as startPosition

            FoundPlot.Area += oNewPlot.Area;
            FoundPlot.Perimiter += oNewPlot.Perimiter;
            FoundPlot.PositionList.Add(_oPosition);
            Console.WriteLine($"Updated Plot: {FoundPlot.ToString()}");

        }

    }



}