using System;
using System.Threading;

class Program11b
{
    static string __sFilePath = "data.txt";
    static int __iMaxLevel = 40;
    static int __iTotalCount = 0;

    static void Main(string[] args)
    {
        string sData = ReadFileToString(__sFilePath);
        string[] sDataList = sData.Split(' ').ToArray();

        DateTime dtStartTime = DateTime.Now;

        foreach (string s in sDataList)
        {
            CountStones(s);
        }


        DateTime dtEndTime = DateTime.Now;

        TimeSpan tsDifference = dtEndTime - dtStartTime;

        print($"Total number of stones after {__iMaxLevel} blinks in {tsDifference.TotalSeconds} seconds:  {__iTotalCount} stones ");

    }

    static void PrintList(string _sStone, int _iX, int _iCount)
    {
        print($"{_iX,2} | {_iCount}");
        // Thread.Sleep(10);
    }


    static void CountStones(string _sStone, int _iLevel = 0)
    {
        int iTotalCount = 1;
        string[] sNewStoneArray;

        if (_iLevel == __iMaxLevel)
        {
            __iTotalCount += iTotalCount;
        }
        else
        {
            if (_sStone == "0")
            {
                sNewStoneArray = ["1"];
            }
            else if (_sStone.Length % 2 == 0)
            {
                int iHalfString = _sStone.Length / 2;

                string leftHalf = IntifyString(_sStone.AsSpan(0, iHalfString).ToString()); // avoid using subsrings 
                string rightHalf = IntifyString(_sStone.AsSpan(_sStone.Length - iHalfString, iHalfString).ToString()); // avoid using subsrings 

                sNewStoneArray = [leftHalf, rightHalf];

                iTotalCount = 2;
            }
            else
            {
                ulong iNumber = ulong.Parse(_sStone) * 2024;
                sNewStoneArray = [iNumber.ToString()];
            }

            foreach (string s in sNewStoneArray)
            {
                CountStones(s, _iLevel + 1);
            }
        }
    }

    static string IntifyString(string _sInput)
    {
        // return ulong.Parse(_sInput).ToString();
        string result = _sInput.TrimStart('0');
        return result == string.Empty ? "0" : result;
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

    static void print(string sText)
    {
        Console.WriteLine(sText);
    }
}