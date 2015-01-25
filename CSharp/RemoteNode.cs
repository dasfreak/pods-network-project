using System;
using System.Collections.Generic;
using System.Text;

namespace Networking
{
    public class RemoteNode : IComparable
    {
        String ip;
        String url;

        public RemoteNode(String ip, String url)
        {
            this.ip = ip;
            this.url = url;
        }
        public String getIP()
        {
            return ip;
        }
        public String getURL()
        {
            return url;
        }

        public int CompareTo(Object node)
        {
            if (node is RemoteNode)
            {
                return this.ip.CompareTo(((RemoteNode) node).getIP());

             
                
            }
          return 0;
        }

       /* 
        public int Compare(RemoteNode x, RemoteNode y)
        {
            return x.getIP().CompareTo(y.getIP());
        }*/

    }
}
