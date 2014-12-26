package networking;

public class Networker {

	public static void main(String[] args) {

		// starting client and show menu

		new Server();
		Client client = new Client();
		new Thread( client ).start();
	}
}
