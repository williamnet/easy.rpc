
using System;

namespace Easy.Rpc.Exception
{
	public class DirectoryNotFoundException:System.Exception
	{
		public DirectoryNotFoundException(string message)
			: base(message)
		{
		}
	}
}
