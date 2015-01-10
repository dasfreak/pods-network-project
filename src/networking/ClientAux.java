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
	 
	 public static boolean startMessage( int value )
	 {
		 try {
			Client.getInstance().setStartValue(value);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return true;
	 }
	 
	 public static boolean nodeExistedNetwork( String ip ){
		 
		 try {
			Client.getInstance().removeNodeFromStructure(ip);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		 
		 return true;
	 }
}
