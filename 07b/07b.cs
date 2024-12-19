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

static void print(string sText)
{
    Console.WriteLine(sText);
}

static ulong CalculateString(string _input, bool _bDebug = false)
{

    ulong iReturnValue = 0;
    List<ulong> iResultList = new List<ulong>();

    string[] sSplitInputArray = _input.Split(':');
    string[] sNumbersArray = sSplitInputArray[1].Trim().Split(' ');
    string sResultList = "";
    ulong iTestValue = ulong.Parse(sSplitInputArray[0]);

    foreach (string sN in sNumbersArray)
    {
        iResultList = UpdateResultList(iResultList, ulong.Parse(sN));
        // if (_bDebug) sResultList = string.Join(", ", iResultList);
        if (_bDebug) print($"Factor: {int.Parse(sN),3} | Iterations: {iResultList.Count,8} | results: {sResultList}");
        if (iResultList.Contains(iTestValue)) return iTestValue;
    }

    return iReturnValue;

}

static List<ulong> UpdateResultList(List<ulong> _iInputResultList, ulong _iFactor)
{

    List<ulong> iOutputResultList = new List<ulong>();
    string sResult;
    bool bConcatUsed = false;

    if (_iInputResultList.Count == 0)
    {
        iOutputResultList.Add(_iFactor);
    }
    else
    {
        foreach (ulong i in _iInputResultList)
        {
            iOutputResultList.Add(i + _iFactor);
            iOutputResultList.Add(i * _iFactor);

            sResult = $"{i}{_iFactor}";
            iOutputResultList.Add(ulong.Parse(sResult));
        }
    }

    return iOutputResultList;

}

string sFilePath = "data.txt";
// string sFilePath = "testdata.txt";

List<string> sDataList = ReadFileToList(sFilePath);

ulong iSum = 0;
ulong iResult = 0;

foreach (string sLine in sDataList)
{
    print("----------------------------------------------------------------------------------------------");
    print(sLine);
    iResult = CalculateString(sLine, false);
    iSum += iResult;
    print($"Line result: {iResult} | Acc result: {iSum}");
    print("----------------------------------------------------------------------------------------------");
}
print($"result 07b: {iSum}");

// int r = CalculateString("3267: 81 40 27");
// int r = CalculateString("292: 11 6 16 20", true);
// ulong r = CalculateString("140428093859: 9 197 4 9 2 9 5 4 2 5 55 9", true);

//   1260333054159 from 07a
// 162059698077915 to high
// 162059698077915
