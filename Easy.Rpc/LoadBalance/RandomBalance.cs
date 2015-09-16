using System;
using System.Collections.Generic;
namespace Easy.Rpc.LoadBalance
{
	public class RandomBalance:ILoadBalance
	{
		private readonly Random random = new Random();
		
		public const string NAME ="RandomBalance";
		
		public String Name(){
			return NAME;
		}
		
		public Node Select(IList<Node> nodes,string path)
		{
			Int32 length = nodes.Count;
			Int32 totalWeight = 0;
			Boolean isSameWeight = true;
			for (var i = 0; i < length; i++) 
			{
				Int32 weight = nodes[i].Weight;
				totalWeight += weight;
				if (isSameWeight && i > 0 && weight != nodes[i - 1].Weight) 
				{
					isSameWeight = false;
				}	
			}
			if (totalWeight > 0 && !isSameWeight) 
			{
				Int32 offset = random.Next(totalWeight);
				for (Int32 i = 0; i < length; i++) 
				{
					offset -= nodes[i].Weight;
					if (offset < 0) 
					{
						return nodes[i];
					}
				}
			}
			return nodes[random.Next(length)];
		}
	}
}
