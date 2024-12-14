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

    // convert the input string into a list of int
    string[] sUpdateLineArray = _sUpdateLine.Split(',');
    List<int> iUpdateLineList = sUpdateLineArray.Select(int.Parse).ToList();

    foreach (int iPage in iUpdateLineList)
    {
        if (_bDebug) Console.WriteLine();
        if (_bDebug) Console.WriteLine($"Page {iPage} in update list: {_sUpdateLine}");
        bResult = pageIsValid(iUpdateLineList, iPage, iPageIx, _iRuleList, _bDebug);
        if (!bResult) break;
        iPageIx++;
    }

    if (bResult)
    {
        return getMiddlePageNumber(iUpdateLineList);
    }
    else return -1;
}

static int getMiddlePageNumber(List<int> _iUpdateLineList, bool _bDebug = true)
{
    int iRecordCount = _iUpdateLineList.Count;
    int iMiddleIndex = iRecordCount / 2;
    if (_bDebug) Console.WriteLine();
    if (_bDebug) Console.WriteLine($"RecordCount: {iRecordCount} | MiddleIndex: {iMiddleIndex}");
    return _iUpdateLineList[iMiddleIndex];
}

static bool pageIsValid(List<int> _iUpdateLineList, int _iPage, int _iPageIx, List<int[]> _iRuleList, bool _bDebug = false)
{

    bool bReturnValue = true;

    // bReturnValue = (_iPage == _iUpdateLineList[_iPageIx]);

    // filter rule list
    List<int[]> iFilteredRuleList = filterRuleList(_iRuleList, _iPage);
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
    else
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

static List<int[]> filterRuleList(List<int[]> _iRuleList, int _iFilterPage)
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
List<string> sDataList = ReadFileToList(sFilePath);
List<int[]> iRulesList = new List<int[]>();
List<string> sPageUpdateList = new List<string>();
List<int> iAllPagesList = new List<int>();
List<string> sPagesLine = new List<string>();
int iResult = 0;

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
    int iLineResult = pageUpdateLineIsValid(sPageUpdateLine, iRulesList);
    if (iLineResult != -1)
    {
        iResult += iLineResult;
        Console.WriteLine($"Approved line: {sPageUpdateLine} | middle value: {iLineResult}");
    }
}




Console.WriteLine($"Result 05a: {iResult}");