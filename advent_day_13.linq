<Query Kind="Program" />

void Main()
{
	var input1 = @"#.##..##.
..#.##.#.
##......#
##......#
..#.##.#.
..##..##.
#.#.##.#.

#...##..#
#....#..#
..##..###
#####.##.
#####.##.
..##..###
#....#..#";

var input2 = @"";

	var preMap = new List<List<List<bool>>>();
	
	var i = 0;
	preMap.Add(new List<List<bool>>());
	
	var parts = input1.Split(Environment.NewLine);
	
	foreach (var part in parts)
	{
		if (!part.Contains(".") && !part.Contains("#"))
		{
			i++;
			preMap.Add(new List<List<bool>>());
			continue;
		}
		
		preMap[i].Add(part.ToCharArray().Select( c => c == '#').ToList());
	}
	
	var map = new List<bool[,]>();
	var heights = new List<int>();
	var widths = new List<int>();
	
	foreach (var input in preMap)
	{
		var height = input.Count();
		var width = input[0].Count();
		heights.Add(height);
		widths.Add(width);
		var inputArray = new bool[height,width];
		for (var a = 0; a < height; a++)
		{
			for (var b = 0; b < width; b++)
			{
				inputArray[a,b] = input[a][b];
			}
		}
		map.Add(inputArray);
	}
	
	decimal sum = 0;
	
	for(i = 0; i < map.Count(); i++)
	{
		var toProcess = map[i];
		toProcess.Dump();
		var height = heights[i];
		var width = widths[i];
		
		var result = CalculateRowValue(toProcess, height, width);
		if (result != null) sum += result.Value;
		
		result = CalculateColValue(toProcess, height, width);
		if (result != null) sum += result.Value;
	}
	sum.Dump();
}

public decimal? CalculateColValue(bool[,] input, int height, int width)
{
	var numErrors = 0;
	var isValid = true;
	decimal sum = 0;
	for (var j = 0; j < width - 1; j++)
	{
		isValid = true;
		numErrors = 0;
		for (var i = 0; i < height; i++)
		{
			for (var a = 0; a < width - j; a++)
			{
				var leftSide = j - a;
				if (leftSide < 0) continue;
				if (leftSide >= width) continue;
				var rightSide = j + a + 1;
				if (rightSide < 0) continue;
				if (rightSide >= width) continue;
				//$"{input[i, leftSide]} == {input[i, rightSide]}".Dump();
				//$"{i}, {leftSide} == {i}, {rightSide}".Dump();
				if (input[i, leftSide] != input[i, rightSide])
				{
					//$"invalid at [{i}, {leftSide}] and [{i}, {rightSide}]".Dump();
					isValid = false;
					numErrors++;
				}
			}
		}
		/*if (isValid)
		{
			$"Found at j = {j + 1}".Dump();	
			sum += Math.Abs(j + 1);
		}*/
		if (numErrors == 1)
		{
			$"Found at j = {j + 1}".Dump();	
			sum += Math.Abs(j + 1);
		}
	}
	return sum == 0 ? null : sum;
}

public decimal? CalculateRowValue(bool[,] input, int height, int width)
{
	var isValid = true;
	var numErrors = 0;
	decimal sum = 0;
	for (var i = 0; i < height-1; i++)
	{
		isValid = true;
		numErrors = 0;
		for (var j = 0; j < width; j++)
		{
			for (var a = 0; a < height - i; a++)
			{
				var leftSide = i - a;
				if (leftSide < 0) continue;
				if (leftSide >= height) continue;
				var rightSide = i + a + 1;
				if (rightSide < 0) continue;
				if (rightSide >= height) continue;
				//$"{input[leftSide, j]} == {input[rightSide, j]}".Dump();
				//$"{leftSide} {j} == {rightSide}, {j}".Dump();
				if (input[leftSide, j] != input[rightSide, j])
				{
					isValid = false;
					numErrors++;
				}
			}
		}
		/*if (isValid)
		{
			$"Found at i = {i + 1}".Dump();	
			sum += Math.Abs((i + 1) * 100);
		}*/
		
		if (numErrors == 1)
		{
			$"Found at i = {i + 1}".Dump();	
			sum += Math.Abs((i + 1) * 100);
		}
	}
	return sum == 0 ? null : sum;
}

// You can define other methods, fields, classes and namespaces here