using System;
using System.Collections.Generic;
using System.Text;

namespace Networking
{
    public class RemoteNode
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
    }
}
