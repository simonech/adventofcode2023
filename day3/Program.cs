using System;
using System.Net.Mail;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string filePath = "input.txt";

        // Read all lines from the file and store them in an array
        string[] lines = File.ReadAllLines(filePath);

        int accumulator = 0;
        int index = 0;
        List<Part> allNums = new();
        List<Part> allGears = new();
        // Print each line
        foreach (string line in lines)
        {
            var nums = ParseString(line, @"\d+", index);
            var symbols = ParseString(line, @"[*]", index);
            index++;
            allNums.AddRange(nums);
            allGears.AddRange(symbols);

            Console.WriteLine($"{line} - {string.Join(" ", nums)} - {string.Join(" ", symbols)}");
        }

        foreach (var gear in allGears)
        {
            var matches = (from n in allNums
                          where Adjacent(n, gear)
                          select n.Value).ToList();

            if (matches.Count == 2)
            {
                accumulator += matches[0] * matches[1];
            }
        }

        Console.WriteLine($"Total value = {accumulator}");
    }

    private static bool Adjacent(Part s, Part n)
    {
        return
            Math.Abs(s.Row - n.Row) <= 1 &&
            s.Col <= n.Col + n.Text.Length &&
            n.Col <= s.Col + s.Text.Length;
    }

    static IEnumerable<Part> ParseString(string input, string pattern, int row)
    {
        // Use Regex.Match to find all matches in the input string
        MatchCollection matches = Regex.Matches(input, pattern);

        // Iterate through the matches and convert them to integers
        foreach (Match match in matches)
        {

            yield return new Part()
            {
                Text = match.Value,
                Col = match.Index,
                Row = row
            };

        }
    }

}


class Part
{
    public int Row { get; set; }
    public int Col { get; set; }

    public string Text { get; set; }
    public int Value { get => Int32.Parse(Text); }

    public override string ToString()
    {
        return Text + " (" + Row + "," + Col + ")";
    }
}
