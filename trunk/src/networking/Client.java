package networking;

import java.net.InetAddress;
import java.net.MalformedURLException;
import java.net.NetworkInterface;
import java.net.SocketException;
import java.net.UnknownHostException;
import java.util.Collections;
import java.util.Enumeration;
import java.util.LinkedList;
import java.util.List;
import java.util.Vector;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

import org.apache.xmlrpc.*;


public class Client implements Runnable {

	private static Client instance;
	private RemoteNode node;

	String ip;
	
	List<RemoteNode> network;
	
	class RemoteNode
	{
		String ip;
		XmlRpcClient rpc;
		
		public RemoteNode( String ip, XmlRpcClient rpc )
		{
			this.ip  = ip;
			this.rpc = rpc;
		}
		public String toString()
		{
			return ip;
		}
		public XmlRpcClient getRpc() {
			return rpc;
		}
	}

    void displayInterfaceInformation(NetworkInterface netint) throws SocketException {
        Enumeration<InetAddress> inetAddresses = netint.getInetAddresses();
        for (InetAddress inetAddress : Collections.list(inetAddresses)) {
            if (netint.getDisplayName().startsWith("e"))
            	ip = inetAddress.toString().replaceFirst("/", "");
        }
     }

	public Client() throws SocketException, UnknownHostException {
		instance = this;
		network = new LinkedList<RemoteNode>();
		Enumeration<NetworkInterface> nets = NetworkInterface.getNetworkInterfaces();
		for (NetworkInterface netint : Collections.list(nets))
		    displayInterfaceInformation(netint);
		
		node = CreateNode(ip);
		network.add(node);
		System.out.println("My IP is: "+ip);
	}

	public RemoteNode CreateNode(String ip)
	{
		RemoteNode node = null;
		try {
		node = new RemoteNode(ip,new XmlRpcClient("http://" + ip + ":5000" + "/RPC2"));
		} catch (MalformedURLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}		
		return node;
	}
	
	public void addNodeToStructure( String ip )
	{
		network.add( CreateNode(ip) );
	}
		
	
	private void addNode(String ip) {
		for ( RemoteNode node : network )
		{
			if ( node.ip != this.ip )
			{
				propogateNewNodeMessage( node.rpc, ip );	
			}
		}
		
		RemoteNode node = CreateNode(ip);
		for ( RemoteNode n : network )
		{
			propogateNewNodeMessage( node.rpc, n.ip);
		}
		network.add(node);
	}

	public boolean showMenu( boolean printMenu) throws IOException {

	    BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
	
	    if ( printMenu )
	    {
			System.out.println("--- Super cool networking client ---");
			System.out.println("(1) 5+3");
			System.out.println("(7) exit network");
			System.out.println("(8) show network");
			System.out.println("(9) join network - specifiy an IP of an exisiting node in the network");
			System.out.println("(0) exit");
			System.out.println("Your choice: ");
	    }

		String line = br.readLine();
		
		int choice;
		try {
		choice = Integer.parseInt(line);
		} catch ( NumberFormatException e)
		{
			return false;
		}
		
		switch (choice) {
		case 1:
			if (network.size() == 0 )
			{
				System.out.println("No nodes yet");
				break;
			}
			
			for ( int i = 0; i < network.size(); i++ )
			{
				System.out.println("Machine #" + i + ": "
						+ network.get(i).toString());
			}
			System.out.print("Please choose machine number: ");
			choice = Integer.parseInt(br.readLine());
			performCalc(network.get(choice).getRpc(), Operation.ADDITION, 5, 3);
			break;
			
		case 7: {
			exitNetwork();
			break;
		}
		case 9: {
			System.out.println("Please enter IP: ");
			String ip = br.readLine();

			// add this server to serverlist
			addNode(ip);
			break;
		}
		case 8:
			System.out.println("Network size is: "+network.size());
			for ( RemoteNode node : network )
			{
				System.out.println(node);
			}
			break;
		case 0:
			System.exit(0);
			break;
		default:
			return false;
		}
		return true;
	}
	
	private void exitNetwork() {
		for ( RemoteNode node : network )
		{
			if ( node.ip != this.ip )
			{
				propogateExitMessage( node.rpc );	
			}
		}
		network.clear();
		network.add(node);
	}
	
	public void propogateExitMessage(XmlRpcClient xmlRpcClient )
	{
		Vector<String> params = new Vector<String>();
		params.add(this.ip);
		try {
			xmlRpcClient.execute("ClientAux.nodeExistedNetwork", params);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}


	public void propogateNewNodeMessage(XmlRpcClient xmlRpcClient, String ip )
	{
		Vector<String> params = new Vector<String>();
		params.add(ip);
		try {
			xmlRpcClient.execute("ClientAux.newNodeInNetwork", params);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}



	public void performCalc(XmlRpcClient xmlRpcClient, Operation op, int x, int y) {

		Vector<Integer> params = new Vector<Integer>();
		params.addElement(x);
		params.addElement(y);
		
		Object result = null;
		try {
			result = xmlRpcClient.execute("Calculator."+op, params);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		int numericalResult = ((Integer) result).intValue();
		System.out.println("The sum is: " + numericalResult);

	}

	@Override
	public void run() {
		// TODO Auto-generated method stub
		boolean printMenu = true;

		while (true)
		{
			try {
				printMenu = showMenu( printMenu );
			} catch (NumberFormatException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
	}

	public static Client getInstance() throws Exception {
		if ( instance == null )
		{
			throw new Exception();
		}
		return instance;
	}

	public void removeNodeFromStructure(String nodeIp) {
		for ( int index = 0; index < network.size(); index++ )
		{
			if ( network.get(index).ip.compareTo(nodeIp) == 0 )
			{
				network.remove(index);
				break;
			}
		}
	}
}
