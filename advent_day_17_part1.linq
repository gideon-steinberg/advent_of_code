<Query Kind="Program" />

void Main()
{
	var input1 = 
@"2413432311323
3215453535623
3255245654254
3446585845452
4546657867536
1438598798454
4457876987766
3637877979653
4654967986887
4564679986453
1224686865563
2546548887735
4322674655533";

var input2 = @"";

	var parts = input1.Split(Environment.NewLine);
	var height = parts.Count();
	var width = parts[0].Count();
	var map = new int[height,width];
	
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			map[i,j] = int.Parse(parts[i][j] + "");
		}
	}
	
	var pointsToProcess = new Dictionary<Point, Point>();
	var startp = new Point{ i = 0, j = 0, cost = 0};
	var p = new Point{ i = 0, j = 0, cost = 0};
	pointsToProcess.Add(p, p);
	
	var NUMBEROFSTEPSALLOWED = 3;
	
	var pointsProcessed = new Dictionary<Point, Point>();
	
	while (pointsToProcess.Any())
	{
		var smallestPoint = pointsToProcess.Keys.OrderBy( p => p.cost).First();
		pointsProcessed.Add(smallestPoint, smallestPoint);
		
		if (smallestPoint.i == (height - 1) && smallestPoint.j == (width -1)) break;
		
		var points = new List<Point>();
		
		// down i + 1
		if (smallestPoint.i + 1 < height && smallestPoint.direction != Direction.Up)
		{
			var p1 = new Point
			{
				i = smallestPoint.i + 1,
				j = smallestPoint.j,
				direction =  Direction.Down,
				cost =  smallestPoint.cost + map[smallestPoint.i + 1, smallestPoint.j],
				steps = smallestPoint.direction == Direction.Down ? smallestPoint.steps + 1 : 1,
			};
			points.Add(p1);
		}
		
		// up i - 1
		if (smallestPoint.i - 1 >= 0 && smallestPoint.direction != Direction.Down)
		{
			var p1 = new Point
			{
				i = smallestPoint.i - 1,
				j = smallestPoint.j,
				direction =  Direction.Up,
				cost =  smallestPoint.cost + map[smallestPoint.i - 1, smallestPoint.j],
				steps = smallestPoint.direction == Direction.Up ? smallestPoint.steps + 1 : 1,
			};
			points.Add(p1);
		}
			
		// right j - 1
		if (smallestPoint.j + 1 < width && smallestPoint.direction != Direction.Left)
		{
			var p1 = new Point
			{
				i = smallestPoint.i,
				j = smallestPoint.j + 1,
				direction = Direction.Right,
				cost =  smallestPoint.cost + map[smallestPoint.i, smallestPoint.j + 1],
				steps = smallestPoint.direction == Direction.Right ? smallestPoint.steps + 1 : 1,
			};
			points.Add(p1);
		}
		
		// left j - 1
		if (smallestPoint.j - 1 >= 0 && smallestPoint.direction != Direction.Right)
		{
			var p1 = new Point
			{
				i = smallestPoint.i,
				j = smallestPoint.j - 1,
				direction =  Direction.Left,
				cost =  smallestPoint.cost + map[smallestPoint.i, smallestPoint.j - 1],
				steps = smallestPoint.direction == Direction.Left ? smallestPoint.steps + 1 : 1,
			};
			points.Add(p1);
		}
		
		foreach (var point in points.Where( p => p.steps <= NUMBEROFSTEPSALLOWED))
		{
			if (point.i < 0) continue;
			if (point.i >= height) continue;
			if (point.j < 0) continue;
			if (point.j >= width) continue;
			
			if (pointsToProcess.ContainsKey(point))
			{
				if (pointsToProcess[point].cost > point.cost)
				{
					pointsToProcess[point].direction = point.direction;
					pointsToProcess[point].cost = point.cost;
//					pointsToProcess[point].pointCameFrom = smallestPoint;
				}
				else
				{
					continue;
				}
			}
			else
			{
//				point.pointCameFrom = smallestPoint;
				if (!pointsProcessed.ContainsKey(point))
				{
					pointsToProcess.Add(point, point);
				}
			}
		}
		pointsToProcess.Remove(smallestPoint);
	}
	
	var endPointsWithCost = pointsProcessed.Keys.Where( p => p.i == (height - 1) && p.j == (width -1)).OrderBy(p => p.cost);
	endPointsWithCost.Dump();
}

public class Point
{
	public int i;
	public int j;
	public Direction direction;
	public int steps = 0;
	public int cost = Int32.MaxValue;
	public Point pointCameFrom = null;
		
	public override bool Equals(object obj)
	{
	    var item = obj as Point;

	    if (item == null)
	    {
	        return false;
	    }

	    return this.i.Equals(item.i) && this.j.Equals(item.j) && this.steps.Equals(item.steps) && this.direction.Equals(item.direction);
	}

	public override int GetHashCode() => (i, j, steps, direction).GetHashCode();
}

public enum Direction
{
	None,
	Right,
	Left,
	Up,
	Down
}