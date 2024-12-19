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
        if (_bDebug) sResultList = string.Join(", ", iResultList);
        if (_bDebug) print($"Factor: {int.Parse(sN),3} | Iterations: {iResultList.Count,4} | results: {sResultList}");
        if (iResultList.Contains(iTestValue)) return iTestValue;
    }

    return iReturnValue;

}

static List<ulong> UpdateResultList(List<ulong> _iInputResultList, ulong _iFactor)
{

    List<ulong> iOutputResultList = new List<ulong>();

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

// int r = CalculateString("3267: 81 40 27");
// int r = CalculateString("292: 11 6 16 20", true);

print($"result 07a: {iSum}");