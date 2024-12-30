using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

class Program13a
{
    static string __sFilePath = "testdata.txt";

    static void Main(string[] args)
    {
        int iResult;

        List<string> sDataList = ReadFileToList(__sFilePath);
        foreach (string sLine in sDataList) print(sLine);

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

        // solve the problem
        // iResult = CalculateTotalCost(new Position(1, 1), new Position(8400, 5400), 94, 34, 3, 22, 67, 1);

        iResult = CalculateCost();

        return iResult;
    }

    static int CalculateCost()
    {
        Position _oInitPosition = new Position(0, 0);
        Position _oiTargetPosition = new Position(8400, 5400);
        Position oAMove = new Position(94, 34);
        Position oBMove = new Position(22, 67);
        Position oPosition = _oInitPosition;

        int A = 0;
        int B;

        for (Position pA = _oInitPosition; pA < _oiTargetPosition; pA += oAMove)
        {
            A++;
            B = 0;
            for (Position pB = pA; pB < _oiTargetPosition; pB += oBMove)
            {
                B++;
                oPosition = pA + pB;
                print($"A: {A,3} | B: {B,3} | {oPosition.Description()}");
                if (oPosition == _oiTargetPosition)
                {
                    print("This is it!");
                    return 1;
                }
                if (oPosition > _oiTargetPosition) break;

            }
            if (oPosition > _oiTargetPosition) break;

        }

        return -1;

        // Position p = GetPosition(new Position(1, 1), 1, 1, 94, 34);

    }


    static Position GetPosition(Position _oInitPosition, int _iXCount, int _iYCount, int _iXValue, int _iYValue)
    {

        Position oMove = new Position(_iXCount * _iXValue, _iYCount * _iYValue);
        return _oInitPosition += oMove;

    }


    // alla kombinationer av antal tryck på a från 1 till 89

    // static int CalculateTotalCost(Position _oInitPosition, Position _oiTargetPosition, int _iAXValue, int _iAYValue, int _iACost, int _iBXValue, int _iBYValue, int _iBCost)
    // {

    //     int x = _oInitPosition.x;
    //     int y = _oInitPosition.y;
    //     Position p = _oInitPosition;

    //     int iXCount = 0;
    //     int iYCount = 0;

    //     int iAXMax = (_oiTargetPosition.x - _oInitPosition.x) / _iAXValue;
    //     int iAYMax = (_oiTargetPosition.y - _oInitPosition.y) / _iAYValue;

    //     int iBXMax = (_oiTargetPosition.x - _oInitPosition.x) / _iBXValue;
    //     int iBYMax = (_oiTargetPosition.y - _oInitPosition.y) / _iBYValue;

    //     for (int ax = 1; ax < 89; ax++)
    //     {
    //         for (int ay = 1; ay <= 158; ax++)
    //         {
    //             for (int bx = 1; bx < 381; bx++)
    //             {
    //                 for (int by = 1; by <= 80; ax++)
    //                 {
    //                     x += ax * _iAXValue;
    //                     y += ay * _iAYValue;
    //                     p = new Position(x,y)
    //                 }
    //             }
    //         }
    //     }

    //     return iAXMax;
    // }

    static string ReadFileToString(string _sFilePath)
    {
        string sReturnValue = "";

        foreach (string line in File.ReadLines(_sFilePath))
        {
            sReturnValue += line.Trim();
        }

        return sReturnValue;
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