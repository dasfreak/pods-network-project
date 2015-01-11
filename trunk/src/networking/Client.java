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

import javax.naming.InitialContext;

import org.apache.xmlrpc.*;


public class Client implements Runnable {

	private static Client instance;
	private RemoteNode node;

	String ip;
	
	List<RemoteNode> network;
	private int startValue;
	private boolean isStartValueSet;
	private int currentValue;
	
	class RemoteNode implements Comparable<RemoteNode>
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
		
		@Override
		public int compareTo(RemoteNode o) {
			return this.ip.compareTo(o.ip);
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
		
		isStartValueSet = false;
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
			System.out.println("(1) join network - specifiy an IP of an exisiting node in the network");
			System.out.println("(2) show network");
			System.out.println("(3) Start calculation with initial value");
			System.out.println("(4) exit network");
			System.out.println("(0) exit ungracefully");
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
		case 1: {
			System.out.println("Please enter IP: ");
			String ip = br.readLine();

			// add this server to serverlist
			addNode(ip);
			break;
		}
		
		case 2:
		{
			System.out.println("Network size is: "+network.size());
			for ( RemoteNode node : network )
			{
				System.out.println(node);
			}
			break;
		}
		
		case 3:{
			if (network.size() <= 1 )
			{
				System.out.println("No nodes yet to start a calculations!");
				break;
			}
			
			for ( int i = 0; i < network.size(); i++ )
			{
				System.out.println("Machine #" + i + ": "
						+ network.get(i).toString());
			}
			System.out.print("Please choose an initial value: ");
			int intitialValue = Integer.parseInt(br.readLine());
			startCalc(intitialValue);
			//performCalc(network.get(choice).getRpc(), Operation.ADDITION, 5, 3);
			break;
		}
		
		case 4: {
			exitNetwork();
			break;
		}

		case 0:
			System.exit(0);
			break;
		default:
			return false;
		}
		return true;
	}
	
	private void startCalc(int intitialValue) {
		setStartValue( intitialValue );
		System.out.println("Starting distributed calc with initial value of: "+intitialValue);
		for ( RemoteNode node : network )
		{
			if ( node.ip != this.ip )
			{
				propogateStartMessage( node.rpc, intitialValue );	
			}
		}
	}

	private void propogateStartMessage( XmlRpcClient xmlRpcClient, int intitialValue) {
		Vector<Integer> params = new Vector<Integer>();
		params.add(intitialValue);
		try {
			xmlRpcClient.execute("ClientAux.startMessage", params);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

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

	
	public void performCalc( Operation op, int x ) {
		for ( RemoteNode node : network )
		{
			if ( node.ip != this.ip )
			{
				performCalc( node.rpc, op, x );	
			}
			else
			{
				switch (op)
				{
				case ADDITION:
					Calculator.add(x);
					break;
				case SUBSTRACTION:
					Calculator.subtract(x);
					break;
				case MULTIPLICATION:
					Calculator.multiply(x);
					break;
				case DIVISION:
					Calculator.divide(x);
					break;
				default:
					break;	
				}
				
			}
		}
	}


	public void performCalc(XmlRpcClient xmlRpcClient, Operation op, int x) {

		Vector<Integer> params = new Vector<Integer>();
		params.addElement(x);
		
		Object result = null;
		try {
			result = xmlRpcClient.execute("Calculator."+op, params);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
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

	public void setStartValue(int value) {
		if ( isStartValueSet )
		{
			System.out.println("Recieved start value: "+value +" But start message is already set - no change!!"); 
		}
		else
		{
			System.out.println("Recieved start value: "+value);
			currentValue = startValue = value;;
			isStartValueSet = true;
			// start thread for 20 seconds that performs the random calculation
			new Thread( new TokenRing(this.network, this.ip));
			new Thread(new CalculatingTask()).start();
		}
	}

	public boolean isStartMessageSet() {
		return isStartValueSet;
	}

	public int getCurrentValue() {
		return currentValue;
	}

	public void storeNewResult(int result) {
		this.currentValue = result;
		System.out.println("Result changed to: "+result);
	}
}
