package networking;

public class ClientAux {
	
	 public static boolean newNodeInNetwork(String ip) {
		 try {
			Client.getInstance().addNode(ip);
		} catch (Exception e) {
			
			e.printStackTrace();
		}
		return true;
		 
	 }
	 public static boolean addNodeToStructure(String ip){
		 
		 try {
			Client.getInstance().addNodeToStructure(ip);
		} catch (Exception e) {
			
			e.printStackTrace();
		}
		 return true;
	 }
	 
	 public static boolean handshakeMessage(String ip){
		 try {
			while (!Client.getInstance().threadsAreRunning());
		} catch (Exception e) {
			
			e.printStackTrace();
		}
		return true;
	 }
	 
	 public static boolean startMessage( int value, int algoChoice )
	 {
		 try {
			Client.getInstance().setStartValue(value, algoChoice);
		} catch (Exception e) {
			
			e.printStackTrace();
		}
		return true;
	 }
	 
	 public static boolean nodeQuitNetwork( String ip ){
		 
		 try {
			Client.getInstance().removeNodeFromStructure(ip);
		} catch (Exception e) {
			
			e.printStackTrace();
		}
		 
		 return true;
	 }
}
