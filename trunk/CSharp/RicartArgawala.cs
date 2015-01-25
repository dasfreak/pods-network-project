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
                Random randomGenerator = new Random();
                timestamp = randomGenerator.Next(50);
                Console.WriteLine("timestamp init to: " + timestamp);

                requestsQueue = new HashSet<string>();
                okayList = new HashSet<string>();
                isPending = false;

                instance = this;
            }

            public virtual void requestReceived(string ip, long timestamp)
            {
                Console.WriteLine("Received request from ip " + ip + " timestamp = " + timestamp + " mystamp = " + this.timestamp);
                if (CalcDone && !Pending)
                {
                   // Console.WriteLine("CalcDone && !Pending");
                    // send OK
                    sendOk(ip);
                }
                else if (!this.isCalcDone)
                {
                    //Console.WriteLine("isCalcDone");
                    requestsQueue.Add(ip);
                }
                else if (this.isPending)
                {
                    // queue request
                    //Console.WriteLine("isPending ");
                    
                    if ((timestamp < this.timestamp)||
                        ( (timestamp == this.timestamp) && (ip.CompareTo(this.ip) > 0) )   )
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
            Console.WriteLine("Sending okay to node " + ip + " timestamp = " + timestamp );
            
            NetworkClientInterface executer = XmlRpcProxyGen.Create<NetworkClientInterface>();
            executer.AttachLogger(new XmlRpcDebugLogger());

            executer.Url = generate_url(ip);
			executer.okReceived(this.ip);
			
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
            {//192.168.0.33
                Console.WriteLine("Thread  RicartArgwala is running");
                while (!getcanStart()) ;

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
                        while ((okayList.Count < (network.Count - 1))&&(_keepRunning)); // -1 because of self node
                        Console.WriteLine("====> CS enter");
                        Access = true;
                        // can access now
                        while ((Pending) && (_keepRunning)) ;
                        while ((!CalcDone)&&(_keepRunning));

                        isPending = false;

                        // send okay to all processes in queue
                        sendOkayToQueueNodes();

                        Access = false;

                        Console.WriteLine("<==== CS exit");
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
                Console.WriteLine("Sending okay to queue node " + node_ip);

                NetworkClientInterface executer = XmlRpcProxyGen.Create<NetworkClientInterface>();
                executer.AttachLogger(new XmlRpcDebugLogger());

                executer.Url = generate_url(node_ip);
                executer.okReceived(this.ip);
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
