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

        var dataList = ReadFileToList(filePath);

        // foreach (string d in dataList)
        // {
        //     if (CheckLinePart1(d)) cnt++;
        // }
        // Console.WriteLine($"Result part1: {cnt}");

        cnt = 0;
        int ix = 0;
        foreach (string d in dataList)
        {
            if (CheckLinePart2(d, true, ix)) cnt++;
            ix++;
        }
        Console.WriteLine($"Result part2: {cnt}");
        // 392 to high
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

            if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
            {
                deviationCnt++;
                if (deviationCnt == 1)
                {
                    lineIntList.RemoveAt(ix);
                    ix = 1; //reset loop
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
                    if (deviationCnt == 1)
                    {
                        lineIntList.RemoveAt(ix);
                        ix = 1; //reset loop
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
                    if (deviationCnt == 1)
                    {
                        lineIntList.RemoveAt(ix);
                        ix = 1; //reset loop
                        returnValue = true;
                    }
                    else if (deviationCnt > 1) returnValue = false;
                }
            }
            else
            {
                returnValue = false;
            }
        }

        int listLengthAfter = lineIntList.Count();

        if ((listLengthBefore - listLengthAfter) > 1) returnValue = false;

        if (_debug) printListOnRow(lineIntList, "After  : ");
        if (_debug) Console.WriteLine($"Result : {returnValue} | Sort: {sortType} | ListCountDiff: {listLengthBefore - listLengthAfter}");

        return returnValue;
    }

    void printList(List<int> _list)
    {
        int rowNo = 1;
        foreach (int i in _list)
        {
            Console.WriteLine($"{rowNo} | {i}");
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