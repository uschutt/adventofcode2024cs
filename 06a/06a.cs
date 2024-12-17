// direction up     ^ (-1, 0 )
// direction right  > ( 0, 1 )
// direction down   v ( 1, 0 )
// direction left   < ( 0,-1 )
// x = row
// y = col

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

static string GetStartPosition(List<string> _sMap)
{
    int x_max = _sMap.Count;
    int y_max = _sMap[0].Length;
    char cDirection;

    for (int x = 0; x < x_max; x++)
    {
        for (int y = 0; y < y_max; y++)
        {
            if (_sMap[x][y] != '#' && _sMap[x][y] != '.')
            {
                cDirection = _sMap[x][y];
                return $"{x}|{y}|{cDirection}";
            }
        }
    }

    return "";
}

static void print(string sText)
{
    Console.WriteLine(sText);
}

static bool CompareDirection(int[] _iDirectionArray1, int[] _iDirectionArray2)
{
    return (_iDirectionArray1[0] == _iDirectionArray2[0] && _iDirectionArray1[1] == _iDirectionArray2[1]);
}

static int[] ChangeDirection(int[] _iDirectionInput)
{
    if (CompareDirection(_iDirectionInput, [-1, 0])) // up
    {
        return [0, 1]; // right
    }
    else if (CompareDirection(_iDirectionInput, [0, 1])) // right
    {
        return [1, 0]; // down
    }
    else if (CompareDirection(_iDirectionInput, [1, 0])) // down
    {
        return [0, -1]; // left
    }
    else
    {
        return [-1, 0]; // up
    }
}

static int[] AddPositionToList(List<int[]> _iVisitedPositions)
{
    _iVisitedPositions.Add([7, 7]);
    return [7, 7];
}

// string sFilePath = "data.txt";
string sFilePath = "testdata.txt";

List<string> sDataList = ReadFileToList(sFilePath);
List<int[]> iVisitedPositions = new List<int[]>();
string sStartPosition;
string[] sStartPositionArray;
int[] iCurrentPosition;

foreach (string sLine in sDataList)
{
    Console.WriteLine(sLine);
}

sStartPosition = GetStartPosition(sDataList);
print(sStartPosition);

sStartPositionArray = sStartPosition.Split('|');

iCurrentPosition = [int.Parse(sStartPositionArray[0]), int.Parse(sStartPositionArray[1])];
iVisitedPositions.Add(iCurrentPosition);

iCurrentPosition = AddPositionToList(iVisitedPositions);

print($"result 06a: {iVisitedPositions.Count}");