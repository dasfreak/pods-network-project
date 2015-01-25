package networking;

import java.util.Random;

public class CalculatingTask implements Runnable {
	
	volatile boolean isCalculating = false;
	static final int TIME_FOR_CALC_IN_MSEC    = 20 * 1000;
	static final int MAX_TIME_FOR_WAIT_IN_MSC =  1 * 1000;
	
	int operationQueueSize = 0;
	Operation op;
	int genNumber;
	
	@Override
	public void run() {
		long timeStart = System.currentTimeMillis();
		long currentTime = 0;
	    Random randomGenerator = new Random();
	    long randomTimeInMSec;
	    
	    try {
			Client.getInstance().handshake();
		} catch (Exception e1) {
			e1.printStackTrace();
		}
	    SyncAlgorithm.getInstance().setCanStart();
		System.out.println("Starting calc session for: "+TIME_FOR_CALC_IN_MSEC/1000+" seconds:");
		do
		{	
			if ( operationQueueSize < 1 )
			{
				operationQueueSize++;
				op = Operation.values()[randomGenerator.nextInt(Operation.values().length)];
				genNumber = randomGenerator.nextInt(100);
				SyncAlgorithm.getInstance().setPending();
			}
			synchronized (SyncAlgorithm.getInstance().mutualLock ) {
				if ( SyncAlgorithm.getInstance().canAccess() )
				{
					System.out.println("==> CS Enter");
					operationQueueSize--;
					SyncAlgorithm.getInstance().setCalcInProgress();
					SyncAlgorithm.getInstance().clearPending();
					// Critical Section
					
					System.out.println("["+System.currentTimeMillis()+"] [ Distributed Calc Request ] calculation: Operation: "+op+" Value: "+genNumber);
	
					try {
						Client.getInstance().performCalc( op, genNumber );
					} catch (Exception e) {
						e.printStackTrace();
					}
					SyncAlgorithm.getInstance().setCalcDone();
					System.out.println("<== CS Exit");
				}
			}
			
			// wait a random time	
			randomTimeInMSec = randomGenerator.nextInt(MAX_TIME_FOR_WAIT_IN_MSC);
			
			try {
				Thread.sleep(randomTimeInMSec);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
			currentTime = System.currentTimeMillis();
		} while ( ( currentTime - timeStart ) < TIME_FOR_CALC_IN_MSEC );
		
		SyncAlgorithm.getInstance().setSessionDone();
	}

	
}
