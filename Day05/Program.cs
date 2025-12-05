using Range = (long From, long To);

var input = File.ReadAllLines("input.txt");
(var ranges, var values) = Parse(input);

var answer1 = values.Count(v => ranges.Any(r => r.From <= v && v <= r.To));
Console.WriteLine($"Answer 1: {answer1}");

ranges = MergeRanges(ranges);
var answer2 = ranges.Sum(Count);
Console.WriteLine($"Answer 2: {answer2}");

List<Range> MergeRanges(List<Range> ranges)
{
    var result = new List<Range>();

    foreach (var range in ranges)
    {
        var overlapping = result.Where(r => r.To >= range.From && r.From <= range.To).Append(range).ToList();

        result.RemoveAll(overlapping.Contains);
        result.Add((overlapping.Min(r => r.From), overlapping.Max(r => r.To))); 
    }
    
    return result;
}

long Count(Range range)
{
    return range.To - range.From + 1;
}

(List<Range> Ranges, List<long> Values) Parse(string[] lines)
{
    var ranges = lines
        .TakeWhile(line => line != "")
        .Select(line => line.Split("-"))
        .Select(parts => (From: long.Parse(parts[0]), To: long.Parse(parts[1])))
        .ToList();
    
    var values = lines.Skip(ranges.Count + 1).Select(long.Parse).ToList();
    
    return (ranges, values);
}
