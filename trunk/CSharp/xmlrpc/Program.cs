using System;
using System.Net;
using System.ServiceModel;
using Microsoft.Samples.XmlRpc;
using System.Collections.Generic;
using System.Threading;

namespace xmlrpc
{

    // describe your service's interface here
    [ServiceContract()]
    interface Methods
    {

        [OperationContract()]
        Boolean AddNewNodeToList(String ip);

        [OperationContract()]
        Boolean RemoveNodeFromList(String ip);

        [OperationContract()]
        double Add(double A, double B);

        [OperationContract()]
        int getIpListSize();

        [OperationContract()]
        string getIpListEntry(int ID);

    }

    public class Global
    {
        public static List<string> IpList = new List<string>();
    }

    public class Server : Methods
    {
        public void start()
        {
            // start server on localhost
            Console.WriteLine("Starting Server");

            Uri baseAddress = new UriBuilder(Uri.UriSchemeHttp, "localhost", 1337, "/xmlrpc").Uri;
            ServiceHost serviceHost = new ServiceHost(typeof(Server));
            var epXmlRpc = serviceHost.AddServiceEndpoint(typeof(Methods), new WebHttpBinding(WebHttpSecurityMode.None), new Uri(baseAddress, "./xmlrpc"));
            epXmlRpc.Behaviors.Add(new XmlRpcEndpointBehavior());
            serviceHost.Open();
            Console.WriteLine("listening at: {0}", epXmlRpc.ListenUri);
        }

        public double Add(double A, double B)
        {
            return A + B;
        }

        public int getIpListSize()
        {
            return Global.IpList.Count;
        }

        public string getIpListEntry(int ID)
        {
            return Global.IpList[ID];
        }

        public Boolean AddNewNodeToList(String ip)
        {          
            // only add if it doesn't exists
            if(!Global.IpList.Exists(item => item == ip)){
                Global.IpList.Add(ip);
            }
            
            return true;
        }

        public Boolean RemoveNodeFromList(String ip)
        {
            Global.IpList.Remove(ip);
            return true;
        }

    }

    public class clientPart
    {
        Methods client;

        public void printNodes()
        {
            Console.WriteLine("");
            Console.WriteLine("List of Nodes:");
            foreach (string ip in Global.IpList)
            {
                Console.WriteLine(ip);
            }
            Console.WriteLine("");
        }

        public void connect(string ip)
        {
           ChannelFactory<Methods> cf = new ChannelFactory<Methods>(new WebHttpBinding(), "http://" + ip + ":1337/xmlrpc");
            cf.Endpoint.Behaviors.Add(new XmlRpcEndpointBehavior());
            client = cf.CreateChannel();
            Console.WriteLine("Connected to: http://" + ip + ":1337/xmlrpc");
        }

        public double doMath(string ip)
        {
            connect(ip);
            Console.WriteLine("Sending 5+3...");
            return client.Add(5, 3);
        }

        public void sendOwnIpAddressToNetwork(String ip)
        {
           foreach (string nodeAddress in Global.IpList)
            {
                connect(nodeAddress);
                client.AddNewNodeToList(ip);
            }
            Console.WriteLine("Everyone should know you");
        }

        public void sendRemoveRequestToNetwork(String ip)
        {
            // delete own address from list
            Global.IpList.Remove(ip);

            foreach (string nodeAddress in Global.IpList)
            {
                connect(nodeAddress);
                client.RemoveNodeFromList(ip);
            }
            Console.WriteLine("You have been removed from the network");
        }

        public int requestIpListSize()
        {
            return client.getIpListSize();
        }

        public void requestIpList(int size)
        {
            string newAddress;
            for (int i = 0; i < size; i++)
            {
                newAddress = client.getIpListEntry(i);
                if (!Global.IpList.Exists(item => item == newAddress))
                {
                    Global.IpList.Add(newAddress);
                }
            }
        }


    }

    class Program
    {     

        static void Main(string[] args)
        {

        // start server thread and wait till started
        Server serverObject = new Server();
        Thread serverThread = new Thread(serverObject.start);
        serverThread.Start();
        while (!serverThread.IsAlive);

        // start client
        clientPart clientObject = new clientPart();

        // pause main thread
        Thread.Sleep(1000);

        // Get local ip address
        string sHostName = Dns.GetHostName();
        IPHostEntry ipE = Dns.GetHostByName(sHostName);
        IPAddress[] IpA = ipE.AddressList;
        string ipAddress = IpA[0].ToString();
        Console.WriteLine("Your IP-Address is: " + ipAddress);

        // Add own ip address to list
        Global.IpList.Add(ipAddress);
        Global.IpList.Add("127.0.0.1");
        Global.IpList.Add("localhost");

        // ask for ip
        Console.WriteLine("Enter ip: ");
        string ip = Console.ReadLine();

        // connect to host
        clientObject.connect(ip);

        // get number of host in network
        int IpListSize = clientObject.requestIpListSize();

        // request every ip
        clientObject.requestIpList(IpListSize);

        // send own ip address to every node
       clientObject.sendOwnIpAddressToNetwork(ipAddress);
        
        
        // print networknodes
        clientObject.printNodes();

        // send Add command to every node and print result
        foreach (string node in Global.IpList)
        {
            double answer = clientObject.doMath(node);
            Console.WriteLine("Result: " + answer.ToString());
            Console.ReadLine();
        }

        // sign off from network
        clientObject.sendRemoveRequestToNetwork(ipAddress);


        Console.ReadLine();
        }
    }
}