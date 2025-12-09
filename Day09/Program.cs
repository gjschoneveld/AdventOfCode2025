using Point = (long X, long Y);

var input = File.ReadAllLines("input.txt");
var redTiles = input.Select(Parse).ToList();

var maxSize = 0L;

for (int i = 0; i < redTiles.Count; i++)
{
    for (int j = i + 1; j < redTiles.Count; j++)
    {
        maxSize = Math.Max(maxSize, Size(redTiles[i], redTiles[j]));
    }
}

Console.WriteLine($"Answer 1: {maxSize}");

long Size(Point a, Point b)
{
    return (Math.Abs(a.X - b.X) + 1) * (Math.Abs(a.Y - b.Y) + 1);
}

Point Parse(string line)
{
    var values = line.Split(',').Select(long.Parse).ToList();

    return (values[0], values[1]);
}
