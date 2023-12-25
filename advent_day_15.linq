<Query Kind="Program" />

void Main()
{
	var input1 = @"rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";
	var input2="";
	
	var parts = input1.Split(",");
	
	var sum = 0;
	foreach (var part in parts)
	{
		var hash = 0;
		foreach (var c in part.ToCharArray())
		{
			hash += (int)c;
			hash *= 17;
			hash %= 256;
		}
		sum += hash;
	}
	sum.Dump();
	
	var hashSet = new List<Item>[256];
	
	for (var i = 0; i < 256; i++)
	{
		hashSet[i] = (new List<Item>());
	}
	
	foreach (var part in parts)
	{
		var hash = 0;
		var array = part.ToCharArray();
		if (array.Last() == '-')
		{
			var label = "";
			for (var i = 0; i < array.Count() - 1; i++)
			{
				hash += (int)array[i];
				hash *= 17;
				hash %= 256;
				label += array[i];
			}
			hashSet[hash] = hashSet[hash].Where(i => i.label != label).ToList();
		}
		else if (array.Contains('='))
		{
			var p = part.Split("=");
			hash = 0;
			var label = p[0];
			foreach (var c in label.ToCharArray())
			{
				hash += (int)c;
				hash *= 17;
				hash %= 256;
			}
			
			var found = false;
			
			for (var i = 0; i < hashSet[hash].Count(); i++)
			{
				if (hashSet[hash][i].label == label)
				{
					found = true;
					hashSet[hash][i].focalLength = Int32.Parse(p[1]);
				}
			}
			
			if (!found)
			{
				var item = new Item { label = label, focalLength = Int32.Parse(p[1])};
				hashSet[hash].Add(item);
			}
		}
	}
	
	sum = 0;
	for (var i = 0; i <256; i++)
	{
		for (var j = 0; j < hashSet[i].Count(); j++)
		{
			var item = hashSet[i][j];
			sum += (i + 1) * item.focalLength * (j + 1);
		}
	}
	
	sum.Dump();
}

public class Item
{
	public string label;
	public int focalLength;
}

// You can define other methods, fields, classes and namespaces here