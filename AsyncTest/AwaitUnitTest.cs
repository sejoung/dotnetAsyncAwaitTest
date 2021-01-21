using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using static AsyncTest.AsyncTest;

namespace AsyncTest
{
	public class AwaitUnitTest
	{
		[Test]
		public void NonAwaitTest()
		{
			Console.WriteLine("start");
			var result = GetHtmlAsync();
			Console.WriteLine($"IsCompleted {result.IsCompleted}");
			Console.WriteLine("end");
		}

		[Test]
		public void AwaitTest()
		{
			Console.WriteLine("start");
			var result = GetFirstCharactersCountAsync(10);
			Console.WriteLine($"IsCompleted {result.IsCompleted}");
			Thread.Sleep(2000);
			Console.WriteLine("end");
		}
		
		[Test]
		public void AsyncTaskTest()
		{
			var task = AsyncTask();
			Console.WriteLine("pass await 1");
			task.Wait();
			Console.WriteLine("pass await 2");
			var result = task.Result;
			Console.WriteLine(result);
			Console.WriteLine("end");
		
		}

	}


	public static class AsyncTest
	{
		public static async Task<int> AsyncTask()
		{
			var task = new Task<int>(() =>
			{
				var sum = 0;
				for (var i = 0; i < 10; i++)
				{
					sum += i;

					Thread.Sleep(100);
				}

				return sum;
			});
			task.Start();
			await task;
			Console.WriteLine(task.Result);
			return 10;
		}


		public static Task<string> GetHtmlAsync()
		{
			Console.WriteLine("GetHtmlAsync start");
			var client = new HttpClient();
			Console.WriteLine("GetHtmlAsync 1");
			var result = client.GetStringAsync("https://www.dotnetfoundation.org");
			Console.WriteLine("GetHtmlAsync 2");
			return result;
		}

		public static async Task<string> GetFirstCharactersCountAsync(int count)
		{
			Console.WriteLine("GetFirstCharactersCountAsync start");
			var client = new HttpClient();
			Console.WriteLine("GetFirstCharactersCountAsync 1");
			var page = await client.GetStringAsync("https://www.dotnetfoundation.org");
			Console.WriteLine("GetFirstCharactersCountAsync 2");
			var result = count > page.Length ? page : page.Substring(0, count);
			Console.WriteLine("GetFirstCharactersCountAsync 3");
			return result;
		}
	}
}