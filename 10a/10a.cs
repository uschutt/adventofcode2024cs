using System;
using System.Linq;

class Program10a
{
    static string FilePath = "data.txt";
    static List<int[]> MapList = new List<int[]>();
    static int MapXMaxIndex = 0;
    static int MapYMaxIndex = 0;
    static Position[] Directions = [];
    static List<Trail> Results = new List<Trail>();

    static void Main(string[] args)
    {

        string sLine = "";

        List<string> sDataList = ReadFileToList(FilePath);
        MapList = CreateMapList(sDataList);

        MapXMaxIndex = MapList[0].Length - 1;
        MapYMaxIndex = MapList.Count - 1;
        Directions = GetDirections();
        List<Position> oTrailHeadsPositionList = GetTrailHeads(0);
        Trail oNewTrail;
        int iScore = 0;

        foreach (string s in sDataList) print(s);

        foreach (int[] iLineArray in MapList)
        {
            sLine = string.Join(", ", iLineArray);
            print(sLine);
        }

        print($"X max index: {MapXMaxIndex} | Y max index: {MapYMaxIndex} | Trailheads: {oTrailHeadsPositionList.Count} ");

        foreach (Position oTrailHeadPosition in oTrailHeadsPositionList)
        {
            oNewTrail = new Trail(oTrailHeadPosition, new List<Position>());
            Results.Add(oNewTrail);
            oTrailHeadPosition.print();
            GetNextPosition(Results.Count - 1, oTrailHeadPosition, oTrailHeadPosition);
            print($"{oTrailHeadPosition.Description()}  | Score: {oNewTrail.TargetPositions.Count} -----------------------------------|");
        }

        // Position p = new Position(0, 6);
        // GetNextPosition(p, 0);

        foreach (Trail t in Results)
        {
            print($"TrailHeadPosition: {t.StartPosition.Description()} | TargetCount: {t.TargetPositions.Count}");
            iScore += t.TargetPositions.Count;
        }

        print($"Score: {iScore}");

    }

    static Position[] GetDirections()
    {
        Position pUp = new Position(0, -1);
        Position pDown = new Position(0, 1);
        Position pLeft = new Position(-1, 0);
        Position pRight = new Position(1, 0);
        return [pUp, pDown, pLeft, pRight];
    }

    static bool ValidPosition(Position _oPosition, bool _bDebug = false)
    {
        bool bReturnValue = false;

        bReturnValue = (_oPosition.x >= 0 && _oPosition.x <= MapXMaxIndex && _oPosition.y >= 0 && _oPosition.y <= MapYMaxIndex);

        if (_bDebug && !bReturnValue) print($"Invalid position: {_oPosition.Description()}");

        return bReturnValue;
    }

    static int GetHight(Position _oPosition)
    {
        int iReturnValue = -1;

        if (ValidPosition(_oPosition)) iReturnValue = MapList[_oPosition.y][_oPosition.x];

        return iReturnValue;
    }

    static void GetNextPosition(int _iResultIndex, Position _oStartPosition, Position _oCurrentPosition, int _iCurrentHight = 0)
    {
        Position oNextPosition;
        int iHight = -1;

        print($"Current position: {_oCurrentPosition.Description()} | Hight: {_iCurrentHight} ");
        if (_iCurrentHight == 9)
        {
            if (!Results[_iResultIndex].TargetPositions.Contains(_oCurrentPosition))
            {
                Results[_iResultIndex].TargetPositions.Add(_oCurrentPosition);
            }
        }

        foreach (Position oDirection in Directions)
        {
            oNextPosition = _oCurrentPosition + oDirection;
            iHight = GetHight(oNextPosition);
            if (ValidPosition(oNextPosition) && iHight == _iCurrentHight + 1)
            {
                GetNextPosition(_iResultIndex, _oStartPosition, oNextPosition, iHight);
            }
        }
    }

    static List<Position> GetTrailHeads(int _iTrailHead = 0)
    {
        List<Position> iListOutput = new List<Position>();

        for (int x = 0; x <= MapXMaxIndex; x++)
        {
            for (int y = 0; y <= MapYMaxIndex; y++)
            {
                //print($"({x},{y}): {MapList[y][x]}");
                if (MapList[y][x] == _iTrailHead) iListOutput.Add(new Position(x, y));
            }
        }

        return iListOutput;
    }

    static List<int[]> CreateMapList(List<string> _sDataList)
    {
        List<int[]> iMapListOutput = new List<int[]>();
        int[] iLineArray;

        foreach (string sLine in _sDataList)
        {
            iLineArray = sLine.Select(c => int.Parse(c.ToString())).ToArray();
            iMapListOutput.Add(iLineArray);
        }
        return iMapListOutput;
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