package networking;

public enum Operation {
	ADDITION ("add"),
	SUBSTRACTION("subtract"),
	MULTIPLICATION("multiply"),
	DIVISION("divide");
	
	
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