package networking;


import java.util.LinkedList;
import java.util.List;

import networking.Client.RemoteNode;

public abstract class SyncAlgorithm {
	
	protected List<RemoteNode> network;
	protected String ip;
	protected volatile boolean isCalcDone;
	protected volatile boolean isPending;
	private volatile boolean isSessionDone;
	private volatile boolean canStart;

	protected static SyncAlgorithm instance = null;

	public SyncAlgorithm(List<RemoteNode> networkInput, String ip)
	{
		canStart     = false;
		isSessionDone = false;
		isPending = false;
		isCalcDone = true;
		// pay attention to the difference between network and this.network
		this.network = new LinkedList<RemoteNode>();
		this.network.addAll(networkInput);
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

	abstract public boolean canAccess();

	public synchronized void setPending() {
		System.out.println("Setting isPending");
		isPending = true;
	}

	public void clearPending() {
		isPending = false;
	}

	public synchronized void setSessionDone() {
		isSessionDone = true;
	}
	
	public synchronized boolean isSessionDone(){
		return isSessionDone;
	}

	public void setCanStart() {
		canStart = true;
	}
	public boolean canStart()
	{
		return canStart;
	}
}
