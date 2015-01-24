package networking;

public class Calculator {
    
	static Object syncObject = new Object();
	public synchronized static int add(int i2) {
		System.out.println("==>add");
		int result;
		synchronized(syncObject)
		{
			int i1 = 0;
			try {
				i1 = Client.getInstance().getCurrentValue();
			} catch (Exception e) {
				e.printStackTrace();
			}
			System.out.println("Calculating: "+ i1 + " + "+ i2);
			result = i1 + i2;
			try {
				Client.getInstance().storeNewResult(result);
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		System.out.println("<==add");
        return result;
    }
    
    public synchronized static int subtract(int i2) {
		System.out.println("==>subtract");
		int i1 = 0;
		int result;
		synchronized(syncObject)
		{
			try {
				i1 = Client.getInstance().getCurrentValue();
			} catch (Exception e) {
				e.printStackTrace();
			}
			System.out.println("Calculating: "+ i1 + " - "+ i2);
			result = i1 - i2;
			try {
				Client.getInstance().storeNewResult(result);
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		System.out.println("<==subtract");
        return result;
    }
    
    public synchronized static int divide(int i2)
    {
		System.out.println("==>divide");

    	int result;
		int i1 = 0;
		synchronized(syncObject)
		{
			try {
				i1 = Client.getInstance().getCurrentValue();
			} catch (Exception e) {
				e.printStackTrace();
			}
			
			result = i1;
			if ( i2 != 0 )
			{
				result = i1/i2;
				System.out.println("Calculating: "+ i1 + " / "+ i2);
				try {
					Client.getInstance().storeNewResult(result);
				} catch (Exception e) {
					e.printStackTrace();
				}
	
			}
		}
		System.out.println("<==divide");
    	return result;
    }
    
    public synchronized static int multiply( int i2 )
    {
		System.out.println("==>Multiply");

    	int result;
		synchronized(syncObject)
		{
			int i1 = 0;
			try {
				i1 = Client.getInstance().getCurrentValue();
			} catch (Exception e) {
				e.printStackTrace();
			}
			
			result = i1*i2;
			System.out.println("Calculating: "+ i1 + " * "+ i2);
			try {
				Client.getInstance().storeNewResult(result);
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		System.out.println("<==Multiply");
    	return result;
    }
}