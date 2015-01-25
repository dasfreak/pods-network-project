using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;

namespace Networking
{
    class TokenRing : SyncAlgorithm
    {
        private int indexInRing = -1;
        private string ipCordinator;
        private volatile Token token = null;

        bool _keepRunning = true;


	
        public TokenRing(IList<RemoteNode> network, string ip) : base(network, ip)
		{
			// pay attension to the difference between network and this.network
       
            token = null;
           
            Console.WriteLine(Client.getInstance().getTable());
			this.network.Sort();

            this.indexInRing = Client.getInstance().SearchTableIndex(this.ip,this.network);

            /*
            for (int index = 0; index < this.network.Count; index++)
            {
                //Console.WriteLine(this.network[index].getIP());
				if (this.network[index].getIP().CompareTo(this.ip) == 0)
				{
					this.indexInRing = index;
					break;
				}
			}*/

            Console.WriteLine("My index in network is: " + this.indexInRing);
			fetchCordinator();
			TokenRing.instance = this;



		}

        public void RequestStop()
        {
            _keepRunning = false;
        }

		private void fetchCordinator()
		{
			ipCordinator = this.network[0].getIP();
			if (ipCordinator.CompareTo(this.ip) == 0)
			{
				// this node won the election according to IP metric:
				token = new Token(this.ip, this.ip);
				Console.WriteLine("Won the election, created the ring!");
			}
		}



		public void run()
        {
            Console.WriteLine("\nThread  TokenRing is running\n");
            while (!getcanStart());
            
            while (_keepRunning)
			{

				if ((hasRing() && CalcDone) && !isPending )
				{
					forwardToken();
				}
			}
            Console.WriteLine("\nThread  TokenRing is terminated\n");
		}



 

		public virtual bool hasRing()
		{
			lock (this)
			{
				if (token != null)
                {
                    if (token.currentHolder.CompareTo(this.ip) == 0)
					      return true;
				}
				return false;
			}
		}




		private void forwardToken()
		{
			// fetch next peer index:
		 int nextPeerIndex = (indexInRing + 1) %this.network.Count;
         String nextIP=this.network[nextPeerIndex].getIP();

         token.currentHolder = nextIP;	
	     Console.WriteLine("Forwarding token to IP "+nextIP );

	     // send request to node

         NetworkClientInterface executer = XmlRpcProxyGen.Create<NetworkClientInterface>();
         executer.AttachLogger(new XmlRpcDebugLogger());

         executer.Url = network[nextPeerIndex].getURL();
		 executer.tokenReceived(token.ipCreator,nextIP);

		}



		public virtual void receivedToken(Token token)
		{
			lock (this)
			{
			Console.WriteLine("Received Token!");
				this.token = token;
			}
		}
		public override bool canAccess()
		{
			lock (this)
			{
				// TODO Auto-generated method stub
				return hasRing();
			}
		}

	}

}



	
	
	

	
