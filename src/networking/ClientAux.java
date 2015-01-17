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
	 
	 public static boolean startMessage( int value, int algoChoice )
	 {
		 try {
			Client.getInstance().setStartValue(value, algoChoice);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return true;
	 }
	 
	 public static boolean nodeQuitNetwork( String ip ){
		 
		 try {
			Client.getInstance().removeNodeFromStructure(ip);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		 
		 return true;
	 }
}
