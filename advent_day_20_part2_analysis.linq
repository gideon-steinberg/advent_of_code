<Query Kind="Program" />

void Main()
{
	var input1 = @"broadcaster -> a, b, c
%a -> b
%b -> c
%c -> inv
&inv -> a";

	var input2 = @"broadcaster -> a
%a -> inv, con
&inv -> b
%b -> con
&con -> output";

var input3 = @"";
var input4 = @"";

	var nodes = new Dictionary<string, Node>();
	var parts = input4.Split(Environment.NewLine);
	
	foreach (var part in parts)
	{
		var split = part.Split(" ");
		if (part.StartsWith('%'))
		{
			var id = split[0].Replace("%", "");
			nodes.Add(id, new Node()
			{
				id = id,
				type = NodeType.FlipFlop,
				connectedNodes = split.Skip(2).Select(s => s.Replace(",", "")).ToList()
			});
		}
		else if (part.StartsWith('&'))
		{
			var id = split[0].Replace("&", "");
			nodes.Add(id, new Node()
			{
				id = id,
				type = NodeType.Conjunction,
				connectedNodes = split.Skip(2).Select(s => s.Replace(",", "")).ToList()
			});
		}
		else if (part.StartsWith("broadcaster"))
		{
			nodes.Add("broadcaster", new Node()
			{
				id = "broadcaster",
				type = NodeType.BroadCaster,
				connectedNodes = split.Skip(2).Select(s => s.Replace(",", "")).ToList()
			});
		}
		else 
		{
			"bad input".Dump();
		}
	}
	
	var cycles = new Dictionary<string, List<int>>();
	
	nodes.Add("rx", new Node{id = "rx"});
	
	foreach (var node in nodes.Values)
	{
		foreach (var nId in node.connectedNodes)
		{
			if (!nodes.ContainsKey(nId))
			{
				continue;
			}
			var n = nodes[nId];
			n.incomingNodes.Add(node.id);
		}
		cycles.Add(node.id, new List<int>());
		cycles[node.id].Add(0);
	}
	
	decimal highPulses = 0;
	decimal lowPulses = 0;
	
	for (var i = 0; i < 100000; i++)
	{
		var pulses = new List<Pulse>();
		pulses.Add(new Pulse { toId = "broadcaster"});
		
		while (pulses.Any())
		{
			highPulses += pulses.Count(p => p.isHigh);
			lowPulses += pulses.Count(p => !p.isHigh);
			var newPulses = new List<Pulse>();
			
			foreach (var pulse in pulses)
			{
				if (!nodes.ContainsKey(pulse.toId))
				{
					continue;
				}
				var node = nodes[pulse.toId];
				/*var countOfLast = cycles[node.id].Count();
				cycles[node.id][countOfLast - 1]++;
				if (node.isOn)
				{
					cycles[node.id].Add(0);
				}*/
				switch (node.type)
				{
					case(NodeType.BroadCaster):
						foreach(var newId in node.connectedNodes)
						{
							newPulses.Add(new Pulse {toId = newId, fromId = node.id});
						}
						break;
					case(NodeType.Conjunction):
						if (pulse.isHigh)
						{
							node.nodesReceivedHighFrom.Add(pulse.fromId);
						}
						else 
						{
							node.nodesReceivedHighFrom.Remove(pulse.fromId);
						}
						var sendHigh = node.nodesReceivedHighFrom.Count() != node.incomingNodes.Count();
						foreach(var newId in node.connectedNodes)
						{
							newPulses.Add(new Pulse {toId = newId, fromId = node.id, isHigh = sendHigh});
						}
						break;
					case(NodeType.FlipFlop):
						if (!pulse.isHigh)
						{
							node.isOn = !node.isOn;
							if (node.isReversed) node.isOn = !node.isOn;
							foreach(var newId in node.connectedNodes)
							{
								newPulses.Add(new Pulse {toId = newId, isHigh = node.isOn, fromId = node.id});
							}
						}
						break;
					case(NodeType.None):
						node.isOn = !pulse.isHigh;
						break;
				}
			}
			pulses = newPulses;
		}
		
		/*foreach (var node in nodes.Values)
		{
			var val = node.isOn ? '1' : '0';
			Console.Write(val);
		}
		Console.WriteLine();*/
		
		foreach (var node in nodes.Values)
		{
			var countOfLast = cycles[node.id].Count();
			cycles[node.id][countOfLast - 1]++;
			if (node.isOn)
			{
				cycles[node.id].Add(0);
			}
		}
	}
	
	var counts = new Dictionary<int, int>(); 
	
	foreach (var cycle in cycles)
	{
		var sum = 0;
		Console.Write(cycle.Key + " : ");
		foreach (var val in cycle.Value)
		{
			sum += val;
			//Console.Write($"{sum}, ");
			if (!counts.ContainsKey(sum))
			{
				counts.Add(sum, 0);
			}
			counts[sum]++;
		}
		//Console.WriteLine();
	}
	
	counts.Dump();
	
	//nodes.Dump();
	
	//cycles.Dump();
}

// You can define other methods, fields, classes and namespaces here

public class Node
{
	public List<string> connectedNodes = new List<string>();
	public string id;
	public NodeType type = NodeType.None;
	public bool isOn;
	public HashSet<string> nodesReceivedHighFrom = new HashSet<string>();
	public HashSet<string> incomingNodes = new HashSet<string>();
	public bool isReversed = false;
}

public enum NodeType
{
	None,
	BroadCaster,
	Conjunction,
	FlipFlop
}

public class Pulse
{
	public string toId;
	public bool isHigh;
	public string fromId;
}