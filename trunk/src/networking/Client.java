package networking;

import java.util.LinkedList;
import java.util.List;
import java.util.Scanner;
import java.util.Vector;

import org.apache.xmlrpc.WebServer;
import org.apache.xmlrpc.XmlRpcClient;

public class Client {
	String ip;
	List<String> ip_list = new LinkedList<String>();
	public static int port = 50000;
	XmlRpcClient server = null;

	public Client() {

	}

	public Integer sum(int x, int y) {
		System.out.println("Received method call (sum)");
		return new Integer(x + y);
	}

	public void startServer() {
		try {
			System.out.println("Starting Server");
			WebServer server = new WebServer(port);
			server.addHandler("sample", new Client());
			server.start();
			System.out.println("Server running on port " + port);
		} catch (Exception exception) {
			System.err.println("JavaServer: " + exception);
		}
	}

	public void connect(String ip) {
		try {
			server = new XmlRpcClient("http://" + ip + ":50000" + "/RPC2");
		} catch (Exception exception) {
			System.err.println("JavaClient: " + exception);
		}

	}

	public void showMenu() {

		Scanner console = new Scanner(System.in);

		System.out.println("--- Super cool networking client ---");
		System.out.println("(1) 5+3");
		System.out.println("(8) show network");
		System.out.println("(9) add known node");
		System.out.println("(0) exit");
		System.out.print("Your choice: ");
		int choice = console.nextInt();

		switch (choice) {
		case 1:
			if(ip_list.size() == 0){ System.out.println("No nodes yet"); break;}
			for (int i = 0; i < ip_list.size(); i++) {
				System.out.println("Machine #" + i + ": "
						+ ip_list.get(i).toString());
			}
			System.out.print("Please choose machine number: ");
			Scanner console2 = new Scanner(System.in);
			choice = console2.nextInt();
			connect(ip_list.get(choice));
			getSum(5, 3);
			break;
		case 9: {
			System.out.print("Please enter IP ");
			Scanner console3 = new Scanner(System.in);
			String ip = console3.nextLine();

			// add this server to serverlist
			ip_list.add(ip);

			break;
		}
		case 8:
			System.out.println(ip_list.toString());
			break;
		case 0:
			System.exit(0);
			break;
		default:
			return;
		}
	}

	public void getSum(Integer x, Integer y) {

		Vector params = new Vector();
		params.addElement(new Integer(x));
		params.addElement(new Integer(y));

		Object result = null;
		try {
			result = server.execute("sample.sum", params);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		int sum = ((Integer) result).intValue();
		System.out.println("The sum is: " + sum);

	}

}
