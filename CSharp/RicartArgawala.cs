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
            private volatile bool isSyncing;

            private long timestamp;
            bool _keepRunning = true;

            public RicartArgawala(IList<RemoteNode> network, string ip) : base(network, ip)
            {
                // pay attention to the difference between network and this.network
                Random randomGenerator = new Random();
                timestamp = randomGenerator.Next(50);
               // Console.WriteLine("timestamp init to: " + timestamp);

                requestsQueue = new HashSet<string>();
                okayList = new HashSet<string>();
                isPending = false;
                canAccess_Renamed = false;
                isSyncing = false;
                instance = this;
            }

            public virtual void requestReceived(string ip, long timestamp)
            {
                // Console.WriteLine("Received request from ip " + ip + " timestamp = " + timestamp + " mystamp = " + this.timestamp);
                bool sendOk_r = false;
                bool addToQueue = false;

                bool calcIsDone = CalcDone;
                bool isPending = Pending;

                if (calcIsDone && !isPending)
                    {
                        // Console.WriteLine("CalcDone && !Pending");
                        // send OK
                        sendOk_r = true;
                    }
                    else if (!calcIsDone)
                    {
                        addToQueue = true;
                    }
                    else if (isPending || _keepRunning)
                    {
                        if ((timestamp == this.timestamp && ip.CompareTo(this.ip) > 0) ||
                               timestamp < this.timestamp)
                        {
                            sendOk_r = true;
                        }
                        else
                        {
                            addToQueue = true;
                        }
                    }
                    if (sendOk_r)
                    {
                        sendOk(ip);
                    }
                    else
                    {
                        lock (this.requestsQueue)
                        {
                            requestsQueue.Add(ip);
                        }
                    }
                }
        

       private void sendOk(string ip)
  		{
            //Console.WriteLine("Sending okay to node " + ip + " timestamp = " + timestamp );
            
            NetworkClientInterface executer = XmlRpcProxyGen.Create<NetworkClientInterface>();
            executer.AttachLogger(new XmlRpcDebugLogger());

            executer.Url = generate_url(ip);

            foreach (RemoteNode node in this.network)
            {
                if (node.getIP().CompareTo(ip) == 0)
                {
                    executer.okReceived(this.ip);
                    break;
                }
            }

			
		}

            public virtual void okReceived(string ip)
            {
                lock (this)
                {
                   // Console.WriteLine("==>Received okay from " + ip);
                    okayList.Add(ip);
                    //Console.WriteLine("   okayList size is" + okayList.Count);
                }
            }

            public void RequestStop()
            {
                _keepRunning = false;
            }

            public void run()
            {//192.168.0.33

                bool isSyncDone = false;

                Console.WriteLine("Thread  RicartArgwala is running");
                while (!getcanStart()) ;

                while (_keepRunning)
                {
                    isSyncDone = false;
                    bool isPending;
			        lock ( this.mutualLock ) {
				        isPending = Pending;
			        }

                    if (isPending)
                    {
                        isSyncing = true;
                     //   Console.WriteLine("Pending request detected\n");
                        // request from all nodes
                        okayList.Clear();
                        broadcastRequest();
                       
                        // wait for okay from all                        
                        while ((okayList.Count < (network.Count - 1))&&(_keepRunning)); // -1 because of self node
                        isSyncDone = true; 
                        Access = true;
                        }

                    if (isSyncDone)
                    {
                        Console.WriteLine("====> CS ra enter");

                        // can access now
                        while (Pending);
                        while (!CalcDone);

                        Access = false;

                        // send okay to all processes in queue
                        lock (this.requestsQueue)
                        {
                            sendOkayToQueueNodes();
                        }

                        timestamp++;

                        Console.WriteLine("<==== CS exit");

                        isPending = false;
                        
                    }
                }
             Console.WriteLine("\nThread  RicartArgwala is terminated\n");
            }


            public String generate_url(String ip)
            {
                return "http://" + ip + ":" + 5000 + "/RPC2";
            }


       private void sendOkayToQueueNodes(){

			foreach (string node_ip in requestsQueue)
  			   {
                //Console.WriteLine("Sending okay to queue node " + node_ip);
                foreach(RemoteNode rNode in network){
                   if (node_ip.CompareTo(rNode.getIP()) == 0)
                   {
                       NetworkClientInterface executer = XmlRpcProxyGen.Create<NetworkClientInterface>();
                       executer.AttachLogger(new XmlRpcDebugLogger());

                       executer.Url = generate_url(node_ip);
                       executer.okReceived(this.ip);
                   }
                }
                }

		}

       private void broadcastRequest()
		{
			Console.WriteLine("Boradcasting request:");
			foreach (RemoteNode node in network)
            {
                
				if (node.getIP().CompareTo(this.ip) != 0)
                {
                    //Console.WriteLine("Boradcasting request to:" + node.getIP());
                    NetworkClientInterface executer = XmlRpcProxyGen.Create<NetworkClientInterface>();
                    executer.AttachLogger(new XmlRpcDebugLogger());

                    executer.Url = node.getURL();
                    executer.requestReceived(this.ip, Convert.ToString(this.timestamp));

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
