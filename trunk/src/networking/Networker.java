package networking;

import java.net.SocketException;
import java.net.UnknownHostException;

public class Networker {

	public static void main(String[] args) throws UnknownHostException, SocketException {

		new Server();
		Client client = new Client();
		new Thread( client ).start();
	}
}
