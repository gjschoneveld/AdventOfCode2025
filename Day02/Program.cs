var input = File.ReadAllText("input.txt");
var ranges = input.Split(',').Select(Parse).ToList();

var answer1 = ranges.SelectMany(r => FindInvalid(r, IsInvalid1)).Sum();
Console.WriteLine($"Answer 1: {answer1}");

var answer2 = ranges.SelectMany(r => FindInvalid(r, IsInvalid2)).Sum();
Console.WriteLine($"Answer 2: {answer2}");

bool IsInvalid1(long value)
{
    var str = value.ToString();
    var half = str.Length / 2;

    return str[..half] == str[half..];
}

bool IsInvalid2(long value)
{
    var str = value.ToString();
    var half = str.Length / 2;

    for (var size = 1; size <= half; size++)
    {
        if (str.Chunk(size).Select(chunk => new string(chunk)).Distinct().Count() == 1)
        {
            return true; 
        }
    }

    return false;
}

List<long> FindInvalid((long From, long To) range, Func<long, bool> isInvalid)
{
    var result = new List<long>();

    for (long id = range.From; id <= range.To; id++)
    {
        if (isInvalid(id))
        {
            result.Add(id);
        }
    }
    
    return result;
}

(long From, long To) Parse(string range)
{
    var parts = range.Split('-');

    return (long.Parse(parts[0]), long.Parse(parts[1]));
}
