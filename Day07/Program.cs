using Point = (int X, int Y);

var input = File.ReadAllLines("input.txt");
var start = input[0].IndexOf('S');
var splitters = FindSplitters(input);

var startNode = new Node();

// Key: X location, Value: parents
var beams = new Dictionary<int, List<Node>>
{
	[start] = [startNode]
};

for (int y = 0; y < input.Length; y++)
{
    var reached = beams
		.Keys
		.Select(x => (X: x, Y: y))
		.Where(splitters.Contains)
		.Select(p => p.X)
		.ToList();

	foreach (var x in reached)
	{
		var child = new Node();

		foreach (var parent in beams[x])
		{
			parent.Children.Add(child);
		}

        beams.TryAdd(x - 1, []);
        beams.TryAdd(x + 1, []);
        beams[x - 1].Add(child);
        beams[x + 1].Add(child);
        beams.Remove(x);
    }
}

// Take first splitter as start node
startNode = startNode.Children[0];

var answer1 = startNode.CountNodes();
Console.WriteLine($"Answer 1: {answer1}");

var answer2 = startNode.CountPaths();
Console.WriteLine($"Answer 2: {answer2}");

HashSet<Point> FindSplitters(string[] input)
{
	var result = new HashSet<Point>();

    for (int y = 0; y < input.Length; y++)
	{
		for (int x = 0; x < input[y].Length; x++)
		{
			if (input[y][x] == '^')
			{
				result.Add((x, y));
            }
		}
	}

	return result;
}

class Node
{
	bool _counted;
    long? _paths;

    public int CountNodes()
    {
		if (_counted)
		{
			return 0;
		}

		_counted = true;

        return 1 + Children.Sum(n => n.CountNodes());
    }

    public long CountPaths()
	{
		if (_paths == null)
		{
			var leafNodes = 2 - Children.Count;
           _paths = leafNodes + Children.Sum(n => n.CountPaths());
        }

		return (long)_paths;
	}

	public List<Node> Children { get; set; } = [];
}
