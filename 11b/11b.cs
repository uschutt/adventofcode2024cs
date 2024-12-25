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

        print("------------------------------------------------------------------------------------------------------------------------------------------");
        print($"| Total number of stones after {__iMaxLevel} blinks in {tsDifference.TotalSeconds} seconds: {ulStoneCount} stones");
        print("------------------------------------------------------------------------------------------------------------------------------------------");

    }

    static ulong CountStones(string _sStones, bool _bDebug = false)
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

            if (_bDebug) print($"Blink {i + 1,2} -------------------------------------------------------------------------------------------------------------------------------");

            // step through previous set of stones
            foreach (var stone in stones)
            {

                // use function StepStone to split current stone according to rules in StepStone function
                // break down the stone and add new stones created by the StepStone function to the empty dict newStones
                // the value added is the number of occurencies the current stone generates for the next run
                foreach (string newStoneKey in StepStone(stone.Key))
                {
                    if (newStones.ContainsKey(newStoneKey))
                    {

                        if (_bDebug) PrintDebugRow(i, newStoneKey, newStones[newStoneKey], stone.Value, stone.Key);

                        // for exampel: 
                        // this level:  stone "2" exists 4 times i previous level
                        //              rules in StepStone creates 4 stones with key "4048" (2*2024)
                        // next level:  4 stones with key 4048 creates 4 stones with key "40" and another 4 stones with key "48"
                        newStones[newStoneKey] += stone.Value;

                    }
                    else
                    {

                        if (_bDebug) PrintDebugRow(i, newStoneKey, 0, stone.Value, stone.Key);

                        newStones[newStoneKey] = stone.Value;

                    }
                }
            }


            ulTotalStoneCount = 0;
            foreach (ulong iStoneCount in newStones.Values) ulTotalStoneCount += iStoneCount;
            if (_bDebug) print($"Blink {i + 1,2} -------------------------------------------------------------------------------------------------------------------------------");
            if (_bDebug) print($"Blink {i + 1,2} | stone count: {ulTotalStoneCount,3}");
            if (_bDebug) print("");

            // result from each level replaces previously stored results
            stones = newStones;

        }

        // sum of all stones
        // foreach (int iStoneCount in stones.Values) ulTotalStoneCount += (ulong)iStoneCount;

        return ulTotalStoneCount;

    }

    static void PrintDebugRow(int _iLevel, string _sNewStoneKey, ulong _ulNewCount, ulong _ulCurrentStoneCount, string _sCurrentStoneKey)
    {

        print($"Blink {_iLevel + 1,2} | {_sCurrentStoneKey,12} -> {_sNewStoneKey,12} | {_ulCurrentStoneCount,2} | Acc: {_ulNewCount + _ulCurrentStoneCount,2} | {_sCurrentStoneKey} exists {_ulCurrentStoneCount} time(s) in level {_iLevel}");
        // print($"Blink {_iLevel + 1,2} | {_sCurrentStoneKey,12} -> {_sNewStoneKey,12} | {_ulNewCount,2} + {_ulCurrentStoneCount,2} = {_ulNewCount + _ulCurrentStoneCount,2} | {_sCurrentStoneKey} exists {_ulCurrentStoneCount} in level {_iLevel}");

        // print($"Blink {_iLevel + 1,2} | {_sNewStoneKey,12} | {_ulNewCount,2} + {_ulCurrentStoneCount,2} = {_ulNewCount + _ulCurrentStoneCount,2} | stone {_sNewStoneKey} created from current stone {_sCurrentStoneKey} creates {_ulNewCount + _ulCurrentStoneCount} stones in level {_iLevel + 2} ");
        // print($"Blink {i + 1,2} | old | {newStoneKey,12} |   {newStones[newStoneKey],2} + {stone.Value,2}          = {newStones[newStoneKey] + stone.Value,2} | stone          {newStoneKey} extracted from   ");
    }

    static string StoneDictToString(Dictionary<string, ulong> _inputStonesDict)
    {
        string sReturnValue = "";

        SortedDictionary<string, ulong> sortedStones = new SortedDictionary<string, ulong>(_inputStonesDict);

        foreach (var stone in sortedStones)
        {
            sReturnValue = $"{sReturnValue} | ({stone.Key},{stone.Value})";
        }
        return sReturnValue;
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