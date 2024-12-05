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

        cnt = GetResultPart2(dataList);

        Console.WriteLine($"Result part2: {cnt}");
        // 392 to high
        // 365 to low
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

    static int GetResultPart2(List<string> _dataList)
    {
        int cnt = 0;
        int ix = 0;
        bool line_result = false;

        List<string> unSafeList = new List<string>();
        List<string> restList = new List<string>();
        foreach (string d in _dataList)
        {
            line_result = CheckLinePart1(d);
            if (line_result)
            {
                cnt++;
            }
            else
            {
                unSafeList.Add(d);
            }
            ix++;

        }

        unSafeList = ApplyProblemDampnerList(unSafeList);

        foreach (string l in unSafeList)
        {
            line_result = CheckLinePart1(l);
            if (line_result) cnt++; else restList.Add(l);
        }

        printList(restList);

        return cnt;
    }

    static List<string> ApplyProblemDampnerList(List<string> _list)
    {
        string s = "";
        List<string> newList = new List<string>();

        foreach (string l in _list)
        {
            s = ApplyProblemDampnerString(l);
            if (s != l) newList.Add(s);
        }

        return newList;

    }

    static string ApplyProblemDampnerString(string _line)
    {

        string sortType = "";
        int diff = 0;
        string[] lineStringList = _line.Split(' ');
        bool UseDampner = false;

        List<int> lineIntList = lineStringList.Select(int.Parse).ToList();

        for (int ix = 1; ix < lineIntList.Count; ix++)
        {
            diff = lineIntList[ix] - lineIntList[ix - 1];

            if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
            {
                UseDampner = true;
            }
            else if (diff > 0)
            {
                if (sortType == "" || sortType == "asc") sortType = "asc"; else UseDampner = true;
            }
            else if (diff < 0)
            {
                if (sortType == "" || sortType == "desc") sortType = "desc"; else UseDampner = true;
            }

            if (UseDampner)
            {
                lineIntList.RemoveAt(ix);
                break;
            }
        }
        string returnValue = string.Join(" ", lineIntList);
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