package networking;

import java.util.Random;

public class CalculatingTask implements Runnable {
	
	volatile boolean isCalculating = false;
	static final int TIME_FOR_CALC_IN_MSEC    = 20 * 1000;
	static final int MAX_TIME_FOR_WAIT_IN_MSC =  1 * 1000;

	@Override
	public void run() {
		long timeStart = System.currentTimeMillis();
		long currentTime = 0;
	    Random randomGenerator = new Random();
	    long randomTimeInMSec;
		System.out.println("Starting calc session for: "+TIME_FOR_CALC_IN_MSEC/1000+" seconds:");
		do
		{			
			Operation op = Operation.values()[randomGenerator.nextInt(Operation.values().length)];
			int genNumber = randomGenerator.nextInt(100);
			System.out.println("["+System.currentTimeMillis()+"] [ Distributed Calc Request ] calculation: Operation: "+op+" Value: "+genNumber);
			
			try {
				Client.getInstance().performCalc( op, genNumber );
			} catch (Exception e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}

			// wait a random time	
			randomTimeInMSec = randomGenerator.nextInt(MAX_TIME_FOR_WAIT_IN_MSC);
			try {
				Thread.sleep(randomTimeInMSec);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			currentTime = System.currentTimeMillis();
		} while ( ( currentTime - timeStart ) < TIME_FOR_CALC_IN_MSEC );
		System.out.println("The time is up!");
	}
	
}
