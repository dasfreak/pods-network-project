package networking;

import java.net.Inet4Address;
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
	
	volatile List<RemoteNode> network;
	private volatile int startValue;
	private volatile int currentValue;
	volatile private Thread syncAlgo;
	volatile private Thread calcTask;
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
	//	Enumeration<NetworkInterface> nets = NetworkInterface.getNetworkInterfaces();
	//	for (NetworkInterface netint : Collections.list(nets))
	//	    displayInterfaceInformation(netint);
		
		// get all ip-addresses and sort out loopback/virtual/offline and ipv6
		Enumeration<NetworkInterface> interfaces = NetworkInterface.getNetworkInterfaces();
		while (interfaces.hasMoreElements()){
		    NetworkInterface current = interfaces.nextElement();
		    if (!current.isUp() || current.isLoopback() || current.isVirtual()) continue;
		    Enumeration<InetAddress> addresses = current.getInetAddresses();
		    while (addresses.hasMoreElements()){
		        InetAddress current_addr = addresses.nextElement();
		        if (current_addr.isLoopbackAddress()) continue;
		        if (current_addr instanceof Inet4Address)
					  ip = current_addr.getHostAddress();
		    }
		}
		
		
		
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
			if ( node.ip.compareTo(this.ip) != 0 )
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
			System.out.println("(3) Start distributed calculation with initial value");
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
		
		case 8:
		{
			performCalc(Operation.ADDITION,5);
		}
		
		case 3:
		{
			if (network.size() < 1 )
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
			System.out.print("For Token Ring please type 1, for Ricart & Argawala please type 2: ");
			int algoChoice = Integer.parseInt(br.readLine());
			startCalc(intitialValue, algoChoice);
			//performCalc(network.get(choice).getRpc(), Operation.ADDITION, 5);
			break;
		}
		
		case 4:
		{
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
	


	private void startCalc(int intitialValue, int algoChoice) {
		setStartValue( intitialValue, algoChoice );
		System.out.println("Starting distributed calc with initial value of: "+intitialValue);
		for ( RemoteNode node : network )
		{
			if ( node.ip.compareTo(this.ip) != 0  )
			{
				propogateStartMessage( node.rpc, intitialValue, algoChoice );	
			}
		}
	}

	private void propogateStartMessage( XmlRpcClient xmlRpcClient, int intitialValue, int algoChoice) {
		Vector<Integer> params = new Vector<Integer>();
		params.add(intitialValue);
		params.add(algoChoice);
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
			if ( node.ip.compareTo(this.ip) != 0 )
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
			xmlRpcClient.execute("ClientAux.nodeQuitNetwork", params);
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
		

//		System.out.println("-----------------------------");
//		for ( RemoteNode node : network )
//		{
//			System.out.println(node.ip);
//		}
//		System.out.println("-----------------------------");
		
//		System.out.println(" ===========> network.size() = "+network.size());
		for ( RemoteNode node : network )
		{
			if ( node.ip.compareTo(this.ip) != 0 )
			{
				System.out.println("# Sending calc operation: "+op+" with value: "+x+" to ip "+node.ip);
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
		
		try {
			xmlRpcClient.execute("Calculator."+op, params);
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

	public void setStartValue(int value, int algoChoice) {
		if ( calcTask != null && calcTask.isAlive() )
		{
			System.out.println("Recieved start value: "+value +" But session is already in progress, please wait until it finishes!"); 
		}
		else
		{
			System.out.println("Recieved start value: "+value);
			currentValue = startValue = value;;
			
			// start thread for 20 seconds that performs the random calculation
			if ( algoChoice == 1)
			{
				System.out.println("Starting TokenRing");
				syncAlgo = new Thread( new TokenRing(this.network, this.ip));
			}
			else
			{
				System.out.println("Starting Ricart Argawala");
				syncAlgo = new Thread( new RicartArgawala(this.network, this.ip));
			}
			
			syncAlgo.start();
			calcTask = new Thread(new CalculatingTask());
			calcTask.start();
		}
	}

	public int getCurrentValue() {
		return currentValue;
	}

	public void storeNewResult(int result) {
		this.currentValue = result;
		System.out.println("Result changed to: "+result);
	}

	public void handshake() {
		int repliesCounter = 0;
		Vector<String> params = new Vector<String>();
		params.addElement(this.ip);
		
		for ( RemoteNode node : network )
		{
			if ( node.ip.compareTo(this.ip) != 0 )
			{
				try {
					Object result = node.rpc.execute("ClientAux.handshakeMessage", params);
					if ( ((Boolean)result) )
						repliesCounter++;
				} catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}

			}
		}
		while (repliesCounter != network.size() - 1)
		{
			System.out.println("WTF someone didn't reply ?!");
		}
		System.out.println("Handshake done successfuly!");
	}

	public boolean threadsAreRunning() {
		return (syncAlgo != null && syncAlgo.isAlive() &&
				calcTask != null && calcTask.isAlive() );
	}
}
