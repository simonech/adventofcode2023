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

        // Print each line
        foreach (string line in lines)
        {
            var lineParts = line.Split(':', '|');
            var winning = Regex.Matches(lineParts[1], @"\d+").Select(m => m.Value);
            var have = Regex.Matches(lineParts[2], @"\d+").Select(m => m.Value);
            var winningCards = winning.Intersect(have).Count();
            double points = 0.0;
            if(winningCards>0)
            {
                points = Math.Pow(2, winningCards - 1);
            }
            
            Console.WriteLine($"{line} - {winningCards} - {points}");
            accumulator += (int)points;
        }


        Console.WriteLine($"Total value = {accumulator}");
    }



}

