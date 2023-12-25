<Query Kind="Program" />

void Main()
{
	var input1 = @"RL

AAA = (BBB, CCC)
BBB = (DDD, EEE)
CCC = (ZZZ, GGG)
DDD = (DDD, DDD)
EEE = (EEE, EEE)
GGG = (GGG, GGG)
ZZZ = (ZZZ, ZZZ)";

	var input2 = @"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)";

var input3 = @"";

var input4= @"LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)";

	var parts = input1.Split(Environment.NewLine);
	
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
	decimal sum = 0;
	var identifier = "AAA";
	int stepsInSequence = steps.Count();
	
	do
	{
		numSteps = numSteps % stepsInSequence;
		
		if (steps[numSteps] == 'L')
		{
			identifier = map[identifier].left;
		}
		else
		{
			identifier = map[identifier].right;
		}
		
		numSteps++;
		sum++;
	} 
	while (identifier != "ZZZ");
	
	sum.Dump();
}

// You can define other methods, fields, classes and namespaces here

public class Node
{
	public string identifier;
	public string left;
	public string right;
}