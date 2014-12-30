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
        double Add(double A, double B);
        string Test(string Name);
    }

    public class Server : Methods
    {
        public double Add(double A, double B)
        {
            return A + B;
        }

        public string Test(string Name)
        {
            return "Hello"+Name;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
        //store ip addresses
        List<string> iplist = new List<string>();


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
        iplist.Add(ip);

        // start client
        Console.WriteLine("Starting Client");
        ChannelFactory<Methods> cf = new ChannelFactory<Methods>(new WebHttpBinding(), "http://"+iplist[0]+"/xmlrpc");

        cf.Endpoint.Behaviors.Add(new XmlRpcEndpointBehavior());

        Methods client = cf.CreateChannel();

        // call client method Add
        string asd = client.Test("Marc");
        Console.WriteLine(asd);
        double answer = client.Add(1,5);
        Console.WriteLine("Sending 1+5...");
        Console.WriteLine("Result: "+answer.ToString());
        Console.ReadLine();
        }
    }
}