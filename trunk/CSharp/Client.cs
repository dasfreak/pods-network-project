using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CookComputing.XmlRpc;


namespace Networking
{

    public struct StateStructRequest
    {
        public int value1;
        public int value2;
    }


    public class Client
  {


    private static Client instance;   // singleton design


    public static Client getInstance() // singleton design
    {
        return instance;
    }

    String ip;
    RemoteNode node;
    List<RemoteNode> network;
    static String port = "5000";
    private int startValue;
    private bool isStartValueSet;
    private int currentValue;

    bool initiator = false;
    Thread RicartArgawala_Thread;
    Thread TokenRing_Thread;
    public Thread CalculatingTask_Thread;
    RicartArgawala RA;
    TokenRing TR;

    public Client(){
        IPHostEntry host;
        string localIP = "?";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
            }
        }
        Initililze(localIP);
    }

    public static Client Instance
		{
			get
			{
				if (instance == null)
				{
					throw new Exception();
				}
				return instance;
			}
		}

    public Client(String ip){
        Initililze(ip);
      }

    public void Initililze(String ip) {

        instance = this;
        network = new List<RemoteNode>();
        this.ip = ip;
        this.node = new RemoteNode(ip, generate_url(ip));
        network.Add(new RemoteNode(ip, generate_url(ip)));
    
     }

   public void setIP(String ip){       

       network.RemoveAt(TableContains(this.ip));
       
       this.ip = ip;
       node = new RemoteNode(ip, generate_url(ip));
       network.Add(node);
       exitNetwork();

    }

   public String getIP()
   {
       return this.ip;
   }

   public String getTable()
   {
       String Table = "";
       //foreach()
      
       for (int i = 0; i < network.Count; i++)
       {
           if (network[i]!=null)
             Table = Table + network[i].getIP() + "\n";

       }
       return Table;
   }


    public String generate_url(String ip){
        return "http://"+ip+":"+port+"/RPC2";
     }

    public bool propogateNewNodeMessage(String url ,String ip)
    {

        bool param = false;

        NetworkClientInterface Join = XmlRpcProxyGen.Create<NetworkClientInterface>();
        Join.AttachLogger(new XmlRpcDebugLogger());

        Join.Url = url;

        param = Join.newNodeInNet(ip);

        return param;
    }


    public bool joinNetwork(String ip )
    {
        bool param = false;
        String url = generate_url(ip);

        RemoteNode node = new RemoteNode(ip,url);

        if (TableContains(ip) ==(-1))
          {
              NetworkClientInterface Join = XmlRpcProxyGen.Create<NetworkClientInterface>();
              Join.AttachLogger(new XmlRpcDebugLogger());

              Join.Url = url;
              network.Add(node);

              param = Join.newNodeInNet(this.ip);              

              //if (param == true) network.Add(node);
              if (param == false) network.RemoveAt(TableContains(ip));
          }

      return param;
	  }

    public void propagate(String ip) {

        String url = generate_url(ip);

        for (int i = 0; i < network.Count; i++)
        {
            if (network[i] != null)
            {
                if ((network[i].getIP() != this.ip) && (network[i].getIP() != ip))
                {
                    propogateNewNodeMessage(network[i].getURL(), ip); // new node to table entry
                    propogateNewNodeMessage(url, network[i].getIP());     // Table content to new node
                }
            }
        }
    
     }

    public int TableContains (String ip){
        int index = -1;

        for (int i = 0; i < network.Count; i++){
            if (network[i] != null){
                if (network[i].getIP() == ip){
                    index = i;
                    break;
                }                   
            }
        }
        return index;
     }

    public void addNode(String ip)
    {
        RemoteNode local_node = new RemoteNode(ip, generate_url(ip));

        if (TableContains(ip)==(-1))
        {
            network.Add(local_node);                                 
            propagate(ip);
            
        }
	}

    public void exitNetwork() {

        for (int i = 0; i < network.Count; i++)	
		{
            if (network[i] != null) 
            {
                if (network[i].getIP() != this.ip)
                  propogateExitMessage(network[i].getURL());	
			}
		}

        network.Clear();        
        network.Add(this.node);
	}
	

    public void propogateExitMessage(String  url)
	{
        NetworkClientInterface Leave = XmlRpcProxyGen.Create<NetworkClientInterface>();
         Leave.AttachLogger(new XmlRpcDebugLogger());

         Leave.Url = url;

         bool param = Leave.nodeQuitNetwork(this.ip); 
		
	}

    public void removeNodeFromStructure(String ip)
    {        
        network.RemoveAt(TableContains(ip));
    }

    public void setStartValue(int value, int algoChoice) {
        if (CalculatingTask_Thread != null && CalculatingTask_Thread.IsAlive)
		{
            Console.WriteLine("Recieved start value: " + value + " But session is already in progress, please wait until it finishes!");
		}
        else
        {
            Console.WriteLine("Recieved start value: " + value);
            currentValue = startValue = value;
            isStartValueSet = true;
            // start thread for 20 seconds that performs the random calculation
            if (algoChoice == 1)
            {
                Console.WriteLine("Starting TokenRing");
                
                TR = new TokenRing(this.network, this.ip);
                TokenRing_Thread = new Thread(TR.run);
                TokenRing_Thread.Start();
           
            }
            else
            {
                Console.WriteLine("Starting Ricart Argawala");
                RA = new RicartArgawala(this.network, this.ip);
                RicartArgawala_Thread = new Thread(RA.run);
                RicartArgawala_Thread.Start();

            }

            StartCalculatingTask(true);
            //StartCalculatingTask(!initiator); // Starts Calculation only if node did't triggerd the calc.
            
            //var thread2 = new Thread(() => new CalculatingTask());
            //thread2.Start();
        }
	}

    public void StopsRicartArgawalaThread()
    {
        
        if (RicartArgawala_Thread!=null)
        {
            if (RicartArgawala_Thread.IsAlive)
                RA.RequestStop();
        }
     }

    public void StopsTokenRingThread()
    {

        if (TokenRing_Thread != null)
        {
            if (TokenRing_Thread.IsAlive)
                TR.RequestStop();
        }
    }

        public virtual void performCalc(Operation op, int x)
		{
			foreach (RemoteNode node in network)
			{
				if (node.getIP() != this.ip)
				{
					performCalc(node.getURL(), op, x);
				}
				else
				{
					switch (op.InnerEnumValue())
					{
					case Operation.InnerEnum.ADDITION:
						Calculator.add(x);
						break;
					case Operation.InnerEnum.SUBSTRACTION:
						Calculator.subtract(x);
						break;
					case Operation.InnerEnum.MULTIPLICATION:
						Calculator.multiply(x);
						break;
					case Operation.InnerEnum.DIVISION:
						Calculator.divide(x);
						break;
					default:
						break;
					}

				}
			}
		}

        public virtual void performCalc(string xmlRpcClient, Operation op, int x)
		{

		
			try
			{

                NetworkClientInterface executer = XmlRpcProxyGen.Create<NetworkClientInterface>();
                executer.AttachLogger(new XmlRpcDebugLogger());

                executer.Url = xmlRpcClient;

                switch (op.InnerEnumValue())
                {
                    case Operation.InnerEnum.ADDITION:
                        executer.add(x);
                        break;
                    case Operation.InnerEnum.SUBSTRACTION:
                        executer.subtract(x);
                        break;
                    case Operation.InnerEnum.MULTIPLICATION:
                        executer.multiply(x);
                        break;
                    case Operation.InnerEnum.DIVISION:
                        executer.divide(x);
                        break;
                    default:
                        break;
                }

				
			}
			catch (Exception e)
			{
				// TODO Auto-generated catch block
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
		}

        public virtual bool StartMessageSet
		{
			get
			{
				return isStartValueSet;
			}
		}

		public virtual int CurrentValue
		{
			get
			{
				return currentValue;
			}
		}

		public virtual void storeNewResult(int result)
		{
			this.currentValue = result;
			Console.WriteLine("Result changed to: " + result);
		}


        void StartCalculatingTask(bool nodeStartsCalc)
        {
            if (nodeStartsCalc)
            {
             
                CalculatingTask CT = new CalculatingTask();
                CalculatingTask_Thread = new Thread(CT.run);
                CalculatingTask_Thread.Start();

              //  if(initiator) 
                //     CalculatingTask_Thread.Join(); // waits for calc. end

                // StopsRicartArgawalaThread(); //moved to end of CalculatingTask_Thread
               
            }
      }

          public bool threadsAreRunning()
           {
            
           return (((RicartArgawala_Thread != null)&&(RicartArgawala_Thread.IsAlive))||
                  ((TokenRing_Thread != null) && (TokenRing_Thread.IsAlive)));       

           }

          public bool SendHandShake(String url)
          {
            NetworkClientInterface HandShake = XmlRpcProxyGen.Create<NetworkClientInterface>();
            HandShake.AttachLogger(new XmlRpcDebugLogger());

            HandShake.Url = url;
            return HandShake.handshakeMessage(this.ip);           
          }



        public void handshake() {

		int repliesCounter = 0;
		bool result=false;

            foreach (RemoteNode node in network)
			  {
               if (node.getIP() != this.ip)
                  {
                    result = SendHandShake(node.getURL()); 
                    if ( ((Boolean)result) )
						repliesCounter++;
                    result=false;
                }
              }

         
		while (repliesCounter != network.Count - 1)
		{
			Console.WriteLine("WTF someone didn't reply ?!"+repliesCounter+" < " +(network.Count-1));
		}

        if(repliesCounter == network.Count - 1)
		  Console.WriteLine("Handshake done successfuly!");
	}



        public void startCalc(int intitialValue, int algoChoice)
		{
            initiator = true;
			setStartValue(intitialValue, algoChoice);
            Console.WriteLine("Starting distributed calc with initial value of: " + startValue);
			foreach (RemoteNode node in network)
			{
				if (node.getIP() != this.ip)
				{
					propogateStartMessage(node.getURL(), intitialValue, algoChoice);
				}
			}

            //StartCalculatingTask(initiator);// node triggerd the calc. starts calc. after all nodes recieved StartMessage
             
            
            initiator = false;
		}

    public void propogateStartMessage(string xmlRpcClient, int intitialValue, int algoChoice)
		{
			try
			{
                NetworkClientInterface executer = XmlRpcProxyGen.Create<NetworkClientInterface>();
                executer.AttachLogger(new XmlRpcDebugLogger());

                executer.Url = xmlRpcClient;

                executer.startMessage(intitialValue, algoChoice);
			}
			catch (Exception e)
			{
				// TODO Auto-generated catch block
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}

		}
  }
}