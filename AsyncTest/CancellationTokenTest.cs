using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using static System.Threading.Tasks.Task;

namespace AsyncTest
{
	public class CancellationTokenTest
	{
		public TestContext TestContext { get; set; }

		private static async Task Get(CancellationToken cancellationToken = default)
		{
			TestContext.WriteLine($"Canceled: {cancellationToken.IsCancellationRequested}");

			await Delay(100, cancellationToken);
			TestContext.WriteLine($"Canceled: {cancellationToken.IsCancellationRequested}");
		}
		
		[Test]
		public void Test()
		{
			
			var tokenSource = new CancellationTokenSource();
			var token = tokenSource.Token;
			TestContext.WriteLine("1");
			Get(token);

			TestContext.WriteLine("2");
			
			tokenSource.Cancel();
			TestContext.WriteLine("3");
			Thread.Sleep(1000);
			TestContext.WriteLine("4");
		}

		
	}
}