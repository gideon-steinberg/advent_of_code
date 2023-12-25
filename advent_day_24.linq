<Query Kind="Program" />

void Main()
{
	var input1 = @"19, 13, 30 @ -2,  1, -2
18, 19, 22 @ -1, -1, -2
20, 25, 34 @ -2, -2, -4
12, 31, 28 @ -1, -2, -1
20, 19, 15 @  1, -5, -3";

var input2 = @"";

	var parts = input1.Split(Environment.NewLine);
	var lines = new List<Line>();
	var numbersRegex = new Regex("(-?\\d+),\\s+(-?\\d+),\\s+(-?\\d+)\\s+@\\s+(-?\\d+),\\s+(-?\\d+),\\s+(-?\\d+)");
	
	foreach (var part in parts)
	{
		var matches = numbersRegex.Matches(part);
		var x = decimal.Parse(matches[0].Groups[1].Value);
		var y = decimal.Parse(matches[0].Groups[2].Value);
		var z = decimal.Parse(matches[0].Groups[3].Value);
		var dx = decimal.Parse(matches[0].Groups[4].Value);
		var dy = decimal.Parse(matches[0].Groups[5].Value);
		var dz = decimal.Parse(matches[0].Groups[6].Value);
		
		var m = dy/dx;
		var c = y - (m * x);
		var line = new Line
		{
		 	m = m,
			c = c,
			x = x,
			y = y,
			dx = dx,
			dy = dy
		};
		
		lines.Add(line);
	}
	
	var sum = 0;
	
	for ( var i = 0; i < lines.Count(); i++)
	{
		var l1 = lines[i];
		for (var j = i+1; j < lines.Count();j++)
		{
			var l2 = lines[j];
			if (l1.m == l2.m) continue;
			if (l1.x == l2.x) continue;
			if (l1.y == l2.y) continue;
			
			var x = (l2.c - l1.c) / (l1.m - l2.m);
			var y = (l1.m * x) + l1.c;
			
			if (x > 200000000000000 && x <400000000000000 && y > 200000000000000 && y < 400000000000000)
			//if (x >= 7 && x <=27 && y >= 7 && y <= 27)
			{
				//if (l1.x >= x * -Math.Sign(l1.dx) && l2.x >= x * -Math.Sign(l2.dx))
				if (Math.Sign(l1.m) != Math.Sign(l2.m))
				{
					sum++;
				}
				//l1.Dump();
				//l2.Dump();
					
				//x.Dump();
				//y.Dump();
			}
		}
	}
	sum.Dump();
}

// You can define other methods, fields, classes and namespaces here

public class Line
{
	public decimal m;
	public decimal c;
	public decimal x;
	public decimal y;
	public decimal dx;
	public decimal dy;
}