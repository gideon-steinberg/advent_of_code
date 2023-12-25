<Query Kind="Program" />

void Main()
{
	var input = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

	decimal sum = 0;
	var gameRegex = new Regex("Game (\\d+)");
	var blueRegex = new Regex("(\\d+) blue");
	var redRegex = new Regex("(\\d+) red");
	var greenRegex = new Regex("(\\d+) green");
	foreach (var line in input.Split(Environment.NewLine))
	{
		var matches = gameRegex.Matches(line);
		var gameNumber = decimal.Parse(matches.First().Groups[1].Value);
		decimal highestBlue = -1;
		decimal highestRed = -1;
		decimal highestGreen = -1;
		
		matches = blueRegex.Matches(line);
		foreach(Match match in matches)
		{
			if (matches.Any())
			{
				highestBlue = Math.Max(highestBlue, decimal.Parse(match.Groups[1].Value));
			}
		}
		
		matches = redRegex.Matches(line);
		foreach(Match match in matches)
		{
			if (matches.Any())
			{
				highestRed = Math.Max(highestRed, decimal.Parse(match.Groups[1].Value));
			}
		}
		
		matches = greenRegex.Matches(line);
		foreach(Match match in matches)
		{
			if (matches.Any())
			{
				highestGreen = Math.Max(highestGreen, decimal.Parse(match.Groups[1].Value));
			}
		}
		
		/*if (highestBlue <= 14 && highestGreen <= 13 && highestRed <= 12)
		{*/
			highestBlue = Math.Max(1, highestBlue);
			highestGreen = Math.Max(1, highestGreen);
			highestRed = Math.Max(1, highestRed);
			var powersum = highestBlue * highestGreen * highestRed;
			sum += powersum;
		//}
	};
	
	sum.Dump();
}

// You can define other methods, fields, classes and namespaces here