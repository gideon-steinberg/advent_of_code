<Query Kind="Program" />

void Main()
{
	var input1 = @"-L|F7
7S-7|
L|7||
-L-J|
L|-JF";

	var input2 = @"7-F7-
.FJ|7
SJLL7
|F--J
LJ.LJ";

var input3 = @"";

	var lines = input1.Split(Environment.NewLine);
	var height = lines.Count();
	var width = lines[0].Count();
	var map = new Node[height, width];
	
	var startI = 0;
	var startJ = 0;
	
	for (var i = 0; i < height; i++)
	{
		var line = lines[i].ToCharArray();
		
		for (var j = 0; j < width; j++)
		{
			
			switch (line[j])
			{
				case '|':
					map[i,j] = new Node {connectsNorth = true, connectsSouth = true};
					break;
				case '-':
					map[i,j] = new Node {connectsWest = true, connectsEast = true};
					break;
				case 'L':
					map[i,j] = new Node {connectsNorth = true, connectsEast = true};
					break;
				case 'J':
					map[i,j] = new Node {connectsNorth = true, connectsWest = true};
					break;
				case '7':
					map[i,j] = new Node {connectsWest = true, connectsSouth = true};
					break;
				case 'F':
					map[i,j] = new Node {connectsEast = true, connectsSouth = true};
					break;
				case '.':
					map[i,j] = new Node();
					break;
				case 'S':
					map[i,j] = new Node {connectsEast = true, connectsWest = true, connectsNorth = true, connectsSouth = true};
					startI = i;
					startJ = j;
					break;
				default:
					map[i,j] = new Node();
					break;
			}
		}
	}
	
	//map.Dump();
	var result = 0;
	
	try {
		result = Traverse(map, startI, startJ, startI-1, startJ, height, width);
		result++;
		(result / 2).Dump();
	} catch (IndexOutOfRangeException){ "-1".Dump();}
	
	try {
		result = Traverse(map, startI, startJ, startI+1, startJ, height, width);
		result++;
		(result / 2).Dump();
	} catch (IndexOutOfRangeException){"-1".Dump();}
	
	try {
		result = Traverse(map, startI, startJ, startI, startJ-1, height, width);
		result++;
		(result / 2).Dump();
	} catch (IndexOutOfRangeException){"-1".Dump();}
	
	try {
		result = Traverse(map, startI, startJ, startI, startJ+1, height, width);
		result++;
		(result / 2).Dump();
	} catch (IndexOutOfRangeException){"-1".Dump();}
	
}

public int Traverse(Node[,] map, int startI, int startJ, int i, int j, int height, int width)
{
	var visitedPoints = new HashSet<Point>();
	visitedPoints.Add(new Point {i = i, j = j});
	
	var startPoint = new Point{i = startI, j = startJ};
	
	var steps = 0;
	
	var currentI = i;
	var currentJ = j;
	var previousI = startI;
	var previousJ = startJ;
	while (true)
	{
		steps++;
		// go  north
		if (currentI < previousI)
		{
			if (map[currentI, currentJ].connectsSouth && map[previousI, previousJ].connectsNorth)
			{
				if (map[currentI, currentJ].connectsNorth)
				{
					previousI = currentI;
					previousJ = currentJ;
					currentI = previousI - 1;
				}
				else if (map[currentI, currentJ].connectsWest)
				{
					previousI = currentI;
					previousJ = currentJ;
					currentJ = previousJ - 1;
				}
				else if (map[currentI, currentJ].connectsEast)
				{
					previousI = currentI;
					previousJ = currentJ;
					currentJ = previousJ + 1;
				}
				else 
				{
					"Error".Dump();
					return -1;
				}
			}
			else 
			{
				//"Not passable N".Dump();
				return -1;
			}
		}
		// go south
		else if (currentI > previousI)
		{
			if (map[currentI, currentJ].connectsNorth && map[previousI, previousJ].connectsSouth)
			{
				if (map[currentI, currentJ].connectsSouth)
				{
					previousI = currentI;
					previousJ = currentJ;
					currentI = previousI + 1;
				}
				else if (map[currentI, currentJ].connectsWest)
				{
					previousI = currentI;
					previousJ = currentJ;
					currentJ = previousJ - 1;
				}
				else if (map[currentI, currentJ].connectsEast)
				{
					previousI = currentI;
					previousJ = currentJ;
					currentJ = previousJ + 1;
				}
				else 
				{
					"Error".Dump();
					return -1;
				}
			}
			else 
			{
				//"Not passable S".Dump();
				return -1;
			}
		}
		// go west
		else if (currentJ < previousJ)
		{
			if (map[currentI, currentJ].connectsEast && map[previousI, previousJ].connectsWest)
			{
				if (map[currentI, currentJ].connectsSouth)
				{
					previousI = currentI;
					previousJ = currentJ;
					currentI = previousI + 1;
				}
				else if (map[currentI, currentJ].connectsWest)
				{
					previousI = currentI;
					previousJ = currentJ;
					currentJ = previousJ - 1;
				}
				else if (map[currentI, currentJ].connectsNorth)
				{
					previousI = currentI;
					previousJ = currentJ;
					currentI = previousI - 1;
				}
				else 
				{
					"Error".Dump();
					return -1;
				}
			}
			else 
			{
				//"Not passable W".Dump();
				return -1;
			}
		}
		// go east
		else if (currentJ > previousJ)
		{
			if (map[currentI, currentJ].connectsWest && map[previousI, previousJ].connectsEast)
			{
				if (map[currentI, currentJ].connectsSouth)
				{
					previousI = currentI;
					previousJ = currentJ;
					currentI = previousI + 1;
				}
				else if (map[currentI, currentJ].connectsEast)
				{
					previousI = currentI;
					previousJ = currentJ;
					currentJ = previousJ + 1;
				}
				else if (map[currentI, currentJ].connectsNorth)
				{
					previousI = currentI;
					previousJ = currentJ;
					currentI = previousI - 1;
				}
				else 
				{
					"Error".Dump();
					return -1;
				}
			}
			else 
			{
				//"Not passable E".Dump();
				return -1;
			}
		}
		else 
		{
			"Not passable at all".Dump();
			return -1;
		}
		var currentPoint = new Point{i = currentI, j = currentJ};
		if (visitedPoints.Contains(currentPoint))
		{
			break;
		}
		visitedPoints.Add(currentPoint);
		if (currentPoint.Equals(startPoint))
		{
			return steps;
		}
	}
	return -1;
}

// You can define other methods, fields, classes and namespaces here

public class Node
{
	public bool connectsNorth;
	public bool connectsSouth;
	public bool connectsEast;
	public bool connectsWest;
}

public class Point
{
	public int i;
	public int j;
	
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