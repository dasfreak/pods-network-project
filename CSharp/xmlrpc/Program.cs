using System;
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
        double Add(double A, double B);
    }

    public class Server : Methods
    {

        public double Add(double A, double B)
        {
            return A + B;
        }
    }

    public class serverPart
    {
        public void start()
        {
            // start server on localhost
            Console.WriteLine("Starting Server");

            Uri baseAddress = new UriBuilder(Uri.UriSchemeHttp, "localhost", -1, "/xmlrpc").Uri;
            ServiceHost serviceHost = new ServiceHost(typeof(Server));
            var epXmlRpc = serviceHost.AddServiceEndpoint(typeof(Methods), new WebHttpBinding(WebHttpSecurityMode.None), new Uri(baseAddress, "./xmlrpc"));
            epXmlRpc.Behaviors.Add(new XmlRpcEndpointBehavior());
            serviceHost.Open();
            Console.WriteLine("listening at: {0}", epXmlRpc.ListenUri);
        }
    }

    public class clientPart
    {
        Methods client;
        List<string> IpList = new List<string>();

        public void start()
        {
            // start client
            Console.WriteLine("Starting Client");
        }

        public void connect(string ip)
        {
            ChannelFactory<Methods> cf = new ChannelFactory<Methods>(new WebHttpBinding(), "http://" + ip + "/xmlrpc");
            cf.Endpoint.Behaviors.Add(new XmlRpcEndpointBehavior());
            client = cf.CreateChannel();
            Console.WriteLine("Connected to: http://" + ip + "/xmlrpc");
        }

        public double doMath()
        {
            Console.WriteLine("Sending 5+3...");
            return client.Add(5, 3);
        }
    }

    class Program
    {     

        static void Main(string[] args)
        {

        // start server thread and wait till started
        serverPart serverObject = new serverPart();
        Thread serverThread = new Thread(serverObject.start);
        serverThread.Start();
        while (!serverThread.IsAlive);

        // start client thread and wait till started
        clientPart clientObject = new clientPart();
        Thread clientThread = new Thread(clientObject.start);
        clientThread.Start();
        while (!clientThread.IsAlive);

        // pause main thread
        Thread.Sleep(1000);

        // connect to ip
        Console.WriteLine("Enter ip: ");
        string ip = Console.ReadLine();
        clientObject.connect(ip);

        // send Add command and print result
        double answer = clientObject.doMath();
        Console.WriteLine("Result: " + answer.ToString());
        Console.ReadLine();
        }
    }
}