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
    for (int i = 1; i <= 3; i++) sOutputList.Add(new string('W', iLength + 3 + 3));

    // Add three W at start and end of every line
    foreach (string sLine in _sInputList)
    {
        sOutputList.Add($"WWW{sLine}WWW");
    }

    // add 3 lines with W at the bottom
    // bottom line with 140 + 3 + 3 W
    for (int i = 1; i <= 3; i++) sOutputList.Add(new string('W', iLength + 3 + 3));

    return sOutputList;

}

static bool CheckRowCol(List<string> _sInputList, int _iRow, int _iCol, string _sXMAS, int _iRowDir, int _iColDir, bool _debug = false)
{
    string sXMAS = "";

    int iRow = _iRow;
    int iCol = _iCol;

    for (int i = 0; i <= 3; i++)
    {
        sXMAS += _sInputList[iRow][iCol];
        iRow += _iRowDir;
        iCol += _iColDir;
        if (_debug) Console.WriteLine($"({iRow},{iCol}) {sXMAS}");
    }

    return (sXMAS == _sXMAS);

}

static int CountXMAS(List<string> _sInputList, string _sXMAS = "XMAS", bool _bDebug = false)
{

    int iStartRow = 3;
    int iEndRow = _sInputList.Count() - 4;
    int iStartCol = 3;
    int iEndCol = _sInputList[0].Length - 4;
    int iXMASCount = 0;

    for (int iRow = iStartRow; iRow <= iEndRow; iRow++)
    {
        if (_bDebug) Console.WriteLine($"{iRow} | {_sInputList[iRow]}");
        for (int iCol = iStartCol; iCol <= iEndCol; iCol++)
        {
            if (_sInputList[iRow][iCol] == _sXMAS[0])
            {
                if (_bDebug) Console.WriteLine($"({iRow},{iCol})={_sXMAS[0]}");
                if (CheckRowCol(_sInputList, iRow, iCol, _sXMAS, 0, 1)) iXMASCount++; // höger
                if (CheckRowCol(_sInputList, iRow, iCol, _sXMAS, 0, -1)) iXMASCount++; // vänster
                if (CheckRowCol(_sInputList, iRow, iCol, _sXMAS, 1, 0)) iXMASCount++; // nedåt
                if (CheckRowCol(_sInputList, iRow, iCol, _sXMAS, -1, 0)) iXMASCount++; // uppåt
                if (CheckRowCol(_sInputList, iRow, iCol, _sXMAS, -1, -1)) iXMASCount++; // uppåt vänster
                if (CheckRowCol(_sInputList, iRow, iCol, _sXMAS, -1, 1)) iXMASCount++; // uppåt höger
                if (CheckRowCol(_sInputList, iRow, iCol, _sXMAS, 1, 1)) iXMASCount++; // nedåt höger
                if (CheckRowCol(_sInputList, iRow, iCol, _sXMAS, 1, -1)) iXMASCount++; // nedåt vänster
            }
        }
    }

    return iXMASCount;

}

string sFilePath = "data.txt";
// string sFilePath = "testdata.txt";
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

iResult = CountXMAS(sDataList);

Console.WriteLine($"Result 04a: {iResult}");