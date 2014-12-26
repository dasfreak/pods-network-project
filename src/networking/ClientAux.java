package networking;

public class ClientAux {
	 public static void newNodeInNetwork(String ip) {
		 try {
			Client.getInstance().addNodeToStructure(ip);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		 
	 }
}
