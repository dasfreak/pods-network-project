using System;
using System.Collections.Generic;

namespace Networking
{

    public abstract class SyncAlgorithm
    {

        protected internal List<RemoteNode> network;
        protected internal string ip;
        protected internal volatile bool isCalcDone;
        protected internal volatile bool isPending;
        protected internal volatile bool canStart;

        protected internal static SyncAlgorithm instance = null;

        public SyncAlgorithm(IList<RemoteNode> network, string ip)
        {
            canStart = false;
            isPending = false;
            isCalcDone = true;
            // pay attention to the difference between network and this.network
            this.network = new List<RemoteNode>();
            this.network.AddRange(network);
            this.ip = ip;
        }

        public virtual bool CalcDone
        {
            get
            {
                lock (this)
                {
                    return isCalcDone;
                }
            }
        }

        public virtual void setCalcDone()
        {
            lock (this)
            {
                isCalcDone = true;
            }
        }

        public virtual void setCalcInProgress()
        {
            lock (this)
            {
                isCalcDone = false;
            }
        }

        public static SyncAlgorithm Instance
        {
            get
            {
                return instance;
            }
        }

        abstract public bool canAccess();


        public virtual void setPending()

        {
            lock (this)
            {
                //Console.WriteLine("Setting isPending");
                isPending = true;
            }
        }

        public virtual void setCanStart() 
        {
            lock (this)
            {
                
             canStart = true;
            }
        }

        public bool getcanStart()
        {
            return canStart;
        }


        public void clearPending()
        {
            isPending = false;
        }

    }

}