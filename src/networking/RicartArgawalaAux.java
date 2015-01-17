package networking;

public class RicartArgawalaAux {

	public static boolean okReceived(String ip)
	{
		((RicartArgawala)(RicartArgawala.getInstance())).okReceived( ip );
		return true;
	}
	
	public static boolean requestReceived( String ip, long timestamp )
	{
		((RicartArgawala)(RicartArgawala.getInstance())).requestReceived( ip, timestamp );
		return true;
	}
}
