<Query Kind="Program" />

void Main()
{
	var input1 = @"0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45";

var input2 = @"";
	
	var lines = input1.Split(Environment.NewLine);
	decimal sum1 = 0;
	decimal sum2 = 0;
	
	foreach (var line in lines)
	{
		var layer = line.Split(" ").Select(n => Decimal.Parse(n)).ToList();
		var result = ProcessLayer(layer);
		sum1 += result + layer.Last();
		
		result = ProcessLayerBack(layer);
		sum2 += layer.First() - result;
	}
	
	sum1.Dump();
	sum2.Dump();
}

public decimal ProcessLayer(List<decimal> layer)
{
	if (layer.All(l => l == 0)) return 0;

	var newLayer = new List<decimal>();
	for (var i = 0; i < layer.Count - 1; i++)
	{
		newLayer.Add(layer[i + 1] - layer[i]);
	}
	
	var processedLayer = ProcessLayer(newLayer);
	
	return processedLayer + newLayer.Last();
}

public decimal ProcessLayerBack(List<decimal> layer)
{
	if (layer.All(l => l == 0)) return 0;

	var newLayer = new List<decimal>();
	for (var i = 0; i < layer.Count - 1; i++)
	{
		newLayer.Add(layer[i + 1] - layer[i]);
	}
	
	var processedLayer = ProcessLayerBack(newLayer);
	
	return newLayer.First() - processedLayer;
}
