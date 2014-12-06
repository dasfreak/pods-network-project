package networking;

public enum Operation {
	ADDITION ("add"),
	SUBSTRACTION("sub"),
	MULTIPLICATION("mul"),
	DIVISION("div");
	
	
	private final String opName;
	
	Operation(String opName)
	{
		this.opName = opName;
	}
	public String toString()
	{
		return opName;
	}
}