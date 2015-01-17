using System;
using CookComputing.XmlRpc;

public struct StateStructRequest
{
  public int state1;  
  public int state2;
  public int state3;
}

public struct my_Node
{
  public int ip;  
  public string url;
}


[XmlRpcUrl("http://192.168.0.20:5000/RPC2")]
//[XmlRpcUrl("http://192.168.0.33:5000/RPC2")]
//[XmlRpcUrl("http://127.0.0.1:5000/RPC2")]


public interface IStateName : IXmlRpcProxy
{
  [XmlRpcMethod("ClientAux.newNodeInNetwork")]
    bool newNodeInNet(String ip);

  [XmlRpcMethod("Calculator.subtract")]
    int subtract(int s1, int s2);
}

