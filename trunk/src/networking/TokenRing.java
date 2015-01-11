package networking;

import java.util.ArrayList;
import java.util.Collections;
import java.util.LinkedList;
import java.util.List;

import networking.Client.RemoteNode;

public class TokenRing implements Runnable {
	
	class Token{
		String ipCreator;
		String currentHolder;
		int    indexOfHolderInRing;
		
		public Token (String ipCreator, String currentHolder) {
			this.ipCreator      = ipCreator;
			this.currentHolder  = currentHolder;
			indexOfHolderInRing = 0;
		}
	}
	
	private List<RemoteNode> network;
	private String ip;
	private String ipCordinator;
	
	private Token token = null;
	
	public TokenRing(List<RemoteNode> network, String ip)
	{
		this.network = new LinkedList<RemoteNode>();
		this.network.addAll(network);
		this.ip      = ip;
		
		fetchCordinator();
	}
	private void fetchCordinator() {
		Collections.sort(network);
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
			if ( isHolderOfRing() && isCalcDone() )
			{
				
			}
		}
	}
	private boolean isCalcDone() {
		// TODO Auto-generated method stub
		return true;
	}
	private boolean isHolderOfRing() {
		if ( token != null && token.currentHolder.compareTo(this.ip) == 0 )
		{
			return true;
		}
		return false;
	}

}
