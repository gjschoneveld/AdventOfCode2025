var input = File.ReadAllLines("input.txt");
var banks = input.Select(Parse).ToList();

var answer1 = banks.Sum(FindLargestJoltage);
Console.WriteLine($"Answer 1: {answer1}");

var answer2 = banks.Sum(b => FindLargestJoltageRecursive(b, 12));
Console.WriteLine($"Answer 2: {answer2}");

int FindLargestJoltage(List<int> values)
{
    var firstDigit = values[..^1].Max();
    var firstIndex = values.IndexOf(firstDigit);

    var secondDigit = values[(firstIndex + 1)..].Max();

    return firstDigit * 10 + secondDigit;
}

long FindLargestJoltageRecursive(List<int> values, int digits)
{
    if (digits == 0)
    {
        return 0; 
    }

    var remainderDigits = digits - 1;
    var digit = values[..^remainderDigits].Max();
    var index = values.IndexOf(digit);

    var remainderValues = values[(index + 1)..];
    var remainderResult = FindLargestJoltageRecursive(remainderValues, remainderDigits);

    return digit * Power10(remainderDigits) + remainderResult;
}

long Power10(int value)
{
    if (value == 0)
    {
        return 1;
    }

    return 10 * Power10(value - 1);
}

List<int> Parse(string line) => [.. line.Select(c => c - '0')];
