using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

class Program13b
{
    static string __sFilePath = "testdata.txt";

    static void Main(string[] args)
    {
        ulong iResult;

        List<string> sDataList = ReadFileToList(__sFilePath);
        // foreach (string sLine in sDataList) print(sLine);

        DateTime dtStartTime = DateTime.Now;

        print($"StartTime: {dtStartTime}");

        iResult = Solution(sDataList);

        DateTime dtEndTime = DateTime.Now;

        TimeSpan tsDifference = dtEndTime - dtStartTime;

        print("------------------------------------------------------------------------------------------------------------------------------------------");
        print($"| Result: {iResult} | calculated in {tsDifference.TotalSeconds} seconds");
        print("------------------------------------------------------------------------------------------------------------------------------------------");

    }

    static ulong Solution(List<string> _sDataList)
    {
        ulong iResult = 0;
        Position oInitPosition = new Position(0, 0);
        List<MachineSettings> MachineList = new List<MachineSettings>();
        List<string> sMachineInputList = new List<string>();
        int iMachineNo = 1;

        test();
        return iResult;

        foreach (string inputLine in _sDataList)
        {

            if (inputLine.Trim() == "")
            {
                // MachineList.Add(new MachineSettings(sMachineInputList, new Position(10000000000000, 10000000000000)));
                MachineList.Add(new MachineSettings(sMachineInputList, new Position(0, 0)));
                sMachineInputList = new List<string>();
            }
            else
            {
                sMachineInputList.Add(inputLine);
            }
        }

        // MachineList.Add(new MachineSettings(sMachineInputList, new Position(10000000000000, 10000000000000)));
        MachineList.Add(new MachineSettings(sMachineInputList, new Position(0, 0)));

        foreach (MachineSettings ms in MachineList)
        {
            print($"MachineNo: {iMachineNo,2} - Start");
            iResult += CalculateCost(oInitPosition, ms.TargetPosition, ms.ButtonAMove, ms.ButtonBMove, 0);
            print($"MachineNo: {iMachineNo,2} - Acc Result: {iResult}");
            iMachineNo++;
        }

        return iResult;
    }

    static ulong CalculateCost(Position _oInitPosition, Position _oiTargetPosition, Position _oAMove, Position _oBMove, ulong _iClickLimit = 0)
    {
        Position oPosition;

        ulong A, B;
        ulong iMinimumCost = 0;
        ClawMoves newClawMoves;
        List<ClawMoves> winningClawMovesList = new List<ClawMoves>();

        A = 0;
        for (Position pA = _oInitPosition; pA < _oiTargetPosition; pA += _oAMove)
        {



            if (A > _iClickLimit && _iClickLimit != 0) break;
            B = 0;
            for (Position pB = _oInitPosition; pB < _oiTargetPosition; pB += _oBMove)
            {
                if (B % 100000000 == 0) print($"A: {A,8} | B: {B,20} | {_iClickLimit - B,20} ", true);
                if (B > _iClickLimit && _iClickLimit != 0) break;
                oPosition = pA + pB;
                if (oPosition == _oiTargetPosition)
                {
                    newClawMoves = new ClawMoves(3, 1, A, B, oPosition);
                    winningClawMovesList.Add(newClawMoves);
                    break;

                }
                if (pB > _oiTargetPosition) break;
                B++;
            }
            if (pA > _oiTargetPosition) break;
            A++;
            if (winningClawMovesList.Count > 0) break;
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

    static void print(string sText, bool bPrintTime = false)
    {

        if (bPrintTime) sText = $"{DateTime.Now} | {sText}";

        Console.WriteLine(sText);

    }


    static void test()
    {
        // Position oAMove = new Position(26, 66);
        // Position oBMove = new Position(67, 21);
        // Position oOffsetPosition = new Position(10000000012748, 10000000012176);

        Position oAMove = new Position(94, 34);
        Position oBMove = new Position(22, 67);
        Position oOffsetPosition = new Position(8400, 5400);

        // ulong iAXOffsetCount = oOffsetPosition.x / oAMove.x;
        // ulong iAYOffsetCount = oOffsetPosition.y / oAMove.y;

        // print($"{iAXOffsetCount}, {iAYOffsetCount}");

        ulong iBXOffsetCount = oOffsetPosition.x / oBMove.x;
        ulong iBYOffsetCount = oOffsetPosition.y / oBMove.y;

        print($"B Max: {iBXOffsetCount}, {iBYOffsetCount}");

        // ulong aMinClicks = Math.Min(iAXOffsetCount, iAYOffsetCount);
        // print($"A min: {aMinClicks}");

        // ulong bMinClicks = Math.Min(iBXOffsetCount, iBYOffsetCount);
        // print($"B min: {bMinClicks}");

        // ulong iLimit = Math.Max(aMinClicks, bMinClicks);
        // print($"ClickLimit: {iLimit}");

        // ulong iResult = CalculateCost(new Position(0, 0), oOffsetPosition, oAMove, oBMove, iLimit);


        ulong iResult = 0;

        // how far would I reach with only B button clicks
        Position currentPosition = new Position(oAMove.x * iBXOffsetCount, oAMove.y * iBYOffsetCount);
        Position checkNextPosition = currentPosition + oBMove;
        print(currentPosition.Description());
        print(checkNextPosition.Description());

        //   add B button clicks as long as x is not passed and y is not passed

        // then add A button clicks until Position passes target x and y

        // now, while currentPosition != targetPosition
        //   subtract one B click and add one A click
        //   if the sum of B cklicks is < 0 then the target is unreachable.





        print($"Result: {iResult}");

    }

}

class ClawMoves
{
    private int __ButtonCostA;
    private int __ButtonCostB;
    private ulong __ButtonClickCountA;
    private ulong __ButtonClickCountB;
    private Position __TargetPosition;
    private Position __InitPosition;

    public ClawMoves(int _iButtonCostA, int _iButtonCostB, ulong _iButtonClickCountA, ulong _iButtonClickCountB, Position _oTargetPosition)
    {
        __ButtonCostA = _iButtonCostA;
        __ButtonCostB = _iButtonCostB;
        __ButtonClickCountA = _iButtonClickCountA;
        __ButtonClickCountB = _iButtonClickCountB;
        __InitPosition = new Position(0, 0);
        __TargetPosition = _oTargetPosition;
    }

    public ulong Cost
    {
        get { return (__ButtonClickCountA * (ulong)__ButtonCostA) + (__ButtonClickCountB * (ulong)__ButtonCostB); }
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


    public MachineSettings(List<string> __sMachineSettingsList, Position _oInitPosition)
    {
        __ButtonAMove = StringToPosition(__sMachineSettingsList[0]);
        __ButtonBMove = StringToPosition(__sMachineSettingsList[1]);
        __TargetPosition = StringToPosition(__sMachineSettingsList[2]) + _oInitPosition;
    }

    private Position StringToPosition(string _sInput)
    {
        string sInput = _sInput.Replace('+', '=');
        string[] sCoordinatesArray = sInput.Split(':')[1].Split(',');
        string sX = sCoordinatesArray[0].Replace("X=", "").Trim();
        string sY = sCoordinatesArray[1].Replace("Y=", "").Trim();
        ulong iX = ulong.Parse(sX);
        ulong iY = ulong.Parse(sY);
        return new Position(iX, iY);
    }

}