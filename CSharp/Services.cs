using System;
using System.Collections.Generic;
using System.Text;

namespace Networking
{
    public class NetworkServices : MarshalByRefObject, NetworkServerInterface
    //public class Networking : XmlRpcService, NetworkInterface
    {


        public bool newNodeInNet(String ip)
        {
            Client.getInstance().addNode( ip);
            return true;
        }

        public bool nodeQuitNetwork(String ip)
        {       
             Client.getInstance().removeNodeFromStructure(ip);
           
            return true;
        }

        public int subtract(int s1, int s2)
        {
            return s1 - s2;
        }

        public int add(int s1, int s2)
        {
            return s1 + s2;
        }

        public int multiply(int s1, int s2)
        {
            return s1 * s2;
        }

        public int divide(int s1, int s2)
        {
            return s1 / s2;
        }


    }
}
