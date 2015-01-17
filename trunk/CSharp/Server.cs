
using System;

using System.Web;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using CookComputing.XmlRpc;

//using System.Text;

namespace Networking
{

    public class Server
    {

        public void startServer(){

          IDictionary props = new Hashtable();

          props["name"] = "MyHttpChannel";
          props["port"] = 5000;
          HttpChannel channel = new HttpChannel(
             props,
             null,
             new XmlRpcServerFormatterSinkProvider()
          );
          ChannelServices.RegisterChannel(channel, false);

          RemotingConfiguration.RegisterWellKnownServiceType(
            typeof(NetworkServices),
            "RPC2",
            WellKnownObjectMode.Singleton);
          
          //Console.WriteLine("Press <ENTER> to shutdown");
          //Console.ReadLine();
          
        }
     
    }
}
