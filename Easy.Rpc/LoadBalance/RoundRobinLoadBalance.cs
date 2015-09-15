using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
namespace Easy.Rpc.LoadBalance
{
	public class RoundRobinLoadBalance:ILoadBalance
	{
		
		private readonly ConcurrentDictionary<String,AtomicPositiveInteger> sequences = new ConcurrentDictionary<String,AtomicPositiveInteger>();
		
		private readonly ConcurrentDictionary<String,AtomicPositiveInteger> weightSequences = new ConcurrentDictionary<String, AtomicPositiveInteger>();
		
		
		public Node Select(IList<Node> nodes, string nodeGroupName)
		{
			int length = nodes.Count;
			int maxWeight = 0;
			int minWeight = int.MaxValue;
			
			for (var i = 0; i < length; i++) 
			{
				int weight = nodes[i].Weight;
				
				maxWeight = Math.Max(weight, maxWeight);
				minWeight = Math.Min(weight, minWeight);
			}
				
			if (maxWeight > 0 && maxWeight != minWeight) 
			{
				AtomicPositiveInteger weightSequence = weightSequences.GetOrAdd(nodeGroupName, new AtomicPositiveInteger());
				
				int currentWeight = weightSequence.GetAndIncrement() % maxWeight;
				IList<Node> weightNodes = new List<Node>();
				foreach (var node in nodes) 
				{
					if (node.Weight > currentWeight) 
					{
						weightNodes.Add(node);
					}
				}
				int weightLength = weightNodes.Count;
				if (weightLength == 1) 
				{
					return weightNodes[0];
				}
				else if (weightLength > 1)
				{
					nodes = weightNodes;
					length = nodes.Count;
				}
			}
			
			AtomicPositiveInteger sequence = sequences.GetOrAdd(nodeGroupName, new AtomicPositiveInteger());
			return nodes[sequence.GetAndIncrement() % length];
		}
	}
}















