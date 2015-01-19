using System;
using CookComputing.XmlRpc;



public interface NetworkClientInterface : IXmlRpcProxy
{
  // ClientAux

  [XmlRpcMethod("ClientAux.newNodeInNetwork")]
    bool newNodeInNet(String ip);

  [XmlRpcMethod("ClientAux.nodeQuitNetwork")]
    bool nodeQuitNetwork(String ip);

  [XmlRpcMethod("ClientAux.startMessage")]
     bool startMessage(int value, int algoChoice);

  // Calculator

  [XmlRpcMethod("Calculator.subtract")]
    int subtract(int s1, int s2);

  [XmlRpcMethod("Calculator.add")]
     int add(int s1, int s2);

  [XmlRpcMethod("Calculator.divide")]
     int divide(int s1, int s2);

  [XmlRpcMethod("Calculator.multiply")]
      int multiply(int s1, int s2);

  //RicartArgawalaAux
  
  [XmlRpcMethod("RicartArgawalaAux.okReceived")]
    bool okReceived(String ip);

  [XmlRpcMethod("RicartArgawalaAux.requestReceived")]
    bool requestReceived(String ip, String timestamp);

  //TokenRingAux

  [XmlRpcMethod("TokenRingAux.tokenReceived")]
     bool tokenReceived(String ipCreator, String ipHolder);
}




public interface NetworkServerInterface 
{
    // ClientAux

    [XmlRpcMethod("ClientAux.newNodeInNetwork")]
       bool newNodeInNet(String ip);

    [XmlRpcMethod("ClientAux.nodeQuitNetwork")]
       bool nodeQuitNetwork(String ip);

    [XmlRpcMethod("ClientAux.startMessage")]
        bool startMessage(int value, int algoChoice);

    // Calculator

    [XmlRpcMethod("Calculator.subtract")]
        int subtract(int s1, int s2);

    [XmlRpcMethod("Calculator.add")]
       int add(int s1, int s2);

    [XmlRpcMethod("Calculator.divide")]
       int divide(int s1, int s2);

    [XmlRpcMethod("Calculator.multiply")]
       int multiply(int s1, int s2);

    //RicartArgawalaAux

    [XmlRpcMethod("RicartArgawalaAux.okReceived")]
      bool okReceived(String ip);

    [XmlRpcMethod("RicartArgawalaAux.requestReceived")]
      bool requestReceived(String ip, String timestamp);

    //TokenRingAux

    [XmlRpcMethod("TokenRingAux.tokenReceived")]
       bool tokenReceived(String ipCreator, String ipHolder);
}