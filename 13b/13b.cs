using System;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

class Program13b
{
    static string __sFilePath = "testdata.txt";

    static void Main(string[] args)
    {
        long iResult;

        List<string> sDataList = ReadFileToList(__sFilePath);
        // foreach (string sLine in sDataList) print(sLine);

        DateTime dtStartTime = DateTime.Now;
        Console.Clear();
        print("------------------------------------------------------------------------------------------------------------------------------------------");
        print($"| 13b: | Start time: {dtStartTime}");
        print("------------------------------------------------------------------------------------------------------------------------------------------");

        iResult = Solution(sDataList);

        DateTime dtEndTime = DateTime.Now;

        TimeSpan tsDifference = dtEndTime - dtStartTime;

        print("------------------------------------------------------------------------------------------------------------------------------------------");
        print($"| Result: {iResult} | calculated in {tsDifference.TotalSeconds} seconds");
        print("------------------------------------------------------------------------------------------------------------------------------------------");

    }

    static long Solution(List<string> _sDataList)
    {
        long iResult = 0;
        Position oInitPosition = new Position(0, 0);
        List<MachineSettings> MachineList = new List<MachineSettings>();
        List<string> sMachineInputList = new List<string>();
        int iMachineNo = 1;

        test4();
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
            // print($"MachineNo: {iMachineNo,2} - Start");
            long iMachineResult = CalculateCost(oInitPosition, ms.TargetPosition, ms.ButtonAMove, ms.ButtonBMove, 0);
            iResult += iMachineResult;
            print($"MachineNo: {iMachineNo,2} | Result: {iMachineResult,4} | Acc result: {iResult,4}");
            iMachineNo++;
        }

        return iResult;
    }

    static long CalculateCost(Position _oInitPosition, Position _oiTargetPosition, Position _oAMove, Position _oBMove, long _iClickLimit = 0)
    {
        Position oPosition;

        long A, B;
        long iMinimumCost = 0;
        ClawMoves newClawMoves;
        List<ClawMoves> winningClawMovesList = new List<ClawMoves>();

        A = 0;
        for (Position pA = _oInitPosition; pA < _oiTargetPosition; pA += _oAMove)
        {
            if (A > _iClickLimit && _iClickLimit != 0) break;
            B = 0;
            for (Position pB = _oInitPosition; pB < _oiTargetPosition; pB += _oBMove)
            {
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

    static void test4()
    {
        // Position oAMove = new Position(26, 66);
        // Position oBMove = new Position(67, 21);
        // Position oTargetPosition = new Position(12748, 12176);
        // Position oTargetPosition = new Position(10000000012748, 10000000012176);

        // #1 A 80 | B 40
        Position oAMove = new Position(94, 34);
        Position oBMove = new Position(22, 67);
        Position oTargetPosition = new Position(8400, 5400);

        // #3 A 38 | B 86
        // Position oAMove = new Position(17, 86);
        // Position oBMove = new Position(84, 37);
        // Position oTargetPosition = new Position(7870, 6450);

        Position oStartPosition = new Position(0, 0);
        Position oCurrentPosition = oStartPosition;
        Position oNewPosition;

        Position Offset = new Position(10000000000000, 10000000000000);

        long clicks = oTargetPosition / oAMove;

        oNewPosition = oAMove * clicks;
        print($"Clicks: {clicks} -> {oNewPosition}");

    }

    static void test3()
    {
        // Position oAMove = new Position(26, 66);
        // Position oBMove = new Position(67, 21);
        // Position oTargetPosition = new Position(12748, 12176);
        // Position oTargetPosition = new Position(10000000012748, 10000000012176);

        Position oAMove = new Position(94, 34);
        Position oBMove = new Position(22, 67);
        Position oTargetPosition = new Position(8400, 5400);

        Position oStartPosition = new Position(0, 0);
        Position oCurrentPosition;

        long iAXOffsetCount = oTargetPosition.x / oAMove.x;
        long iAYOffsetCount = oTargetPosition.y / oAMove.y;

        long iBXOffsetCount = oTargetPosition.x / oBMove.x;
        long iBYOffsetCount = oTargetPosition.y / oBMove.y;

        long A, B;
        long Atotal = 0, Btotal = 0;
        int iItr = 0;

        print($"A Max: {iAXOffsetCount}, {iAYOffsetCount}");
        print($"B Max: {iBXOffsetCount}, {iBYOffsetCount}");

        print($"A x {iAXOffsetCount} = {(oAMove * iAXOffsetCount).Description}");
        print($"B x {iBXOffsetCount} = {(oBMove * iBXOffsetCount).Description}");


        print($"---------------------------------------------------------------------------------------- {iItr}");
        // klickar 89 ggr på A och nått (8366,3026)
        A = Math.Min(iAXOffsetCount, iAYOffsetCount);
        Atotal = A;
        oCurrentPosition = oStartPosition + oAMove * A;
        print($"{A} klick på A ger positionen: {oCurrentPosition.Description} ");

        B = Math.Min(iBXOffsetCount, iBYOffsetCount);
        Btotal = B;
        oCurrentPosition = oCurrentPosition + oBMove * B;
        print($"{B} klick på B ger positionen: {oCurrentPosition.Description} ");

        oCurrentPosition = oStartPosition;

        while (oCurrentPosition != oTargetPosition)
        {
            iItr++;
            print($"{oCurrentPosition.Description} |----------------------------------------------------------------------------------------| {iItr}");
            // iAXOffsetCount = (oTargetPosition.x - oCurrentPosition.x) / oAMove.x;
            // iAYOffsetCount = (oTargetPosition.y - oCurrentPosition.y) / oAMove.y;
            // iBXOffsetCount = (oTargetPosition.x - oCurrentPosition.x) / oBMove.x;
            // iBYOffsetCount = (oTargetPosition.y - oCurrentPosition.y) / oBMove.y;

            // print($"{iAXOffsetCount} klicks on A will reach {(oCurrentPosition + (oAMove * iAXOffsetCount)).Description}");
            // print($"{iAYOffsetCount} klicks on A will reach {(oCurrentPosition + (oAMove * iAYOffsetCount)).Description}");
            // print($"{iBXOffsetCount} klicks on B will reach {(oCurrentPosition + (oBMove * iBXOffsetCount)).Description}");
            // print($"{iBYOffsetCount} klicks on B will reach {(oCurrentPosition + (oBMove * iBYOffsetCount)).Description}");


            print($"{oCurrentPosition.Description} | Target position {oTargetPosition.Description} - Current position {oCurrentPosition.Description} = {(oTargetPosition - oCurrentPosition).Description}");


            A = GetNextClickCount(oStartPosition, oTargetPosition, oCurrentPosition, oAMove);
            Atotal += A;
            oCurrentPosition = oCurrentPosition + oAMove * A;
            print($"{oCurrentPosition.Description} | {A} klick på A ger positionen: {oCurrentPosition.Description} ");

            if (A == 0)
            {
                print($"{oCurrentPosition.Description} | Target position {oTargetPosition.Description} - Current position {oCurrentPosition.Description} = {(oTargetPosition - oCurrentPosition).Description}");

                B = GetNextClickCount(oStartPosition, oTargetPosition, oCurrentPosition, oBMove);
                Btotal += B;
                oCurrentPosition = oCurrentPosition + oBMove * B;
                print($"{oCurrentPosition.Description} | {B} klick på B ger positionen: {oCurrentPosition.Description} ");
            }


            if (A + B == 0) break;


            if (iItr > 10) break;

        }


        print($"{oCurrentPosition.Description} | A klick: {Atotal} | B klick: {Btotal}");

    }

    static long GetNextClickCount(Position _oStartPosition, Position _oTargetPosition, Position _oCurrentPosition, Position _oMove)
    {
        long iAXClickCount = (_oTargetPosition.x - _oCurrentPosition.x) / _oMove.x;
        long iAYClickCount = (_oTargetPosition.y - _oCurrentPosition.y) / _oMove.y;

        print("");
        print($"{_oCurrentPosition.Description} | ----------------------------------------- GetNextClickCount({_oStartPosition}, {_oTargetPosition}, {_oCurrentPosition}, {_oMove})");
        print($"{_oCurrentPosition.Description} | X: {iAXClickCount} = ({_oTargetPosition.x} - {_oCurrentPosition.x}) / {_oMove.x}");
        print($"{_oCurrentPosition.Description} | Y: {iAYClickCount} = ({_oTargetPosition.y} - {_oCurrentPosition.y}) / {_oMove.x}");

        Position newPosition;
        long iReturnValue = 0;

        newPosition = _oCurrentPosition + (_oMove * iAXClickCount);
        if (PositionWithinScope(_oStartPosition, _oTargetPosition, newPosition))
        {
            iReturnValue = iAXClickCount;
        }

        if (iReturnValue == 0)
        {

            newPosition = _oCurrentPosition + (_oMove * iAYClickCount);
            if (PositionWithinScope(_oStartPosition, _oTargetPosition, newPosition))
            {
                iReturnValue = iAYClickCount;
            }

        }


        print($"{_oCurrentPosition.Description} | {iReturnValue} clicks on {_oMove} = {_oMove * iReturnValue}");


        return iReturnValue;

    }




    static void test2()
    {
        // Position oAMove = new Position(26, 66);
        // Position oBMove = new Position(67, 21);
        // Position oTargetPosition = new Position(12748, 12176);
        // Position oTargetPosition = new Position(10000000012748, 10000000012176);

        // #1 A 80 | B 40
        Position oAMove = new Position(94, 34);
        Position oBMove = new Position(22, 67);
        Position oTargetPosition = new Position(8400, 5400);

        // #3 A 38 | B 86
        // Position oAMove = new Position(17, 86);
        // Position oBMove = new Position(84, 37);
        // Position oTargetPosition = new Position(7870, 6450);

        Position oStartPosition = new Position(0, 0);
        Position oCurrentPosition;
        Position oNewPosition;

        long aX = oTargetPosition.x / oAMove.x;
        long aY = oTargetPosition.y / oAMove.y;

        long bX = oTargetPosition.x / oBMove.x;
        long bY = oTargetPosition.y / oBMove.y;

        long A, B;
        long Atotal = 0, Btotal = 0;
        int iItr = 0;

        print($"A Max: ({aX}, {aY})");
        print($"B Max: ({bX}, {bY})");


        print($"---------------------------------------------------------------------------------------- {iItr}");
        A = Math.Min(aX, aY);
        Atotal = A;
        oCurrentPosition = oStartPosition + oAMove * A;
        print($"Total {Atotal,3} ({A,3}) clicks on A -> {oCurrentPosition.Description} ");


        B = Math.Min(bX, bY);
        Btotal = B;
        oCurrentPosition = oCurrentPosition + oBMove * B;
        print($"Total {Btotal,3} ({B,3}) clicks on B -> {oCurrentPosition.Description}");

        while (oCurrentPosition != oTargetPosition)
        {
            iItr++;
            print($"---------------------------------------------------------------------------------------- {iItr}");

            // B ---------------------------------------------------
            bX = (oTargetPosition.x - oCurrentPosition.x) / oBMove.x;
            bY = (oTargetPosition.y - oCurrentPosition.y) / oBMove.y;
            B = Math.Min(bX, bY);
            oNewPosition = oCurrentPosition + (B * oBMove);

            // if (B <= 0) B--;
            Btotal += B;

            print($"{oCurrentPosition} | Total {Btotal,3} ({B,3}) clicks on B -> {oNewPosition.Description}");

            oCurrentPosition = oNewPosition;

            if (oCurrentPosition == oTargetPosition) break;
            // -----------------------------------------------------


            // A ---------------------------------------------------
            aX = (oTargetPosition.x - oCurrentPosition.x) / oAMove.x;
            aY = (oTargetPosition.y - oCurrentPosition.y) / oAMove.y;
            A = Math.Min(aX, aY);
            oNewPosition = oCurrentPosition + (A * oAMove);

            // if (A <= 0) A--;
            Atotal += A;

            print($"{oCurrentPosition} | {Atotal,3} ({A,3}) clicks on A -> {oNewPosition.Description} ");

            oCurrentPosition = oNewPosition;

            if (oCurrentPosition == oTargetPosition) break;
            // -----------------------------------------------------


            if (A + B == 0) break;
            if (iItr > 20) break;

        }

        print("");
        print($"{oCurrentPosition.Description} | A click: {Atotal} | B click: {Btotal}");
        print($"{((Btotal * oBMove) + (Atotal * oAMove)).Description} | A click: {Atotal} x {oAMove.Description} = {Atotal * oAMove} | B click: {Btotal} x {oBMove} = {Btotal * oBMove} ");



    }

    static bool PositionWithinScope(Position _oStartPosition, Position _oTargetPosition, Position _p)
    {
        return (_p >= _oStartPosition && _p <= _oTargetPosition);
    }

    static void test()
    {
        // Position oAMove = new Position(26, 66);
        // Position oBMove = new Position(67, 21);
        // Position oOffsetPosition = new Position(10000000012748, 10000000012176);

        // Position oAMove = new Position(94, 34);
        Position oBMove = new Position(22, 67);
        Position oOffsetPosition = new Position(8400, 5400);

        // long iAXOffsetCount = oOffsetPosition.x / oAMove.x;
        // long iAYOffsetCount = oOffsetPosition.y / oAMove.y;

        // print($"{iAXOffsetCount}, {iAYOffsetCount}");

        long iBXOffsetCount = oOffsetPosition.x / oBMove.x;
        long iBYOffsetCount = oOffsetPosition.y / oBMove.y;

        print($"B Max: {iBXOffsetCount}, {iBYOffsetCount}");

        // long aMinClicks = Math.Min(iAXOffsetCount, iAYOffsetCount);
        // print($"A min: {aMinClicks}");

        // long bMinClicks = Math.Min(iBXOffsetCount, iBYOffsetCount);
        // print($"B min: {bMinClicks}");

        // long iLimit = Math.Max(aMinClicks, bMinClicks);
        // print($"ClickLimit: {iLimit}");

        // long iResult = CalculateCost(new Position(0, 0), oOffsetPosition, oAMove, oBMove, iLimit);


        long iResult = 0;

        // how far would I reach with only B button clicks

        long xOffset = oBMove.x * iBXOffsetCount;
        long yOffset = oBMove.y * iBYOffsetCount;

        Position currentPosition = new Position(xOffset, yOffset);
        Position checkNextPosition = currentPosition + oBMove;
        print(currentPosition.Description);
        print(checkNextPosition.Description);

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
    private long __ButtonClickCountA;
    private long __ButtonClickCountB;
    private Position __TargetPosition;
    private Position __InitPosition;

    public ClawMoves(int _iButtonCostA, int _iButtonCostB, long _iButtonClickCountA, long _iButtonClickCountB, Position _oTargetPosition)
    {
        __ButtonCostA = _iButtonCostA;
        __ButtonCostB = _iButtonCostB;
        __ButtonClickCountA = _iButtonClickCountA;
        __ButtonClickCountB = _iButtonClickCountB;
        __InitPosition = new Position(0, 0);
        __TargetPosition = _oTargetPosition;
    }

    public long Cost
    {
        get { return (__ButtonClickCountA * (long)__ButtonCostA) + (__ButtonClickCountB * (long)__ButtonCostB); }
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
        long iX = long.Parse(sX);
        long iY = long.Parse(sY);
        return new Position(iX, iY);
    }

}