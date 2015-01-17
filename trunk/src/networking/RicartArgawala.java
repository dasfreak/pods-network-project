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
	private volatile boolean isPending;
	
	public RicartArgawala(List<RemoteNode> network, String ip)
	{
		// pay attention to the difference between network and this.network
		super(network, ip);
		timestamp = 0;
		requestsQueue = new TreeSet<String>();
		okayList      = new TreeSet<String>();
		isPending     = false;
	}
	
	public void requestReceived( String ip, long timestamp )
	{

		if ( isCalcDone() && !this.isPending )
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
		Vector<String> params = new Vector<String>();
		params.add(ip);

		for ( RemoteNode node : this.network )
		{
			if ( node.ip.compareTo(ip) == 0 )
			{
				try {
					node.rpc.execute("okReceived", params);
				} catch (XmlRpcException | IOException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				break;
			}
		}
	}

	public synchronized void okReceived(String ip) {
		okayList.add(ip);
	}

	@Override
	public void run() {
		while (true)
		{
			if ( isPending )
			{
				// request from all nodes
				okayList.clear();
				broadcastRequest();
				timestamp++;
				// wait for okay from all
				while( okayList.size() != ( network.size() - 1 ) ); // -1 because of self node
				
				canAccess = true;
				// can access now
				while (!isCalcDone);
				
				isPending = false;
				
				// send okay to all processes in queue
				sendOkayToQueueNodes();
				canAccess = false;
			}
		}
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
					try {
						rNode.rpc.execute("okReceived", params);
					} catch (XmlRpcException | IOException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}				
			}
		}

	}

	private void broadcastRequest() {
		// TODO Auto-generated method stub
		Vector<Object> params = new Vector<Object>();
		params.add(this.ip);
		params.add(this.timestamp);
		
		
		for (RemoteNode node : network )
		{
			if (node.ip.compareTo(this.ip) != 0 )
			{
				try {
					node.rpc.execute("requestReceived", params);
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
}
