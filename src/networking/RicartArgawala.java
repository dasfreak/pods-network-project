package networking;

import java.io.IOException;
import java.util.List;
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
	
	public RicartArgawala(List<RemoteNode> network, String ip)
	{
		// pay attention to the difference between network and this.network
		super(network, ip);
		timestamp = 0;
		requestsQueue = new TreeSet<String>();
		okayList      = new TreeSet<String>();
		isPending     = false;
		
		instance = this;
	}
	
	public void requestReceived( String ip, long timestamp )
	{
		System.out.println("Received request from ip "+ip+" timestamp = "+timestamp);
		if ( isCalcDone() && !isPending() )
		{
			// send OK
			sendOk(ip);
		}
		else if ( !this.isCalcDone )
		{
			requestsQueue.add(ip);
		}
		else if ( this.isPending )
		{
			// queue request
			if ( timestamp < this.timestamp )
			{
				sendOk(ip);
			}
			else
			{
				requestsQueue.add(ip);
			}
		}
	}

	private void sendOk(String ip) {
		System.out.println("Sending okay to node "+ip+" timestamp = "+timestamp);
		Vector<String> params = new Vector<String>();
		params.add(this.ip);

		for ( RemoteNode node : this.network )
		{
			if ( node.ip.compareTo(ip) == 0 )
			{
				try {
					node.rpc.execute("RicartArgawalaAux.okReceived", params);
				} catch (XmlRpcException | IOException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				break;
			}
		}
	}

	public synchronized void okReceived(String ip) {
		System.out.println("==>Received okay from "+ip);
		okayList.add(ip);
		System.out.println("   okayList size is"+okayList.size());
	}

	@Override
	public void run() {
		while (true)
		{
			if ( isPending() )
			{
				System.out.println("Pending request detected\n");
				// request from all nodes
				okayList.clear();
				broadcastRequest();
				timestamp++;
				// wait for okay from all
				while( okayList.size() <= ( network.size() - 1 ) ); // -1 because of self node
				
				setAccess( true );
				// can access now
				while (!isCalcDone());
				
				isPending = false;
				
				// send okay to all processes in queue
				sendOkayToQueueNodes();
				
				setAccess( false );
			}
		}
	}

	private synchronized boolean isPending() {
		return isPending;
	}

	private void sendOkayToQueueNodes() {
		
		Vector<String> params = new Vector<String>();

		params.add(this.ip);
		
		for (String node : requestsQueue )
		{
			for ( RemoteNode rNode : network )
			{
				if (node.compareTo(rNode.ip) == 0)
				{
					System.out.println("Sending okay to queue node "+rNode.ip);
					try {
						rNode.rpc.execute("RicartArgawalaAux.okReceived", params);
					} catch (XmlRpcException | IOException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}				
			}
		}

	}

	private void broadcastRequest() {
		System.out.println("Boradcasting request:");
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
					// TODO Auto-generated catch block
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
