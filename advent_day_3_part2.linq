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
	
	var pointMap = new Point[height + 2, width + 2];

	var isNumberRegex = new Regex("[0-9]");
	
	for (var i = 1; i <= height; i++)
	{
		for(var j = 1; j <= width; j++)
		{
			if (engineMap[i,j] == '*')
			{
				var numberOfMatches = 0;
				     if (isNumberRegex.IsMatch(engineMap[i -1, j -1] + "")) numberOfMatches++;
				else if (isNumberRegex.IsMatch(engineMap[i -1, j] + ""))    numberOfMatches++;
				else if (isNumberRegex.IsMatch(engineMap[i -1, j +1] + "")) numberOfMatches++;
				     if (isNumberRegex.IsMatch(engineMap[i   , j -1] + "")) numberOfMatches++;
				     if (isNumberRegex.IsMatch(engineMap[i +1, j -1] + "")) numberOfMatches++;
				else if (isNumberRegex.IsMatch(engineMap[i +1, j] + ""))    numberOfMatches++;
				else if (isNumberRegex.IsMatch(engineMap[i +1, j +1] + "")) numberOfMatches++;
				     if (isNumberRegex.IsMatch(engineMap[i   , j +1] + "")) numberOfMatches++;
				
				if(isNumberRegex.IsMatch(engineMap[i +1, j +1] + "") && !isNumberRegex.IsMatch(engineMap[i +1, j] + "")) numberOfMatches++;
				if(isNumberRegex.IsMatch(engineMap[i -1, j +1] + "") && !isNumberRegex.IsMatch(engineMap[i -1, j] + "")) numberOfMatches++;
				
				if (numberOfMatches >= 2)
				{
					if (isNumberRegex.IsMatch(engineMap[i -1, j -1] + "")) pointMap[i -1, j -1] = new Point{i = i,j = j};
					if (isNumberRegex.IsMatch(engineMap[i   , j -1] + "")) pointMap[i   , j -1] = new Point{i = i,j = j};
					if (isNumberRegex.IsMatch(engineMap[i +1, j -1] + "")) pointMap[i +1, j -1] = new Point{i = i,j = j};
					if (isNumberRegex.IsMatch(engineMap[i -1, j +1] + "")) pointMap[i -1, j +1] = new Point{i = i,j = j};
					if (isNumberRegex.IsMatch(engineMap[i   , j +1] + "")) pointMap[i   , j +1] = new Point{i = i,j = j};
					if (isNumberRegex.IsMatch(engineMap[i +1, j +1] + "")) pointMap[i +1, j +1] = new Point{i = i,j = j};
					if (isNumberRegex.IsMatch(engineMap[i -1, j   ] + "")) pointMap[i -1, j   ] = new Point{i = i,j = j};
					if (isNumberRegex.IsMatch(engineMap[i +1, j   ] + "")) pointMap[i +1, j   ] = new Point{i = i,j = j};
				}
			}
		}
	}
	
	var hasChanged = false;
	
	do 
	{
		hasChanged = false;
		for (var i = 1; i <= height; i++)
		{
			for(var j = 1; j <= width; j++)
			{
				if (isNumberRegex.IsMatch(engineMap[i , j] + "") && pointMap[i , j - 1] != null)
				{
					if (pointMap[i,j] == null)
					{
						hasChanged = true;
					}
					pointMap[i,j] =  pointMap[i , j - 1];
				}
				
				if (isNumberRegex.IsMatch(engineMap[i , j] + "") && pointMap[i -1 , j - 1] != null)
				{
					if (pointMap[i,j] == null)
					{
						hasChanged = true;
					}
					pointMap[i,j] =  pointMap[i -1 , j - 1];
				}
				
				if (isNumberRegex.IsMatch(engineMap[i , j] + "") && pointMap[i - 1 , j] != null)
				{
					if (pointMap[i,j] == null)
					{
						hasChanged = true;
					}
					pointMap[i,j] =  pointMap[i - 1 , j];
				}
				
				if (isNumberRegex.IsMatch(engineMap[i , j] + "") && pointMap[i , j + 1] != null)
				{
					if (pointMap[i,j] == null)
					{
						hasChanged = true;
					}
					pointMap[i,j] =  pointMap[i , j + 1];
				}

				if (isNumberRegex.IsMatch(engineMap[i , j] + "") && pointMap[i - 1 , j + 1] != null)
				{
					if (pointMap[i,j] == null)
					{
						hasChanged = true;
					}
					pointMap[i,j] =  pointMap[i , j + 1];
				}

				if (isNumberRegex.IsMatch(engineMap[i , j] + "") && pointMap[i + 1 , j] != null)
				{
					if (pointMap[i,j] == null)
					{
						hasChanged = true;
					}
					pointMap[i,j] =  pointMap[i + 1 , j];
				}
				
				if (isNumberRegex.IsMatch(engineMap[i , j] + "") && pointMap[i + 1 , j - 1] != null)
				{
					if (pointMap[i,j] == null)
					{
						hasChanged = true;
					}
					pointMap[i,j] =  pointMap[i + 1 , j - 1];
				}
				
				if (isNumberRegex.IsMatch(engineMap[i , j] + "") && pointMap[i + 1 , j + 1] != null)
				{
					if (pointMap[i,j] == null)
					{
						hasChanged = true;
					}
					pointMap[i,j] =  pointMap[i + 1 , j + 1];
				}
			}
		}
		
	} while (hasChanged);
	
	decimal sum = 0;
	
	var hasData = false;
	Point point = null;
	var carry = new StringBuilder();
	
	var pointToNumbers = new Dictionary<Point,List<decimal>>();
	
	for (var i = 1; i <= height; i++)
	{
		if (hasData)
		{
			hasData = false;
			if (!pointToNumbers.ContainsKey(point))
			{
				pointToNumbers[point] = new List<decimal>();
			}
			
			pointToNumbers[point].Add(decimal.Parse(carry.ToString()));
			carry = new StringBuilder();
			point = null;
		}
			
		for(var j = 1; j <= width; j++)
		{
			if (pointMap[i,j] != null && isNumberRegex.IsMatch(engineMap[i, j] + ""))
			{
				carry.Append(engineMap[i,j]);
				hasData = true;
				point = pointMap[i,j];
			}
			else if (hasData)
			{
				hasData = false;
				if (!pointToNumbers.ContainsKey(point))
				{
					pointToNumbers[point] = new List<decimal>();
				}
				
				pointToNumbers[point].Add(decimal.Parse(carry.ToString()));
				carry = new StringBuilder();
				point = null;
			}
		}
	}
	//engineMap.Dump();
	pointMap.Dump();
	pointToNumbers.Dump();
	
	foreach(var keyValue in pointToNumbers)
	{
		if (keyValue.Value.Count == 2)
		{
			sum += keyValue.Value[0] * keyValue.Value[1];
		}
	}
	sum.Dump();
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