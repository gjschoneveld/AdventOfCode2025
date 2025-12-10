using Point = (long X, long Y);

var input = File.ReadAllLines("input.txt");
var redTiles = input.Select(Parse).ToList();

var candidates = new List<(Point A, Point B)>();

for (int i = 0; i < redTiles.Count; i++)
{
    for (int j = i + 1; j < redTiles.Count; j++)
    {
        candidates.Add((redTiles[i], redTiles[j]));
    }
}

var answer1 = MaxSize(candidates);
Console.WriteLine($"Answer 1: {answer1}");


var xMap = CreateMap(redTiles, t => t.X);
var yMap = CreateMap(redTiles, t => t.Y);
var redTilesMapped = redTiles.Select(t => (X: xMap[t.X], Y: yMap[t.Y])).ToList();

var borderTiles = redTilesMapped.ToHashSet();

for (int i = 0; i < redTilesMapped.Count; i++)
{
    var current = redTilesMapped[i];
    var next = redTilesMapped[(i + 1) % redTilesMapped.Count];

    var dX = Math.Sign(next.X - current.X);
    var dY = Math.Sign(next.Y - current.Y);

    current = (current.X + dX, current.Y + dY);

    while (current != next)
    {
        borderTiles.Add(current);
        current = (current.X + dX, current.Y + dY);
    }
}

var minX = borderTiles.Min(t => t.X) - 1;
var maxX = borderTiles.Max(t => t.X) + 1;
var minY = borderTiles.Min(t => t.Y) - 1;
var maxY = borderTiles.Max(t => t.Y) + 1;

var otherTiles = new HashSet<Point>();
var toVisit = new List<Point> { (minX, minY) };

while (toVisit.Count > 0)
{
    otherTiles.UnionWith(toVisit);

    toVisit = toVisit
        .SelectMany(Neighbours)
        .Distinct()
        .Where(t => minX <= t.X && t.X <= maxX && minY <= t.Y && t.Y <= maxY)
        .Where(t => !otherTiles.Contains(t) && !borderTiles.Contains(t))
        .ToList();
}

candidates = [];

for (int i = 0; i < redTilesMapped.Count; i++)
{
    for (int j = i + 1; j < redTilesMapped.Count; j++)
    {
        if (!HasOtherTile(otherTiles, redTilesMapped[i], redTilesMapped[j]))
        {
            candidates.Add((redTiles[i], redTiles[j]));
        }
    }
}

var answer2 = MaxSize(candidates);
Console.WriteLine($"Answer 2: {answer2}");

Dictionary<long, long> CreateMap(List<Point> points, Func<Point, long> selector)
{
    var values = points.Select(selector).Distinct().Order().ToList();

    var result = new Dictionary<long, long>();

    for (int i = 0; i < values.Count; i++)
    {
        result[values[i]] = 2 * i + 1;
    }

    return result;
}

bool HasOtherTile(HashSet<Point> otherTiles, Point a, Point b)
{
    var minX = Math.Min(a.X, b.X);
    var maxX = Math.Max(a.X, b.X);
    var minY = Math.Min(a.Y, b.Y);
    var maxY = Math.Max(a.Y, b.Y);

    for (long x = minX; x <= maxX; x++)
    {
        if (otherTiles.Contains((x, minY)) || otherTiles.Contains((x, maxY)))
        {
            return true;
        }
    }

    for (long y = minY; y <= maxY; y++)
    {
        if (otherTiles.Contains((minX, y)) || otherTiles.Contains((maxX, y)))
        {
            return true;
        }
    }

    return false;
}

List<Point> Neighbours(Point point)
{
    return
    [
        (point.X - 1, point.Y),
        (point.X + 1, point.Y),
        (point.X, point.Y - 1),
        (point.X, point.Y + 1),
    ];
}

long MaxSize(List<(Point A, Point B)> candidates)
{
    return candidates.Max(c => Size(c.A, c.B));
}

long Size(Point a, Point b)
{
    return (Math.Abs(a.X - b.X) + 1) * (Math.Abs(a.Y - b.Y) + 1);
}

Point Parse(string line)
{
    var values = line.Split(',').Select(long.Parse).ToList();

    return (values[0], values[1]);
}
