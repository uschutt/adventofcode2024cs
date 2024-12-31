using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

class Program13a
{
    static string __sFilePath = "data.txt";

    static void Main(string[] args)
    {
        int iResult;

        List<string> sDataList = ReadFileToList(__sFilePath);
        // foreach (string sLine in sDataList) print(sLine);

        DateTime dtStartTime = DateTime.Now;

        iResult = Solution(sDataList);

        DateTime dtEndTime = DateTime.Now;

        TimeSpan tsDifference = dtEndTime - dtStartTime;

        print("------------------------------------------------------------------------------------------------------------------------------------------");
        print($"| Result: {iResult} | calculated in {tsDifference.TotalSeconds} seconds");
        print("------------------------------------------------------------------------------------------------------------------------------------------");

    }

    static int Solution(List<string> _sDataList)
    {
        int iResult = 0;
        Position oInitPosition = new Position(0, 0);
        List<MachineSettings> MachineList = new List<MachineSettings>();
        List<string> sMachineInputList = new List<string>();

        foreach (string inputLine in _sDataList)
        {

            if (inputLine.Trim() == "")
            {
                MachineList.Add(new MachineSettings(sMachineInputList));
                sMachineInputList = new List<string>();
            }
            else
            {
                sMachineInputList.Add(inputLine);
            }
        }

        MachineList.Add(new MachineSettings(sMachineInputList));

        foreach (MachineSettings ms in MachineList)
        {
            iResult += CalculateCost(oInitPosition, ms.TargetPosition, ms.ButtonAMove, ms.ButtonBMove);
        }

        return iResult;
    }

    static int CalculateCost(Position _oInitPosition, Position _oiTargetPosition, Position _oAMove, Position _oBMove)
    {
        Position oPosition;

        int A, B;
        int iMinimumCost = 0;
        ClawMoves newClawMoves;
        List<ClawMoves> winningClawMovesList = new List<ClawMoves>();

        A = 0;
        for (Position pA = _oInitPosition; pA < _oiTargetPosition; pA += _oAMove)
        {
            if (A > 100) break;
            B = 0;
            for (Position pB = _oInitPosition; pB < _oiTargetPosition; pB += _oBMove)
            {
                if (B > 100) break;
                oPosition = pA + pB;
                if (oPosition == _oiTargetPosition)
                {
                    newClawMoves = new ClawMoves(3, 1, A, B, oPosition);
                    winningClawMovesList.Add(newClawMoves);
                }
                if (pB > _oiTargetPosition) break;
                B++;
            }
            if (pA > _oiTargetPosition) break;
            A++;
        }

        foreach (ClawMoves cm in winningClawMovesList)
        {
            if (iMinimumCost == 0 || cm.Cost < iMinimumCost) iMinimumCost = cm.Cost;
        }

        return iMinimumCost;

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

class ClawMoves
{
    private int __ButtonCostA;
    private int __ButtonCostB;
    private int __ButtonClickCountA;
    private int __ButtonClickCountB;
    private Position __TargetPosition;
    private Position __InitPosition;

    public ClawMoves(int _iButtonCostA, int _iButtonCostB, int _iButtonClickCountA, int _iButtonClickCountB, Position _oTargetPosition)
    {
        __ButtonCostA = _iButtonCostA;
        __ButtonCostB = _iButtonCostB;
        __ButtonClickCountA = _iButtonClickCountA;
        __ButtonClickCountB = _iButtonClickCountB;
        __InitPosition = new Position(0, 0);
        __TargetPosition = _oTargetPosition;
    }

    public int Cost
    {
        get { return (__ButtonClickCountA * __ButtonCostA) + (__ButtonClickCountB * __ButtonCostB); }
    }
}

class MachineSettings
{
    private Position __ButtonAMove;
    private Position __ButtonBMove;
    private Position __TargetPosition;

    public Position ButtonAMove
    {
        get { return __ButtonAMove; }
    }

    public Position ButtonBMove
    {
        get { return __ButtonBMove; }
    }

    public Position TargetPosition
    {
        get { return __TargetPosition; }
    }

    public MachineSettings(List<string> __sMachineSettingsList)
    {
        __ButtonAMove = StringToPosition(__sMachineSettingsList[0]);
        __ButtonBMove = StringToPosition(__sMachineSettingsList[1]);
        __TargetPosition = StringToPosition(__sMachineSettingsList[2]);
    }

    private Position StringToPosition(string _sInput)
    {
        string sInput = _sInput.Replace('+', '=');
        string[] sCoordinatesArray = sInput.Split(':')[1].Split(',');
        string sX = sCoordinatesArray[0].Replace("X=", "").Trim();
        string sY = sCoordinatesArray[1].Replace("Y=", "").Trim();
        int iX = int.Parse(sX);
        int iY = int.Parse(sY);
        return new Position(iX, iY);
    }

}