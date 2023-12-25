<Query Kind="Program" />

void Main()
{
	var input1 = @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";

var input2 = @"";

	var seedToSoilMap = new List<Triple>();
	var soilToFertilizerMap = new List<Triple>();
	var fertilizerToWaterMap = new List<Triple>();
	var waterToLightMap = new List<Triple>();
	var lightToTemperatureMap = new List<Triple>();
	var temperatureToHumidityMap = new List<Triple>();
	var humidityToLocationMap = new List<Triple>();
	
	var seeds = new List<decimal>();
	
	var parts = input1.Split(Environment.NewLine);
	var triplesRegex = new Regex("(\\d+) (\\d+) (\\d+)");
	var numberRegex = new Regex("(\\d+)");
	
	var mode = Mode.None;
	
	foreach (var line in parts)
	{
		if (line.StartsWith("seeds:"))
		{
			var matches = numberRegex.Matches(line);
			foreach (Match match in matches)
			{
				seeds.Add(decimal.Parse(match.Groups[0].Value));
			}
		}		
		else if (line.StartsWith("seed-to-soil map:"))
		{
			mode = Mode.SeedToSoil;
		}
		else if (line.StartsWith("soil-to-fertilizer map:"))
		{
			mode = Mode.SoilToFertilizer;
		} 
		else if (line.StartsWith("fertilizer-to-water map:"))
		{
			mode = Mode.FertilizerToWater;
		}
		else if (line.StartsWith("water-to-light map:"))
		{
			mode = Mode.WaterToLight;
		}
		else if (line.StartsWith("light-to-temperature map:"))
		{
			mode = Mode.LightToTemperature;
		}
		else if (line.StartsWith("temperature-to-humidity map:"))
		{
			mode = Mode.TemperatureToHumidity;
		}
		else if (line.StartsWith("humidity-to-location map:"))
		{
			mode = Mode.HumidityToLocation;
		}
		else 
		{
			var matches = triplesRegex.Matches(line);
			if (matches.Count > 0)
			{
				var destinationRange = decimal.Parse(matches[0].Groups[1].Value);
				var sourceRange = decimal.Parse(matches[0].Groups[2].Value);
				var length = decimal.Parse(matches[0].Groups[3].Value);
				switch (mode)
				{
					case Mode.None:
						break;
					case Mode.SeedToSoil:
						AddToMap(seedToSoilMap, destinationRange, sourceRange, length);
						break;
					case Mode.SoilToFertilizer:
					    AddToMap(soilToFertilizerMap, destinationRange, sourceRange, length);
						break;
					case Mode.FertilizerToWater:
						AddToMap(fertilizerToWaterMap, destinationRange, sourceRange, length);
						break;
					case Mode.WaterToLight:
						AddToMap(waterToLightMap, destinationRange, sourceRange, length);
						break;
					case Mode.LightToTemperature:
						AddToMap(lightToTemperatureMap, destinationRange, sourceRange, length);
						break;
					case Mode.TemperatureToHumidity:
						AddToMap(temperatureToHumidityMap, destinationRange, sourceRange, length);
						break;
					case Mode.HumidityToLocation:
						AddToMap(humidityToLocationMap, destinationRange, sourceRange, length);
						break;
				}
			}
		}
	}
	
	decimal? smallestLocation = null;
	
	foreach(var seed in seeds)
	{
		var soil = Convert(seedToSoilMap, seed);
		var fert = Convert(soilToFertilizerMap, soil);
		var water = Convert(fertilizerToWaterMap, fert);
		var light = Convert(waterToLightMap, water);
		var temp = Convert(lightToTemperatureMap, light);
		var humid = Convert(temperatureToHumidityMap, temp);
		var location = Convert(humidityToLocationMap, humid);
		if (smallestLocation == null || location < smallestLocation)
		{
			smallestLocation = location;
		}
	}
	//smallestLocation.Dump();
	
	smallestLocation = null;
	
	var i = 0;
	var isSuccessful = false;
	while (!isSuccessful)
	{
		if (i % 10000000 == 0)
		{
			i.Dump();
		}
	
		var humid = ConvertOpposite(humidityToLocationMap, i);
		var temp = ConvertOpposite(temperatureToHumidityMap, humid);
		var light = ConvertOpposite(lightToTemperatureMap, temp);
		var water = ConvertOpposite(waterToLightMap, light);
		var fert = ConvertOpposite(fertilizerToWaterMap, water);
		var soil = ConvertOpposite(soilToFertilizerMap, fert);
		var seed = ConvertOpposite(seedToSoilMap, soil);
		for (var j = 0; j < seeds.Count(); j+=2)
		{
			var start = seeds[j];
			var range = seeds[j+1];
			var offset = seed - start;
			if (offset >= 0 && offset < range)
			{
				seed.Dump();
				soil.Dump();
				fert.Dump();
				water.Dump();
				light.Dump();
				temp.Dump();
				humid.Dump();
				i.Dump();
				isSuccessful = true;
			}
		}
		i++;
	}
}

public void AddToMap(List<Triple> map, decimal destinationRange, decimal sourceRange, decimal length)
{
	map.Add(new Triple {destinationRange = destinationRange, sourceRange = sourceRange, length = length});
}

public decimal Convert(List<Triple> map, decimal input)
{
	foreach (var triple in map)
	{
		var offset = input - triple.sourceRange;
		if (offset >= 0 && offset < triple.length)
		{
			return triple.destinationRange + offset;
		}
	}
	return input;
}

public decimal ConvertOpposite(List<Triple> map, decimal input)
{
	foreach (var triple in map)
	{
		var offset = input - triple.destinationRange;
		if (offset >= 0 && offset < triple.length)
		{
			return triple.sourceRange + offset;
		}
	}
	return input;
}


public class Triple
{
	public decimal destinationRange;
	public decimal sourceRange;
	public decimal length;
}

enum Mode
{
	None,
	SeedToSoil,
	SoilToFertilizer,
	FertilizerToWater,
	WaterToLight,
	LightToTemperature,
	TemperatureToHumidity,
	HumidityToLocation
}