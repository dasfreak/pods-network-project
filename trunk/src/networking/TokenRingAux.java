package networking;

public class TokenRingAux {
	public static boolean tokenReceived( String ipCreator, String ipHolder )
	{
		((TokenRing) TokenRing.getInstance()).receivedToken( new Token( ipCreator, ipHolder ) );
		return true;
	}
}
