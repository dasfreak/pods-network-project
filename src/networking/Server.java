package networking;


import org.apache.xmlrpc.*;

public class Server {

	private static final int port = 5000;

	private WebServer server;
	
	public Server()
	{
		server = new WebServer(port);
		server.addHandler("Calculator", Calculator.class );
		server.addHandler("ClientAux",  ClientAux.class );
		server.addHandler("TokenRingAux", TokenRingAux.class);
		server.addHandler("RicartArgawalaAux", RicartArgawalaAux.class);
		server.addHandler("$default", new Echo());
		System.out.println("Attempting to start XML-RPC Server at port: "+port);
		server.start();
		System.out.println("Started successfully.");
		System.out.println("Accepting requests. (Halt program to stop.)");
	}


}
