
using System;
using System.Threading;

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

static string CompressDiskMap(string _sInputDiskMap)
{
    string sOutputDiskMap = _sInputDiskMap;
    int ixf, ixb;
    string sFile, sComment;
    int iEmptyStartPosition;


    ixf = 0; // from the front
    ixb = sOutputDiskMap.Length - 1; // from the back

    // PrintDiskLayout(sOutputDiskMap, true);

    while (ixb > ixf)
    {
        sFile = GetFile(sOutputDiskMap, ixb);
        if (sFile.Length > 0)
        {
            iEmptyStartPosition = GetEmptyStartPosition(sOutputDiskMap, ixb, sFile.Length);

            // sComment = $"ixb: {ixb,3} | ixf: {ixf,3} | FileLength: {sFile.Length}| Empty slot: {iEmptyStartPosition,3} | {sOutputDiskMap.Length}";
            // print(sComment);
            // PrintDiskLayout(sOutputDiskMap, false, sComment);
            // Thread.Sleep(1000);

            if (iEmptyStartPosition != -1)
            {
                sOutputDiskMap = StringSwap(sOutputDiskMap, iEmptyStartPosition, ixb - sFile.Length + 1, sFile.Length);
                ixf += sFile.Length;
            }

            ixb -= sFile.Length;
        }
        else
        {
            ixb--;
        }

    }

    return sOutputDiskMap;
}



static int GetEmptyStartPosition(string _sDiskMap, int _iEndIndex, int _iLength)
{
    char cEmpty = (char)65000;
    int iEmptyStartPosition = -1;
    int iEmptyLength = 0;

    //find empty pos
    for (int i = 0; i < _iEndIndex - 1; i++)
    {
        if (_sDiskMap[i] == cEmpty)
        {
            iEmptyLength++;

            // store the position of the first empty block
            if (iEmptyLength == 1) iEmptyStartPosition = i;

            // if the empty blocks length is correct then return the start pos of the empty blocks, else return -1
            if (iEmptyLength == _iLength) return iEmptyStartPosition;

        }
        else
        {
            iEmptyLength = 0;
            iEmptyStartPosition = -1;
        }
    }

    return -1;
}

static string GetFile(string _sDiskMap, int _iStartIndex)
{
    char cEmpty = (char)65000;
    string sFile = "";
    int iX = _iStartIndex;

    while (_sDiskMap[iX] != cEmpty && _sDiskMap[iX] == _sDiskMap[_iStartIndex])
    {
        sFile += $"{_sDiskMap[iX]}";
        iX--;
    }

    return sFile;
}


static ulong CalculateChecksum(string _sString)
{
    char cEmpty = (char)65000;
    ulong iChecksum = 0;
    int iValue;

    for (int i = 0; i <= _sString.Length - 1; i++)
    {
        if (_sString[i] != cEmpty)
        {
            iValue = (int)_sString[i];
            iChecksum = iChecksum + ((ulong)iValue * (ulong)i);
        }
    }

    return iChecksum;
}

static int CountFileBlocks(string _sData)
{
    int cnt = 0;
    char cEmpty = (char)65000;

    foreach (char c in _sData)
    {
        if ((int)c != (int)cEmpty) cnt++;
    }

    return cnt;
}

static string StringSwap(string _sInput, int _iIndex1, int _iIndex2, int _iLength)
{
    string sOutput = "";

    string sPart1 = _sInput.Substring(_iIndex1, _iLength);
    string sPart2 = _sInput.Substring(_iIndex2, _iLength);

    sOutput += _sInput.Substring(0, _iIndex1) + sPart2;
    sOutput += _sInput.Substring(_iIndex1 + _iLength, _iIndex2 - (_iIndex1 + _iLength)) + sPart1;
    sOutput += _sInput.Substring(_iIndex2 + _iLength);

    return sOutput;
}

static void PrintDiskLayout(string _sData, bool _bPrintHeader = false, string _sComment = "")
{
    string sHeader = "";
    string sData = "";

    if (_bPrintHeader) for (int i = 0; i < _sData.Length; i++) sHeader += $"| {i,5} ";
    for (int i = 0; i < _sData.Length; i++) sData += $"| {(int)_sData[i],5} ";

    if (_sComment != "") sData += $"| {_sComment}";

    if (_bPrintHeader) print(sHeader);
    print(sData);

}

string sFilePath = "data.txt";
// string sFilePath = "testdata.txt";
string sData = ReadFileToString(sFilePath);

int iID = 0;
bool bFile = true;
int iBlockCount;
string sBlocks = "";
char cID;
string sDiskMap = "", sCompressedDiskMap;
ulong iChecksum = 0;
bool bDebug = false;
char cEmpty = (char)65000;

foreach (char c in sData)
{
    iBlockCount = int.Parse($"{c}");
    cID = (char)iID;
    if (bFile) sBlocks = new string(cID, iBlockCount);
    else sBlocks = new string(cEmpty, iBlockCount);
    if (bDebug) print($"id: {iID,4} | {c} | isFile: {bFile,5} | {iID} * {iBlockCount}");
    if (bFile) iID++;
    bFile = !bFile;
    sDiskMap += sBlocks;

    if (bDebug && sDiskMap.Length >= 100) break;

}

sCompressedDiskMap = CompressDiskMap(sDiskMap);
iChecksum = CalculateChecksum(sCompressedDiskMap);

print($"Indata:                {sData.Length}");
print("--------------------------------------------------");
print($"DiskMap:               {sDiskMap.Length}");
print($"DiskMap fileblocks:    {CountFileBlocks(sDiskMap)}");
print($"Compressed DiskMap:    {sCompressedDiskMap.Length}");
print($"Compressed fileblocks: {CountFileBlocks(sCompressedDiskMap)}");
print($"Checksum:              {iChecksum}");