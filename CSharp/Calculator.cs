using System;

namespace Networking
{

	public class Calculator
	{

		public static int add(int i2)
		{
			int i1 = 0;
			try
			{
				i1 = Client.Instance.CurrentValue;
			}
			catch (Exception e)
			{
				// TODO Auto-generated catch block
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
			Console.WriteLine("Calculating: " + i1 + " + " + i2);
			int result = i1 + i2;
			try
			{
				Client.Instance.storeNewResult(result);
			}
			catch (Exception e)
			{
				// TODO Auto-generated catch block
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
			return result;
		}

		public static int subtract(int i2)
		{
			int i1 = 0;

			try
			{
				i1 = Client.Instance.CurrentValue;
			}
			catch (Exception e)
			{
				// TODO Auto-generated catch block
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
			Console.WriteLine("Calculating: " + i1 + " - " + i2);
			int result = i1 - i2;
			try
			{
				Client.Instance.storeNewResult(result);
			}
			catch (Exception e)
			{
				// TODO Auto-generated catch block
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}

			return result;
		}

		public static int divide(int i2)
		{
			int i1 = 0;
			try
			{
				i1 = Client.Instance.CurrentValue;
			}
			catch (Exception e)
			{
				// TODO Auto-generated catch block
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}

			int result = i1;
			if (i2 != 0)
			{
				result = i1 / i2;
				Console.WriteLine("Calculating: " + i1 + " / " + i2);
				try
				{
					Client.Instance.storeNewResult(result);
				}
				catch (Exception e)
				{
					// TODO Auto-generated catch block
					Console.WriteLine(e.ToString());
					Console.Write(e.StackTrace);
				}

			}

			return result;
		}

		public static int multiply(int i2)
		{
			int i1 = 0;
			try
			{
				i1 = Client.Instance.CurrentValue;
			}
			catch (Exception e)
			{
				// TODO Auto-generated catch block
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}

			int result = i1 * i2;
			Console.WriteLine("Calculating: " + i1 + " * " + i2);
			try
			{
				Client.Instance.storeNewResult(result);
			}
			catch (Exception e)
			{
				// TODO Auto-generated catch block
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}

			return result;
		}
	}
}