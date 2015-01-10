package networking;

public class Calculator {
    
	public static int add(int i1) {
		int i2 = 0;
		try {
			i2 = Client.getInstance().getCurrentValue();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        return i1 + i2;
    }
    
    public static int subtract(int i1) {
		int i2 = 0;
		try {
			i2 = Client.getInstance().getCurrentValue();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        return i1 - i2;
    }
    
    public static int divide(int i1)
    {
		int i2 = 0;
		try {
			i2 = Client.getInstance().getCurrentValue();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

    	return i1/i2;
    }
    
    public static int multiply( int i1 )
    {
		int i2 = 0;
		try {
			i2 = Client.getInstance().getCurrentValue();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
    	return i1*i2;
    }
}