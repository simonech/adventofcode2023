using System;
using System.Net.Mail;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string filePath = "input.txt";

        // Read all lines from the file and store them in an array
        string[] lines = File.ReadAllLines(filePath);

        var seeds = ParseNumbers(lines[0]);
        Console.WriteLine($"{string.Join(" ", seeds)}");
        Dictionary<string, List<Map>> maps = new();
        // Print each line
        lines[0]=String.Empty;
        var currentMap = String.Empty;
        long aggregate = long.MaxValue;
        foreach (string line in lines)
        {
            if(line.Contains("map"))
            {
                currentMap = line.Remove(line.Length-5);
                maps.Add(currentMap,new List<Map>());
                Console.WriteLine(currentMap);
            }
            else if(string.IsNullOrEmpty(line))
            {
                continue;
            }
            else
            {
                maps[currentMap].Add(GetRange(line));
            }
        }

        foreach (var seed in seeds)
        {
            Console.WriteLine($"Seed: {seed}");
            var input = seed;
            foreach (var map in maps)
            {
                input = ExecuteMapping(map.Value,input);

            }
            aggregate = Math.Min(aggregate, input);
            Console.WriteLine($"Final: {input}");
            Console.WriteLine($"Min: {aggregate}");
        }
    }

    private static long ExecuteMapping(List<Map> maps, long input)
    {
        foreach (var map in maps)
        {
            if(IsBetween(input, map.Source))
            {
                return map.Destination.Start + (input-map.Source.Start);
            }
        }
        return input;
    }

    static bool IsBetween(long number, Range range)
    {
        return number >= range.Start && number <= range.End;
    }

    private static Map GetRange(string line)
    {
        var numbers = ParseNumbers(line).ToList();
        return new Map()
        {
            Destination = new Range(numbers[0],numbers[2]),
            Source = new Range(numbers[1],numbers[2]),
        };
    }

    private static IEnumerable<long> ParseNumbers(string input) =>
        from m in Regex.Matches(input, @"\d+") select long.Parse(m.Value);
}

class Range
{
    public Range(long start, long length)
    {
        Start = start;
        End = start + length - 1;
    }
    public long Start { get; set; }
    public long End { get; set; }
}

class Map 
{
    public Range Source { get; set; }
    public Range Destination { get; set; }
}
