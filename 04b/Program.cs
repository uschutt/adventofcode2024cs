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

static bool CheckRowCol(List<string> _sInputList, int _iRow, int _iCol, string _sXMAS, int _iRowDir, int _iColDir, bool _debug = false, string _sDebugText = "")
{
    string sXMAS = "";
    bool bReturnValue = false;

    if (_debug) Console.WriteLine($"{_sDebugText} | _iRow = {_iRow} | _iCol = {_iCol}");

    int iRow = _iRow;
    int iCol = _iCol;

    for (int i = 0; i <= _sXMAS.Length - 1; i++)
    {

        sXMAS += _sInputList[iRow][iCol];
        if (_debug) Console.WriteLine($"({iRow},{iCol}) {sXMAS}");
        iRow += _iRowDir;
        iCol += _iColDir;

    }

    bReturnValue = (sXMAS == _sXMAS);

    if (_debug) Console.WriteLine($"({sXMAS} == {_sXMAS}) == {bReturnValue}");

    if (!bReturnValue)
    {
        bReturnValue = (sXMAS == reverseString(_sXMAS));
        if (_debug) Console.WriteLine($"({sXMAS} == {reverseString(_sXMAS)}) == {bReturnValue}"); ;
    }

    return bReturnValue;

}

static int CountXMAS(List<string> _sInputList, string _sXMAS = "MAS", bool _bDebug = false, int _iCharIndex = 1)
{

    int iStartRow = 3;
    int iEndRow = _sInputList.Count() - 4;
    int iStartCol = 3;
    int iEndCol = _sInputList[0].Length - 4;
    int iXMASCount = 0;
    bool bCheck1 = false;
    bool bCheck2 = false;

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
                if (_bDebug) Console.WriteLine("----------------------------------------------------------------------------start");
                if (_bDebug) Console.WriteLine($"({iRow},{iCol})={_sXMAS[_iCharIndex]}");
                // uppåt vänster och uppåt höger

                // look Up & Left
                // first move down & right -> down = iRow +1 | right = iCol +1
                bCheck1 = CheckRowCol(_sInputList, iRow + 1, iCol + 1, _sXMAS, -1, -1, _bDebug, "up le");

                // look Up & Right
                // first move down and left -> down = iRow +1 | left = iCol - 1
                bCheck2 = CheckRowCol(_sInputList, iRow + 1, iCol - 1, _sXMAS, -1, 1, _bDebug, "up ri");

                if (bCheck1 && bCheck2) iXMASCount++;

                if (_bDebug) Console.WriteLine("------------------------------------------------------------------------------end");
                if (_bDebug) Console.WriteLine(iXMASCount);
            }
        }
    }

    return iXMASCount;

}

static string reverseString(string _sInput)
{

    // Convert the string to a character array
    char[] charArray = _sInput.ToCharArray();

    // Reverse the character array
    Array.Reverse(charArray);

    // Create a new string from the reversed character array
    return new string(charArray);

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

iResult = CountXMAS(sDataList, "MAS", bDebug, 1);

Console.WriteLine($"Result 04b: {iResult}");