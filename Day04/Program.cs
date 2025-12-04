using Point = (int X, int Y);

var input = File.ReadAllLines("input.txt");
var paper = Parse(input);

var startCount = paper.Count;
paper = paper.Where(p => Neighbours(p).Count(paper.Contains) >= 4).ToHashSet();

var answer1 = startCount - paper.Count;
Console.WriteLine($"Answer 1: {answer1}");

var previousCount = -1;

while (paper.Count != previousCount)
{
    previousCount = paper.Count;
    paper = paper.Where(p => Neighbours(p).Count(paper.Contains) >= 4).ToHashSet();
}

var answer2 = startCount - paper.Count;
Console.WriteLine($"Answer 2: {answer2}");

List<Point> Neighbours(Point point)
{
    return
    [
        (point.X - 1, point.Y - 1),
        (point.X, point.Y - 1),
        (point.X + 1, point.Y - 1),
        (point.X - 1, point.Y),
        (point.X + 1, point.Y),
        (point.X - 1, point.Y + 1),
        (point.X, point.Y + 1),
        (point.X + 1, point.Y + 1),
    ];
}

HashSet<Point> Parse(string[] input)
{
    var result = new HashSet<Point>();
    
    for (int y = 0; y < input.Length; y++)
    {
        for (int x = 0; x < input[y].Length; x++)
        {
            if (input[y][x] == '@')
            {
                result.Add((x, y));
            }
        }
    }
    
    return result;
}
