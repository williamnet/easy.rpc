using System;
using Easy.Rpc.LoadBalance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Easy.Rpc.Test
{
	[TestClass]
	public class AtomicPositiveIntegerTest
	{
		[TestMethod]
		public void MinValueTest()
		{
			var atomic =new AtomicPositiveInteger(Int32.MaxValue);
			
			Int32 result = atomic.GetAndIncrement();
			
			Assert.AreEqual(1,result);	
		}
        [TestMethod]
		public void MaxValueTest()
		{
			var atomic =new AtomicPositiveInteger(Int32.MaxValue -1);
			
			Int32 result = atomic.GetAndIncrement();
			Assert.AreEqual(Int32.MaxValue,result);
			
			result = atomic.GetAndIncrement();
			Assert.AreEqual(1,result);
			
		}
	}
}
