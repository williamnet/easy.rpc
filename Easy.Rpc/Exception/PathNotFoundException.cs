
using System;

namespace Easy.Rpc.Exception
{
	
	public class PathNotFoundException:System.Exception
	{
		public PathNotFoundException(string message)
			: base(message)
		{
		}
	}
}
