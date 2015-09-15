/*
 * 由SharpDevelop创建。
 * 用户： 晓静
 * 日期: 2015-09-13
 * 时间: 12:29
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using NUnit.Framework;
using Easy.Rpc.LoadBalance;
namespace RandomTest
{
	[TestFixture]
	public class AtomicPositiveIntegerTest
	{
		[Test]
		public void MinValueTest()
		{
			var atomic =new AtomicPositiveInteger(Int32.MaxValue);
			
			Int32 result = atomic.GetAndIncrement();
			
			Assert.AreEqual(1,result);	
		}
		[Test]
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
