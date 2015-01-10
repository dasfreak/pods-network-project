package networking;

public class ClientAux {
	
	 static int startValue;
	 public static boolean newNodeInNetwork(String ip) {
		 try {
			Client.getInstance().addNodeToStructure(ip);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return true;
		 
	 }
	 
	 public static boolean startMessage( Integer value )
	 {
		 startValue = value;
		 System.out.println("Recieved start value: "+value);
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
