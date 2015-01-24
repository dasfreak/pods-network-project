using System;
using System.Threading;

namespace Networking
{

	public class CalculatingTask
	{

		internal volatile bool isCalculating = false;
		internal const int TIME_FOR_CALC_IN_MSEC = 20 * 1000;
        internal const int TIME_corection = 10000;  // 100 pico s. to 1 ms
        internal const int MAX_TIME_FOR_WAIT_IN_MSC = 1 * 100;

		internal int operationQueueSize = 0;
		internal Operation op;
		internal int genNumber;
      

        


		public void run()
        {
            Console.WriteLine("Thread  CalcultingTask is running");
			long timeStart = DateTimeHelperClass.CurrentUnixTimeMillis();
			long currentTime = 0;
			Random randomGenerator = new Random();
			long randomTimeInMSec;
			Console.WriteLine("Starting calc session for: " + TIME_FOR_CALC_IN_MSEC / 1000 + " seconds:");
            DateTime startUtcNow = DateTime.UtcNow;

            try
            {
                Client.getInstance().handshake();
            }
            catch (Exception e1)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e1.ToString());
                Console.Write(e1.StackTrace);
            
            }

            SyncAlgorithm.Instance.setCanStart();



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
                    SyncAlgorithm.Instance.clearPending();
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
                //TIME_MilliSec
				try
				{
                    TimeSpan sleepTime = GenTimeSpanFromTicks(randomTimeInMSec * TIME_corection + TIME_corection);
                    // min wait 1ms
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


            Client.getInstance().StopsRicartArgawalaThread();
            Client.getInstance().StopsTokenRingThread();

            //Console.WriteLine("The timeC=" + currentTime + "The timeS=" + timeStart);
			//Console.WriteLine("The time is up!");
            //DateTime saveUtcNow = DateTime.UtcNow;
            //Console.WriteLine("Zeit " + saveUtcNow+ "Zeits"+startUtcNow);
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