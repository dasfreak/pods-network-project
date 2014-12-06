package networking;

import java.net.InetAddress;
import java.net.UnknownHostException;
import java.util.LinkedList;
import java.util.List;
import java.util.Scanner;
import java.util.Vector;
import java.io.BufferedReader;
import java.io.Console;
import java.io.IOException;
import java.io.InputStreamReader;

import org.apache.xmlrpc.*;


public class Client implements Runnable {
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

	public void addNode(String ip) {
		try {
			network.add(new RemoteNode(ip,new XmlRpcClient("http://" + ip + ":50000" + "/RPC2")));
		} catch (Exception exception) {
			System.err.println("JavaClient: " + exception);
		}
	}

	public void showMenu() throws NumberFormatException, IOException {

	    BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
	
		System.out.println("--- Super cool networking client ---");
		System.out.println("(1) 5+3");
		System.out.println("(8) show network");
		System.out.println("(9) add known node");
		System.out.println("(0) exit");
		System.out.println("Your choice: ");
		String line = br.readLine();
		
		int choice = Integer.parseInt(line);
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
			return;
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
		while (true)
		{
			try {
				showMenu();
			} catch (NumberFormatException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
	}
	
}
