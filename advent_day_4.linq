<Query Kind="Program" />

void Main()
{
	var input1 = @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";

var input2 = @"";

	decimal sum = 0;
	var numberRegex = new Regex("(\\d+)");
	var cardRegex = new Regex("Card\\s+(\\d+):");
	var cardToScore = new Dictionary<decimal, decimal>();
	
	foreach (var line in input2.Split(Environment.NewLine))
	{
		var matches = cardRegex.Matches(line);
		var cardNumber = decimal.Parse(matches.First().Groups[1].Value);
		
		var lineWithoutCardNumber = line.Split(":")[1];
		var parts = lineWithoutCardNumber.Split("|");
		matches = numberRegex.Matches(parts[0]);
		var winningNumbers = new List<decimal>();
		foreach (Match match in matches)
		{
			winningNumbers.Add(decimal.Parse(match.Groups[0].Value));
		}
		
		matches = numberRegex.Matches(parts[1]);
		var numbers = new List<decimal>();
		foreach (Match match in matches)
		{
			numbers.Add(decimal.Parse(match.Groups[0].Value));
		}
		
		var amount = 0;
		foreach (var n in winningNumbers)
		{
			if (numbers.Contains(n)) amount++;
		}
		if (amount > 0)
		{
			//cardToScore.Add(cardNumber, (decimal)Math.Pow(2,amount-1));
			cardToScore.Add(cardNumber, (decimal)amount);
		}
		else 
		{
			cardToScore.Add(cardNumber, 0);
		}
	}
	
	var cardNumbers = cardToScore.ToDictionary( kv => kv.Key, kv => 1);
	
	foreach(var cardNumber in cardNumbers.Keys.OrderBy(k => k))
	{
		for (var j = 0; j < cardNumbers[cardNumber]; j++)
		{
			for (var i = cardNumber+1; i <= cardNumber + cardToScore[cardNumber]; i++)
			{
				if (cardNumbers.ContainsKey(i))
				{
					cardNumbers[i]++;
				}
			}
		}
	}
	sum = 0;
	foreach (var val in cardNumbers.Values)
	{
		sum += val;
	}
	sum.Dump();
}

// You can define other methods, fields, classes and namespaces here