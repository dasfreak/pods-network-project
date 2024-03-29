﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Networking
{
    public class NetworkServices : MarshalByRefObject, NetworkServerInterface
    //public class Networking : XmlRpcService, NetworkInterface
    {


        public bool newNodeInNet(String ip)
        {
            Client.getInstance().addNode(ip);            
            return true;
        }

        public bool startMessage(int value, int algoChoice)
        {
            Client.getInstance().setStartValue(value, algoChoice);
            return true;
        }

        public bool nodeQuitNetwork(String ip)
        {       
             Client.getInstance().removeNodeFromStructure(ip);
           
            return true;
        }

        
        public bool addNodeToStructure(String ip){

            Client.getInstance().addNodeToStruct(ip);

            return true;

           }

        public  bool handshakeMessage(String ip)
        {
            try
            {
                while (!Client.getInstance().threadsAreRunning()) ;
                Form1.getInstance().setCalcEvent();
                    
            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            return true;
        }




        public int add(int i2)
        {
            int i1 = 0;
            try
            {
                i1 = Client.Instance.CurrentValue;
            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            Console.WriteLine("Calculating: " + i1 + " + " + i2);
            int result = i1 + i2;
            try
            {
                Client.Instance.storeNewResult(result);
            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            return result;
        }

        public int subtract(int i2)
        {
            int i1 = 0;

            try
            {
                i1 = Client.Instance.CurrentValue;
            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            Console.WriteLine("Calculating: " + i1 + " - " + i2);
            int result = i1 - i2;
            try
            {
                Client.Instance.storeNewResult(result);
            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }

            return result;
        }

        public int divide(int i2)
        {
            int i1 = 0;
            try
            {
                i1 = Client.Instance.CurrentValue;
            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }

            int result = i1;
            if (i2 != 0)
            {
                result = i1 / i2;
                Console.WriteLine("Calculating: " + i1 + " / " + i2);
                try
                {
                    Client.Instance.storeNewResult(result);
                }
                catch (Exception e)
                {
                    // TODO Auto-generated catch block
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }

            }

            return result;
        }

        public int multiply(int i2)
        {
            int i1 = 0;
            try
            {
                i1 = Client.Instance.CurrentValue;
            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }

            int result = i1 * i2;
            Console.WriteLine("Calculating: " + i1 + " * " + i2);
            try
            {
                Client.Instance.storeNewResult(result);
            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }

            return result;
        }

        //TokenRing
        public bool tokenReceived(String ipCreator, String ipHolder)
        {
            ((TokenRing)TokenRing.Instance).receivedToken(new Token(ipCreator, ipHolder));
            return true;
        }

        //RicartArgawala
        public bool okReceived(string ip)
        {
            ((RicartArgawala)(RicartArgawala.Instance)).okReceived(ip);
            return true;
        }

        public bool requestReceived(string ip, string timestamp)
        {
            Console.WriteLine("requestReceived in Aux");
            
                ((RicartArgawala)(RicartArgawala.Instance)).requestReceived(ip, long.Parse(timestamp));
            return true;
        }

    }
}
