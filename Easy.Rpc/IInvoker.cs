
using System;
using Easy.Rpc.LoadBalance;
namespace Easy.Rpc
{
	
	public interface IInvoker<T>
	{
		String JoinURL(Node node,String path);
		T DoInvoke(Node node, string path);
	}
}
