using System;
using System.Collections.Generic;
using System.Text;
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
    List<RemoteNode> routingTable;
    static String port="5000";

    public Client(){        
        Initililze("127.0.0.1");
    }

    public Client(String ip){
        Initililze(ip);
      }

    public void Initililze(String ip) {

        instance = this;
        routingTable = new List<RemoteNode>();
        this.ip = ip;
        this.node = new RemoteNode(ip, generate_url(ip));
        routingTable.Add(new RemoteNode(ip, generate_url(ip)));
    
     }

   public void setIP(String ip){       

       routingTable.RemoveAt(TableContains(this.ip));
       
       this.ip = ip;
       node = new RemoteNode(ip, generate_url(ip));
       routingTable.Add(node);
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
      
       for (int i = 0; i < routingTable.Count; i++)
       {
           if (routingTable[i]!=null)
             Table = Table + routingTable[i].getIP() + "\n";

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
              routingTable.Add(node);

              param = Join.newNodeInNet(this.ip);              

              //if (param == true) routingTable.Add(node);
              if (param == false) routingTable.RemoveAt(TableContains(ip));
          }

      return param;
	  }

    public void propagate(String ip) {

        String url = generate_url(ip);

        for (int i = 0; i < routingTable.Count; i++)
        {
            if (routingTable[i] != null)
            {
                if ((routingTable[i].getIP() != this.ip) && (routingTable[i].getIP() != ip))
                {
                    propogateNewNodeMessage(routingTable[i].getURL(), ip); // new node to table entry
                    propogateNewNodeMessage(url, routingTable[i].getIP());     // Table content to new node
                }
            }
        }
    
     }

    public int TableContains (String ip){
        int index = -1;

        for (int i = 0; i < routingTable.Count; i++){
            if (routingTable[i] != null){
                if (routingTable[i].getIP() == ip){
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
            routingTable.Add(local_node);                                 
            propagate(ip);
            
        }
	}

    public void exitNetwork() {

        for (int i = 0; i < routingTable.Count; i++)	
		{
            if (routingTable[i] != null) 
            {
                if (routingTable[i].getIP() != this.ip)
                  propogateExitMessage(routingTable[i].getURL());	
			}
		}

        routingTable.Clear();        
        routingTable.Add(this.node);
	}
	

    public void propogateExitMessage(String  url)
	{
        NetworkClientInterface Leave = XmlRpcProxyGen.Create<NetworkClientInterface>();
         Leave.AttachLogger(new XmlRpcDebugLogger());

         Leave.Url = url;

         bool param = Leave.nodeExistedNetwork(this.ip); 
		
	}

    public void removeNodeFromStructure(String ip)
    {        
        routingTable.RemoveAt(TableContains(ip));
    }


}
}
