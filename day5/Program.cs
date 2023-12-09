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

        var seedRanges = ParseSeeds(lines[0]);
        Dictionary<string, List<Map>> maps = new();
        // Print each line
        lines[0] = String.Empty;
        var currentMap = String.Empty;
        long aggregate = long.MaxValue;
        foreach (string line in lines)
        {
            if (line.Contains("map"))
            {
                currentMap = line.Remove(line.Length - 5);
                maps.Add(currentMap, new List<Map>());
            }
            else if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            else
            {
                maps[currentMap].Add(GetRange(line));
            }
        }

        foreach (var seedRange in seedRanges)
        {
            Console.WriteLine($"Seed: {seedRange.Start}");
            for (long i = seedRange.Start; i <= seedRange.End; i++)
            {
                
                var input = i;
                foreach (var map in maps)
                {
                    input = ExecuteMapping(map.Value, input);

                }
                aggregate = Math.Min(aggregate, input);
                //Console.WriteLine($"Final: {input}");
            }
            Console.WriteLine($"Min: {aggregate}");

        }
    }

    private static IEnumerable<Range> ParseSeeds(string line)
    {
        var seeds = ParseNumbers(line);
        return seeds.Chunk(2).Select(s => new Range(s[0], s[1]));
        //return seeds.Select(s => new Range(s, 1));
    }

    private static long ExecuteMapping(List<Map> maps, long input)
    {
        foreach (var map in maps)
        {
            if (IsBetween(input, map.Source))
            {
                return map.Destination.Start + (input - map.Source.Start);
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
            Destination = new Range(numbers[0], numbers[2]),
            Source = new Range(numbers[1], numbers[2]),
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
