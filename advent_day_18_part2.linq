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
	
	var colourRegex = new Regex("#([a-f|\\d]+)");
	
	var instructions = new List<Instruction>();
	
	foreach (var line in parts)
	{
		var matches = colourRegex.Matches(line);
		var colour = matches.First().Groups[1].Value.ToCharArray();
		
		var direction = "";
		switch (colour[5])
		{
			case '0':
				direction = "R";
				break;
			case '1':
				direction = "D";
				break;
			case '2':
				direction = "L";
				break;
			case '3':
				direction = "U";
				break;
		}
		var number = 0;
		for (var a = 0; a < 5; a++)
		{
			var converted = ConvertFromHexToInt(colour[a]);
			number += (int)Math.Pow(16, 4-a) * converted;
		}
		
		var instruction = new Instruction
		{
			direction = direction,
			number = number
		};
		
		instructions.Add(instruction);
	}
	
	instructions.Dump();
	
	var height = instructions.Count() * 1000;
	var width = instructions.Count() * 1000;
	var image = new Bitmap(height, width);
	if (height < 1000) height = 1000;
	if (width < 1000) width = 1000;
	var isDugMap = new bool[height,width];
	
	var i = height / 2;
	var j = width / 2;
	foreach (var instruction in instructions)
	{
		var number = instruction.number;
		var direction = instruction.direction;
		for (var a = 0; a < number; a++)
		{
			switch(direction)
			{
				case "U":
					i--;
					isDugMap[i,j] = true;
					break;
				case "D":
					i++;
					isDugMap[i,j] = true;
					break;
				case "R":
					j++;
					isDugMap[i,j] = true;
					break;
				case "L":
					j--;
					isDugMap[i,j] = true;
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

public int ConvertFromHexToInt(char c)
{
	switch(c)
	{
		case '0':
			return 0;
		case '1':
			return 1;
		case '2':
			return 2;
		case '3':
			return 3;
		case '4':
			return 4;
		case '5':
			return 5;
		case '6':
			return 6;
		case '7':
			return 7;
		case '8':
			return 8;
		case '9':
			return 9;
		case 'a':
			return 10;
		case 'b':
			return 11;
		case 'c':
			return 12;
		case 'd':
			return 13;
		case 'e':
			return 14;
		case 'f':
			return 15;		
	}
	return 0;
}

public class Instruction
{
	public string direction;
	public int number;
}