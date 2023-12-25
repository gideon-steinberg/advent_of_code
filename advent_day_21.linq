<Query Kind="Program" />

void Main()
{
	var input1 = @"...........
.....###.#.
.###.##..#.
..#.#...#..
....#.#....
.##..S####.
.##..#...#.
.......##..
.##.#.####.
.##..##.##.
...........";

var input2 = @"";

	var steps = 64;
	
	var parts = input1.Split(Environment.NewLine);
	
	var height = parts.Count();
	var width = parts[0].Count();
	height.Dump();
	width.Dump();
	
	var map = new Point[height, width];
	
	var pointsToVisit = new HashSet<Point>();
	var visitedPoints = new HashSet<Point>();
	
	var i = 0;
	foreach (var line in parts)
	{
		var array = line.ToCharArray();
		for (var j = 0; j < width; j++)
		{
			var p = new Point()
			{
				i = i,
				j = j,
				isPassable = array[j] != '#',
				steps = int.MaxValue
			};
			
			if (array[j] == 'S')
			{
				p.steps = 0;
				pointsToVisit.Add(p);
			}
			map[i,j] = p;
		}
		i++;
	}
	
	while (pointsToVisit.Any(p => p.steps < steps))
	{
		var point = pointsToVisit.OrderBy(p => p.steps).First();
		
		pointsToVisit.Remove(point);
		visitedPoints.Add(point);
		
		if ((point.i - 1) >= 0     && map[point.i - 1,point.j   ].isPassable && !visitedPoints.Contains(map[point.i - 1, point.j    ]))
		{
			map[point.i - 1, point.j    ].steps = point.steps + 1;
			pointsToVisit.Add(map[point.i - 1, point.j    ]);
		}
		if ((point.i + 1) < height && map[point.i + 1,point.j   ].isPassable && !visitedPoints.Contains(map[point.i + 1, point.j    ]))
		{
			map[point.i + 1, point.j    ].steps = point.steps + 1;
			pointsToVisit.Add(map[point.i + 1, point.j    ]);
		}
		if ((point.j - 1) >= 0     && map[point.i    ,point.j - 1].isPassable && !visitedPoints.Contains(map[point.i   , point.j - 1]))
		{
			map[point.i    , point.j - 1].steps = point.steps + 1;
			pointsToVisit.Add(map[point.i    , point.j - 1]);
		}
		if ((point.j + 1) < width  && map[point.i    ,point.j + 1].isPassable && !visitedPoints.Contains(map[point.i   , point.j + 1]))
		{
			map[point.i    , point.j + 1].steps = point.steps + 1;
			pointsToVisit.Add(map[point.i    , point.j + 1]);
		}
	}
	
	decimal sum = 0;
	
	for (i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			if (map[i,j].steps <= steps && map[i,j].steps % 2 == 0) sum++;
		}
	}
	
	//map.Dump();
	
	sum.Dump();
}


public class Point
{
	public int i;
	public int j;
	public int steps = 0;
	public bool isPassable = false;
	public Point pointCameFrom = null;
		
	public override bool Equals(object obj)
	{
	    var item = obj as Point;

	    if (item == null)
	    {
	        return false;
	    }

	    return this.i.Equals(item.i) && this.j.Equals(item.j);
	}

	public override int GetHashCode() => (i, j).GetHashCode();
}