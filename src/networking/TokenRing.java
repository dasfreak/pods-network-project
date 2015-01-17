package networking;

import java.io.IOException;
import java.util.Collections;
import java.util.LinkedList;
import java.util.List;
import java.util.Vector;

import org.apache.xmlrpc.XmlRpcException;

import networking.Client.RemoteNode;

public class TokenRing implements Runnable {
		
	private static TokenRing instance = null;
	private List<RemoteNode> network;
	private int indexInRing = -1;
	
	private String ip;
	private String ipCordinator;
	
	private boolean isCalcDone = true;
	
	private volatile Token token = null;
	
	public static TokenRing getInstance()
	{
		return instance;
	}
	
	public TokenRing(List<RemoteNode> network, String ip)
	{
		// pay attension to the difference between network and this.network
		this.network = new LinkedList<RemoteNode>();
		this.network.addAll(network);
		this.ip      = ip;
		
		Collections.sort(this.network);
		
		for ( int index = 0; index < network.size(); index++ )
		{
			if ( this.network.get(index).ip.compareTo(this.ip) == 0 )
			{
				this.indexInRing = index;
				break;
			}
		}
		
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
		while (true)
		{
			if ( hasRing() && isCalcDone() )
			{
				forwardToken();
			}
		}
	}
	public synchronized boolean isCalcDone() {
		return isCalcDone;
	}
	
	public synchronized void setCalcDone(){
		isCalcDone = true;
	}
	
	public synchronized void setCalcInProgress(){
		isCalcDone = false;
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
		int nextPeer = ( indexInRing + 1 ) % network.size();
		//System.out.println("Forwarding token to IP "+network.get(nextPeer).ip);

		Vector<String> params = new Vector<String>();

		params.add(token.ipCreator);
		params.add(network.get(nextPeer).ip);
		
		token = null;
		
		// send request to node
		try {
			network.get(nextPeer).rpc.execute("TokenRingAux.tokenReceived", params );
		} catch (XmlRpcException | IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public synchronized void receivedToken(Token token)
	{
	//	System.out.println("Received Token!");
		this.token = token;
	}

}
