// using System;
// using System.Collections.Generic;
// using System.Linq;

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

static int pageUpdateLineIsValid(string _sUpdateLine, List<int[]> _iRuleList, bool _bDebug = false)
{
    int iReturnValue = -1;
    bool bResult = false;
    int iPageIx = 0;
    int[] iTestRuleArr;
    int iTemp;
    string sNewUpdateLine = _sUpdateLine;
    int ix = 0;

    // convert the input string into a list of int
    string[] sUpdateLineArray = _sUpdateLine.Split(',');
    List<int> iUpdateLineList = sUpdateLineArray.Select(int.Parse).ToList();

    foreach (int iPage in iUpdateLineList)
    {
        bResult = pageIsValid(iUpdateLineList, iPage, iPageIx, _iRuleList, false);
        if (!bResult) break;
        iPageIx++;
    }

    if (!bResult) // now we are looking for the invalid updates
    {
        if (_bDebug) Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
        if (_bDebug) Console.WriteLine(_sUpdateLine);

        // filter rule list, only pages occuring in iUpdateLineList shall be kept
        List<int[]> iFilteredRuleList = filterRuleListByPageList(_iRuleList, iUpdateLineList);

        // printRuleList(iFilteredRuleList);



        if (!bResult)
        {

            while (ix <= iUpdateLineList.Count - 2)
            {
                iTestRuleArr = [iUpdateLineList[ix], iUpdateLineList[ix + 1]];
                if (!RuleExists(iTestRuleArr, iFilteredRuleList))
                {
                    // if (_bDebug) Console.WriteLine();
                    if (_bDebug) Console.WriteLine($"{sNewUpdateLine} | Rule: {iTestRuleArr[0]}|{iTestRuleArr[1]} missing!");

                    // switch place
                    iTemp = iUpdateLineList[ix];
                    iUpdateLineList[ix] = iUpdateLineList[ix + 1];
                    iUpdateLineList[ix + 1] = iTemp;

                    sNewUpdateLine = string.Join(",", iUpdateLineList);

                    ix = 0; //start over
                }
                else
                {
                    ix++;
                }

            }

            sNewUpdateLine = string.Join(",", iUpdateLineList);

            iReturnValue = getMiddlePageNumber(iUpdateLineList);
        }
    }
    else
    {
        iReturnValue = -1; // -1 is returned if the line is valid - then it shall NOT be counted.
    }

    return iReturnValue;
}

static bool RuleExists(int[] _iTestRuleArr, List<int[]> _iRuleList)
{
    bool bReturnValue = false;

    foreach (int[] iRuleArr in _iRuleList)
    {
        if (RuleIntArrToString(iRuleArr) == RuleIntArrToString(_iTestRuleArr))
        {
            bReturnValue = true;
            break;
        }
    }

    return bReturnValue;
}

static string RuleIntArrToString(int[] _iRuleArr)
{
    return $"{_iRuleArr[0]}|{_iRuleArr[1]}";
}

static int getMiddlePageNumber(List<int> _iUpdateLineList, bool _bDebug = true)
{
    int iRecordCount = _iUpdateLineList.Count;
    int iMiddleIndex = iRecordCount / 2;

    if (_bDebug) Console.WriteLine($"RecordCount: {iRecordCount,3} | MiddleIndex: {iMiddleIndex,2} | Middle value: {_iUpdateLineList[iMiddleIndex]}");

    return _iUpdateLineList[iMiddleIndex];
}

static bool pageIsValid(List<int> _iUpdateLineList, int _iPage, int _iPageIx, List<int[]> _iRuleList, bool _bDebug = false)
{
    bool bReturnValue = true;

    // filter rule list
    List<int[]> iFilteredRuleList = filterRuleListByPage(_iRuleList, _iPage);

    if (_bDebug) printRuleList(iFilteredRuleList);

    // foreach rule in filterd list
    //  - call function with page number and filtered rule list
    //  - function will test page against all rules i filtered list

    foreach (int[] rule in iFilteredRuleList)
    {
        bReturnValue = pageMatchRule(_iUpdateLineList, _iPage, _iPageIx, rule, false);
        if (!bReturnValue) break;
    }

    return bReturnValue;

}

static bool pageMatchRule(List<int> _iUpdateLineList, int _iPage, int _iPageIx, int[] _iRule, bool _bDebug = false)
{

    bool bReturnValue = true;

    if (_iPage == _iRule[0])
    {
        for (int ix = _iPageIx; ix >= 0; ix--)
        {
            if (_iUpdateLineList[ix] == _iRule[1])
            {
                bReturnValue = false;
                break;
            }
        }
    }
    else if (_iPage == _iRule[1])
    {
        for (int ix = _iPageIx; ix < _iUpdateLineList.Count; ix++)
        {
            if (_iUpdateLineList[ix] == _iRule[0])
            {
                bReturnValue = false;
                break;
            }
        }
    }

    return bReturnValue;

}

static void printRuleList(List<int[]> _iRuleList)
{
    foreach (int[] rule in _iRuleList)
    {
        string s = $"{rule[0]}|{rule[1]}";
        Console.WriteLine(s);
    }
}

static List<int[]> filterRuleListByPageList(List<int[]> _iRuleList, List<int> _iUpdateLineList)
{
    List<int[]> iFilteredRuleList = new List<int[]>();

    foreach (int[] rule in _iRuleList)
    {
        if (_iUpdateLineList.Contains(rule[0]) && _iUpdateLineList.Contains(rule[1]))
        {
            iFilteredRuleList.Add(rule);
        }
    }

    return iFilteredRuleList;
}

static List<int[]> filterRuleListByPage(List<int[]> _iRuleList, int _iFilterPage)
{
    List<int[]> iFilteredRuleList = new List<int[]>();

    foreach (int[] rule in _iRuleList)
    {
        if (Array.Exists(rule, element => element == _iFilterPage))
        {
            iFilteredRuleList.Add(rule);
        }
    }

    return iFilteredRuleList;
}

string sFilePath = "data.txt";
// string sFilePath = "testdata.txt";
// string sFilePath = "05bdata.txt";

List<string> sDataList = ReadFileToList(sFilePath);
List<int[]> iRulesList = new List<int[]>();
List<string> sPageUpdateList = new List<string>();
List<int> iAllPagesList = new List<int>();
List<string> sPagesLine = new List<string>();
int iCount05a = 0;
int iCount05b = 0;
int iResult05a = 0;
int iResult05b = 0;

foreach (string sLine in sDataList)
{
    // Console.WriteLine(sLine);
    if (sLine.Contains('|'))
    {
        string[] sLineArr = sLine.Split('|');
        int[] iLineArr = [int.Parse(sLineArr[0]), int.Parse(sLineArr[1])];

        iRulesList.Add(iLineArr);
    }
    else if (sLine.Contains(','))
    {
        sPageUpdateList.Add(sLine);
    }
}

// foreach (string sLine in sRulesList) Console.WriteLine(sLine);
// Console.WriteLine();
// foreach (string sLine in sPageUpdateList) Console.WriteLine(sLine);

foreach (string sPageUpdateLine in sPageUpdateList)
{
    int iLineResult = pageUpdateLineIsValid(sPageUpdateLine, iRulesList, false);
    if (iLineResult != -1)
    {
        iCount05b++;
        iResult05b += iLineResult;
    }
    else
    {
        iCount05a++;
        iResult05a += iLineResult;
    }
}

Console.WriteLine("------------------------------------------------------");
Console.WriteLine($"Result 05b: {iResult05b,5} | Count 05b: {iCount05b,4}");
Console.WriteLine("------------------------------------------------------");