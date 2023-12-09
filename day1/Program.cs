using System;

class Program
{

        static Dictionary<string, int> digitMapping = new Dictionary<string, int>
        {
            {"one", 1},
            {"two", 2},
            {"three", 3},
            {"four", 4},
            {"five", 5},
            {"six", 6},
            {"seven", 7},
            {"eight", 8},
            {"nine", 9},
        };

    static void Main()
    {

        string filePath = "input.txt";

        // Read all lines from the file and store them in an array
        string[] lines = File.ReadAllLines(filePath);

        int accumulator = 0;
        // Print each line
        foreach (string line in lines)
        {
            var listOfDigits = ConvertStringToNumbers(line);
            var first = listOfDigits[0];
            var last = listOfDigits[listOfDigits.Count - 1];
            var value = Int32.Parse(first+""+last);
            Console.WriteLine($"{line}: {string.Join(" ", listOfDigits)} - {first}{last}, {value}");
            accumulator+=value;

        }
        Console.WriteLine($"Total value = {accumulator}");
    }

    private static List<int> ConvertStringToNumbers(string line)
    {
        List<int> list = new();
        while (!String.IsNullOrEmpty(line))
        {
            char firstChar = line[0];
            if(char.IsDigit(firstChar))
            {
                list.Add(Int32.Parse(firstChar+""));
            }
            else 
            {
                int value = StartsWithASpelledOutDigit(line);
                if(value!=0)
                {
                    list.Add(value);
                }
            }
            line = line.Remove(0,1);
        }
        return list;
    }

    private static int StartsWithASpelledOutDigit(string line)
    {
        foreach (var item in digitMapping)
        {
            if(line.StartsWith(item.Key))
            {
                return item.Value;
            }
        }
        return 0;
    }
}