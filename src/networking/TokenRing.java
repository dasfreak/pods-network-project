package networking;

import java.io.IOException;
import java.util.Collections;
import java.util.List;
import java.util.Vector;

import org.apache.xmlrpc.XmlRpcException;

import networking.Client.RemoteNode;

public class TokenRing extends SyncAlgorithm implements Runnable {
		
	private int indexInRing = -1;
	private String ipCordinator;
	private volatile Token token = null;
	
	
	public TokenRing(List<RemoteNode> network, String ip)
	{
		// pay attension to the difference between network and this.network
		super ( network, ip );
		
		Collections.sort(super.network);
		
		for ( int index = 0; index < super.network.size(); index++ )
		{
			if ( super.network.get(index).ip.compareTo(this.ip) == 0 )
			{
				this.indexInRing = index;
				break;
			}
		}
		
		System.out.println("My index in network is: " + this.indexInRing);
		
		//System.out.println("Initial network size = "+super.network.size());
		fetchCordinator();
		TokenRing.instance = this;
	}
	private void fetchCordinator() {
		ipCordinator = network.get(0).ip;
		if ( ipCordinator.compareTo(this.ip) == 0 )
		{
			// this node won the election according to IP metric:
			token = new Token( this.ip, this.ip );
			System.out.println("Won the election, created the ring!");
		}
	}
	

	
	@Override
	public void run() {
		while ( !isSessionDone() )
		{
			if ( hasRing() && isCalcDone() && !isPending )
			{
				forwardToken();
			}
			//System.out.println("TokenStatus: " + hasRing());	
		}
	}

	
	public synchronized boolean hasRing() {
		if ( token != null && token.currentHolder.compareTo(this.ip) == 0 )
		{
			return true;
		}
		return false;
	}
	
	private void forwardToken()
	{
		// fetch next peer:		
		int nextPeer = ( indexInRing + 1 ) % super.network.size();
		
		System.out.println("Forwarding token to IP "+network.get(nextPeer).ip);

		Vector<String> params = new Vector<String>();

		params.add(token.ipCreator);
		params.add(network.get(nextPeer).ip);
				
		// send request to node
		try {
			network.get(nextPeer).rpc.execute("TokenRingAux.tokenReceived", params );
		} catch (XmlRpcException | IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		token = null;
	}
	
	public synchronized void receivedToken(Token token)
	{
	System.out.println("Received Token!");
		this.token = token;
	}
	@Override
	public synchronized boolean canAccess() {
		// TODO Auto-generated method stub
		return hasRing();
	}
	

}
