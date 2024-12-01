using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "data.txt";
        // string filePath = "testdata.txt";

        var dataList = ReadFileToList(filePath);

        var data = dataList;
        var lList = new List<int>();
        var rList = new List<int>();
        int part1_sum = 0;

        foreach (string s in data)
        {
            // split line
            string one_line = ReplaceMultipleSpaces(s);
            string[] line_parts = one_line.Split(' ');

            lList.Add(int.Parse(line_parts[0]));
            rList.Add(int.Parse(line_parts[1]));

            // Console.WriteLine(one_line);
        }

        lList.Sort();
        rList.Sort();

        part1_sum = getPart1result(lList, rList);

        Console.WriteLine($"Part 1 result: {part1_sum}");

    }

    static int getPart1result(List<int> _lList, List<int> _rList)
    {
        
        int sum = 0;
        
        for (int ix = 0; ix < _lList.Count; ix++)
        {
            sum += GetDiff(_lList[ix], _rList[ix]);
        }        
        
        return sum;

    }

    static int GetDiff(int _int1, int _int2)
    {
        return Math.Abs(_int2- _int1);
    }

    static string ReplaceMultipleSpaces(string input)
    {
        // Använd Regex för att ersätta multipla mellanslag med ett enda
        // @ Verbatim strin literal -  C# används @ före en sträng för att skapa en verbatim string literal. Detta innebär att specialtecken som \ tolkas bokstavligen, utan att behöva dubbla dem som \\.
        // \s är en regex-klass som representerar alla typer av vittecken. Vittecken = Mellanslag, Tabbar, Radbrytningar och Fromfeeds
        // + är en kvantifierare som betyder "en eller flera"
        return Regex.Replace(input, @"\s+", " ");
    }


    static List<string> ReadFileToList(string filePath)
    {
        // Läs alla rader från filen
        var lines = new List<string>();

        foreach (var line in File.ReadLines(filePath))
        {
            lines.Add(line.Trim());
        }

        return lines;
    }

}