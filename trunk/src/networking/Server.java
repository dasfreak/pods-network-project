package networking;


import org.apache.xmlrpc.*;

public class Server {

	private static final int port = 50000;

	private WebServer server;
	
	public Server()
	{
		server = new WebServer(port);
		server.addHandler("Calculator", Calculator.class);
		server.addHandler("$default", new Echo());
		// TODO Auto-generated method stub
		System.out.println("Attempting to start XML-RPC Server at port: "+port);
		server.start();
		System.out.println("Started successfully.");
		System.out.println("Accepting requests. (Halt program to stop.)");
	
	}


}
