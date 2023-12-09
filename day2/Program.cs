using System;
using System.Net.Mail;

class Program
{
    static void Main()
    {
        string filePath = "input.txt";

        // Read all lines from the file and store them in an array
        string[] lines = File.ReadAllLines(filePath);

        int accumulator = 0;
        // Print each line
        foreach (string line in lines)
        {
            Game game = ParseGame(line);
            Console.WriteLine($"{line} - {game.GameID}");
            int minGreen = 0;
            int minBlue = 0;
            int minRed = 0;
            foreach (var draw in game.Draws)
            {
                minGreen = Math.Max(minGreen, draw.Green);
                minBlue = Math.Max(minBlue, draw.Blue);
                minRed = Math.Max(minRed, draw.Red);
            }
            Console.WriteLine($"Min Green {minGreen}");
            Console.WriteLine($"Min Blue {minBlue}");
            Console.WriteLine($"Min Red {minRed}");
            var power = minBlue * minGreen * minRed;
            Console.WriteLine($"power {power}");
            accumulator+=power;
        }
        Console.WriteLine($"Total value = {accumulator}");
    }

    private static Game ParseGame(string line)
    {
        Game result = new Game();
        var gameIdValue = line.Split(":");
        result.GameID = Int32.Parse(gameIdValue[0].Replace("Game ", ""));
        var draws = gameIdValue[1].Split(";");
        foreach (var draw in draws)
        {
            var drawObj = new Draw();
            var colors = draw.Split(",");
            foreach (var color in colors)
            {
                var keyvalue = color.Trim().Split(" ");
                var colorName = keyvalue[1];
                var colorNum = Int32.Parse(keyvalue[0]);

                switch (colorName)
                {
                    case "red":
                        drawObj.Red = colorNum;
                        break;
                    case "green":
                        drawObj.Green = colorNum;
                        break;
                    case "blue":
                        drawObj.Blue = colorNum;
                        break;
                }
            }
            result.Draws.Add(drawObj);
        }
        return result;
    }
}

class Game
{
    public int GameID { get; set; }
    public List<Draw> Draws { get; set; } = new();
}

class Draw
{
    public int Blue { get; set; }
    public int Red { get; set; }
    public int Green { get; set; }
}