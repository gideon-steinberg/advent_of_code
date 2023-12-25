<Query Kind="Program" />

void Main()
{
	var input1=@"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483";

var input2=@"";

	var parts = input1.Split(Environment.NewLine);
	var cards = new List<Card>();
	
	foreach (var line in parts)
	{
		var split = line.Split(" ");
		var hand = split[0].ToCharArray();
		var type = HandType.HighCard;
		
		var numJokers = hand.Where( c => c == 'J').Count();
		
		var handWithoutJokers = hand.Where( c => c != 'J');
		
		if (handWithoutJokers.Distinct().Count() == 0)
		{
			type = HandType.FiveOfAKind;
		}
		else if (handWithoutJokers.Distinct().Count() == 1)
		{
			type = HandType.FiveOfAKind;
		}
		else if (handWithoutJokers.Distinct().Count() == 2)
		{
			var numberPerCard = new Dictionary<char, int>();
			foreach (var c in handWithoutJokers)
			{
				if (!numberPerCard.ContainsKey(c))
				{
					numberPerCard[c] = 0;
				}
				numberPerCard[c]++;
			}
			if (numberPerCard.Max(kv => kv.Value) == (4 - numJokers))
			{
				type = HandType.FourOfAKind;
			}
			else
			{
				type = HandType.FullHouse;
			}
		}
		else if (handWithoutJokers.Distinct().Count() == 3)
		{
			var numberPerCard = new Dictionary<char, int>();
			foreach (var c in handWithoutJokers)
			{
				if (!numberPerCard.ContainsKey(c))
				{
					numberPerCard[c] = 0;
				}
				numberPerCard[c]++;
			}
			if (numberPerCard.Max(kv => kv.Value) == (3 - numJokers))
			{
				type = HandType.ThreeOfAKind;
			}
			else
			{
				type = HandType.TwoPair;
			}
		}
		else if (handWithoutJokers.Distinct().Count() ==  4)
		{
			type = HandType.Pair;
		}
		
		var card = new Card()
		{
			bid = int.Parse(split[1]),
			type = type,
			hand = hand
		};
		
		cards.Add(card);
	}
	cards.Sort();
	//cards.Dump();
	
	decimal sum = 0;
	
	for (var i = 0; i < cards.Count(); i++)
	{
		sum += (i+1) * cards[i].bid;
	}
	
	sum.Dump();
}

public class Card : IComparable
{
	public int bid;
	public HandType type;
	public char[] hand;
	
	private Dictionary<char, int> rankings = new Dictionary<char, int>
	{
		{ 'J', -1},
		{ '2', 0},
		{ '3', 1},
		{ '4', 2},
		{ '5', 3},
		{ '6', 4},
		{ '7', 5},
		{ '8', 6},
		{ '9', 7},
		{ 'T', 8},
		{ 'Q', 10},
		{ 'K', 11},
		{ 'A', 12}
	};
	
	int IComparable.CompareTo(object obj)
	{
	
		Card c1=(Card)this;
		Card c2=(Card)obj;
		if (c1.type > c2.type)
			return 1;
		if (c1.type < c2.type)
			return -1;
		else
			for (var i = 0; i < 5; i++)
			{
				if (rankings[c1.hand[i]] > rankings[c2.hand[i]])
					return 1;
				if (rankings[c1.hand[i]] < rankings[c2.hand[i]])
					return -1;
			}
		c1.Dump();
		c2.Dump();
		return 0;
   }
}

public enum HandType
{
	HighCard = 0,
	Pair = 1,
	TwoPair = 2,
	ThreeOfAKind = 3,
	FullHouse = 4,
	FourOfAKind = 5,
	FiveOfAKind = 6
}


// You can define other methods, fields, classes and namespaces here