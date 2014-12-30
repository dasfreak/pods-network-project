using System;
using System.ServiceModel;
using Microsoft.Samples.XmlRpc;
using System.Collections.Generic;

namespace xmlrpc
{

    // describe your service's interface here
    [ServiceContract()]
    interface Methods
    {
        [OperationContract()]
        List<string> AddToList();

        [OperationContract()]
        double Add(double A, double B);
    }

    public class Global
    {
        public static List<string> IpList = new List<string>();
    }

    public class Server : Methods
    {

        public double Add(double A, double B)
        {
            return A + B;
        }

        public List<string> AddToList()
        {
            return Global.IpList;
        }
    }



    class Program
    {     

        static void Main(string[] args)
        {

        List<string> IpList = new List<string>();

        // start server on localhost
        Console.WriteLine("Starting Server");

        Uri baseAddress = new UriBuilder(Uri.UriSchemeHttp, "localhost", -1, "/xmlrpc").Uri;
        ServiceHost serviceHost = new ServiceHost(typeof(Server));
        var epXmlRpc = serviceHost.AddServiceEndpoint(typeof(Methods), new WebHttpBinding(WebHttpSecurityMode.None), new Uri(baseAddress, "./xmlrpc"));
        epXmlRpc.Behaviors.Add(new XmlRpcEndpointBehavior());
        serviceHost.Open();
        Console.WriteLine("listening at: {0}", epXmlRpc.ListenUri);


        Console.WriteLine("Please enter the IP adress you want to connect to: ");
        string ip = Console.ReadLine();

        // start client
        Console.WriteLine("Starting Client");
        ChannelFactory<Methods> cf = new ChannelFactory<Methods>(new WebHttpBinding(), "http://"+ip+"/xmlrpc");
        cf.Endpoint.Behaviors.Add(new XmlRpcEndpointBehavior());
        Methods client = cf.CreateChannel();
        Console.WriteLine("Connected to: http://" + ip + "/xmlrpc");

        Console.WriteLine("Sending 1+5...");
        double answer = client.Add(1, 5);
        Console.WriteLine("Result: "+answer.ToString());
        Console.ReadLine();
        }
    }
}