<Query Kind="Program" />

void Main()
{
	var input1  = @".|...\....
|.-.\.....
.....|-...
........|.
..........
.........\
..../.\\..
.-.-/..|..
.|....-|.\
..//.|....";

var input2 = @"";

	var parts = input1.Split(Environment.NewLine);
	var height = parts.Count();
	var width = parts[0].Count();
	var map = new char[height,width];
	
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			map[i,j] = parts[i][j];
		}
	}
	var sum = 0;
	
	for (var i = 0; i < height; i++)
	{
		sum = Math.Max(sum, CalculateSum(map, height, width, i, -1, Direction.Right));
		sum = Math.Max(sum, CalculateSum(map, height, width, i, width +1, Direction.Left));
		//sum.Dump();
	}
	
	for (var j = 0; j < height; j++)
	{
		sum = Math.Max(sum, CalculateSum(map, height, width, -1, j, Direction.Down));
		sum = Math.Max(sum, CalculateSum(map, height, width, height +1, j, Direction.Up));
	}
	
	//output.Dump();
	sum.Dump();
}

public int CalculateSum(char[,] map, int height, int width, int startingI, int startingJ, Direction startingDirection)
{
	var output = new bool[height,width];
	//output[startingI, startingJ] = true;
	
	var beams = new List<Beam>();
	
	beams.Add(new Beam { i = startingI, j = startingJ, direction = startingDirection});
	
	var seenBeams = new HashSet<Beam>();
	
	while (beams.Any())
	{
		var newBeams = new List<Beam>();
		
		foreach(var beam in beams)
		{
			var newI = beam.i;
			var newJ = beam.j;
			switch (beam.direction)
			{
				case (Direction.Right):
					newJ = beam.j +1;
					break;
				case (Direction.Left):
					newJ = beam.j - 1;
					break;
				case (Direction.Up):
					newI = beam.i -1;
					break;
				case (Direction.Down):
					newI = beam.i + 1;
					break;
			}
			if (newI >= width) 
			{
				continue;
			}
			if (newI < 0) continue;
			if (newJ >= height) continue;
			if (newJ < 0) continue;
			
			switch (map[newI, newJ])
			{
				case '.':
					newBeams.Add(new Beam {i = newI, j = newJ, direction = beam.direction});
					break;
				case '|':
					if (beam.direction == Direction.Up || beam.direction == Direction.Down)
					{
						newBeams.Add(new Beam {i = newI, j = newJ, direction = beam.direction});
					}
					else
					{
						newBeams.Add(new Beam {i = newI, j = newJ, direction = Direction.Up});
						newBeams.Add(new Beam {i = newI, j = newJ, direction = Direction.Down});
					}
					break;
				case '-':
					if (beam.direction == Direction.Right || beam.direction == Direction.Left)
					{
						newBeams.Add(new Beam {i = newI, j = newJ, direction = beam.direction});
					}
					else
					{
						newBeams.Add(new Beam {i = newI, j = newJ, direction = Direction.Right});
						newBeams.Add(new Beam {i = newI, j = newJ, direction = Direction.Left});
					}
					break;
				case '\\':
					switch(beam.direction)
					{
						case (Direction.Up):
							newBeams.Add(new Beam {i = newI, j = newJ, direction = Direction.Left});
							break;
						case (Direction.Down):
							newBeams.Add(new Beam {i = newI, j = newJ, direction = Direction.Right});
							break;
						case (Direction.Right):
							newBeams.Add(new Beam {i = newI, j = newJ, direction = Direction.Down});
							break;
						case (Direction.Left):
							newBeams.Add(new Beam {i = newI, j = newJ, direction = Direction.Up});
							break;
					}
					break;
				case '/':
					switch(beam.direction)
					{
						case (Direction.Up):
							newBeams.Add(new Beam {i = newI, j = newJ, direction = Direction.Right});
							break;
						case (Direction.Down):
							newBeams.Add(new Beam {i = newI, j = newJ, direction = Direction.Left});
							break;
						case (Direction.Right):
							newBeams.Add(new Beam {i = newI, j = newJ, direction = Direction.Up});
							break;
						case (Direction.Left):
							newBeams.Add(new Beam {i = newI, j = newJ, direction = Direction.Down});
							break;
					}
					break;
			}
		}
		
		beams = new List<Beam>();
		
		foreach (var beam in newBeams)
		{
			output[beam.i, beam.j] = true;
			if (!seenBeams.Contains(beam))
			{
				beams.Add(beam);
				seenBeams.Add(beam);
			}
		}
	}
	
	var sum = 0;
	for (var i = 0; i < height; i++)
	{
		for (var j = 0; j < width; j++)
		{
			if (output[i,j])
			{
				sum++;
			}
		}
	}
	if (sum == 7635)
	{
		startingI.Dump();
		startingJ.Dump();
		startingDirection.Dump();
		output.Dump();
		//return sum;
	}	
	return sum;
}

public class Beam
{
	public int i;
	public int j;
	public Direction direction;
	
	public override bool Equals(object obj)
	{
	    var item = obj as Beam;

	    if (item == null)
	    {
	        return false;
	    }

	    return this.i.Equals(item.i) && this.j.Equals(item.j) && this.direction.Equals(item.direction);
	}

	public override int GetHashCode() => (i, j, direction).GetHashCode();
}

public enum Direction
{
	Right,
	Left,
	Up,
	Down
}