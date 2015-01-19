using System;
using System.Threading;

namespace Networking
{

	public class CalculatingTask
	{

		internal volatile bool isCalculating = false;
		internal const int TIME_FOR_CALC_IN_MSEC = 20 * 1000;
		internal const int MAX_TIME_FOR_WAIT_IN_MSC = 1 * 1000;

		internal int operationQueueSize = 0;
		internal Operation op;
		internal int genNumber;

		public override void run()
		{
			long timeStart = DateTimeHelperClass.CurrentUnixTimeMillis();
			long currentTime = 0;
			Random randomGenerator = new Random();
			long randomTimeInMSec;
			Console.WriteLine("Starting calc session for: " + TIME_FOR_CALC_IN_MSEC / 1000 + " seconds:");
			do
			{
				if (operationQueueSize < 1)
				{
					operationQueueSize++;
					op = Operation.values()[randomGenerator.Next(Operation.values().Count)];
					genNumber = randomGenerator.Next(100);
					SyncAlgorithm.Instance.setPending();
				}

				if (SyncAlgorithm.Instance.canAccess())
				{
					// Critical Section
					SyncAlgorithm.Instance.setCalcInProgress();

					Console.WriteLine("[" + DateTimeHelperClass.CurrentUnixTimeMillis() + "] [ Distributed Calc Request ] calculation: Operation: " + op + " Value: " + genNumber);

					try
					{
						Client.Instance.performCalc(op, genNumber);
					}
					catch (Exception e)
					{
						// TODO Auto-generated catch block
						Console.WriteLine(e.ToString());
						Console.Write(e.StackTrace);
					}
					operationQueueSize--;
					SyncAlgorithm.Instance.setCalcDone();
				}

				// wait a random time	
				randomTimeInMSec = randomGenerator.Next(MAX_TIME_FOR_WAIT_IN_MSC);
				try
				{
                    TimeSpan sleepTime = GenTimeSpanFromTicks(randomTimeInMSec);
                    Thread.Sleep(sleepTime);
				}
				catch
				{
					// TODO Auto-generated catch block
					//Console.WriteLine(e.ToString());
					//Console.Write(e.StackTrace);
				}
				currentTime = DateTimeHelperClass.CurrentUnixTimeMillis();
			} while ((currentTime - timeStart) < TIME_FOR_CALC_IN_MSEC);
			Console.WriteLine("The time is up!");
		}

        static TimeSpan GenTimeSpanFromTicks(long ticks)
        {
            // Create a TimeSpan object and TimeSpan string from 
            // a number of ticks.
            TimeSpan interval = TimeSpan.FromTicks(ticks);
            return interval;
        } 

	}

}