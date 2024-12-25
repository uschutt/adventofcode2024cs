class Program11a
{
    static string __sFilePath = "data.txt";

    static void Main(string[] args)
    {
        string sData = ReadFileToString(__sFilePath);
        List<string> sDataList = sData.Split(' ').ToList();
        int iBliks = 0;

        DateTime dtStartTime = DateTime.Now;

        PrintList(sDataList, iBliks);
        iBliks++;

        while (iBliks <= 25)
        {
            sDataList = RebuildList(sDataList);
            PrintList(sDataList, iBliks);
            iBliks++;
        }

        DateTime dtEndTime = DateTime.Now;

        TimeSpan tsDifference = dtEndTime - dtStartTime;

        print($"Total number of stones after {iBliks - 1} blinks: {sDataList.Count} stones in {tsDifference.TotalSeconds} seconds");

    }

    static void PrintList(List<string> _sInputList, int _iX = 0)
    {
        // string sData = string.Join(' ', _sInputList);
        string sData = "";
        print($"{_iX,2} | {_sInputList.Count,8} | {sData} ");
    }

    static List<string> RebuildList(List<string> _sInputList)
    {
        List<string> sNewDataList = new List<string>();
        int iHalfString;
        ulong iNumber;

        foreach (string s in _sInputList)
        {
            if (s == "0")
            {
                sNewDataList.Add("1");
            }
            else if (s.Length % 2 == 0)
            {
                iHalfString = s.Length / 2;
                sNewDataList.Add(IntifyString(s.Substring(0, iHalfString)));
                sNewDataList.Add(IntifyString(s.Substring(s.Length - iHalfString, iHalfString)));
            }
            else
            {
                iNumber = ulong.Parse(s) * 2024;
                sNewDataList.Add($"{iNumber}");
            }
        }

        return sNewDataList;

    }

    static string IntifyString(string _sInput)
    {
        return $"{ulong.Parse(_sInput)}";
    }

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
}