package networking;

import java.net.InetAddress;
import java.net.UnknownHostException;
import java.util.LinkedList;
import java.util.List;

public class Node {
	InetAddress ip;
	List<InetAddress> network;
	private static Node node;
	
	private Node(){
		try {
			ip = InetAddress.getLocalHost();
		} catch (UnknownHostException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		network = new LinkedList<InetAddress>();
	}
	
	public static Node getInstance(){
		if ( null == node )
		{
			node = new Node();
		}
		return node;
	}
	
	public void joinWithPropagate( InetAddress ip ) // a.k.a broadcast
	{
		for ( InetAddress addr : network )
		{
			sendJoinNetworkOnlyRequest( ip );
		}
		network.add(ip);
	}
	
	public void removeRequestReceived( InetAddress ip )
	{
		network.remove( ip );
	}
	
	public void disconnect()
	{
		for ( InetAddress addr : network )
		{
			sendDisconnectRequest( addr );
		}
	}
	
	public void sendConnectRequest( InetAddress knownIPinNetwork )
	{
		// send the request ...
	}

	private void sendDisconnectRequest(InetAddress addr) {
		// TODO Auto-generated method stub
		
	}

	private void sendJoinNetworkOnlyRequest(InetAddress ip) {
		// TODO Auto-generated method stub
		
	}

	public void joinNetworkOnly( InetAddress ip )
	{
		network.add(ip);
	}
	
	public static void main(String[] args)  {
		System.out.println("IP = "+Node.getInstance().ip);
	}
	
}

