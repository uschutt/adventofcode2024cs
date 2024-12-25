using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

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
        ConcurrentBag<int> results = new ConcurrentBag<int>();

        DateTime dtStartTime = DateTime.Now;

        // Parallel.ForEach(sDataList, (s) =>
        // {
        //     iTotalCount = CountStones([s]);
        //     results.Add(iTotalCount);
        // });

        iTotalCount = CountStones(sDataList);


        DateTime dtEndTime = DateTime.Now;

        TimeSpan tsDifference = dtEndTime - dtStartTime;

        print($"Total number of stones after {__iMaxLevel} blinks in {tsDifference.TotalSeconds} seconds:  {results.Sum()} stones ");
        print($"Total number of stones after {__iMaxLevel} blinks in {tsDifference.TotalSeconds} seconds:  {iTotalCount} stones ");


    }

    static void PrintList(string _sStone, int _iX, int _iCount)
    {
        print($"{_iX,2} | {_iCount}");
        // Thread.Sleep(10);
    }


    static int CountStones(string[] _sStoneArray, int _iLevel = 0)
    {
        int iTotalCount = 1;
        string[] sNewStoneArray;

        foreach (string s in _sStoneArray)
        {

            if (_iLevel == __iMaxLevel)
            {
                __iTotalCount += iTotalCount;
                continue; // Exit early?
            }

            if (_iLevel < __iMaxLevel)
            {
                if (s == "0")
                {
                    sNewStoneArray = ["1"];
                }
                else if (s.Length % 2 == 0)
                {
                    int iHalfString = s.Length / 2;

                    string leftHalf = IntifyString(s.AsSpan(0, iHalfString).ToString()); // avoid using subsrings 
                    string rightHalf = IntifyString(s.AsSpan(s.Length - iHalfString, iHalfString).ToString()); // avoid using subsrings 

                    sNewStoneArray = [leftHalf, rightHalf];

                    iTotalCount = 2;
                }
                else
                {
                    ulong iNumber = ulong.Parse(s) * 2024;
                    sNewStoneArray = [iNumber.ToString()];
                }

                iTotalCount += CountStones(sNewStoneArray, _iLevel + 1);

            }


        }

        return iTotalCount;

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