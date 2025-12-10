var input = File.ReadAllLines("input.txt");
var machines = input.Select(Parse).ToList();

var answer1 = machines.Sum(m => PressesNeeded(m.Lights, m.Buttons));
Console.WriteLine($"Answer 1: {answer1}");

int PressesNeeded(List<bool> lights, List<List<int>> buttons)
{
    var presses = 0;

    while (true)
    {
        var sequences = GenerateSequences(buttons.Count, presses);

        foreach (var sequence in sequences)
        {
            var current = Enumerable.Repeat(false, lights.Count).ToList();

            for (int buttonIndex = 0; buttonIndex < buttons.Count; buttonIndex++)
            {
                if (!sequence[buttonIndex])
                {
                    continue;
                }

                foreach (var lightIndex in buttons[buttonIndex])
                {
                    current[lightIndex] = !current[lightIndex];
                }
            }

            if (lights.SequenceEqual(current))
            {
                return presses;
            }
        }

        presses++;
    }
}

List<List<bool>> GenerateSequences(int count, int trues)
{
    if (trues == 0)
    {
        return [[.. Enumerable.Repeat(false, count)]];
    }

    if (trues == count)
    {
        return [[.. Enumerable.Repeat(true, count)]];
    }

    var innerWhenFalse = GenerateSequences(count - 1, trues);
    var innerWhenTrue = GenerateSequences(count - 1, trues - 1);

    foreach (var inner in innerWhenFalse)
    {
        inner.Add(false);
    }

    foreach (var inner in innerWhenTrue)
    {
        inner.Add(true);
    }

    return [.. innerWhenFalse, .. innerWhenTrue];
}

(List<bool> Lights, List<List<int>> Buttons, List<int> Joltages) Parse(string line)
{
    var parts = line.Split(' ');

    var lights = parts[0]
        .Trim('[', ']')
        .Select(c => c == '#')
        .ToList();

    var buttons = parts[1..^1]
        .Select(x => x
            .Trim('(', ')')
            .Split(',')
            .Select(int.Parse)
            .ToList()
        )
        .ToList();

    var joltages = parts[^1]
        .Trim('{', '}')
        .Split(',')
        .Select(int.Parse)
        .ToList();

    return (lights, buttons, joltages);
}
