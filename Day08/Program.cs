using Point = (long X, long Y, long Z);

var input = File.ReadAllLines("input.txt");
var boxes = input.Select(Parse).ToList();

var connections = new List<Connection>();

for (int i = 0; i < boxes.Count; i++)
{
    for (int j = i + 1; j < boxes.Count; j++)
    {
        connections.Add(new Connection(boxes[i], boxes[j]));
    }
}

connections.Sort((a, b) => Math.Sign(a.DistanceSquared - b.DistanceSquared));

var circuits = boxes.Select(b => new HashSet<Point> { b }).ToHashSet();

// Needs to be 10 for example
for (int i = 0; i < 1000; i++)
{
    var circuitA = circuits.First(c => c.Contains(connections[i].A));
    var circuitB = circuits.First(c => c.Contains(connections[i].B));

    if (circuitA == circuitB)
    {
        continue;
    }

    circuits.Remove(circuitA);
    circuits.Remove(circuitB);
    circuits.Add([.. circuitA, .. circuitB]);
}

var answer1 = circuits.Select(c => c.Count).OrderDescending().Take(3).Aggregate((a, b) => a * b);
Console.WriteLine($"Answer 1: {answer1}");


circuits = boxes.Select(b => new HashSet<Point> { b }).ToHashSet();
int index = 0;

while (circuits.Count > 1)
{
    var circuitA = circuits.First(c => c.Contains(connections[index].A));
    var circuitB = circuits.First(c => c.Contains(connections[index].B));
    index++;

    if (circuitA == circuitB)
    {
        continue;
    }

    circuits.Remove(circuitA);
    circuits.Remove(circuitB);
    circuits.Add([.. circuitA, .. circuitB]);
}

var last = connections[index - 1];
var answer2 = last.A.X * last.B.X;
Console.WriteLine($"Answer 2: {answer2}");

Point Parse(string line)
{
    var values = line.Split(',').Select(long.Parse).ToList();

    return (values[0], values[1], values[2]);
}

class Connection
{
    public Point A { get; }
    public Point B { get; }
    public long DistanceSquared { get; }

    public Connection(Point a, Point b)
    {
        A = a;
        B = b;

        var dX = a.X - b.X;
        var dY = a.Y - b.Y;
        var dZ = a.Z - b.Z;
        DistanceSquared = dX * dX + dY * dY + dZ * dZ;
    }
}
