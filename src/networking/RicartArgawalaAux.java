package networking;

public class RicartArgawalaAux {

	public static boolean okReceived(String ip)
	{
		((RicartArgawala)(RicartArgawala.getInstance())).okReceived( ip );
		return true;
	}
	
	public static boolean requestReceived( String ip, String timestamp )
	{
		((RicartArgawala)(RicartArgawala.getInstance())).requestReceived( ip, Long.parseLong(timestamp) );
		return true;
	}
}
