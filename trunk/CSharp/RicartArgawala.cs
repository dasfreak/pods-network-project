using System;
using System.Collections.Generic;
using CookComputing.XmlRpc;
using System.Linq;
using System.Text;

namespace Networking
{
    class RicartArgawala : SyncAlgorithm
        {

            private volatile HashSet<string> requestsQueue;
            private volatile HashSet<string> okayList;

            private volatile bool canAccess_Renamed;
            private long timestamp;
            bool _keepRunning = true;

            public RicartArgawala(IList<RemoteNode> network, string ip) : base(network, ip)
            {
                // pay attention to the difference between network and this.network
                timestamp = 0;
                requestsQueue = new HashSet<string>();
                okayList = new HashSet<string>();
                isPending = false;

                instance = this;
            }

            public virtual void requestReceived(string ip, long timestamp)
            {
                Console.WriteLine("Received request from ip " + ip + " timestamp = " + timestamp);
                if (CalcDone && !Pending)
                {
                    // send OK
                    sendOk(ip);
                }
                else if (!this.isCalcDone)
                {
                    requestsQueue.Add(ip);
                }
                else if (this.isPending)
                {
                    // queue request
                    if (timestamp <= this.timestamp)
                    {
                        sendOk(ip);
                    }
                    else
                    {
                        requestsQueue.Add(ip);
                    }
                }
            }

            private void sendOk(string ip)
		{
			Console.WriteLine("Sending okay to node " + ip + " timestamp = " + timestamp);
			List<string> @params = new List<string>();
			@params.Add(this.ip);

			foreach (RemoteNode node in this.network)
			{
				if (node.getIP().CompareTo(ip) == 0)
				{

                    NetworkClientInterface executer = XmlRpcProxyGen.Create<NetworkClientInterface>();
                    executer.AttachLogger(new XmlRpcDebugLogger());

                    executer.Url = node.getURL();

					executer.okReceived(ip);
					break;
				}
			}
		}

            public virtual void okReceived(string ip)
            {
                lock (this)
                {
                    Console.WriteLine("==>Received okay from " + ip);
                    okayList.Add(ip);
                    Console.WriteLine("   okayList size is" + okayList.Count);
                }
            }

            public void RequestStop()
            {
                _keepRunning = false;
            }

            public void run()
            {
                Console.WriteLine("\nThread  RicartArgwala is running\n");

                while (_keepRunning)
                {
                    if (Pending)
                    {
                        Console.WriteLine("Pending request detected\n");
                        // request from all nodes
                        okayList.Clear();
                        broadcastRequest();
                        timestamp++;
                        // wait for okay from all
                        while ((okayList.Count <= (network.Count - 1))&&(_keepRunning)); ; // -1 because of self node

                        Access = true;
                        // can access now
                        while ((!CalcDone)&&(_keepRunning));

                        isPending = false;

                        // send okay to all processes in queue
                        sendOkayToQueueNodes();

                        Access = false;
                    }
                }
             Console.WriteLine("\nThread  RicartArgwala is terminated\n");
            }

            private bool Pending
            {
                get
                {
                    lock (this)
                    {
                        return isPending;
                    }
                }
            }

            private void sendOkayToQueueNodes()
		{

			List<string> @params = new List<string>();

			@params.Add(this.ip);

			foreach (string node in requestsQueue)
			{
				foreach (RemoteNode rNode in network)
				{
					if (node.CompareTo(rNode.getIP()) == 0)
					{
						Console.WriteLine("Sending okay to queue node " + rNode.getIP());

                        NetworkClientInterface executer = XmlRpcProxyGen.Create<NetworkClientInterface>();
                        executer.AttachLogger(new XmlRpcDebugLogger());

                        executer.Url = rNode.getURL();

                        executer.okReceived(ip);

						//rNode.getURL().execute("RicartArgawalaAux.okReceived", @params);
					}
				}
			}

		}

            private void broadcastRequest()
		{
			Console.WriteLine("Boradcasting request:");
			List<string> @params = new List<string>();

			@params.Add(this.ip);
			@params.Add(Convert.ToString(this.timestamp));

			foreach (RemoteNode node in network)
            {
                
				if (node.getIP().CompareTo(this.ip) != 0)
                {
                    Console.WriteLine("Boradcasting request to:" + node.getIP());
                    NetworkClientInterface executer = XmlRpcProxyGen.Create<NetworkClientInterface>();
                    executer.AttachLogger(new XmlRpcDebugLogger());

                    executer.Url = node.getURL();

                    executer.requestReceived(this.ip, Convert.ToString(this.timestamp));


						//node.getURL().execute("RicartArgawalaAux.requestReceived", @params);
				}
			}
		}

            public override bool canAccess()
            {
                lock (this)
                {
                    return canAccess_Renamed;
                }
            }

            private bool Access
            {
                set
                {
                    lock (this)
                    {
                        canAccess_Renamed = value;
                    }
                }
            }
        }

    }
