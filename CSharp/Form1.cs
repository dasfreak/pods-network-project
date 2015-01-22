using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Windows.Forms;
using System.Collections.Generic;



using CookComputing.XmlRpc;

namespace Networking
{
  /// <summary>
  /// Summary description for Form1.
  /// </summary>
  /// 
   
  public class Form1 : System.Windows.Forms.Form
  {
    private System.Windows.Forms.TextBox IpNumber;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button Join_button;
    private System.Windows.Forms.Label ConnectionStatus;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox2;
    private Label SubtResult;
    private Button leave_button;
    private GroupBox groupBox3;
    private Button enter;
    private Label my_ip_print;
    private TextBox my_ip;
    private Label label6;
    private Label label7;
    private Button Show_button1;
    private Label NetworkOutput;
    private GroupBox ServerGroupBox4;
    private Label ServerData;
    private TextBox txtStateNumber1;
    private Label label3;
    private Button butGetStateNames;
    private Button button1;
    private Button button2;
    private TextBox textBox_startvalue;
    private Label label5;
    private Label current_value;
    private Label label8;
    private Label Message;
    Client My_Client;
    //public Client My_Client;

    public Form1()
    {
      //
      // Required for Windows Form Designer support
      //

      
      InitializeComponent();
      My_Client = new Client();

      my_ip_print.Text = My_Client.getIP();
      ServerData.Text = "Port: 5000";
      Server My_server=new Server();
      My_server.startServer();

      //
      // TODO: Add any constructor code after InitializeComponent call
      //
    }

   
    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
      }
      base.Dispose( disposing );
    }

		#region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.ConnectionStatus = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.leave_button = new System.Windows.Forms.Button();
            this.Join_button = new System.Windows.Forms.Button();
            this.IpNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SubtResult = new System.Windows.Forms.Label();
            this.txtStateNumber1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.butGetStateNames = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.enter = new System.Windows.Forms.Button();
            this.my_ip_print = new System.Windows.Forms.Label();
            this.my_ip = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Show_button1 = new System.Windows.Forms.Button();
            this.NetworkOutput = new System.Windows.Forms.Label();
            this.ServerGroupBox4 = new System.Windows.Forms.GroupBox();
            this.ServerData = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox_startvalue = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.current_value = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Message = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.ServerGroupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnectionStatus
            // 
            this.ConnectionStatus.Location = new System.Drawing.Point(104, 93);
            this.ConnectionStatus.Name = "ConnectionStatus";
            this.ConnectionStatus.Size = new System.Drawing.Size(137, 24);
            this.ConnectionStatus.TabIndex = 1;
            this.ConnectionStatus.Text = "disconnected";
            this.ConnectionStatus.Click += new System.EventHandler(this.labStateName_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.leave_button);
            this.groupBox1.Controls.Add(this.Join_button);
            this.groupBox1.Controls.Add(this.ConnectionStatus);
            this.groupBox1.Controls.Add(this.IpNumber);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(32, 228);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 120);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Network Connection";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // leave_button
            // 
            this.leave_button.Location = new System.Drawing.Point(239, 52);
            this.leave_button.Name = "leave_button";
            this.leave_button.Size = new System.Drawing.Size(81, 24);
            this.leave_button.TabIndex = 3;
            this.leave_button.Text = "Leave";
            this.leave_button.Click += new System.EventHandler(this.leave_button_Click_1);
            // 
            // Join_button
            // 
            this.Join_button.Location = new System.Drawing.Point(239, 22);
            this.Join_button.Name = "Join_button";
            this.Join_button.Size = new System.Drawing.Size(81, 24);
            this.Join_button.TabIndex = 2;
            this.Join_button.Text = "Join";
            this.Join_button.Click += new System.EventHandler(this.Join_Network_Click);
            // 
            // IpNumber
            // 
            this.IpNumber.Location = new System.Drawing.Point(108, 25);
            this.IpNumber.Name = "IpNumber";
            this.IpNumber.Size = new System.Drawing.Size(103, 22);
            this.IpNumber.TabIndex = 0;
            this.IpNumber.TextChanged += new System.EventHandler(this.IpNumber_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Status:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Network Ip:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SubtResult);
            this.groupBox2.Controls.Add(this.txtStateNumber1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.butGetStateNames);
            this.groupBox2.Location = new System.Drawing.Point(32, 354);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(342, 125);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Calculation";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // SubtResult
            // 
            this.SubtResult.Location = new System.Drawing.Point(115, 69);
            this.SubtResult.Name = "SubtResult";
            this.SubtResult.Size = new System.Drawing.Size(61, 22);
            this.SubtResult.TabIndex = 3;
            this.SubtResult.Click += new System.EventHandler(this.SubtResult_Click);
            // 
            // txtStateNumber1
            // 
            this.txtStateNumber1.Location = new System.Drawing.Point(118, 33);
            this.txtStateNumber1.Name = "txtStateNumber1";
            this.txtStateNumber1.Size = new System.Drawing.Size(58, 22);
            this.txtStateNumber1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "Value 1:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // butGetStateNames
            // 
            this.butGetStateNames.Location = new System.Drawing.Point(236, 30);
            this.butGetStateNames.Name = "butGetStateNames";
            this.butGetStateNames.Size = new System.Drawing.Size(84, 24);
            this.butGetStateNames.TabIndex = 6;
            this.butGetStateNames.Text = " Subtract ";
            this.butGetStateNames.Click += new System.EventHandler(this.butGetStateNames_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.enter);
            this.groupBox3.Controls.Add(this.my_ip_print);
            this.groupBox3.Controls.Add(this.my_ip);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(32, 112);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(342, 110);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Enter IP";
            // 
            // enter
            // 
            this.enter.Location = new System.Drawing.Point(239, 21);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(81, 24);
            this.enter.TabIndex = 2;
            this.enter.Text = "enter";
            this.enter.Click += new System.EventHandler(this.enter_Click);
            // 
            // my_ip_print
            // 
            this.my_ip_print.Location = new System.Drawing.Point(104, 75);
            this.my_ip_print.Name = "my_ip_print";
            this.my_ip_print.Size = new System.Drawing.Size(137, 23);
            this.my_ip_print.TabIndex = 1;
            this.my_ip_print.Click += new System.EventHandler(this.my_ip_print_Click);
            // 
            // my_ip
            // 
            this.my_ip.Location = new System.Drawing.Point(108, 25);
            this.my_ip.Name = "my_ip";
            this.my_ip.Size = new System.Drawing.Size(103, 22);
            this.my_ip.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(20, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "Autodetected: ";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(20, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 18);
            this.label7.TabIndex = 0;
            this.label7.Text = "My IP:";
            // 
            // Show_button1
            // 
            this.Show_button1.Location = new System.Drawing.Point(540, 21);
            this.Show_button1.Name = "Show_button1";
            this.Show_button1.Size = new System.Drawing.Size(240, 24);
            this.Show_button1.TabIndex = 4;
            this.Show_button1.Text = "Show Network";
            this.Show_button1.Click += new System.EventHandler(this.Show_button1_Click);
            // 
            // NetworkOutput
            // 
            this.NetworkOutput.Location = new System.Drawing.Point(593, 58);
            this.NetworkOutput.Name = "NetworkOutput";
            this.NetworkOutput.Size = new System.Drawing.Size(229, 245);
            this.NetworkOutput.TabIndex = 5;
            this.NetworkOutput.Click += new System.EventHandler(this.NetworkOutput_Click);
            // 
            // ServerGroupBox4
            // 
            this.ServerGroupBox4.Controls.Add(this.ServerData);
            this.ServerGroupBox4.Location = new System.Drawing.Point(32, 12);
            this.ServerGroupBox4.Name = "ServerGroupBox4";
            this.ServerGroupBox4.Size = new System.Drawing.Size(342, 94);
            this.ServerGroupBox4.TabIndex = 6;
            this.ServerGroupBox4.TabStop = false;
            this.ServerGroupBox4.Text = "Server";
            this.ServerGroupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // ServerData
            // 
            this.ServerData.Location = new System.Drawing.Point(25, 33);
            this.ServerData.Name = "ServerData";
            this.ServerData.Size = new System.Drawing.Size(277, 40);
            this.ServerData.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(436, 384);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 24);
            this.button1.TabIndex = 7;
            this.button1.Text = "Start TokenRing";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(596, 384);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 24);
            this.button2.TabIndex = 8;
            this.button2.Text = "Start RicartArgawala";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox_startvalue
            // 
            this.textBox_startvalue.Location = new System.Drawing.Point(540, 350);
            this.textBox_startvalue.Name = "textBox_startvalue";
            this.textBox_startvalue.Size = new System.Drawing.Size(59, 22);
            this.textBox_startvalue.TabIndex = 7;
            this.textBox_startvalue.TextChanged += new System.EventHandler(this.textBox_startvalue_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(432, 354);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 19);
            this.label5.TabIndex = 7;
            this.label5.Text = "StartValue:";
            // 
            // current_value
            // 
            this.current_value.Location = new System.Drawing.Point(719, 351);
            this.current_value.Name = "current_value";
            this.current_value.Size = new System.Drawing.Size(61, 22);
            this.current_value.TabIndex = 7;
            this.current_value.Click += new System.EventHandler(this.current_value_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(606, 354);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 19);
            this.label8.TabIndex = 9;
            this.label8.Text = "CurrentValue:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // Message
            // 
            this.Message.Location = new System.Drawing.Point(476, 423);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(260, 56);
            this.Message.TabIndex = 10;
            this.Message.Click += new System.EventHandler(this.Message_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(887, 509);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.current_value);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_startvalue);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ServerGroupBox4);
            this.Controls.Add(this.NetworkOutput);
            this.Controls.Add(this.Show_button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Networking";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ServerGroupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }
		#endregion

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    /// 
   

    [STAThread]
    static void Main() 
    {
        
      Application.Run(new Form1());
    }



    


    private void Join_Network_Click(object sender, System.EventArgs e)
    {


      Cursor = Cursors.WaitCursor;
      //ConnectionStatus.Text = "connecting to " + IpNumber.Text;

      try
      {
          if ((StringOK(IpNumber.Text)) && (!IpNumber.ReadOnly))
          {
              bool para = My_Client.joinNetwork(IpNumber.Text);


              if (para == true)
              {
                  ConnectionStatus.Text = "connected";
                  IpNumber.ReadOnly = true;
              }
              else
                  ConnectionStatus.Text = "Conection failed!";

          }
      }
      catch (Exception ex)
      {
        HandleException(ex);
      }
      Cursor = Cursors.Default;
    }





    private void butGetStateNames_Click(object sender, System.EventArgs e)
    {
        /*List<RemoteNode> net = new List<RemoteNode>();
        net.Add(new RemoteNode("A\n"," "));
        net.Add(new RemoteNode("B\n", " "));
        net.Add(new RemoteNode("C\n", " "));
        net.Add(new RemoteNode("D\n", " "));
        net.Add(new RemoteNode("E\n", " "));
        String Out = "S\n";
        net.RemoveAt(1);
        net.Add(new RemoteNode("L\n", " "));
        net.RemoveAt(3);
       // net.Add(new RemoteNode("M\n", " "));
        //foreach (String node in net)getIP()
        for (int i = 0;i< net.Count ; i++)
        {
            Out = Out + " " + net[i].getIP();
        }
        NetworkOutput.Text = Convert.ToString(net.Count);*/
        
      NetworkClientInterface NetSubtract = (NetworkClientInterface)XmlRpcProxyGen.Create(typeof(NetworkClientInterface));
      NetSubtract.AttachLogger(new XmlRpcDebugLogger());
      NetSubtract.Url = "http://" + My_Client.getIP() + ":5000/RPC2";
      StateStructRequest request;
      int retstr;
      
      Cursor = Cursors.WaitCursor;
      try
      {
          request.value1 = Convert.ToInt32(txtStateNumber1.Text);
          

          retstr = NetSubtract.subtract(request.value1);
       

        String name=Convert.ToString(retstr);
        SubtResult.Text = name;

      }
      catch (Exception ex)
      {
        HandleException(ex);
      }
      Cursor = Cursors.Default;
         
    }




    private void HandleException(Exception ex)
    {
      String msgBoxTitle = "Error";
      try
      {
        throw ex;
      }
      catch(XmlRpcFaultException fex)
      {
        MessageBox.Show("Fault Response: " + fex.FaultCode + " " 
          + fex.FaultString, msgBoxTitle,
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      catch(WebException webEx)
      {
        MessageBox.Show("WebException: " + webEx.Message, msgBoxTitle,
          MessageBoxButtons.OK, MessageBoxIcon.Error);
        if (webEx.Response != null)
          webEx.Response.Close();
      }
      catch(Exception excep)
      {
        MessageBox.Show(excep.Message, msgBoxTitle,
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

 private bool StringOK(String s){

     if (s != null && s != String.Empty)
         return true;
     return false;    
    }

    private void enter_Click(object sender, System.EventArgs e)
    {
        Cursor = Cursors.WaitCursor;

        try
        {
            if(StringOK(my_ip.Text))
            {
                my_ip_print.Text = my_ip.Text;
                My_Client.setIP(my_ip.Text);
            }
       }
      catch (Exception ex)
      {
        HandleException(ex);
      }
      Cursor = Cursors.Default;
    }

      
    private void groupBox1_Enter(object sender, EventArgs e)
    {
     
    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void labStateName_Click(object sender, EventArgs e)
    {

    }

    private void labStateNames1_Click(object sender, EventArgs e)
    {

    }

    private void Form1_Load(object sender, EventArgs e)
    {
        
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void label3_Click(object sender, EventArgs e)
    {
        
    }

    private void groupBox2_Enter(object sender, EventArgs e)
    {

    }

    private void groupBox3_Enter(object sender, EventArgs e)
    {
        my_ip_print.Text = my_ip.Text;
        
    }




    private void SubtResult_Click(object sender, EventArgs e)
    {

    }

    private void leave_button_Click_1(object sender, EventArgs e)
    {
        Cursor = Cursors.WaitCursor;

        try
        {
            My_Client.exitNetwork();
            ConnectionStatus.Text = "disconnected";
            IpNumber.ReadOnly = false;
            NetworkOutput.Text = string.Empty;
            //My_Client.setIP(my_ip.Text);
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
        Cursor = Cursors.Default;

    }

    private void my_ip_print_Click(object sender, EventArgs e)
    {

    }

    private void Show_button1_Click(object sender, EventArgs e)
    {
        //String Table = My_Client.getTable();

        NetworkOutput.Text = string.Empty;
        NetworkOutput.Text = My_Client.getTable();
  
   }



      private void NetworkOutput_Click(object sender, EventArgs e)
      {
      
      }

   

      private void groupBox4_Enter(object sender, EventArgs e)
      {

      }

      private void label6_Click(object sender, EventArgs e)
      {

      }

      private void button1_Click(object sender, EventArgs e)
      {
          if (StringOK(textBox_startvalue.Text))
          {
              current_value.Text = textBox_startvalue.Text;
              Message.Text = "TokenRing is working for 20 Second\n              See Console!";
              My_Client.startCalc(Convert.ToInt32(textBox_startvalue.Text), 1);
              
              Message.Text = "";
          }
          else
              Message.Text = "           Error: Enter StartValue!";
      }

      private void button2_Click(object sender, EventArgs e)
      {

          if (StringOK(textBox_startvalue.Text)){
              //My_Client.EndRicartArgawalaThread();
              current_value.Text = textBox_startvalue.Text;
              Message.Text = "RicartArgawala is working for 20 Second\n              See Console!";
              My_Client.startCalc(Convert.ToInt32(textBox_startvalue.Text), 0);
              Message.Text = "";
          }
          else
              Message.Text = "           Error: Enter StartValue!";

      }

      private void textBox_startvalue_TextChanged(object sender, EventArgs e)
      {

      }

      private void label8_Click(object sender, EventArgs e)
      {

      }

      private void current_value_Click(object sender, EventArgs e)
      {

      }

      private void IpNumber_TextChanged(object sender, EventArgs e)
      {

      }

      private void Message_Click(object sender, EventArgs e)
      {

      }

      

      
      

      

  }
}
