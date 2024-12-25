using System;
using System.Threading;

class Program11b
{
    static string __sFilePath = "data.txt";
    static int __iMaxLevel = 25;
    static int __iTotalCount = 0;

    static void Main(string[] args)
    {
        string sData = ReadFileToString(__sFilePath);
        string[] sDataList = sData.Split(' ').ToArray();
        int iTotalCount = 0;

        iTotalCount = CountStones(sDataList);

        print($"Total number of stones after {__iMaxLevel} iterations is: {__iTotalCount} stones");

    }

    static void PrintList(string _sStone, int _iX, int _iCount)
    {
        string sData = "";
        print($"{_iX,2} | {_iCount}");
        // Thread.Sleep(10);
    }


    static int CountStones(string[] _sStoneArray, int _iLevel = 0)
    {
        int iHalfString;
        ulong iNumber;
        int iTotalCount = 1;
        string[] sNewStoneArray;

        foreach (string s in _sStoneArray)
        {

            if (_iLevel == __iMaxLevel)
            {
                __iTotalCount += iTotalCount;
            }

            if (_iLevel < __iMaxLevel)
            {
                if (s == "0")
                {
                    sNewStoneArray = ["1"];
                }
                else if (s.Length % 2 == 0)
                {
                    iHalfString = s.Length / 2;
                    sNewStoneArray = [IntifyString(s.Substring(0, iHalfString)), IntifyString(s.Substring(s.Length - iHalfString, iHalfString))];
                    iTotalCount = 2;
                }
                else
                {
                    iNumber = ulong.Parse(s) * 2024;
                    sNewStoneArray = [$"{iNumber}"];
                }

                iTotalCount += CountStones(sNewStoneArray, _iLevel + 1);

            }


        }

        return iTotalCount;

    }


    static string IntifyString(string _sInput)
    {
        return $"{ulong.Parse(_sInput)}";
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