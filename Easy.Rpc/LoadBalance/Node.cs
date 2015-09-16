using System;
namespace Easy.Rpc.LoadBalance
{
	public class Node
	{
		public Node(string providerName, string url, int weight, bool isAvailable)
		{
			this.ProviderName = providerName;
			this.Url = url;
			this.Weight = weight;
			this.IsAvailable = isAvailable;
		}
		
		public String ProviderName {
			get;
			private set;
		}
		
		public String Url {
			get;
			private set;
		}
		public Int32 Weight {
			get;
			private set;
		}
		public Boolean IsAvailable {
			get;
			private set;
		}
		public override int GetHashCode()
		{
			return this.Url.GetHashCode();
		}
		public override bool Equals(object obj)
		{  
			if (obj == null) {
				return false;  
			}
			if (GetType() != obj.GetType()) {
				return false; 
			}
            
			Node d = (Node)obj;
			if (this.Url == d.Url) {
				return true;
			}
			return false;  
		}
	}
}
