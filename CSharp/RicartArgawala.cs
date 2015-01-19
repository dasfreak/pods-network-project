﻿using System;
using System.Collections.Generic;
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
					node.getURL().execute("RicartArgawalaAux.okReceived", @params);
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

            public override void run()
            {
                while (true)
                {
                    if (Pending)
                    {
                        Console.WriteLine("Pending request detected\n");
                        // request from all nodes
                        okayList.Clear();
                        broadcastRequest();
                        timestamp++;
                        // wait for okay from all
                        while (okayList.Count <= (network.Count - 1)) ; // -1 because of self node

                        Access = true;
                        // can access now
                        while (!CalcDone) ;

                        isPending = false;

                        // send okay to all processes in queue
                        sendOkayToQueueNodes();

                        Access = false;
                    }
                }
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
						rNode.getURL().execute("RicartArgawalaAux.okReceived", @params);
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
						node.getURL().execute("RicartArgawalaAux.requestReceived", @params);
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
