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



    public class Server : Methods
    {
        public void start()
        {
            // start server on localhost
            Console.WriteLine("Starting Server");

            Uri baseAddress = new UriBuilder(Uri.UriSchemeHttp, "localhost", Global.port, "/xmlrpc").Uri;
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
        Global.IpList.Add("asd");

        int choice = 1;
        while (choice != 0)
        {
            choice = clientObject.showMenu();

            switch (choice)
            {
                case 0:
                    Console.WriteLine("Auf Wiedersehen!");
                    break;
                case 1:
                    Console.WriteLine("Please enter ip: ");
                    string ip = Console.ReadLine();

                    // connect to host
                    clientObject.connect(ip);

                    // get number of host in network
                    int IpListSize = clientObject.requestIpListSize();

                    // request every ip
                    clientObject.requestIpList(IpListSize);

                    // send own ip address to every node
                    clientObject.sendOwnIpAddressToNetwork(ipAddress);

                    break;
                case 2:
                    // print networknodes
                    clientObject.printNodes();
                    break;
                case 3:
                    // send Add command to every node and print result
                    foreach (string node in Global.IpList)
                    {
                        double answer = clientObject.doMath(node);
                        Console.WriteLine("Result: " + answer.ToString());
                        Console.ReadLine();
                    }
                    break;
                case 4:
                    // sign off from network
                    clientObject.sendRemoveRequestToNetwork(ipAddress);
                    break;
                default:
                    Console.WriteLine("wrong input");
                    break;
            }
        }  
  
        Console.ReadLine();
        }
    }
}