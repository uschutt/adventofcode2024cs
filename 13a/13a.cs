using System;

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
        iResult = CalculateCost();

        return iResult;
    }

    static int CalculateCost()
    {
        Position _oInitPosition = new Position(0, 0);
        Position _oiTargetPosition = new Position(8400, 5400);
        Position _oAMove = new Position(94, 34);
        Position _oBMove = new Position(22, 67);
        Position oPosition = _oInitPosition;

        int A = 0;
        int B;

        for (Position pA = _oInitPosition; pA < _oiTargetPosition; pA += _oAMove)
        {
            B = 0;
            for (Position pB = _oInitPosition; pB < _oiTargetPosition; pB += _oBMove)
            {

                oPosition = pA + pB;
                print($"A: {A,3} {pA.Description()} | B: {B,3} {pB.Description()} | {oPosition.Description()}");
                if (oPosition == _oiTargetPosition)
                {
                    print("This is it!");
                    return 1;
                }
                if (pB > _oiTargetPosition) break;
                B++;
            }
            if (pA > _oiTargetPosition) break;
            A++;
        }

        return -1;

    }

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