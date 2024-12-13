using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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

    static bool CheckReportAsc(List<int> _iReportList, bool _bIncreasing, bool _bSkip = true, bool _bDebug = false)
    {

        bool bSkip = _bSkip;
        bool bResult = true;
        int diff;
        int iLowLimit;
        int iHighLimit;
        List<int> iReportList;

        if (_bIncreasing)
        {
            iLowLimit = 1;
            iHighLimit = 3;
        }
        else
        {
            iLowLimit = -3;
            iHighLimit = -1;
        }

        for (int ix = 1; ix < _iReportList.Count; ix++)
        {

            diff = (_iReportList[ix] - _iReportList[ix - 1]);

            bResult = !(diff < iLowLimit || diff > iHighLimit);

            if (!bResult) break;
        }

        // if skip but result still == false - remove first level and test again
        if (bSkip && !bResult)
        {
            for (int i = 0; i < _iReportList.Count; i++)
            {
                iReportList = _iReportList.ToList();
                iReportList.RemoveAt(i);
                bResult = CheckReportAsc(iReportList, _bIncreasing, false, _bDebug);
                if (bResult) break;
            }
        }

        return bResult;
    }

    static void debugPrint(List<int> _iReportList, bool _bSkip = true, string _sText = "")
    {
        string sReport = "";
        foreach (int iReport in _iReportList) sReport += ' ' + iReport.ToString();

        sReport = $"data: {sReport} | skip: {_bSkip,-5}";
        if (_sText != "") sReport = $"{sReport} | {_sText}";

        Console.WriteLine(sReport);
    }


    static int GetResultPart2(List<string> _dataList)
    {
        int cnt = 0;
        int ix = 0;
        bool bResult = false;
        string sResult = "";
        string sResultLine = "";
        string[] sReportArray;
        List<int> iReportList;

        List<string> resultList = new List<string>();

        foreach (string sReport in _dataList)
        {

            // split row to array
            sReportArray = sReport.Split(' ');

            // convert string array to int list
            iReportList = sReportArray.Select(int.Parse).ToList();

            // check increasing 
            bResult = CheckReportAsc(iReportList, true, true);

            // check decreasing
            if (!bResult) bResult = CheckReportAsc(iReportList, false, true);

            if (bResult) cnt++;

            ix++;
            if (bResult) sResult = "True"; else sResult = "False";

            sResultLine = $"{ix} | {sReport} | {sResult}";

            resultList.Add(sResultLine);

        }

        writeListToFile(resultList, "result_cs.txt");

        return cnt;
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

    static void writeListToFile(List<string> _list, string _filePath)
    {
        // Write list to file
        using (StreamWriter writer = new StreamWriter(_filePath))
        {
            foreach (string item in _list)
            {
                writer.WriteLine(item);
            }
        }
    }
}