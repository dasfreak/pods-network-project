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

        public TokenRing(IList<RemoteNode> network, string ip) : base(network, ip)
		{
			// pay attension to the difference between network and this.network

			this.network.Sort();

			for (int index = 0; index < network.Count; index++)
			{
				if (this.network[index].getIP().CompareTo(this.ip) == 0)
				{
					this.indexInRing = index;
					break;
				}
			}

			fetchCordinator();
			TokenRing.instance = this;
		}

		private void fetchCordinator()
		{
			ipCordinator = network[0].getIP();
			if (ipCordinator.CompareTo(this.ip) == 0)
			{
				// this node won the election according to IP metric:
				token = new Token(this.ip, this.ip);
				Console.WriteLine("Won the election, created the ring!");
			}
		}



		public void run()
		{
			while (true)
			{
				if (hasRing() && CalcDone)
				{
					forwardToken();
				}
			}
		}


		public virtual bool hasRing()
		{
			lock (this)
			{
				if (token != null && token.currentHolder.CompareTo(this.ip) == 0)
				{
					return true;
				}
				return false;
			}
		}

		private void forwardToken()
		{
			// fetch next peer:
			int nextPeer = (indexInRing + 1) % network.Count;
			//System.out.println("Forwarding token to IP "+network.get(nextPeer).ip);

			List<string> @params = new List<string>();

			@params.Add(token.ipCreator);
			@params.Add(network[nextPeer].getIP());

			token = null;

			// send request to node

            NetworkClientInterface executer = XmlRpcProxyGen.Create<NetworkClientInterface>();
            executer.AttachLogger(new XmlRpcDebugLogger());

            executer.Url = network[nextPeer].getURL();

			executer.tokenReceived(token.ipCreator,network[nextPeer].getIP());

		}

		public virtual void receivedToken(Token token)
		{
			lock (this)
			{
			//	System.out.println("Received Token!");
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
