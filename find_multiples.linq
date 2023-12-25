<Query Kind="Program" />

void Main()
{
	var i = 2409215;
	//var i = 131;
	for (var j = 2; j < Math.Sqrt(i) + 1; j++)
	{
		if (i % j == 0) $"{j}, {i/j}".Dump();
	}
	
//	5, 5300273
//11, 2409215
//55, 481843
//26501365
}

// You can define other methods, fields, classes and namespaces here