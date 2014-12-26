package networking;

public class ClientAux {
	 public static boolean newNodeInNetwork(String ip) {
		 try {
			Client.getInstance().addNodeToStructure(ip);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return true;
		 
	 }
}
