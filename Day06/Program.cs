var input = File.ReadAllLines("input.txt");
var numbers = Transpose(input[..^1].Select(ParseNumbers).ToList());
var operations = ParseOperations(input[^1]);

var answer1 = Calculate(numbers, operations);
Console.WriteLine($"Answer 1: {answer1}");

numbers = ParseVerticalNumbers(input[..^1]);
var answer2 = Calculate(numbers, operations);
Console.WriteLine($"Answer 2: {answer2}");

long Calculate(List<List<long>> numbers, List<Operation> operations)
{
    var result = 0L;

    for (var i = 0; i < operations.Count; i++)
    {
        result += numbers[i].Aggregate((a, b) =>
            operations[i] switch
            {
                Operation.Add => a + b,
                Operation.Multiply => a * b,
                _ => throw new NotImplementedException()
            }
        );
    }

    return result;
}

List<List<long>> ParseVerticalNumbers(string[] input)
{
    var result = new List<List<long>>();
    var current = new List<long>();

    var columns = input.Max(x => x.Length);

    for (int column = 0; column < columns; column++)
    {
        var digits = new List<char>();

        for (int row = 0; row < input.Length; row++)
        {
            if (column < input[row].Length && char.IsDigit(input[row][column]))
            {
                digits.Add(input[row][column]);
            }
        }

        if (digits.Count == 0)
        {
            result.Add(current);
            current = [];

            continue;
        }

        current.Add(long.Parse(new string([.. digits])));
    }

    result.Add(current);

    return result;
}

List<List<long>> Transpose(List<List<long>> numbers)
{
    var result = new List<List<long>>();

    for (int i = 0; i < numbers[0].Count; i++)
    {
        var current = new List<long>();

        foreach (var row in numbers)
        {
            current.Add(row[i]);
        }

        result.Add(current);
    }

    return result;
}

List<long> ParseNumbers(string line)
{
    return line
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(long.Parse)
        .ToList();
}

List<Operation> ParseOperations(string line)
{
    return line
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(x => x switch {
            "+" => Operation.Add,
            "*" => Operation.Multiply,
            _ => throw new NotImplementedException()
        })
        .ToList();
}

enum Operation
{
    Add,
    Multiply
}
