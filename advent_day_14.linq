<Query Kind="Program" />

void Main()
{
	var input1 = @"O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....";

	var input2 = @"";
	

	var parts = input1.Split(Environment.NewLine);
	var height = parts.Count();
	var width = parts[0].Count();
	
	var map = new char[height, width];
	
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			map[i,j] = parts[i][j];
		}
	}
	
	var turns = 0;
	var pattern = new List<decimal>();
	var patternsSeen = new HashSet<decimal>();
	while (turns < 1000)
	{
		MoveNorth(map, height, width);
		MoveWest(map, height, width);
		MoveSouth(map, height, width);
		MoveEast(map, height, width);
		var sum = GetSum(map, height, width);
		/*if (patternsSeen.Contains(sum))
		{
			//break;
		}*/
		pattern.Add(sum);
		//patternsSeen.Add(sum);
		turns++;
	}
	
	//pattern.Dump();
	int aim = 1000000000 -1;
	while (aim >= pattern.Count())
	{
		aim -= 7; // gotten from analysing the pattern. Couldn't be bothered programming it.
	}
	aim.Dump();
	
	pattern[(int)aim].Dump();
	pattern[(int)aim - 7].Dump();
}

public decimal GetSum(char[,] map, int height, int width)
{
	decimal sum = 0;
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			if (map[i,j] == 'O')
			{
				sum += height - i;
			}
		}
	}
	return sum;
}

public void MoveNorth(char[,] map, int height, int width)
{
	var openSpace = -1;
	var openSpaceWithPillar = -1;
	var lastPillar = -1;
	for (var j = 0; j < width; j++)
	{
		openSpace = -1;
		lastPillar = -1;
		openSpaceWithPillar = -1;
		for (var i = 0; i < height; i++)
		{
			if (map[i,j] == '.' && openSpace == -1) openSpace = i;
			if (map[i,j] == '.' && openSpaceWithPillar == -1 && lastPillar >= 0) openSpaceWithPillar = i;
			if (map[i,j] == '#') 
			{
				lastPillar = i;
				openSpaceWithPillar = -1;
			}
			if (map[i,j] == 'O')
			{
				if (openSpace >= 0)
				{
					if (lastPillar == -1)
					{
						map[openSpace,j] = 'O';
						map[i,j] = '.';
						i = openSpace;
						openSpace = -1;
					} 
					else if (openSpaceWithPillar >=0)
					{
						map[openSpaceWithPillar,j] = 'O';
						map[i,j] = '.';
						i = openSpaceWithPillar;
						openSpaceWithPillar = -1;
					}
				}
			}
		}
	}
}

public void MoveSouth(char[,] map, int height, int width)
{
	var openSpace = -1;
	var openSpaceWithPillar = -1;
	var lastPillar = -1;
	for (var j = 0; j < width; j++)
	{
		openSpace = -1;
		lastPillar = -1;
		openSpaceWithPillar = -1;
		for (var i = height -1; i >= 0; i--)
		{
			if (map[i,j] == '.' && openSpace == -1) openSpace = i;
			if (map[i,j] == '.' && openSpaceWithPillar == -1 && lastPillar >= 0) openSpaceWithPillar = i;
			if (map[i,j] == '#') 
			{
				lastPillar = i;
				openSpaceWithPillar = -1;
			}
			if (map[i,j] == 'O')
			{
				if (openSpace >= 0)
				{
					if (lastPillar == -1)
					{
						map[openSpace,j] = 'O';
						map[i,j] = '.';
						i = openSpace;
						openSpace = -1;
					} 
					else if (openSpaceWithPillar >=0)
					{
						map[openSpaceWithPillar,j] = 'O';
						map[i,j] = '.';
						i = openSpaceWithPillar;
						openSpaceWithPillar = -1;
					}
				}
			}
		}
	}
}

public void MoveEast(char[,] map, int height, int width)
{
	var openSpace = -1;
	var openSpaceWithPillar = -1;
	var lastPillar = -1;
	for (var i = 0; i < height; i++)
	{
		openSpace = -1;
		lastPillar = -1;
		openSpaceWithPillar = -1;
		for (var j = width -1; j >= 0; j--)
		{
			if (map[i,j] == '.' && openSpace == -1) openSpace = j;
			if (map[i,j] == '.' && openSpaceWithPillar == -1 && lastPillar >= 0) openSpaceWithPillar = j;
			if (map[i,j] == '#') 
			{
				lastPillar = j;
				openSpaceWithPillar = -1;
			}
			if (map[i,j] == 'O')
			{
				if (openSpace >= 0)
				{
					if (lastPillar == -1)
					{
						map[i,openSpace] = 'O';
						map[i,j] = '.';
						j = openSpace;
						openSpace = -1;
					} 
					else if (openSpaceWithPillar >=0)
					{
						map[i, openSpaceWithPillar] = 'O';
						map[i,j] = '.';
						j = openSpaceWithPillar;
						openSpaceWithPillar = -1;
					}
				}
			}
		}
	}
}

public void MoveWest(char[,] map, int height, int width)
{
	var openSpace = -1;
	var openSpaceWithPillar = -1;
	var lastPillar = -1;
	for (var i = 0; i < height; i++)
	{
		openSpace = -1;
		lastPillar = -1;
		openSpaceWithPillar = -1;
		for (var j = 0; j < width; j++)
		{
			if (map[i,j] == '.' && openSpace == -1) openSpace = j;
			if (map[i,j] == '.' && openSpaceWithPillar == -1 && lastPillar >= 0) openSpaceWithPillar = j;
			if (map[i,j] == '#') 
			{
				lastPillar = j;
				openSpaceWithPillar = -1;
			}
			if (map[i,j] == 'O')
			{
				if (openSpace >= 0)
				{
					if (lastPillar == -1)
					{
						map[i,openSpace] = 'O';
						map[i,j] = '.';
						j = openSpace;
						openSpace = -1;
					} 
					else if (openSpaceWithPillar >=0)
					{
						map[i, openSpaceWithPillar] = 'O';
						map[i,j] = '.';
						j = openSpaceWithPillar;
						openSpaceWithPillar = -1;
					}
				}
			}
		}
	}
}

// You can define other methods, fields, classes and namespaces here