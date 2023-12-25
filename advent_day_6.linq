<Query Kind="Program" />

void Main()
{
	var input1 = @"Time:      7  15   30
	Distance:  9  40  200";
	var input2 = @"";

	var parts = input1.Split(Environment.NewLine);
	decimal number = 0;
	var times = parts[0].Split(" ").Select(p => p.Trim()).Where(p=>Decimal.TryParse(p, out number)).Select(p => Decimal.Parse(p)).ToList();
	var distances = parts[1].Split(" ").Select(p => p.Trim()).Where(p=>Decimal.TryParse(p, out number)).Select(p => Decimal.Parse(p)).ToList();
	var possibilities = new List<decimal>();
	
	var ways = 0;
	for(var i = 0; i < times.Count();i++)
	{
		ways = 0;	
		var time = times[i];
		var distance = distances[i];
		for (var j = 0; j < time; j++)
		{
			var temp = j * (time - j);
			if (temp > distance) ways++;
		}
		possibilities.Add(ways);
	}
	
	var sum = possibilities.Aggregate(1m, (sum, next) => sum * next);
	sum.Dump();
	
	var totalTime = Decimal.Parse(times.Aggregate("", (total, next) => total + next));
	var totalDistance = Decimal.Parse(distances.Aggregate("", (total, next) => total + next));
	
	ways = 0;
	for (var j = 0; j < totalTime; j++)
	{
		var temp = j * (totalTime - j);
		if (temp > totalDistance) ways++;
	}
	ways.Dump();
}

// You can define other methods, fields, classes and namespaces here
