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
        List<int> cards = new();

        // Print each line
        foreach (string line in lines)
        {
            var lineParts = line.Split(':', '|');
            var winning = Regex.Matches(lineParts[1], @"\d+").Select(m => m.Value);
            var have = Regex.Matches(lineParts[2], @"\d+").Select(m => m.Value);
            var winningCards = winning.Intersect(have).Count();

            Console.WriteLine($"{line} - {winningCards}");
            cards.Add(winningCards);
        }

        var counts = cards.Select(c => 1).ToList();

        for (int i = 0; i < cards.Count; i++)
        {
            var card = cards[i];
            var count = counts[i];
            for (int j = 0; j < card; j++)
            {
                counts[i + j + 1] += count;
            }
        }

        Console.WriteLine($"{string.Join(" ", cards)}");
        Console.WriteLine($"{string.Join(" ", counts)}");

        Console.WriteLine($"Total value = {counts.Sum()}");
    }



}

