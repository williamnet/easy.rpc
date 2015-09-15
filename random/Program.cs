
using System;
using System.Threading.Tasks;
namespace random
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			
			TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
		{
			e.SetObserved();
		}
	}
}