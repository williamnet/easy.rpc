/*
 * 由SharpDevelop创建。
 * 用户： 晓静
 * 日期: 2015-09-12
 * 时间: 20:28
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
namespace LoadBalance
{
	/// <summary>
	/// Description of IBalance.
	/// </summary>
	public interface ILoadBalance
	{
		Node Select(IList<Node> nodes,string nodeGroupName);
	}
}
