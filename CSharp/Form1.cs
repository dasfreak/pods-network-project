using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Windows.Forms;



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
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtStateNumber1;
    private System.Windows.Forms.TextBox txtStateNumber2;
    private Label SubtResult;
    private System.Windows.Forms.Button butGetStateNames;
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
    Client My_Client;
    //public Client My_Client;

    public Form1()
    {
      //
      // Required for Windows Form Designer support
      //

      
      InitializeComponent();
      My_Client = new Client();
        /*
      String strHostName = Dns.GetHostName();
      IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
      IPAddress[] addr = ipEntry.AddressList;

      for (int i = 0; i < addr.Length; i++)
      {
          strHostName = strHostName+"\n"+addr[i].ToString();
      }*/
        /*
      NetworkInterface card = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault();
      if (card == null)
          return null;
      GatewayIPAddressInformation address = card.GetIPProperties().GatewayAddresses.FirstOrDefault();
      if (address == null)
          return null;
      NetworkOutput.Text = strHostName;
        */
      my_ip_print.Text = "default(" + My_Client.getIP() + ")";
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
            this.txtStateNumber2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
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
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.ServerGroupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnectionStatus
            // 
            this.ConnectionStatus.Location = new System.Drawing.Point(105, 94);
            this.ConnectionStatus.Name = "ConnectionStatus";
            this.ConnectionStatus.Size = new System.Drawing.Size(136, 23);
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
            this.leave_button.Size = new System.Drawing.Size(82, 24);
            this.leave_button.TabIndex = 3;
            this.leave_button.Text = "Leave";
            this.leave_button.Click += new System.EventHandler(this.leave_button_Click_1);
            // 
            // Join_button
            // 
            this.Join_button.Location = new System.Drawing.Point(239, 22);
            this.Join_button.Name = "Join_button";
            this.Join_button.Size = new System.Drawing.Size(82, 24);
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
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 19);
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
            this.groupBox2.Controls.Add(this.txtStateNumber2);
            this.groupBox2.Controls.Add(this.label4);
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
            this.SubtResult.Location = new System.Drawing.Point(115, 100);
            this.SubtResult.Name = "SubtResult";
            this.SubtResult.Size = new System.Drawing.Size(61, 22);
            this.SubtResult.TabIndex = 3;
            this.SubtResult.Click += new System.EventHandler(this.SubtResult_Click);
            // 
            // txtStateNumber2
            // 
            this.txtStateNumber2.Location = new System.Drawing.Point(118, 67);
            this.txtStateNumber2.Name = "txtStateNumber2";
            this.txtStateNumber2.Size = new System.Drawing.Size(58, 22);
            this.txtStateNumber2.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "Value 2:";
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
            this.butGetStateNames.Location = new System.Drawing.Point(237, 30);
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
            this.enter.Size = new System.Drawing.Size(82, 24);
            this.enter.TabIndex = 2;
            this.enter.Text = "enter";
            this.enter.Click += new System.EventHandler(this.enter_Click);
            // 
            // my_ip_print
            // 
            this.my_ip_print.Location = new System.Drawing.Point(105, 75);
            this.my_ip_print.Name = "my_ip_print";
            this.my_ip_print.Size = new System.Drawing.Size(136, 23);
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
            this.label6.Size = new System.Drawing.Size(96, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "My IP:";
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
            this.NetworkOutput.Size = new System.Drawing.Size(229, 246);
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
            this.ServerData.Location = new System.Drawing.Point(25, 34);
            this.ServerData.Name = "ServerData";
            this.ServerData.Size = new System.Drawing.Size(278, 39);
            this.ServerData.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(887, 509);
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
         
         bool para = My_Client.joinNetwork(IpNumber.Text);
         
        
        if(para==true)
              ConnectionStatus.Text = "connected";
         else
              ConnectionStatus.Text = "Conection failed!";

        
      }
      catch (Exception ex)
      {
        HandleException(ex);
      }
      Cursor = Cursors.Default;
    }





    private void butGetStateNames_Click(object sender, System.EventArgs e)
    {

      NetworkClientInterface NetSubtract = (NetworkClientInterface)XmlRpcProxyGen.Create(typeof(NetworkClientInterface));
      NetSubtract.AttachLogger(new XmlRpcDebugLogger());
      NetSubtract.Url = "http://127.0.0.1:5000/RPC2";
      StateStructRequest request;
      int retstr;
      
      Cursor = Cursors.WaitCursor;
      try
      {
          request.value1 = Convert.ToInt32(txtStateNumber1.Text);
          request.value2 = Convert.ToInt32(txtStateNumber2.Text);

          retstr = NetSubtract.subtract(request.value1, request.value2);
       

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




    private void enter_Click(object sender, System.EventArgs e)
    {
        Cursor = Cursors.WaitCursor;

        try
        {
        my_ip_print.Text = my_ip.Text;
        My_Client.setIP(my_ip.Text);
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

    private void button1_Click(object sender, EventArgs e)
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
            NetworkOutput.Text = string.Empty;
            My_Client.setIP(my_ip.Text);
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

  }
}
