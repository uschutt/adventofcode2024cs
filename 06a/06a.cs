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

static void AddPositionToList(List<string> _sVisitedPositionsList, int[] _iNewPositionArray)
{
    string sNewPosition = string.Join("|", _iNewPositionArray);

    if (!_sVisitedPositionsList.Contains(sNewPosition)) _sVisitedPositionsList.Add(sNewPosition);
}

string sFilePath = "data.txt";
// string sFilePath = "testdata.txt";

List<string> sMapList = ReadFileToList(sFilePath);
List<string> sVisitedPositionsList = new List<string>();
string sStartPosition;
string[] sStartPositionArray;
int[] iCurrentPositionArray;
int[] iCurrentDirectionArray;
int x_max = sMapList.Count;
int y_max = sMapList[0].Length;
int x;
int y;

print($"x_max: {x_max} | y_max: {y_max}");

// foreach (string sLine in sMapList) Console.WriteLine(sLine);

sStartPosition = GetStartPosition(sMapList);
print(sStartPosition);

sStartPositionArray = sStartPosition.Split('|');

iCurrentPositionArray = [int.Parse(sStartPositionArray[0]), int.Parse(sStartPositionArray[1])];

AddPositionToList(sVisitedPositionsList, iCurrentPositionArray);

x = iCurrentPositionArray[0];
y = iCurrentPositionArray[1];
iCurrentDirectionArray = [-1, 0];

while (x < x_max && y < y_max && x >= 0 && y >= 0)
{
    print($"x: {x,3} | y: {y,3}");
    if (sMapList[x][y] == '#')
    {
        // print($"x: {x,3} | y: {y,3} | turn");
        // we are on the obsticle - back one step
        x = x - iCurrentDirectionArray[0];
        y = y - iCurrentDirectionArray[1];
        // ... and change direction
        iCurrentDirectionArray = ChangeDirection(iCurrentDirectionArray);
    }
    else
    {
        AddPositionToList(sVisitedPositionsList, [x, y]);
        x = x + iCurrentDirectionArray[0];
        y = y + iCurrentDirectionArray[1];
    }
}


print($"result 06a: {sVisitedPositionsList.Count}");