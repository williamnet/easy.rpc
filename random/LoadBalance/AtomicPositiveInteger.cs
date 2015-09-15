/*
 * 由SharpDevelop创建。
 * 用户： 晓静
 * 日期: 2015-09-12
 * 时间: 23:33
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;

namespace LoadBalance
{
	/// <summary>
	/// Description of AtomicPositiveInteger.
	/// </summary>
	public class AtomicPositiveInteger
	{
		private int i = 0;
		
		public AtomicPositiveInteger(int initValue)
		{
			this.i = initValue;
		}
		public AtomicPositiveInteger()
		{
		}
		
		public int GetAndIncrement()
		{
			System.Threading.Interlocked.CompareExchange(ref i ,0 ,int.MaxValue);
			return System.Threading.Interlocked.Increment(ref i);
		}
		public int Value()
		{
			return this.i;
		}
	}
}
