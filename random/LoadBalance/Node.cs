using System;
namespace LoadBalance
{
	public class Node
	{
		public Node(string url, int weight, bool isAvailable)
		{
			this.Url = url;
			this.Weight = weight;
			this.IsAvailable = isAvailable;
		}
		
		public String Url
		{
			get;
			private set;
		}
		public Int32 Weight
		{
			get;
			private set;
		}
		public Boolean IsAvailable
		{
			get;private set;
		}
		public override int GetHashCode()
		{
			return this.Url.GetHashCode();
		}
		 public override bool Equals(object obj)  
         {  
            if (obj == null)
            {
                return false;  
            }
            if (GetType() != obj.GetType())
            {
            	return false; 
            }
            
            Node d = (Node)obj;
            if (this.Url == d.Url)
            {
            	return true;
            }
            return false;  
        }  
	}
}
