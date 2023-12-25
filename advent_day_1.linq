<Query Kind="Program" />

void Main()
{
	string input = @"1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet";

	decimal sum = 0;
	var regex = new Regex("(\\d)");
	input = input.Replace("two", "t2o").Replace("five", "5e").Replace("one", "1e").Replace("eight", "8t").Replace("three", "3").Replace("four", "4").Replace("six", "6").Replace("seven", "7").Replace("nine", "9");
	foreach (var line in input.Split(Environment.NewLine))
	{
		var digits = "";
		var matches = regex.Matches(line);
		digits += matches.First().Value;
		digits += matches.Last().Value;
		sum += decimal.Parse(digits);
	};
	
	sum.Dump();
	
}

// You can define other methods, fields, classes and namespaces here