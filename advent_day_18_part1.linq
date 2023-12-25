<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
  <Namespace>System.Drawing.Imaging</Namespace>
</Query>

void Main()
{
	var input1 = @"R 6 (#70c710)
D 5 (#0dc571)
L 2 (#5713f0)
D 2 (#d2c081)
R 2 (#59c680)
D 2 (#411b91)
L 5 (#8ceee2)
U 2 (#caa173)
L 1 (#1b58a2)
U 2 (#caa171)
R 2 (#7807d2)
U 3 (#a77fa3)
L 2 (#015232)
U 2 (#7a21e3)";

var input2 = @"";

	var parts = input1.Split(Environment.NewLine);

	var height = parts.Count();
	var width = parts.Count();
	if (height < 30) height = 30;
	if (width < 30) width = 30;
	var isDugMap = new bool[height,width];
	var colourMap  = new string[height,width];
	
	var directionAndNumberRegex = new Regex("([U|L|R|D]) (\\d+)");
	var colourRegex = new Regex("#([a-f|\\d]+)");
	
	var i = height / 2;
	var j = width / 2;
	
	foreach (var line in parts)
	{
		var matches = directionAndNumberRegex.Matches(line);
		var number = decimal.Parse(matches.First().Groups[2].Value);
		var direction = matches.First().Groups[1].Value;
		matches = colourRegex.Matches(line);
		var colour = matches.First().Groups[1].Value;
		for (var a = 0; a < number; a++)
		{
			switch(direction)
			{
				case "U":
					i--;
					isDugMap[i,j] = true;
					colourMap[i,j] = colour;
					break;
				case "D":
					i++;
					isDugMap[i,j] = true;
					colourMap[i,j] = colour;
					break;
				case "R":
					j++;
					isDugMap[i,j] = true;
					colourMap[i,j] = colour;
					break;
				case "L":
					j--;
					isDugMap[i,j] = true;
					colourMap[i,j] = colour;
					break;
			}
		}
	}
	
	var inRowBoundary = new bool[height,width];
	
	for (i = 0; i < height; i++)
	{
		var leftBoundary = -1;
		for (j = 0; j < width; j++)
		{
			if (isDugMap[i,j])
			{
				leftBoundary = j;
				j = int.MaxValue - 1;
			}
		}
		var rightBoundary = -1;
		for (j = width - 1; j >= 0; j--)
		{
			if (isDugMap[i,j])
			{
				rightBoundary = j;
				j = -100;
			}
		}
		for (j = 0; j < width; j++)
		{
			if (!isDugMap[i,j])
			{
				if (j > leftBoundary && j < rightBoundary)
				{
					inRowBoundary[i,j] = true;
				}
			}
		}
	}
	
	for (j = 0; j < width; j++)
	{
		var upBoundary = -1;
		for (i = 0; i < height; i++)
		{
			if (isDugMap[i,j])
			{
				upBoundary = i;
				i = int.MaxValue - 1;
			}
		}
		var downBoundary = -1;
		for (i = height - 1; i >= 0; i--)
		{
			if (isDugMap[i,j])
			{
				downBoundary = i;
				i = -100;
			}
		}
		for (i = 0; i < height; i++)
		{
			if (!isDugMap[i,j])
			{
				if (i > upBoundary && i < downBoundary && inRowBoundary[i,j])
				{
					//isDugMap[i,j] = true;
				}
			}
		}
	}
	
	var image = new Bitmap(height, width);
	
	var sum = 0;
	for (i = 0; i < height; i++)
	{
		for (j = 0; j < width; j++)
		{
			if (isDugMap[i,j])
			{
				image.SetPixel(i,j, Color.Red);
			} 
			else 
			{
				image.SetPixel(i,j, Color.Blue);
			}
			Console.Write(isDugMap[i,j] ? "1" : " ");
			if (isDugMap[i,j]) sum++;
		}
		Console.WriteLine();
	}
	
	Directory.GetCurrentDirectory().Dump();
	image.Save("day18.png");
	sum.Dump();
}