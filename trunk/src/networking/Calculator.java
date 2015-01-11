package networking;

public class Calculator {
    
	public static int add(int i2) {
		int i1 = 0;
		try {
			i1 = Client.getInstance().getCurrentValue();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		System.out.println("Calculating: "+ i1 + " + "+ i2);
		int result = i1 + i2;
		try {
			Client.getInstance().storeNewResult(result);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        return result;
    }
    
    public static int subtract(int i2) {
		int i1 = 0;
		
		try {
			i1 = Client.getInstance().getCurrentValue();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		System.out.println("Calculating: "+ i1 + " - "+ i2);
		int result = i1 - i2;
		try {
			Client.getInstance().storeNewResult(result);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

        return result;
    }
    
    public static int divide(int i2)
    {
		int i1 = 0;
		try {
			i1 = Client.getInstance().getCurrentValue();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		int result = i1;
		if ( i2 != 0 )
		{
			result = i1/i2;
			System.out.println("Calculating: "+ i1 + " / "+ i2);
			try {
				Client.getInstance().storeNewResult(result);
			} catch (Exception e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}

		}

    	return result;
    }
    
    public static int multiply( int i2 )
    {
		int i1 = 0;
		try {
			i1 = Client.getInstance().getCurrentValue();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		int result = i1*i2;
		System.out.println("Calculating: "+ i1 + " * "+ i2);
		try {
			Client.getInstance().storeNewResult(result);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

    	return result;
    }
}