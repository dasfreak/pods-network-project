package networking;


import java.util.LinkedList;
import java.util.List;

import networking.Client.RemoteNode;

public abstract class SyncAlgorithm {
	
	protected List<RemoteNode> network;
	protected String ip;
	protected volatile boolean isCalcDone = true;
	protected volatile boolean isPending = false;

	protected static SyncAlgorithm instance = null;

	public SyncAlgorithm(List<RemoteNode> network, String ip)
	{
		// pay attention to the difference between network and this.network
		this.network = new LinkedList<RemoteNode>();
		this.network.addAll(network);
		this.ip      = ip;
	}
	
	public synchronized boolean isCalcDone() {
		return isCalcDone;
	}
	
	public synchronized void setCalcDone(){
		isCalcDone = true;
	}
	
	public synchronized void setCalcInProgress(){
		isCalcDone = false;
	}
	
	public static SyncAlgorithm getInstance()
	{
		return instance;
	}

	abstract  public boolean canAccess();

	public synchronized void setPending() {
		System.out.println("Settig isPending");
		isPending = true;
	}
}
