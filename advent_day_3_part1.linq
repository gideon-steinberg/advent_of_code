<Query Kind="Program" />

void Main()
{
	var input1 = @"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";

	var input2 = @"";

	var parts = input1.Split(Environment.NewLine).Select(line => line.ToCharArray()).ToList();
	var height = parts.Count();
	var width = parts.First().Count();
	var engineMap = new char[height + 2, width + 2];
	for (var i = 0; i < height; i++)
	{
		for(var j = 0; j < width; j++)
		{
			engineMap[i + 1, j + 1] = parts[i][j];
		}
	}
	
	for (var i = 0; i < height + 2; i++)
	{
		engineMap[i, 0] = '.';
		engineMap[i, width + 1] = '.';
	}
	
	for (var j = 0; j < width + 2; j++)
	{
		engineMap[0, j] = '.';
		engineMap[height + 1, j] = '.';	
	}
	
	var numberMap = new bool[height + 2, width + 2];
	
	var hasChanged = false;
	
	var isNumberOrDotRegex = new Regex("[0-9.]");
	var isNumberRegex = new Regex("[0-9]");
	
	do 
	{
		hasChanged = false;
		for (var i = 1; i <= height; i++)
		{
			for(var j = 1; j <= width; j++)
			{
				if (numberMap[i, j] || !isNumberRegex.IsMatch(engineMap[i, j] + ""))
				{
					continue;
				}
			
				if (!isNumberOrDotRegex.IsMatch(engineMap[i , j - 1] + "") || numberMap[i , j - 1])
				{
					if (!numberMap[i,j])
					{
						hasChanged = true;
					}
					 numberMap[i,j] = true;
				}
				
				if (!isNumberOrDotRegex.IsMatch(engineMap[i -1 , j - 1] + "") || numberMap[i -1 , j - 1])
				{
					if (!numberMap[i,j])
					{
						hasChanged = true;
					}
					 numberMap[i,j] = true;
				}
				
				if (!isNumberOrDotRegex.IsMatch(engineMap[i - 1 , j] + "") || numberMap[i - 1 , j])
				{
					if (!numberMap[i,j])
					{
						hasChanged = true;
					}
					 numberMap[i,j] = true;
				}
				
				if (!isNumberOrDotRegex.IsMatch(engineMap[i , j + 1] + "") || numberMap[i , j + 1])
				{
					if (!numberMap[i,j])
					{
						hasChanged = true;
					}
					 numberMap[i,j] = true;
				}
				
				if (!isNumberOrDotRegex.IsMatch(engineMap[i - 1 , j + 1] + "") || numberMap[i - 1 , j + 1])
				{
					if (!numberMap[i,j])
					{
						hasChanged = true;
					}
					 numberMap[i,j] = true;
				}
				
				if (!isNumberOrDotRegex.IsMatch(engineMap[i + 1 , j] + "") || numberMap[i + 1 , j])
				{
					if (!numberMap[i,j])
					{
						hasChanged = true;
					}
					 numberMap[i,j] = true;
				}
				
				if (!isNumberOrDotRegex.IsMatch(engineMap[i + 1 , j - 1] + "") || numberMap[i + 1 , j - 1])
				{
					if (!numberMap[i,j])
					{
						hasChanged = true;
					}
					 numberMap[i,j] = true;
				}
				
				if (!isNumberOrDotRegex.IsMatch(engineMap[i + 1 , j + 1] + "") || numberMap[i + 1 , j + 1])
				{
					if (!numberMap[i,j])
					{
						hasChanged = true;
					}
					 numberMap[i,j] = true;
				}
			}
		}
		
	} while (hasChanged);
	
	decimal sum = 0;
	
	var numbers = new List<string>();
	var hasData = false;
	var carry = new StringBuilder();
	
	for (var i = 1; i <= height; i++)
	{
		if (hasData)
		{
			hasData = false;
			sum += decimal.Parse(carry.ToString());
			numbers.Add(carry.ToString());
			carry = new StringBuilder();
		}
			
		for(var j = 1; j <= width; j++)
		{
			if (numberMap[i,j] && isNumberRegex.IsMatch(engineMap[i, j] + ""))
			{
				carry.Append(engineMap[i,j]);
				hasData = true;
			}
			else if (hasData)
			{
				hasData = false;
				sum += decimal.Parse(carry.ToString());
				numbers.Add(carry.ToString());
				carry = new StringBuilder();
			}
		}
	}
	//engineMap.Dump();
	numberMap.Dump();
	//numbers.Dump();
	sum.Dump();
}

// You can define other methods, fields, classes and namespaces here