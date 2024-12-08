using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

static string ReadFileToString(string _sFilePath)
{
    string sReturnValue = "";

    foreach (string line in File.ReadLines(_sFilePath))
    {
        sReturnValue += line.Trim();
    }

    return sReturnValue;
}

static List<(int, int)> ExtractPairs(string _sInput, string _sPattern)
{
    // Regular expression to match the pattern (n1,n2) where n1 and n2 are 1-3 digits


    // Create a regex object
    Regex regex = new Regex(_sPattern);

    // List to store the extracted pairs
    List<(int, int)> pairs = new List<(int, int)>();

    // Match the pattern in the input string
    MatchCollection matches = regex.Matches(_sInput);

    foreach (Match match in matches)
    {
        // Extract n1 and n2 from the match
        int n1 = int.Parse(match.Groups[1].Value);
        int n2 = int.Parse(match.Groups[2].Value);

        // Add the pair to the list
        pairs.Add((n1, n2));
    }

    return pairs;
}


string filePath = "data.txt";
string sData = ReadFileToString(filePath);

// string sData = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

sData = $"do(){sData}";

if (sData.Contains('|')) Console.WriteLine("The string contains '|'."); else Console.WriteLine("The string does NOT contains '|'.");

sData = sData.Replace("do()", "|do()");
sData = sData.Replace("don't()", "|don't()");

List<string> sDataList = sData.Split('|').ToList();

Console.Clear();

int ix = 0;

foreach (string part in sDataList)
{
    ix++;
    Console.WriteLine($"{ix,3} | {part}");
}


// string sPattern = @"mul\((\d{1,3}),(\d{1,3})\)";

// List<(int, int)> iMulsList = ExtractPairs(sData, sPattern);

// int iSum = 0;
// int iFactor1 = 1;
// int iFactor2 = 1;
// int iProduct = 0;
// int ix = 0;

// foreach (var mul in iMulsList)
// {

//     iFactor1 = mul.Item1;
//     iFactor2 = mul.Item2;
//     iProduct = iFactor1 * iFactor2;
//     iSum += iProduct;
//     ix++;

//     Console.WriteLine($"{ix,3} | {iFactor1,3} * {iFactor2,3} = {iProduct,7} | accSum: {iSum,9}");
// }

