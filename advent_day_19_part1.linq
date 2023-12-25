<Query Kind="Program" />

void Main()
{
	var input1 = @"px{a<2006:qkq,m>2090:A,rfg}
pv{a>1716:R,A}
lnx{m>1548:A,A}
rfg{s<537:gd,x>2440:R,A}
qs{s>3448:A,lnx}
qkq{x<1416:A,crn}
crn{x>2662:A,R}
in{s<1351:px,qqz}
qqz{s>2770:qs,m<1801:hdj,R}
gd{a>3333:R,R}
hdj{m>838:A,pv}

{x=787,m=2655,a=1222,s=2876}
{x=1679,m=44,a=2067,s=496}
{x=2036,m=264,a=79,s=2244}
{x=2461,m=1339,a=466,s=291}
{x=2127,m=1623,a=2188,s=1013}";

var input2 = @"";

	var lines = input1.Split(Environment.NewLine);
	
	var xValueRegex = new Regex("x=(\\d+),");
	var mValueRegex = new Regex("m=(\\d+),");
	var aValueRegex = new Regex("a=(\\d+),");
	var sValueRegex = new Regex("s=(\\d+)");
	
	var ruleIdRegex = new Regex("(\\w+){");
	
	var lessThanRegex = new Regex("(\\w+)<(\\d+):(\\w+)");
	var greaterThanRegex = new Regex("(\\w+)>(\\d+):(\\w+)");
	
	var completedRules = false;
	
	var parts = new List<Part>();
	var rules = new Dictionary<string, Rule>();
	
	foreach (var line in lines)
	{
		if (!completedRules)
		{
			if (line.Count() == 0)
			{
				completedRules = true;
			}
			else 
			{
				var matches = ruleIdRegex.Matches(line);
				var id = matches.First().Groups[1].Value;
				var replacedLine = line.Split("{")[1].Replace("}", "");
				var rs = replacedLine.Split(",");
				
				var rule = new Rule
				{
					id = id	
				};
				
				foreach (var r in rs)
				{
					if (lessThanRegex.IsMatch(r))
					{
						matches = lessThanRegex.Matches(r);
						var statement = new Statement
						{
							hasCondition = true,
							isLessThan = true,
							isGreaterThan = false,
							idToCheck = matches.First().Groups[1].Value,
							valueToCheck = Int32.Parse(matches.First().Groups[2].Value),
							labelToGoTo = matches.First().Groups[3].Value
						};
						rule.statements.Add(statement);
					}
					else if (greaterThanRegex.IsMatch(r))
					{
						matches = greaterThanRegex.Matches(r);
						var statement = new Statement
						{
							hasCondition = true,
							isLessThan = false,
							isGreaterThan = true,
							idToCheck = matches.First().Groups[1].Value,
							valueToCheck = Int32.Parse(matches.First().Groups[2].Value),
							labelToGoTo = matches.First().Groups[3].Value
						};
						rule.statements.Add(statement);
					}
					else
					{
						var statement = new Statement
						{
							hasCondition = false,
							isLessThan = false,
							isGreaterThan = false,
							labelToGoTo = r
						};
						rule.statements.Add(statement);
					}
				}
				
				rules.Add(rule.id, rule);
			}
		}
		else 
		{
				var matches = xValueRegex.Matches(line);
				var x = Int32.Parse(matches.First().Groups[1].Value);
				
				matches = mValueRegex.Matches(line);
				var m = Int32.Parse(matches.First().Groups[1].Value);
				
				matches = aValueRegex.Matches(line);
				var a = Int32.Parse(matches.First().Groups[1].Value);
				
				matches = sValueRegex.Matches(line);
				var s = Int32.Parse(matches.First().Groups[1].Value);
				
				parts.Add(new Part { x = x, m = m, a = a, s = s});
		}
	}
	
	decimal sum = 0;
	
	foreach (var part in parts)
	{
		var currentRule = rules["in"];
		var isComplete = false;
		while (!isComplete)
		{
			var nextLabel = ProcessRule(part, currentRule, rules);
			if (nextLabel == "A")
			{
				sum += part.x + part.a + part.s + part.m;
				isComplete = true;
			}
			else if (nextLabel == "R")
			{
				isComplete = true;
			}
			else
			{
				currentRule = rules[nextLabel];
			}
		}
	}
	
	sum.Dump();
}

public string ProcessRule(Part part, Rule rule, Dictionary<string, Rule> rules)
{
	var currentRule = rule;
	while (true)
	{
		foreach (var statement in rule.statements)
		{
			if (statement.hasCondition)
			{
				if (statement.isLessThan)
				{
					switch (statement.idToCheck)
					{
						case "x":
							if (part.x < statement.valueToCheck) return statement.labelToGoTo;
							break;
						case "m":
							if (part.m < statement.valueToCheck) return statement.labelToGoTo;
							break;
						case "a":
							if (part.a < statement.valueToCheck) return statement.labelToGoTo;
							break;
						case "s":
							if (part.s < statement.valueToCheck) return statement.labelToGoTo;
							break;
					}
				}
				else if (statement.isGreaterThan)
				{
					switch (statement.idToCheck)
					{
						case "x":
							if (part.x > statement.valueToCheck) return statement.labelToGoTo;
							break;
						case "m":
							if (part.m > statement.valueToCheck) return statement.labelToGoTo;
							break;
						case "a":
							if (part.a > statement.valueToCheck) return statement.labelToGoTo;
							break;
						case "s":
							if (part.s > statement.valueToCheck) return statement.labelToGoTo;
							break;
					}
				}
				else 
				{
					$"Bad Rule {rule} on part {part}".Dump();
				}
			}
			else
			{
				return statement.labelToGoTo;
			}
		}
	}
}

public class Rule
{
	public List<Statement> statements = new List<Statement>();
	public string id;
}

public class Statement
{
	public string idToCheck;
	public int valueToCheck;
	public bool hasCondition;
	public bool isLessThan;
	public bool isGreaterThan;
	public string labelToGoTo;
}

public class Part
{
	public int x;
	public int m;
	public int a;
	public int s;
}