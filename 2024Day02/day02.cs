using System;
using System.Collections.Generic;
using System.Linq;

class day02
{
    static void Main(string[] args)
    {
        string filePath = "data.txt";
        // string filePath = "testdata.txt";
        int cnt = 0;
        List<string> safeListPart1 = new List<string>();
        List<string> unSafeListPart1 = new List<string>();
        var dataList = ReadFileToList(filePath);
        bool line_result = false;

        foreach (string d in dataList)
        {
            line_result = CheckLinePart1(d);

            if (line_result)
            {
                cnt++;
                safeListPart1.Add(d);
            }
            else unSafeListPart1.Add($"{line_result} - {d}");
        }
        Console.WriteLine($"Result part1: {cnt}");

        cnt = 0;
        int ix = 0;

        List<string> safeListPart2 = new List<string>();
        List<string> unSafeListPart2 = new List<string>();
        foreach (string d in dataList)
        {
            line_result = CheckLinePart2(d, false, ix);
            if (line_result)
            {
                cnt++;
                safeListPart2.Add(d);
            }
            else unSafeListPart2.Add($"{line_result} - {d}");
            // Console.WriteLine($"ix: {ix} | result: {line_result} | safeCount: {cnt}");
            ix++;

        }
        Console.WriteLine($"Result part2: {cnt}");
        // 392 to high
        // 365 to low - fler skall godkännas, hur hittar jag vilka som angetts som unsafe men som skall vara safe, då behöver jag titta på datamängden som är unsafe

        // List<string> diffList = safeListPart2.Except(safeListPart1).ToList();
        // Console.WriteLine($"safeListPart1Count: {safeListPart1.Count()}");
        // Console.WriteLine($"safeListPart2Count: {safeListPart2.Count()}");
        // Console.WriteLine($"diffListCount: {diffList.Count()}");

        // printList(diffList);

        List<string> diffList = unSafeListPart1.Except(unSafeListPart2).ToList();
        // diffList = unSafeListPart1.Except(unSafeListPart2).ToList();
        Console.WriteLine($"unSafeListPart1Count: {unSafeListPart1.Count()}");
        Console.WriteLine($"unSafeListPart2Count: {unSafeListPart2.Count()}");
        Console.WriteLine($"diffListCount: {diffList.Count()}");

        printList(diffList);

        // CheckLinePart2("71 71 74 71 69", true, 1);
        bool r = CheckLinePart2("27 30 31 32 32 33", true, 1);
        Console.WriteLine(r);



    }


    static bool CheckLinePart1(string _line)
    {
        string[] lineStringList = _line.Split(' ');

        bool returnValue = true;

        string sortType = "";

        int diff = 0;

        List<int> lineIntList = lineStringList.Select(int.Parse).ToList();

        for (int ix = 1; ix < lineIntList.Count; ix++)
        {
            diff = lineIntList[ix] - lineIntList[ix - 1];

            if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
            {
                return false;
            }
            else if (diff > 0)
            {
                // Console.WriteLine($"asc diff {diff}");
                if (sortType == "" || sortType == "asc") sortType = "asc"; else return false;
            }
            else if (diff < 0)
            {
                // Console.WriteLine($"desc diff {diff}");
                if (sortType == "" || sortType == "desc") sortType = "desc"; else return false;
            }
            else
            {
                return false;
            }
        }
        return returnValue;
    }

    static bool CheckLinePart2(string _line, bool _debug = false, int _listIndex = -1)
    {
        string[] lineStringList = _line.Split(' ');

        bool returnValue = true;

        string sortType = "";

        int diff = 0;

        List<int> lineIntList = lineStringList.Select(int.Parse).ToList();

        int deviationCnt = 0;

        string printIndex = "";

        int listLengthBefore = lineIntList.Count();

        if (_listIndex > 0) printIndex = _listIndex.ToString();

        if (_debug) Console.WriteLine($"-----------------------------------------------------------  {printIndex}");
        if (_debug) printListOnRow(lineIntList, "Before : ", "");



        for (int ix = 1; ix < lineIntList.Count; ix++)
        {


            diff = lineIntList[ix] - lineIntList[ix - 1];

            if (_debug) Console.WriteLine($"{ix - 1}: {lineIntList[ix - 1]} | {ix}: {lineIntList[ix]} | diff: {diff}");

            if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
            {
                deviationCnt++;
                if (_debug) Console.WriteLine($"type: Wrong range | deviationCnt: {deviationCnt}");
                if (deviationCnt == 1)
                {
                    lineIntList.RemoveAt(ix);
                    ix = 0; //reset loop
                    returnValue = true;
                }
                else if (deviationCnt > 1) returnValue = false;
            }
            else if (diff > 0)
            {
                // Console.WriteLine($"asc diff {diff}");
                if (sortType == "" || sortType == "asc")
                {
                    sortType = "asc";
                }
                else
                {
                    deviationCnt++;
                    if (_debug) Console.WriteLine($"type: Changed direction | acs != {sortType} | deviationCnt: {deviationCnt}");
                    if (deviationCnt == 1)
                    {
                        lineIntList.RemoveAt(ix);
                        ix = 0; //reset loop
                        returnValue = true;
                    }
                    else if (deviationCnt > 1) returnValue = false;
                }
            }
            else if (diff < 0)
            {
                // Console.WriteLine($"desc diff {diff}");
                if (sortType == "" || sortType == "desc") { sortType = "desc"; }
                else
                {
                    deviationCnt++;
                    if (_debug) Console.WriteLine($"type: Changed direction | desc != {sortType} | deviationCnt: {deviationCnt}");
                    if (deviationCnt == 1)
                    {
                        lineIntList.RemoveAt(ix);
                        ix = 0; //reset loop
                        returnValue = true;
                    }
                    else if (deviationCnt > 1) returnValue = false;
                }
            }
            else
            {
                if (_debug) Console.WriteLine($"type: else | sortType: {sortType} | deviationCnt: {deviationCnt}");
                returnValue = false;
            }
            if (_debug) Console.WriteLine($"Sort: {sortType}");
            if (_debug) Console.WriteLine($"-------------------------------------------------------");
        }

        int listLengthAfter = lineIntList.Count();

        if ((listLengthBefore - listLengthAfter) > 1) returnValue = false;

        if (_debug) printListOnRow(lineIntList, "After  : ");
        if (_debug) Console.WriteLine($"Result : {returnValue} | Sort: {sortType} | ListCountDiff: {listLengthBefore - listLengthAfter}");

        return returnValue;
    }

    static void printList(List<string> _list)
    {
        int rowNo = 1;
        foreach (string s in _list)
        {
            Console.WriteLine($"{rowNo,4} | {s}");
            rowNo++;
        }
    }

    static void printListOnRow(List<int> _list, string _textPre = "", string _textPost = "")
    {
        string s = string.Join(", ", _list);
        s = _textPre + s + " | " + _textPost;
        Console.WriteLine(s);
    }

    static List<string> ReadFileToList(string filePath)
    {
        // Läs alla rader från filen
        var lines = new List<string>();

        foreach (var line in File.ReadLines(filePath))
        {
            lines.Add(line.Trim());
        }

        return lines;
    }
}