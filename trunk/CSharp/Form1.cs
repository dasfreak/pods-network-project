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
    private static Form1 instance;
    private System.Windows.Forms.TextBox IpNumber;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button Join_button;
    private System.Windows.Forms.Label ConnectionStatus;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox2;
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
    private Button button1;
    private Button button2;
    private TextBox textBox_startvalue;
    private Label label5;
    private Label current_value;
    private Label label8;
    private Label Message;
    Client My_Client;

    public delegate void Recieved1(object sender, EventArgs e);  // cahnge Form1 Window connection
    public event Recieved1 _connectedEvent;

    public delegate void Recieved2(object sender, EventArgs e);  // cahnge Form1 Window connection
    public event Recieved2 _calcEvent;
    //public Client My_Client;

    public Form1()
    {
      //
      // Required for Windows Form Designer support
      //

      
      InitializeComponent();
      instance = this;
      My_Client = new Client();
      _connectedEvent += new Recieved1(this.Connected);
      _calcEvent += new Recieved2(this.Calc);
      //IpNumber.Text="192.168.0.33";
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
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_startvalue = new System.Windows.Forms.TextBox();
            this.Message = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.current_value = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
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
            this.ConnectionStatus.Location = new System.Drawing.Point(87, 81);
            this.ConnectionStatus.Name = "ConnectionStatus";
            this.ConnectionStatus.Size = new System.Drawing.Size(114, 20);
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
            this.groupBox1.Location = new System.Drawing.Point(27, 198);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 104);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Network Connection";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // leave_button
            // 
            this.leave_button.Location = new System.Drawing.Point(248, 45);
            this.leave_button.Name = "leave_button";
            this.leave_button.Size = new System.Drawing.Size(68, 21);
            this.leave_button.TabIndex = 3;
            this.leave_button.Text = "Leave";
            this.leave_button.Click += new System.EventHandler(this.leave_button_Click_1);
            // 
            // Join_button
            // 
            this.Join_button.Location = new System.Drawing.Point(248, 19);
            this.Join_button.Name = "Join_button";
            this.Join_button.Size = new System.Drawing.Size(68, 21);
            this.Join_button.TabIndex = 2;
            this.Join_button.Text = "Join";
            this.Join_button.Click += new System.EventHandler(this.Join_Network_Click);
            // 
            // IpNumber
            // 
            this.IpNumber.Location = new System.Drawing.Point(90, 22);
            this.IpNumber.Name = "IpNumber";
            this.IpNumber.Size = new System.Drawing.Size(86, 20);
            this.IpNumber.TabIndex = 0;
            this.IpNumber.TextChanged += new System.EventHandler(this.IpNumber_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Status:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Network Ip:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox_startvalue);
            this.groupBox2.Controls.Add(this.Message);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.current_value);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(27, 307);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(361, 127);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Calculation";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(45, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "StartValue:";
            // 
            // textBox_startvalue
            // 
            this.textBox_startvalue.Location = new System.Drawing.Point(125, 19);
            this.textBox_startvalue.Name = "textBox_startvalue";
            this.textBox_startvalue.Size = new System.Drawing.Size(49, 20);
            this.textBox_startvalue.TabIndex = 7;
            this.textBox_startvalue.TextChanged += new System.EventHandler(this.textBox_startvalue_TextChanged);
            // 
            // Message
            // 
            this.Message.Location = new System.Drawing.Point(63, 88);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(217, 28);
            this.Message.TabIndex = 10;
            this.Message.Click += new System.EventHandler(this.Message_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(180, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "CurrentValue:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(199, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 21);
            this.button2.TabIndex = 8;
            this.button2.Text = "Start RicartArgawala";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // current_value
            // 
            this.current_value.Location = new System.Drawing.Point(265, 22);
            this.current_value.Name = "current_value";
            this.current_value.Size = new System.Drawing.Size(51, 19);
            this.current_value.TabIndex = 7;
            this.current_value.Click += new System.EventHandler(this.current_value_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(58, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 21);
            this.button1.TabIndex = 7;
            this.button1.Text = "Start TokenRing";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.enter);
            this.groupBox3.Controls.Add(this.my_ip_print);
            this.groupBox3.Controls.Add(this.my_ip);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(27, 97);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(361, 95);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Enter IP";
            // 
            // enter
            // 
            this.enter.Location = new System.Drawing.Point(248, 19);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(68, 21);
            this.enter.TabIndex = 2;
            this.enter.Text = "enter";
            this.enter.Click += new System.EventHandler(this.enter_Click);
            // 
            // my_ip_print
            // 
            this.my_ip_print.Location = new System.Drawing.Point(87, 65);
            this.my_ip_print.Name = "my_ip_print";
            this.my_ip_print.Size = new System.Drawing.Size(114, 20);
            this.my_ip_print.TabIndex = 1;
            this.my_ip_print.Click += new System.EventHandler(this.my_ip_print_Click);
            // 
            // my_ip
            // 
            this.my_ip.Location = new System.Drawing.Point(90, 22);
            this.my_ip.Name = "my_ip";
            this.my_ip.Size = new System.Drawing.Size(86, 20);
            this.my_ip.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(17, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Autodetected: ";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(17, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "My IP:";
            // 
            // Show_button1
            // 
            this.Show_button1.Location = new System.Drawing.Point(450, 18);
            this.Show_button1.Name = "Show_button1";
            this.Show_button1.Size = new System.Drawing.Size(200, 21);
            this.Show_button1.TabIndex = 4;
            this.Show_button1.Text = "Show Network";
            this.Show_button1.Click += new System.EventHandler(this.Show_button1_Click);
            // 
            // NetworkOutput
            // 
            this.NetworkOutput.Location = new System.Drawing.Point(494, 50);
            this.NetworkOutput.Name = "NetworkOutput";
            this.NetworkOutput.Size = new System.Drawing.Size(191, 213);
            this.NetworkOutput.TabIndex = 5;
            this.NetworkOutput.Click += new System.EventHandler(this.NetworkOutput_Click);
            // 
            // ServerGroupBox4
            // 
            this.ServerGroupBox4.Controls.Add(this.ServerData);
            this.ServerGroupBox4.Location = new System.Drawing.Point(27, 10);
            this.ServerGroupBox4.Name = "ServerGroupBox4";
            this.ServerGroupBox4.Size = new System.Drawing.Size(361, 82);
            this.ServerGroupBox4.TabIndex = 6;
            this.ServerGroupBox4.TabStop = false;
            this.ServerGroupBox4.Text = "Server";
            this.ServerGroupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // ServerData
            // 
            this.ServerData.Location = new System.Drawing.Point(21, 29);
            this.ServerData.Name = "ServerData";
            this.ServerData.Size = new System.Drawing.Size(231, 34);
            this.ServerData.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(887, 542);
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
          if ((StringOK(IpNumber.Text)) && (!IpNumber.ReadOnly))
          {
              bool para = My_Client.joinNetwork(IpNumber.Text);


              if (para == true)
              {
                  ConnectionStatus.Text = "connected";
                  IpNumber.ReadOnly = true;
                  NetworkOutput.Text = string.Empty;
                  NetworkOutput.Text = My_Client.getTable();
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




      /*
    private void butGetStateNames_Click(object sender, System.EventArgs e)
    {
       
       
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
         
    }*/




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
              My_Client.CalculatingTask_Thread.Join();
              Message.Text = "";
          }
          else
              Message.Text = "           Error: Enter StartValue!";

          Message.Text = "            Calculation Result: " + Convert.ToString(My_Client.CurrentValue);
      }

      private void button2_Click(object sender, EventArgs e)
      {

          if (StringOK(textBox_startvalue.Text)){
       
              current_value.Text = textBox_startvalue.Text;
              Message.Text = "RicartArgawala is working for 20 Second\n              See Console!";
              My_Client.startCalc(Convert.ToInt32(textBox_startvalue.Text), 0);

              My_Client.CalculatingTask_Thread.Join();
              Message.Text = "";
          }
          else
              Message.Text = "           Error: Enter StartValue!";

          
          Message.Text = "            Calculation Result: "+Convert.ToString(My_Client.CurrentValue);
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

      private void button3_Click(object sender, EventArgs e)
      {
          DateTime saveUtcNow = DateTime.UtcNow;
          Console.WriteLine("Zeit" + saveUtcNow);

      }




      delegate void StringParameterDelegate(string value);

      void UpdateForm(string value)
      {
          if (InvokeRequired)
          {
              // We're not in the UI thread, so we need to call BeginInvoke
              BeginInvoke(new StringParameterDelegate(UpdateForm), new object[] { value });
              return;
          }
          // Must be on the UI thread if we've got this far
          ConnectionStatus.Text = value;
          
          NetworkOutput.Text = string.Empty;
          NetworkOutput.Text = My_Client.getTable();
          IpNumber.ReadOnly = true;
      }
   
      private void Connected(object sender, EventArgs e)
      {
          UpdateForm("connected");       
      }



      void UpdateForm2(string value)
      {
          if (InvokeRequired)
          {
              // We're not in the UI thread, so we need to call BeginInvoke
              BeginInvoke(new StringParameterDelegate(UpdateForm2), new object[] { value });
              return;
          }
          // Must be on the UI thread if we've got this far
          
          Message.Text = value;
          current_value.Text = Convert.ToString(My_Client.CurrentValue);
          My_Client.CalculatingTask_Thread.Join();
          Message.Text = "            Calculation Result: " + Convert.ToString(My_Client.CurrentValue);
      }
   
      private void Calc(object sender, EventArgs e)
      {
          UpdateForm2("Calculation is working for 20 Second\n              See Console!");       
          

      }

      public void setCalcEvent()
      {

          _calcEvent(this, new EventArgs());
      }

      public void setConnectedEvent()
      {

          _connectedEvent(this, new EventArgs());
      }

  public static Form1 getInstance(){
    return instance;
   }      

  }
}
