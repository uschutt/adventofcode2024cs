using System;

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
static List<string> AddFrame(List<string> _sInputList)
{

    List<string> sOutputList = new List<string>();



    int iLength = _sInputList[0].Length;

    // add three lines with W at the top
    // top line width 140 + 3 + 3 W
    for (int i = 1; i <= 3; i++) sOutputList.Add(new string('-', iLength + 3 + 3));

    // Add three W at start and end of every line
    foreach (string sLine in _sInputList)
    {
        sOutputList.Add($"---{sLine}---");
    }

    // add 3 lines with W at the bottom
    // bottom line with 140 + 3 + 3 W
    for (int i = 1; i <= 3; i++) sOutputList.Add(new string('-', iLength + 3 + 3));

    return sOutputList;

}

static bool CheckRowCol(List<string> _sInputList, int _iRow, int _iCol, string _sXMAS, int _iRowDir, int _iColDir, bool _debug = false)
{
    string sXMAS = "";

    // int iRow = _iRow + _iRowDir;
    // int iCol = _iCol + _iColDir;

    int iRow = _iRow;
    int iCol = _iCol;

    // om dir = ( 1, 1) = 4,5 -> 3,4
    // om dir = (-1,-1) = 4,5 -> 5,6
    if (_iRowDir == -1) iRow = iRow + 1; else if (_iRowDir == 1) iRow = iRow - 1;
    if (_iColDir == -1) iCol = iCol + 1; else if (_iColDir == 1) iCol = iCol - 1;



    for (int i = 0; i <= _sXMAS.Length - 1; i++)
    {
        if (_debug) Console.WriteLine($"({iRow},{iCol}) {sXMAS}");
        sXMAS += _sInputList[iRow][iCol];
        iRow += _iRowDir;
        iCol += _iColDir;

    }

    return (sXMAS == _sXMAS);

}

static int CountXMAS(List<string> _sInputList, string _sXMAS = "MAS", bool _bDebug = false, int _iCharIndex = 1)
{

    int iStartRow = 3;
    int iEndRow = _sInputList.Count() - 4;
    int iStartCol = 3;
    int iEndCol = _sInputList[0].Length - 4;
    int iXMASCount = 0;

    for (int iRow = iStartRow; iRow <= iEndRow; iRow++)
    {
        if (_bDebug) Console.WriteLine();
        if (_bDebug) Console.WriteLine($"{iRow - 1} | {_sInputList[iRow - 1]}");
        if (_bDebug) Console.WriteLine($"{iRow - 0} | {_sInputList[iRow - 0]}");
        if (_bDebug) Console.WriteLine($"{iRow + 1} | {_sInputList[iRow + 1]}");
        for (int iCol = iStartCol; iCol <= iEndCol; iCol++)
        {
            if (_sInputList[iRow][iCol] == _sXMAS[_iCharIndex])
            {
                if (_bDebug) Console.WriteLine($"({iRow},{iCol})={_sXMAS[_iCharIndex]}");
                // uppåt vänster och uppåt höger
                if (CheckRowCol(_sInputList, iRow, iCol, _sXMAS, -1, -1, _bDebug) && CheckRowCol(_sInputList, iRow, iCol, _sXMAS, -1, 1, _bDebug)) iXMASCount++;

                // nedåt höger och nedåt vänster
                if (CheckRowCol(_sInputList, iRow, iCol, _sXMAS, 1, 1, _bDebug) && CheckRowCol(_sInputList, iRow, iCol, _sXMAS, 1, -1, _bDebug)) iXMASCount++;
            }
        }
    }

    return iXMASCount;

}

// string sFilePath = "data.txt";
string sFilePath = "testdata.txt";
int iResult = 0;
int ix = 0;
bool bDebug = false;

List<string> sDataList = ReadFileToList(sFilePath);

// first of all we make sure that we dont get out of bounds when checking in every direction
sDataList = AddFrame(sDataList);

if (bDebug)
{
    foreach (string sLine in sDataList)
    {
        Console.WriteLine($"{ix,3} | {sLine}");
        ix++;
    }
}

iResult = CountXMAS(sDataList, "MAS", true, 1);

Console.WriteLine($"Result 04b: {iResult}");