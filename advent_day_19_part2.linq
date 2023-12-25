<Query Kind="Program">
  <Namespace>System.Numerics</Namespace>
</Query>

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
	
	var ruleIdRegex = new Regex("(\\w+){");
	
	var lessThanRegex = new Regex("(\\w+)<(\\d+):(\\w+)");
	var greaterThanRegex = new Regex("(\\w+)>(\\d+):(\\w+)");
	
	var completedRules = false;
	
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
	}
	
	decimal sum = 0;
	
	var maxNumber = 4000;
	var minNumber = 1;
	
	var currentPart = new PartRange
	{
		xMin = minNumber,
		xMax = maxNumber,
		mMin = minNumber,
		mMax = maxNumber,
		aMin = minNumber,
		aMax = maxNumber,
		sMin = minNumber,
		sMax = maxNumber,
		ruleId = "in"
	};
	
	var parts = new List<PartRange>();
	parts.Add(currentPart);
	while (parts.Any())
	{
		var newParts = new List<PartRange>();
		foreach (var part in parts)
		{
			var currentRule = rules[part.ruleId];
			var newRanges = ProcessRule(part, currentRule, rules);
			foreach (var newRange in newRanges)
			{
				if (newRange.ruleId == "A")
				{
					sum += (newRange.xMax - newRange.xMin + 1) * (newRange.aMax - newRange.aMin + 1) * (newRange.sMax - newRange.sMin + 1) * (newRange.mMax - newRange.mMin + 1);
				}
				else if (newRange.ruleId == "R")
				{
					
				}
				else
				{
					newParts.Add(newRange);
				}
			}

		}
		
		parts = newParts;
	}
	
	sum.Dump();
}

public List<PartRange> ProcessRule(PartRange part, Rule rule, Dictionary<string, Rule> rules)
{
	var currentPart = part.Clone();
	var currentRule = rule;
	PartRange p;
	var partRanges = new List<PartRange>();
	foreach (var statement in rule.statements)
	{
		if (statement.hasCondition)
		{
			if (statement.isLessThan)
			{
				switch (statement.idToCheck)
				{
					case "x":
						if (currentPart.xMax < statement.valueToCheck)
						{
							p = currentPart.Clone();
							p.ruleId = statement.labelToGoTo;
							partRanges.Add(p);
							currentPart.xMin = statement.valueToCheck;
							break;
						}
						if (currentPart.xMin > statement.valueToCheck) break;
						p = currentPart.Clone();
						p.xMax = statement.valueToCheck - 1;
						p.ruleId = statement.labelToGoTo;
						partRanges.Add(p);
						currentPart.xMin = statement.valueToCheck;
						break;
					case "m":
						if (currentPart.mMax < statement.valueToCheck)
						{
							p = currentPart.Clone();
							p.ruleId = statement.labelToGoTo;
							partRanges.Add(p);
							currentPart.mMin = statement.valueToCheck;
							break;
						}
						if (currentPart.mMin > statement.valueToCheck) break;
						p = currentPart.Clone();
						p.mMax = statement.valueToCheck - 1;
						p.ruleId = statement.labelToGoTo;
						partRanges.Add(p);
						currentPart.mMin = statement.valueToCheck;
						break;
					case "a":
						if (currentPart.aMax < statement.valueToCheck)
						{
							p = currentPart.Clone();
							p.ruleId = statement.labelToGoTo;
							partRanges.Add(p);
							currentPart.aMin = statement.valueToCheck;
							break;
						}
						if (currentPart.aMin > statement.valueToCheck) break;
						p = currentPart.Clone();
						p.aMax = statement.valueToCheck - 1;
						p.ruleId = statement.labelToGoTo;
						partRanges.Add(p);
						currentPart.aMin = statement.valueToCheck;
						break;
					case "s":
						if (currentPart.sMax < statement.valueToCheck)
						{
							p = currentPart.Clone();
							p.ruleId = statement.labelToGoTo;
							partRanges.Add(p);
							currentPart.sMin = statement.valueToCheck;
							break;
						}
						if (currentPart.sMin > statement.valueToCheck) break;
						p = currentPart.Clone();
						p.sMax = statement.valueToCheck - 1;
						p.ruleId = statement.labelToGoTo;
						partRanges.Add(p);
						currentPart.sMin = statement.valueToCheck;
						break;
				}
			}
			else if (statement.isGreaterThan)
			{
				switch (statement.idToCheck)
				{
					case "x":
						if (currentPart.xMin > statement.valueToCheck)
						{
							p = currentPart.Clone();
							p.ruleId = statement.labelToGoTo;
							partRanges.Add(p);
							currentPart.xMax = statement.valueToCheck;
							break;
						}
						if (currentPart.xMax < statement.valueToCheck) break;
						p = currentPart.Clone();
						p.xMin = statement.valueToCheck + 1;
						p.ruleId = statement.labelToGoTo;
						partRanges.Add(p);
						currentPart.xMax = statement.valueToCheck;
						break;
					case "m":
						if (currentPart.mMin > statement.valueToCheck)
						{
							p = currentPart.Clone();
							p.ruleId = statement.labelToGoTo;
							partRanges.Add(p);
							currentPart.mMax = statement.valueToCheck;
							break;
						}
						if (currentPart.mMax < statement.valueToCheck) break;
						p = currentPart.Clone();
						p.mMin = statement.valueToCheck + 1;
						p.ruleId = statement.labelToGoTo;
						partRanges.Add(p);
						currentPart.mMax = statement.valueToCheck;
						break;
					case "a":
						if (currentPart.aMin > statement.valueToCheck)
						{
							p = currentPart.Clone();
							p.ruleId = statement.labelToGoTo;
							partRanges.Add(p);
							currentPart.aMax = statement.valueToCheck;
							break;
						}
						if (currentPart.aMax < statement.valueToCheck) break;
						p = currentPart.Clone();
						p.aMin = statement.valueToCheck + 1;
						p.ruleId = statement.labelToGoTo;
						partRanges.Add(p);
						currentPart.aMax = statement.valueToCheck;
						break;
					case "s":
						if (currentPart.sMin > statement.valueToCheck)
						{
							p = currentPart.Clone();
							p.ruleId = statement.labelToGoTo;
							partRanges.Add(p);
							currentPart.sMax = statement.valueToCheck;
							break;
						}
						if (currentPart.sMax < statement.valueToCheck) break;
						p = currentPart.Clone();
						p.sMin = statement.valueToCheck + 1;
						p.ruleId = statement.labelToGoTo;
						partRanges.Add(p);
						currentPart.sMax = statement.valueToCheck;
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
			p = currentPart.Clone();
			p.ruleId = statement.labelToGoTo;
			partRanges.Add(p);
		}
	}
	
	return partRanges;
}

public class Rule
{
	public List<Statement> statements = new List<Statement>();
	public string id;
}

public class Statement
{
	public string idToCheck;
	public decimal valueToCheck;
	public bool hasCondition;
	public bool isLessThan;
	public bool isGreaterThan;
	public string labelToGoTo;
}

public class PartRange
{
	public decimal xMin;
	public decimal xMax;
	public decimal mMin;
	public decimal mMax;
	public decimal aMin;
	public decimal aMax;
	public decimal sMin;
	public decimal sMax;
	public string ruleId;
	
	public PartRange Clone()
	{
		return new PartRange
		{
			xMin = xMin,
			xMax = xMax,
			mMin = mMin,
			mMax = mMax,
			aMin = aMin,
			aMax = aMax,
			sMin = sMin,
			sMax = sMax
		};
	}	
}