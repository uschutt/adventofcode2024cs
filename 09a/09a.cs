
using System;

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
    string sOutputDiskMap = "";
    int ixf, ixb;
    char cEmpty = (char)65000;
    int iFileBlockCount = CountFileBlocks(_sInputDiskMap);

    ixf = 0; // from the front
    ixb = _sInputDiskMap.Length - 1; // from the back

    while (sOutputDiskMap.Length < iFileBlockCount)
    {

        if ((int)_sInputDiskMap[ixf] != (int)cEmpty)
        {
            sOutputDiskMap += _sInputDiskMap[ixf];
        }
        else
        {
            while ((int)_sInputDiskMap[ixb] == (int)cEmpty) ixb--;

            sOutputDiskMap += _sInputDiskMap[ixb];
            ixb--;
        }

        ixf++;

    }
    return sOutputDiskMap;
}

static ulong CalculateChecksum(string _sString)
{
    ulong iChecksum = 0;
    int iValue;

    for (int i = 0; i <= _sString.Length - 1; i++)
    {
        iValue = (int)_sString[i];
        iChecksum = iChecksum + ((ulong)iValue * (ulong)i);

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
// 6604426148532 to low
// 6607783130715 to high
// 6607511583593 is the correct answer