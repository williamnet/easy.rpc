
using System;

namespace Easy.Rpc.Exception
{
	public class NodeNoFoundException:System.Exception
	{
		public NodeNoFoundException(string message)
			: base(message)
		{
		}
	}
}
