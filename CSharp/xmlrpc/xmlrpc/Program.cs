using System;
using System.ServiceModel;
using Microsoft.Samples.XmlRpc;

namespace xmlrpc
{

    // describe your service's interface here
    [ServiceContract()]
    interface IMath
    {
        [OperationContract()]
        double Add(double A, double B);
    }

    public class Math : IMath
    {
        public double Add(double A, double B)
        {
            return A + B;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Starting Server");

            Uri baseAddress = new UriBuilder(Uri.UriSchemeHttp, "localhost", -1, "/xmlrpc").Uri;
            ServiceHost serviceHost = new ServiceHost(typeof(Math));
            var epXmlRpc = serviceHost.AddServiceEndpoint(typeof(IMath), new WebHttpBinding(WebHttpSecurityMode.None), new Uri(baseAddress, "./xmlrpc"));
            epXmlRpc.Behaviors.Add(new XmlRpcEndpointBehavior());

            serviceHost.Open();

            Console.WriteLine("listening at: {0}", epXmlRpc.ListenUri);


            Console.WriteLine("Starting Client");

            ChannelFactory<IMath> cf = new ChannelFactory<IMath>(new WebHttpBinding(), "http://localhost/xmlrpc");

            cf.Endpoint.Behaviors.Add(new XmlRpcEndpointBehavior());

            IMath client = cf.CreateChannel();

            // you can now call methods from your remote service
            double answer = client.Add(1,5);
            Console.WriteLine("Sending 1+5...");
            Console.WriteLine("Result: "+answer.ToString());
            Console.ReadLine();
        }
    }
}