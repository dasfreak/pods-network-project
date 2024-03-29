package networking;

import java.io.IOException;
import java.util.Collections;
import java.util.List;
import java.util.Random;
import java.util.Set;
import java.util.TreeSet;
import java.util.Vector;

import org.apache.xmlrpc.XmlRpcException;

import networking.Client.RemoteNode;

public class RicartArgawala extends SyncAlgorithm implements Runnable {
	
	private volatile Set<String> requestsQueue;
	private volatile Set<String> okayList;
	
	private volatile boolean canAccess;
	private long timestamp;
	private volatile boolean isSyncing;
	
	public RicartArgawala(List<RemoteNode> networkInput, String ip)
	{
		// pay attention to the difference between network and this.network
		super(networkInput, ip);
	    Random randomGenerator = new Random();
		timestamp = randomGenerator.nextInt(50);
		requestsQueue = new TreeSet<String>();
		okayList      = Collections.synchronizedSet( new TreeSet<String>() );
		isPending     = false;
		canAccess     = false;
		isSyncing     = false;
		instance      = this;
	}
	
	public void requestReceived( String ip, long timestamp )
	{
//		System.out.println("Received request from ip "+ip+" timestamp = "+timestamp+" own timestamp is "+this.timestamp);
		boolean sendOk = false;
		boolean addToQueue = false;
		
		boolean calcIsDone = isCalcDone();
		boolean isPending = isPending();

		if ( calcIsDone && !isPending )
		{
			sendOk = true;
		}
		else if ( !calcIsDone )
		{
			addToQueue = true;
		}
		else if ( isPending || isSyncing )
		{
			if ( ( timestamp == this.timestamp && ip.compareTo(this.ip) > 0 ) || 
					   timestamp < this.timestamp )
			{
				sendOk = true;
			}
			else
			{
				addToQueue = true;
			}
		}

		if ( sendOk )
		{
			sendOk(ip);
		}
		else
		{
			synchronized (this.requestsQueue)
			{
				requestsQueue.add(ip);
			}
		}



		
//		synchronized (this.requestsQueue) {
//			if ( isCalcDone() && !isPending() )
//			{
////					System.out.println("Case isCalcDone() && !isPending()");
//					// send OK
//					
//					sendOk(ip);
//			}
//			else if ( !isCalcDone() )
//			{
////				System.out.println("!isCalcDone");
//				requestsQueue.add(ip);
//			}
//			else if ( isPending() )
//			{
//				// queue request
//				if ( ( timestamp == this.timestamp && ip.compareTo(this.ip) > 0 ) || 
//					   timestamp < this.timestamp )
//				{
////					System.out.println("case timestamp is smaller");
//						sendOk(ip);
//				}
//				else
//				{
////					System.out.println("case timestamp is bigger");
//					requestsQueue.add(ip);
//				}
//			}
//		}
	}

	private void sendOk(String ip) {
//		System.out.println("Sending okay to node "+ip+" timestamp = "+timestamp);
		Vector<String> params = new Vector<String>();
		params.add(this.ip);

		for ( RemoteNode node : this.network )
		{
			if ( node.ip.compareTo(ip) == 0 )
			{
				try {
					node.rpc.execute("RicartArgawalaAux.okReceived", params);
				} catch (XmlRpcException | IOException e) {
				
					e.printStackTrace();
				}
				break;
			}
		}
	}

	public synchronized void okReceived(String ip) {
//		System.out.println("==>Received okay from "+ip);
		okayList.add(ip);
//		System.out.println("  okayList size is "+okayList.size());
	}

	@Override
	public void run() {

		boolean isSyncDone = false;
		while (!canStart());
		
		while ( !isSessionDone() )
		{
			isSyncDone = false;
			boolean isPending;
			synchronized ( this.mutualLock ) {
				isPending = isPending();
			}
			
			if ( isPending )
			{
				isSyncing = true;
//						System.out.println("Pending request detected\n");
				// request from all nodes
				okayList.clear();
				broadcastRequest();
				
				// wait for okay from all
				while( okayList.size() < ( network.size() - 1 ) ); // -1 because of self node
				isSyncDone = true;
				setAccess( true );
			}
				
				
				
			if ( isSyncDone )
			{
				System.out.println("====> CS ra enter");
				
					// can access now
					while (isPending());
					while (!isCalcDone());

					setAccess( false );
					// send okay to all processes in queue
					synchronized ( this.requestsQueue) {
						sendOkayToQueueNodes();
					}

					timestamp++;
				
				System.out.println("<==== CS ra exit");
				isSyncing = false;
			}
		}
	}

	private synchronized boolean isPending() {
		return isPending;
	}

	private synchronized void sendOkayToQueueNodes() {
		
		Vector<String> params = new Vector<String>();

		params.add(this.ip);
		
		for (String node : requestsQueue )
		{
			for ( RemoteNode rNode : network )
			{
				if (node.compareTo(rNode.ip) == 0)
				{
//					System.out.println("Sending okay to queue node "+rNode.ip);
					try {
						rNode.rpc.execute("RicartArgawalaAux.okReceived", params);
					} catch (XmlRpcException | IOException e) {
						e.printStackTrace();
					}
				}				
			}
		}

	}

	private void broadcastRequest() {
		System.out.println("Broadcasting request:");
		Vector<String> params = new Vector<String>();
		
		params.add(this.ip);
		params.add(Long.toString(this.timestamp));
		
		for (RemoteNode node : network )
		{
			if (node.ip.compareTo(this.ip) != 0 )
			{
				try {
					node.rpc.execute("RicartArgawalaAux.requestReceived", params);
				} catch (XmlRpcException | IOException e) {
					e.printStackTrace();
				}
			}
		}
	}

	@Override
	public synchronized boolean canAccess() {
		return canAccess;
	}
	
	private synchronized void setAccess(boolean accessValue)
	{
		canAccess = accessValue;
	}
}
