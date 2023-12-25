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
					map[i,j] = new Node {connectsNorth = true, connectsSouth = true, c = '|'};
					break;
				case '-':
					map[i,j] = new Node {connectsWest = true, connectsEast = true, c = '-'};
					break;
				case 'L':
					map[i,j] = new Node {connectsNorth = true, connectsEast = true, c = 'L'};
					break;
				case 'J':
					map[i,j] = new Node {connectsNorth = true, connectsWest = true, c = 'J'};
					break;
				case '7':
					map[i,j] = new Node {connectsWest = true, connectsSouth = true, c = '7'};
					break;
				case 'F':
					map[i,j] = new Node {connectsEast = true, connectsSouth = true, c = 'F'};
					break;
				case '.':
					map[i,j] = new Node{c = '.'};
					break;
				case 'S':
					map[i,j] = new Node {connectsEast = true, connectsWest = true, connectsNorth = true, connectsSouth = true, c = 'S'};
					startI = i;
					startJ = j;
					break;
				default:
					map[i,j] = new Node{c = '.'};
					break;
			}
		}
	}
	
	try {
		Traverse(map, startI, startJ, startI-1, startJ, height, width);
	} catch (IndexOutOfRangeException){}
	
	try {
		//Traverse(map, startI, startJ, startI+1, startJ, height, width);
	} catch (IndexOutOfRangeException){"-1".Dump();}
	
	try {
		//Traverse(map, startI, startJ, startI, startJ-1, height, width);
	} catch (IndexOutOfRangeException){}
	
	try {
		//Traverse(map, startI, startJ, startI, startJ+1, height, width);
	} catch (IndexOutOfRangeException){}
	
}

public void Traverse(Node[,] map, int startI, int startJ, int i, int j, int height, int width)
{
	var visitedPoints = new HashSet<Point>();
	visitedPoints.Add(new Point {i = i, j = j});
	
	var startPoint = new Point{i = startI, j = startJ};
	
	var drawnPath = new char[height,width];
	
	for (var x = 0; x < height; x++)
	{
		for (var y = 0; y < width; y++)
		{
			drawnPath[x,y]  = ' ';
		}	
	}
	
	var currentI = i;
	var currentJ = j;
	var previousI = startI;
	var previousJ = startJ;
	while (true)
	{
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
					return;
				}
			}
			else 
			{
				//"Not passable N".Dump();
				return;
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
					return;
				}
			}
			else 
			{
				//"Not passable S".Dump();
				return;
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
					return;
				}
			}
			else 
			{
				//"Not passable W".Dump();
				return;
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
					return;
				}
			}
			else 
			{
				//"Not passable E".Dump();
				return;
			}
		}
		else 
		{
			"Not passable at all".Dump();
			return;
		}
		var currentPoint = new Point{i = currentI, j = currentJ};
		if (visitedPoints.Contains(currentPoint))
		{
			break;
		}
		drawnPath[currentI,currentJ] = 'X';
		visitedPoints.Add(currentPoint);
		
		if (currentPoint.Equals(startPoint))
		{
			var sum = 0;
			for (var x = 1; x < height -1; x++)
			{
				for (var y = 1; y < width -1; y++)
				{
					var p = new Point{ i = x, j = y};
					if (visitedPoints.Contains(p))
					{
						var n = map[x,y];
						Console.Write(n.c);
					}
					else 
					{
						Console.Write(' ');
					}
					/*if (drawnPath[x,y] != 'X'
					&& drawnPath[x + 1,y    ] == 'X'
					&& drawnPath[x - 1,y    ] == 'X'
					&& drawnPath[x    ,y + 1] == 'X'
					&& drawnPath[x    ,y - 1] == 'X'
					)
					{
						sum++;
					}*/
				}	
				Console.WriteLine();

			}
			Console.WriteLine(sum);
			return;
		}
	}
	return;
}

// You can define other methods, fields, classes and namespaces here

public class Node
{
	public bool connectsNorth;
	public bool connectsSouth;
	public bool connectsEast;
	public bool connectsWest;
	public char c;
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