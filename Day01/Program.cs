var input = File.ReadAllLines("input.txt");
var commands = input.Select(Parse).ToList();

var value = 50;
var zeros1 = 0;
var zeros2 = 0;

foreach (var command in commands)
{
    (value, var part1, var part2) = Rotate(value, command.Direction, command.Amount);

    zeros1 += part1;
    zeros2 += part2;
}

Console.WriteLine($"Answer 1: {zeros1}");
Console.WriteLine($"Answer 2: {zeros2}");

(int Value, int Part1, int Part2) Rotate(int value, Direction direction, int amount)
{
    var modulus = 100;
    var zeros = 0;

    for (int i = 0; i < amount; i++)
    {
        value += direction switch
        {
            Direction.Left => -1,
            Direction.Right => 1,
            _ => throw new NotImplementedException()
        };

        if (value < 0)
        {
            value += modulus;
        }

        if (value >= modulus)
        {
            value -= modulus;
        }

        if (value == 0)
        {
            zeros++;
        }
    }

    return (value, value == 0 ? 1 : 0, zeros);
}

(Direction Direction, int Amount) Parse(string line)
{
    var direction = line[0] switch
    {
        'L' => Direction.Left,
        'R' => Direction.Right,
        _ => throw new NotImplementedException()
    };

    var amount = int.Parse(line[1..]);

    return (direction, amount);
}

enum Direction
{
    Left,
    Right
}
