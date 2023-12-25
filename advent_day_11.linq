<Query Kind="Program" />

void Main()
{
	var input1 = @"...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....";

var input2 = @"";

	var parts = input1.Split(Environment.NewLine);
	
	var height = parts.Count();
	var width = parts[0].Count();
	
	var galaxies = new List<Point>();
	
	var rowsWithGalaxies = new HashSet<int>();
	var colsWithGalaxies = new HashSet<int>();
	
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			if (parts[i][j] == '#')
			{
				galaxies.Add(new Point{ i = i, j = j});
				rowsWithGalaxies.Add(i);
				colsWithGalaxies.Add(j);
			}
		}
	}
	
	decimal sum = 0;
	var pairs = 0;
	
	var amountExpaned = 1000000 - 1;
	//amountExpaned = 100 -1;
	
	for (var a = 0; a < galaxies.Count(); a++)
	{
		for (var b = a + 1; b < galaxies.Count(); b++)
		{
			var firstGalaxy = galaxies[a];
			var secondGalaxy = galaxies[b];
			
			var rowLength = 0;
			for ( var c = firstGalaxy.i + 1; c <= secondGalaxy.i;c++)
			{
				rowLength++;
				if (!rowsWithGalaxies.Contains(c)) rowLength+=amountExpaned;
			}
			
			if (firstGalaxy.j > secondGalaxy.j)
			{
				firstGalaxy = galaxies[b];
				secondGalaxy = galaxies[a];
			}
			
			var colLength = 0;
			for ( var c = firstGalaxy.j + 1; c <= secondGalaxy.j;c++)
			{
				colLength++;
				if (!colsWithGalaxies.Contains(c)) colLength+= amountExpaned;
			}
			sum += rowLength;
			sum += colLength;
			pairs++;
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