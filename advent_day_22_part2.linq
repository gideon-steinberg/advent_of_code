<Query Kind="Program" />

void Main()
{
	var input1 = @"1,0,1~1,2,1
0,0,2~2,0,2
0,2,3~2,2,3
0,0,4~0,2,4
2,0,5~2,2,5
0,1,6~2,1,6
1,1,8~1,1,9";

var input2 = @"";

	var height = 15;
	var width = 15;
	var depth = 500;
	var map = new int[height, width, depth];
	var points = new Dictionary<Point, Point>();
	
	var parts = input1.Split(Environment.NewLine);
	
	var i = 1;
	
	foreach (var part in parts)
	{
		var coords = part.Split("~");
		var temp = coords[0].Split(",");
		var x1 = Int32.Parse(temp[0]);
		var y1 = Int32.Parse(temp[1]);
		var z1 = Int32.Parse(temp[2]);
		
		temp = coords[1].Split(",");
		var x2 = Int32.Parse(temp[0]);
		var y2 = Int32.Parse(temp[1]);
		var z2 = Int32.Parse(temp[2]);
		
		var point = new Point
		{
			x1 = x1,
			y1 = y1,
			z1 = z1,
			x2 = x2,
			y2 = y2 ,
			z2 = z2,
			id = i
		};
		
		points.Add(point, point);
		
		i++;
	}
	
	foreach (var point in points.Keys)
	{
		if (point.increasingX)
		{
			for (var a = point.x1; a <= point.x2; a++)
			{
				map[a,point.y1,point.z1] = point.id;
			}
		}
		else if (point.increasingY)
		{
			for (var a = point.y1; a <= point.y2; a++)
			{
				map[point.x1,a,point.z1] = point.id;
			}
		}
		else
		{
			for (var a = point.z1; a <= point.z2; a++)
			{
				map[point.x1,point.y1, a] = point.id;
			}
		}
	}
	
	var madeChange = true;
	while (madeChange)
	{
		madeChange = false;
		foreach (var point in points.Keys)
		{
			if (point.increasingX)
			{
				if (point.z1 <= 1) continue;
				var isSupported = false;
				for (var a = point.x1; a <= point.x2; a++)
				{
					if (map[a, point.y1, point.z1 - 1] != 0)
					{
						isSupported = true;
					}
				}
				
				if (!isSupported)
				{
					madeChange = true;
						
					for (var a = point.x1; a <= point.x2; a++)
					{
						map[a, point.y1, point.z1] = 0;
					}
					for (var a = point.x1; a <= point.x2; a++)
					{
						map[a, point.y1, point.z1 - 1] = point.id;
					}
					
					point.z1--;
					point.z2--;
				}
			}
			else if (point.increasingY)
			{
				if (point.z1 <= 1) continue;
				var isSupported = false;
				for (var a = point.y1; a <= point.y2; a++)
				{
					if (map[point.x1, a, point.z1 - 1] != 0)
					{
						isSupported = true;
					}
				}
				
				if (!isSupported)
				{
					madeChange = true;
						
					for (var a = point.y1; a <= point.y2; a++)
					{
						map[point.x1, a, point.z1] = 0;
					}
					for (var a = point.y1; a <= point.y2; a++)
					{
						map[point.x1, a, point.z1 - 1] = point.id;
					}
					
					point.z1--;
					point.z2--;
				}
			}
			else //if (point.increasingZ) or none
			{
				if (point.z1 <= 1) continue;
				if (map[point.x1, point.y1, point.z1 - 1] == 0)
				{
					madeChange = true;
					
					for (var a = point.z1; a <= point.z2; a++)
					{
						map[point.x1, point.y1, a] = 0;
					}
					for (var a = point.z1 - 1; a < point.z2; a++)
					{
						map[point.x1, point.y1, a] = point.id;
					}
					
					point.z1--;
					point.z2--;
				}
			}
		}
	}
	
	//Print(map);
	
	var supportedBricks = new Dictionary<int, HashSet<int>>();
	
	foreach (var point in points.Keys)
	{
		var bricksSupportingThisOne = new HashSet<int>();
		if (point.increasingX)
		{
			for (var a = point.x1; a <= point.x2; a++)
			{
				if (map[a,point.y1,point.z1 - 1] != 0)
				{
					bricksSupportingThisOne.Add(map[a,point.y1,point.z1 - 1]);
				}
			}
		}
		else if (point.increasingY)
		{
			for (var a = point.y1; a <= point.y2; a++)
			{
				if (map[point.x1,a,point.z1 - 1] != 0)
				{
					bricksSupportingThisOne.Add(map[point.x1,a,point.z1 - 1]);
				}
			}
		}
		else 
		{
			if (map[point.x1,point.y1,point.z1 - 1] != 0)
			{
				bricksSupportingThisOne.Add(map[point.x1,point.y1,point.z1 - 1]);
			}
		}
		//bricksSupportingThisOne.Dump();
		/*if (bricksSupportingThisOne.Count() == 1)
		{
			var p = new Point{id = bricksSupportingThisOne.First()};
			points[p].canBeRemoved = false;
		}*/
		supportedBricks.Add(point.id, bricksSupportingThisOne);
	}
	
	var sum = 0;
	
	foreach (var p in points.Keys)
	{
		var excludedBlocks = new HashSet<int>();
		excludedBlocks.Add(p.id);
		madeChange = true;
		while (madeChange)
		{
			madeChange = false;
			foreach (var pk in points.Keys.Where(p => !excludedBlocks.Contains(p.id)))
			{
				var isValid = supportedBricks[pk.id].Except(excludedBlocks).Count() == 0 && pk.z1 != 1;
				if (isValid)
				{
					excludedBlocks.Add(pk.id);
					madeChange = true;
				}
			}
		}
		sum += excludedBlocks.Count() - 1;
	}
	sum.Dump();
}

// You can define other methods, fields, classes and namespaces here

public void Print(int[,,] map)
{
	for (var a = 15; a >= 0; a--)
	{
		for (var b = 0; b < 4; b++)
		{
			var y = 0;
			while (map[b,y,a] == 0 && y < 5) y++;
			Console.Write(map[b,y,a] + " ");
		}
		Console.WriteLine();
	}
	
	Console.WriteLine("--------------------");
	
	for (var a = 15; a >= 0; a--)
	{
		for (var b = 0; b < 4; b++)
		{
			var x = 0;
			while (map[x,b,a] == 0 && x < 5) x++;
			
			Console.Write(map[x,b,a]);
		}
		Console.WriteLine();
	}
}

public class Point
{
	public int x1;
	public int y1;
	public int z1;
	public int x2;
	public int y2;
	public int z2;
	public int id;
	public bool increasingX => x1 != x2;
	public bool increasingY => y1 != y2;
	public bool increasingZ => z1 != z2;
		
	public override bool Equals(object obj)
	{
	    var item = obj as Point;

	    if (item == null)
	    {
	        return false;
	    }

	    return this.id.Equals(item.id);
	}

	public override int GetHashCode() => (id).GetHashCode();
}