﻿
using System;
using System.Collections.Generic;
using Easy.Rpc.LoadBalance;

namespace Easy.Rpc.directory
{
	public interface IDirectory
	{
		String Name();		
		IList<Node> GetNodes(String providerName);
		void Refresh(string file = null);
	}
}
