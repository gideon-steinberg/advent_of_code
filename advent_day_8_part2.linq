<Query Kind="Program" />

void Main()
{
var input3 = @"";

	var parts = input3.Split(Environment.NewLine);
	
	var steps = parts[0].ToCharArray();
	
	var map = new Dictionary<string, Node>();
	
	var identifierRegex = new Regex("(\\w\\w\\w)");
	
	foreach(var line in parts.Skip(2))
	{
		var matches = identifierRegex.Matches(line);
		
		var node = new Node
		{
			identifier = matches[0].Groups[0].Value,
			left = matches[1].Groups[0].Value,
			right = matches[2].Groups[0].Value,
		};
		
		map.Add(node.identifier, node);
	}
	
	var combinations = new HashSet<string>();
	
	int numSteps = 0;
	var identifiers = map.Where(kv => kv.Value.identifier.EndsWith("A")).Select(kv => kv.Value.identifier).ToHashSet();
	int stepsInSequence = steps.Count();
	
	var periods = new Dictionary<string, List<int>>();
	
	foreach (var identifier in identifiers)
	{
		var temp = identifier;
		periods.Add(identifier, new List<int>());
		
		for (var i = 0; i < 1000000; i++)
		{
			numSteps = i % stepsInSequence;
		
			if (steps[numSteps] == 'L')
			{
				temp = map[temp].left;
			}
			else
			{
				temp = map[temp].right;
			}
			if (temp[2] == 'Z')
			{
				periods[identifier].Add(i);
			}
		}
	}
	
	var dedupedPeriods = new Dictionary<int, decimal>();
	
	foreach(var val in periods.Values)
	{
		dedupedPeriods.Add((val[1] - val[0]), val[1] - val[0]);
	}
	
	decimal sum = 1;
	
	var targets = new Dictionary<int, decimal>();
	
	foreach(var val in dedupedPeriods.Keys)
	{
		targets.Add(val/stepsInSequence, val/stepsInSequence);
		sum*= val/stepsInSequence;
	}
	
	/*while (targets.Values.Distinct().Count() > 1)
	{
		var lowest = targets.OrderBy(p => p.Value).First();
		targets[lowest.Key] = lowest.Key + lowest.Value;
		lowest.Dump();
	}
	
	targets.Dump();*/
	
	/*foreach(var val in dedupedPeriods.Keys)
	{
		for (var i = 2; i < Math.Sqrt(val);i++)
		{
			if (val % i == 0)
			{
				val.Dump();
				i.Dump();
				(val / i).Dump();
			}
		}
	}*/
	sum *= 277;
	sum.Dump();
	
	/*foreach(var val in dedupedPeriods.Keys)
	{
		var num = (double)sum / val;
		for (var i = 2; i < Math.Sqrt(num);i++)
		{
			if (num % i == 0)
			{
				num.Dump();
				i.Dump();
				(num / i).Dump();
			}
		}
	}*/
	
}

// You can define other methods, fields, classes and namespaces here

public class Node
{
	public string identifier;
	public string left;
	public string right;
}