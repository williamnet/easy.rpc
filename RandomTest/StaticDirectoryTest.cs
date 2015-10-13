using System;
using System.IO;
using Easy.Rpc.directory;
using NUnit.Framework;

namespace RandomTest
{
	[TestFixture]
	public class StaticDirectoryTest
	{
		[Test]
		public void StaticDirectoryLoadXMLTest()
		{
			string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rpc.config");
			var staticDirectory = new StaticDirectory(file,"order");
			
			var nodes = staticDirectory.GetNodes();
			
			Assert.IsTrue(nodes.Count > 0);
		}
	}
}
