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

	var nodes = new Dictionary<string, Node>();
	var parts = input1.Split(Environment.NewLine);
	
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
	}
	
	decimal highPulses = 0;
	decimal lowPulses = 0;
	
	for (var i = 0; i < 1000; i++)
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
							foreach(var newId in node.connectedNodes)
							{
								newPulses.Add(new Pulse {toId = newId, isHigh = node.isOn, fromId = node.id});
							}
						}
						break;
				}
			}
			pulses = newPulses;
		}
	}
	
	lowPulses.Dump();
	highPulses.Dump();
	(lowPulses * highPulses).Dump();
}

// You can define other methods, fields, classes and namespaces here

public class Node
{
	public List<string> connectedNodes;
	public string id;
	public NodeType type;
	public bool isOn;
	public HashSet<string> nodesReceivedHighFrom = new HashSet<string>();
	public HashSet<string> incomingNodes = new HashSet<string>();
}

public enum NodeType
{
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