using System;
using System.Threading;
using System.Collections.Generic;

class Program11b
{
    static string __sFilePath = "data.txt";
    static int __iMaxLevel = 75;

    static void Main(string[] args)
    {
        string sData = ReadFileToString(__sFilePath);
        ulong ulStoneCount = 0;

        DateTime dtStartTime = DateTime.Now;

        ulStoneCount = CountStones(sData);

        DateTime dtEndTime = DateTime.Now;

        TimeSpan tsDifference = dtEndTime - dtStartTime;

        print($"Total number of stones after {__iMaxLevel} blinks in {tsDifference.TotalSeconds} seconds:  {ulStoneCount} stones ");

    }

    static ulong CountStones(string _sStones)
    {
        ulong ulTotalStoneCount = 0;

        // create dictionarey with a string key (stone) and a int value
        Dictionary<string, ulong> stones = new Dictionary<string, ulong>();

        // init dictionary
        foreach (string sStone in _sStones.Split(' ')) stones[sStone] = 1;

        // for every level
        for (int i = 0; i < __iMaxLevel; i++)
        {
            // temporary dictionary to store new stones
            Dictionary<string, ulong> newStones = new Dictionary<string, ulong>();

            // step through previous set of stones
            foreach (var stone in stones)
            {
                // use function StepStone to split current stone according to rules in StepStone function
                foreach (string newStoneKey in StepStone(stone.Key))
                {

                    if (newStones.ContainsKey(newStoneKey))
                    {

                        newStones[newStoneKey] += stone.Value;
                    }
                    else
                    {
                        newStones[newStoneKey] = stone.Value;
                    }
                }
            }

            ulTotalStoneCount = 0;
            foreach (ulong iStoneCount in newStones.Values) ulTotalStoneCount += iStoneCount;
            print($"Level {i + 1,2} | stone count: {ulTotalStoneCount} ");

            // result from each level replaces previously stored results
            stones = newStones;

        }

        // sum of all stones
        // foreach (int iStoneCount in stones.Values) ulTotalStoneCount += (ulong)iStoneCount;

        return ulTotalStoneCount;

    }

    static string[] StepStone(string _sStone)
    {
        if (_sStone == "0")
        {
            return ["1"];
        }
        else if (_sStone.Length % 2 == 0)
        {
            int iHalfString = _sStone.Length / 2;
            return [IntifyString(_sStone.AsSpan(0, iHalfString).ToString()), IntifyString(_sStone.AsSpan(_sStone.Length - iHalfString, iHalfString).ToString())];
        }
        else
        {
            ulong iNumber = ulong.Parse(_sStone) * 2024;
            return [iNumber.ToString()];
        }
    }

    static string IntifyString(string _sInput)
    {
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