package networking;

import java.net.InetAddress;
import java.net.MalformedURLException;
import java.net.UnknownHostException;
import java.util.LinkedList;
import java.util.List;
import java.util.Vector;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

import org.apache.xmlrpc.*;


public class Client implements Runnable {
	
	private static Client instance;

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

	public Client() {
		instance = this;
		network = new LinkedList<RemoteNode>();
		try {
			ip = InetAddress.getLocalHost().getHostAddress().toString();
		} catch (UnknownHostException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		new LinkedList<String>();
		System.out.println("My IP is: "+ip);
	}

	public void addNodeToStructure(String ip)
	{
		try {
			network.add(new RemoteNode(ip,new XmlRpcClient("http://" + ip + ":5000" + "/RPC2")));
		} catch (MalformedURLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	private void addNode(String ip) {
		addNodeToStructure(ip);
		for ( RemoteNode node : network )
		{
			propogateMessage( node.rpc, ip );
		}
	}

	public boolean showMenu( boolean printMenu) throws IOException {

	    BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
	
	    if ( printMenu )
	    {
			System.out.println("--- Super cool networking client ---");
			System.out.println("(1) 5+3");
			System.out.println("(8) show network");
			System.out.println("(9) add known node");
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
	
	public void propogateMessage(XmlRpcClient xmlRpcClient, String ip )
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
}
