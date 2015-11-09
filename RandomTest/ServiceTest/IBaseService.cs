
using System;
using Easy.Rpc;
using Easy.Rpc.Cluster;
using Easy.Rpc.LoadBalance;
using Easy.Rpc.directory;

namespace RandomTest.ServiceTest
{
	/// <summary>
	/// Description of IBaseService.
	/// </summary>
	public interface IBaseService
	{
		
		String Select(string a, string b, InvokerContext context);
	}
}
