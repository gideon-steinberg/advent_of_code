<Query Kind="Program" />

void Main()
{
	var input1= @"???.### 1,1,3
.??..??...?##. 1,1,3
?#?#?#?#?#?#?#? 1,3,1,6
????.#...#... 4,1,1
????.######..#####. 1,6,5
?###???????? 3,2,1";

var input2 = @"";

var input3 = "?#???##?????????? 1,5,1,1,1";

	var parts = input1.Split(Environment.NewLine);
	
	foreach (var line in parts)
	{
		var split = line.Split(" ");
		var puzzleInput = split[0];

		var puzzleOutput = split[1].Split(",").Select(l=> Int32.Parse(l)).ToList();
		
		waysComplete.Add(new HashSet<string>());
		
		/*var realPuzzleInput = "";
		var realPuzzleOutput = new List<int>();
		
		for (var i = 0; i < 5; i++)
		{
			realPuzzleInput += puzzleInput;
			if (i != 4)
			{
				realPuzzleInput += "?";
			}
			realPuzzleOutput.AddRange(puzzleOutput);
		}
		
		//realPuzzleOutput.Dump();
		
		while (realPuzzleInput.Contains(".."))
		{
			realPuzzleInput = realPuzzleInput.Replace("..", ".");
		}
		
		realPuzzleInput.Dump();*/
		
		Processline(puzzleInput, puzzleOutput, 0, "");
	}
	
	ways.Dump();
	
	waysComplete.Select( w => w.Count()).Sum().Dump();
	//waysComplete.Select( w => w.Count()).Dump();
	//waysComplete.SelectMany( w => w).Distinct().Count().Dump();
}

public decimal ways = 0;

public List<HashSet<string>> waysComplete = new List<HashSet<string>>();

public void Processline(string puzzleInput, List<int> puzzleOutput, int currentIndex, string currentTest)
{
	if (currentIndex >= puzzleOutput.Count())
	{
		if (currentTest.Count() > puzzleInput.Count())
		{
			return;
		}
		
		for (var i = currentTest.Count(); i < puzzleInput.Count(); i++)
		{
			currentTest = currentTest += '.';
		}
		
		for (var i =0; i < puzzleInput.Count(); i++)
		{
			if (puzzleInput[i] == '.' && currentTest[i] == '#')
			{
				return;
			}
			if (puzzleInput[i] == '#' && currentTest[i] == '.')
			{
				return;
			}
		}
		//puzzleInput.Dump();
		//currentTest.Dump();
		waysComplete.Last().Add(currentTest);
		ways++;
		return;
	}
	
	/*if (currentTest.Count() >= puzzleInput.Count())
	{
		return;
	}*/
	
	for (var i = 0; i < currentTest.Count(); i++)
	{
		if (i >= puzzleInput.Count())
		{
			return;
		}
		if (puzzleInput[i] == '.' && currentTest[i] == '#')
		{
			return;
		}
		if (puzzleInput[i] == '#' && currentTest[i] == '.')
		{
			return;
		}
	}
	
	var currentNumber = puzzleOutput[currentIndex];
	
	var numberOfDots = currentIndex == 0 ? 0 : 1;
	
	var toBePlacedIndex = currentTest.Count();
	
	while (toBePlacedIndex + currentNumber <= puzzleInput.Count())
	{
		var toTest = currentTest;
		for (var i=0; i < numberOfDots;i++)
		{
			toTest += '.';
		}
		for (var i=0; i < currentNumber;i++)
		{
			toTest += '#';
		}
		//toTest.Dump();
		Processline(puzzleInput, puzzleOutput, currentIndex + 1, toTest);
		toBePlacedIndex++;
		numberOfDots++;
	}
}