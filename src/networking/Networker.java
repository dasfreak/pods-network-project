package networking;

import org.apache.xmlrpc.WebServer;

public class Networker {

	public static void main(String[] args) {

		// starting client and show menu
		Client client = new Client();
		client.startServer();

		while (true) {
			client.showMenu();
		}
	}
}
