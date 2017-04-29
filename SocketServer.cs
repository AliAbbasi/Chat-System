using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;

namespace DefaultNamespace
{
	/// <summary>
	/// Description of SocketServer.	
	/// </summary>
	public class SocketServer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RichTextBox richTextBoxReceivedMsg;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxMsg;
		private System.Windows.Forms.Button buttonStopListen;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RichTextBox richTextBoxSendMsg;
		private System.Windows.Forms.TextBox textBoxIP;
		private System.Windows.Forms.Button buttonStartListen;
		private System.Windows.Forms.Button buttonSendMsg;
		private System.Windows.Forms.Button buttonClose;
		
		public delegate void UpdateRichEditCallback(string text);
		public delegate void UpdateClientListCallback();
				
		public AsyncCallback pfnWorkerCallBack ;
		private  Socket m_mainSocket;

		
		private System.Collections.ArrayList m_workerSocketList = 
				ArrayList.Synchronized(new System.Collections.ArrayList());

		
		private int m_clientCount = 0;
		public string Source_Client = null;
		public string Destination_Client = null;
		public int int_Source_Client ;
		public int int_Destination_Client ;
		byte[] byData = null;
		string mount_of_client = null;
		int allow_sendMessage = 0;


		private System.Windows.Forms.ListBox listBoxClientList;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnClear;

		public SocketServer()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			string hn = Dns.GetHostName();
			IPHostEntry ihe = Dns.GetHostByName(hn);
			IPAddress ia = ihe.AddressList[0];
			textBoxIP.Text = ia.ToString();
		}
            
		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new SocketServer());
        }
        
        #region Windows Forms Designer generated code
        /// <summary>
	/// This method is required for Windows Forms designer support.
	/// Do not change the method contents inside the source code editor. The Forms designer might
	/// not be able to load this method if it was changed manually.
	/// </summary>
	private void InitializeComponent() 
	{
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSendMsg = new System.Windows.Forms.Button();
            this.buttonStartListen = new System.Windows.Forms.Button();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.richTextBoxSendMsg = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonStopListen = new System.Windows.Forms.Button();
            this.textBoxMsg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.richTextBoxReceivedMsg = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxClientList = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.buttonClose.BackColor = System.Drawing.Color.Red;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.buttonClose.ForeColor = System.Drawing.Color.Yellow;
            this.buttonClose.Location = new System.Drawing.Point(322, 353);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(88, 24);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
            this.buttonSendMsg.BackColor = System.Drawing.SystemColors.HotTrack;
            this.buttonSendMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.buttonSendMsg.ForeColor = System.Drawing.Color.Yellow;
            this.buttonSendMsg.Location = new System.Drawing.Point(17, 169);
            this.buttonSendMsg.Name = "buttonSendMsg";
            this.buttonSendMsg.Size = new System.Drawing.Size(192, 24);
            this.buttonSendMsg.TabIndex = 7;
            this.buttonSendMsg.Text = "Send Message";
            this.buttonSendMsg.UseVisualStyleBackColor = false;
            this.buttonSendMsg.Click += new System.EventHandler(this.ButtonSendMsgClick);
            this.buttonStartListen.BackColor = System.Drawing.Color.Blue;
            this.buttonStartListen.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartListen.ForeColor = System.Drawing.Color.Yellow;
            this.buttonStartListen.Location = new System.Drawing.Point(27, 21);
            this.buttonStartListen.Name = "buttonStartListen";
            this.buttonStartListen.Size = new System.Drawing.Size(159, 26);
            this.buttonStartListen.TabIndex = 4;
            this.buttonStartListen.Text = "Start Listening";
            this.buttonStartListen.UseVisualStyleBackColor = false;
            this.buttonStartListen.Click += new System.EventHandler(this.ButtonStartListenClick);
            this.textBoxIP.Location = new System.Drawing.Point(89, 62);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.ReadOnly = true;
            this.textBoxIP.Size = new System.Drawing.Size(120, 20);
            this.textBoxIP.TabIndex = 12;
            this.richTextBoxSendMsg.BackColor = System.Drawing.Color.White;
            this.richTextBoxSendMsg.Location = new System.Drawing.Point(17, 112);
            this.richTextBoxSendMsg.Name = "richTextBoxSendMsg";
            this.richTextBoxSendMsg.Size = new System.Drawing.Size(192, 57);
            this.richTextBoxSendMsg.TabIndex = 6;
            this.richTextBoxSendMsg.Text = "";
            this.richTextBoxSendMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBoxSendMsg_KeyDown);
            this.label1.Location = new System.Drawing.Point(230, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            this.buttonStopListen.BackColor = System.Drawing.Color.Red;
            this.buttonStopListen.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStopListen.ForeColor = System.Drawing.Color.Yellow;
            this.buttonStopListen.Location = new System.Drawing.Point(233, 21);
            this.buttonStopListen.Name = "buttonStopListen";
            this.buttonStopListen.Size = new System.Drawing.Size(156, 26);
            this.buttonStopListen.TabIndex = 5;
            this.buttonStopListen.Text = "Stop Listening";
            this.buttonStopListen.UseVisualStyleBackColor = false;
            this.buttonStopListen.Click += new System.EventHandler(this.ButtonStopListenClick);
            this.textBoxMsg.BackColor = System.Drawing.Color.SkyBlue;
            this.textBoxMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMsg.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.textBoxMsg.Location = new System.Drawing.Point(113, 365);
            this.textBoxMsg.Name = "textBoxMsg";
            this.textBoxMsg.ReadOnly = true;
            this.textBoxMsg.Size = new System.Drawing.Size(192, 13);
            this.textBoxMsg.TabIndex = 14;
            this.textBoxMsg.Text = "None";
            this.label4.Location = new System.Drawing.Point(17, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Broadcast Message To Clients";
            this.label5.Location = new System.Drawing.Point(218, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(192, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Message Received From Clients";
            this.textBoxPort.Location = new System.Drawing.Point(269, 63);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(120, 20);
            this.textBoxPort.TabIndex = 0;
            this.textBoxPort.Text = "10000";
            this.textBoxPort.TextChanged += new System.EventHandler(this.textBoxPort_TextChanged);
            this.richTextBoxReceivedMsg.BackColor = System.Drawing.Color.White;
            this.richTextBoxReceivedMsg.Location = new System.Drawing.Point(218, 113);
            this.richTextBoxReceivedMsg.Name = "richTextBoxReceivedMsg";
            this.richTextBoxReceivedMsg.ReadOnly = true;
            this.richTextBoxReceivedMsg.Size = new System.Drawing.Size(192, 232);
            this.richTextBoxReceivedMsg.TabIndex = 9;
            this.richTextBoxReceivedMsg.Text = "";
            this.label2.Location = new System.Drawing.Point(27, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server IP";
            this.label3.Location = new System.Drawing.Point(1, 363);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Status Message:";
            this.listBoxClientList.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxClientList.Location = new System.Drawing.Point(17, 224);
            this.listBoxClientList.Name = "listBoxClientList";
            this.listBoxClientList.Size = new System.Drawing.Size(192, 121);
            this.listBoxClientList.TabIndex = 15;
            this.label6.Location = new System.Drawing.Point(17, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(184, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Connected Clients";
            this.btnClear.BackColor = System.Drawing.Color.Red;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnClear.ForeColor = System.Drawing.Color.Yellow;
            this.btnClear.Location = new System.Drawing.Point(233, 353);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 24);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(423, 392);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listBoxClientList);
            this.Controls.Add(this.textBoxMsg);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.richTextBoxReceivedMsg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonSendMsg);
            this.Controls.Add(this.richTextBoxSendMsg);
            this.Controls.Add(this.buttonStopListen);
            this.Controls.Add(this.buttonStartListen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SocketServer";
            this.Text = "SocketServer";
            this.Load += new System.EventHandler(this.SocketServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

	}
	#endregion
	void ButtonStartListenClick(object sender, System.EventArgs e)
	{
		try
		{
			if(textBoxPort.Text == "")
			{
				MessageBox.Show("Please enter a Port Number");
				return;
			}
			string portStr = textBoxPort.Text;
			int port = System.Convert.ToInt32(portStr);

			m_mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPEndPoint ipLocal = new IPEndPoint (IPAddress.Any, port);
			m_mainSocket.Bind( ipLocal );
			m_mainSocket.Listen(9);
			m_mainSocket.BeginAccept(new AsyncCallback (OnClientConnect), null);

			UpdateControls(true);

		}
		catch(SocketException se)
		{
			MessageBox.Show ( se.Message );
		}

	}
	private void UpdateControls( bool listening ) 
	{
		buttonStartListen.Enabled 	= !listening;
		buttonStopListen.Enabled 	= listening;
	}	

	public void OnClientConnect(IAsyncResult asyn)
	{
		try
		{
			Socket workerSocket = m_mainSocket.EndAccept (asyn);
			Interlocked.Increment(ref m_clientCount);
			m_workerSocketList.Add(workerSocket);
			string msg = "Welcome client " + m_clientCount + "\n";
			SendMsgToClient(msg, m_clientCount);
			UpdateClientListControl();
			WaitForData(workerSocket, m_clientCount);
			m_mainSocket.BeginAccept(new AsyncCallback ( OnClientConnect ),null);
		}
		catch(SocketException se)
		{
			MessageBox.Show ( se.Message );
		}
	}


	public class SocketPacket
	{

		public SocketPacket(System.Net.Sockets.Socket socket, int clientNumber)
		{
			m_currentSocket = socket;
			m_clientNumber  = clientNumber;
		}
		public System.Net.Sockets.Socket m_currentSocket;
		public int m_clientNumber;
		public byte[] dataBuffer = new byte[1024];
	}


	public void WaitForData(System.Net.Sockets.Socket soc, int clientNumber)
	{
		try
		{
			if  ( pfnWorkerCallBack == null )
			{		
				pfnWorkerCallBack = new AsyncCallback (OnDataReceived);
			}

			SocketPacket theSocPkt = new SocketPacket (soc, clientNumber);
			soc.BeginReceive(theSocPkt.dataBuffer, 0,  
			    theSocPkt.dataBuffer.Length,SocketFlags.None, 
			    pfnWorkerCallBack,theSocPkt);
            	}
		catch(SocketException se)
		{
			MessageBox.Show (se.Message );
		}
	}

	public  void OnDataReceived(IAsyncResult asyn)
	{
		SocketPacket socketData = null;
		socketData = (SocketPacket)asyn.AsyncState;
		try
		{

			int iRx = socketData.m_currentSocket.EndReceive(asyn);
			char[] chars = new char[iRx + 1];
			System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
			int charLen = d.GetChars(socketData.dataBuffer, 0, iRx, chars, 0);  
			System.String szData = new System.String(chars);


			int length_of_msg;
			length_of_msg = szData.Length;
			Destination_Client = szData.Substring(length_of_msg - 2, 1);
			string First_Word = szData.Substring(0, 6);

			if (First_Word != "%%^^@@")   //request the number of client from client
			{
			    if (Destination_Client != "s")
			    {
				int_Destination_Client = Convert.ToInt32(Destination_Client);

				for (int i = 0; i < listBoxClientList.Items.Count - 1; i++) //check exist cient 
				{
				    if (int_Destination_Client.ToString() == listBoxClientList.Items[i].ToString())
				    {

					allow_sendMessage = 1;

				    }
				    else 
				    {
					allow_sendMessage = 0;
				    }
				}
				if (allow_sendMessage == 1)
				{
				    byData = System.Text.Encoding.ASCII.GetBytes(szData);
				    Socket workerSocket = null;
				    workerSocket = (Socket)m_workerSocketList[int_Destination_Client - 1];
				    if (workerSocket != null)
				    {
					if (workerSocket.Connected)
					{
					    workerSocket.Send(byData);
					}
				    }
				}
			    }
			    if (Destination_Client == "s")
			    {

				string msg = "" + socketData.m_clientNumber + ":";
				AppendToRichEditControl(msg + szData);

				msg = msg + szData;
				msg = "$$##@@" + msg;   // message from server to all client with "$$##@@"
				msg = msg.Substring(0, msg.Length - 5);
				byte[] byData = System.Text.Encoding.ASCII.GetBytes(msg);
				Socket workerSocket = null;
				for (int i = 0; i < m_workerSocketList.Count; i++)
				{
				    workerSocket = (Socket)m_workerSocketList[i];
				    if (workerSocket != null)
				    {
					if (workerSocket.Connected)
					{
					    workerSocket.Send(byData);
					}
				    }
				}
			    }
			}
			if (First_Word == "%%^^@@")    //request the number of client from client
			{
			    string client_number = szData.Substring(8, 1);
			    int clientNumber = Convert.ToInt32(client_number);
			    string msg = "%%^^@@";
			    int i = listBoxClientList.Items.Count;
			    for (i--; i > -1; i--) 
			    {
				msg = msg + listBoxClientList.Items[i].ToString();
			    }
			    byte[] byData = System.Text.Encoding.ASCII.GetBytes(msg);
			    Socket workerSocket = null;
			    workerSocket = (Socket)m_workerSocketList[clientNumber - 1];

			    if (workerSocket != null)
			    {
				if (workerSocket.Connected)
				{
				    workerSocket.Send(byData);
				}
			    }

			}

					// Continue the waiting for data on the Socket
					WaitForData(socketData.m_currentSocket, socketData.m_clientNumber );

		}
		catch(SocketException se)
		{
			if(se.ErrorCode == 10054) // Error code for Connection reset by peer
			{	
				string msg = "Client " + socketData.m_clientNumber + " Disconnected" ;
				string msg_t = "$D$##@" + msg;
				msg = "S@#%" + msg;
				AppendToRichEditControl(msg);

				byte[] byData = System.Text.Encoding.ASCII.GetBytes(msg_t);
				Socket workerSocket = null;
				for (int i = 0; i < m_workerSocketList.Count; i++)
				{
					workerSocket = (Socket)m_workerSocketList[i];
					if (workerSocket != null)
					{
					    if (workerSocket.Connected)
					    {
						workerSocket.Send(byData);
					    }
					}
				} 
					m_workerSocketList[socketData.m_clientNumber - 1] = null;
					UpdateClientListControl();
			}
			else
			{
				MessageBox.Show (se.Message );
			}
		}
	}


	private void AppendToRichEditControl(string msg) 
	{
		if (InvokeRequired) 
		{
			object[] pList = {msg};
			richTextBoxReceivedMsg.BeginInvoke(new UpdateRichEditCallback(OnUpdateRichEdit), pList);
		}
		else
		{
			OnUpdateRichEdit(msg);
		}
	}

	private void OnUpdateRichEdit(string msg) 
	{
            string First_Word = msg.Substring(0,4);

            if (First_Word != "S@#%")
            {
                int length_of_msg = msg.Length;
                msg = msg.Substring(0, length_of_msg - 5);
                msg = "\n" + msg;
                richTextBoxReceivedMsg.AppendText(msg);
            }
            if (First_Word == "S@#%") 
            {

                msg = msg.Substring(4);
                msg = "\n" + msg;
                richTextBoxReceivedMsg.AppendText(msg);

            }
            
		}

		private void UpdateClientListControl() 
		{
			if (InvokeRequired) 
			{
				listBoxClientList.BeginInvoke(new UpdateClientListCallback(UpdateClientList), null);
			}
			else
			{
				UpdateClientList();
			}
		}

		void ButtonSendMsgClick(object sender, System.EventArgs e)
		{
            try
            {
                string msg = richTextBoxSendMsg.Text;
                msg = "$$##@@" + msg;
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(msg);
                Socket workerSocket = null;
                for (int i = 0; i < m_workerSocketList.Count; i++)
                {
                    workerSocket = (Socket)m_workerSocketList[i];
                    if (workerSocket != null)
                    {
                        if (workerSocket.Connected)
                        {
                            workerSocket.Send(byData);
                        }
                    }
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
            finally 
            {
                richTextBoxSendMsg.Text = "";
            }
	}
		
	void ButtonStopListenClick(object sender, System.EventArgs e)
	{ 
		CloseSockets();			
		UpdateControls(false);
	}
	

	void ButtonCloseClick(object sender, System.EventArgs e)
	{
		CloseSockets();
		Close();
	}

	void CloseSockets()
	{
		if(m_mainSocket != null)
		{
			m_mainSocket.Close();
		}
		Socket workerSocket = null;
		for(int i = 0; i < m_workerSocketList.Count; i++)
		{
			workerSocket = (Socket)m_workerSocketList[i];
			if(workerSocket != null)
			{
				workerSocket.Close();
				workerSocket = null;
			}
		}	
	}

	void UpdateClientList()
	{
		listBoxClientList.Items.Clear();
		for(int i = 0; i < m_workerSocketList.Count; i++)
		{
			string clientKey = Convert.ToString(i+1);
	mount_of_client = mount_of_client + clientKey;
			Socket workerSocket = (Socket)m_workerSocketList[i];
			if(workerSocket != null)
			{
					listBoxClientList.Items.Add(clientKey);
			}
		}
	}

	void SendMsgToClient(string msg, int clientNumber)
	{
		byte[] byData = System.Text.Encoding.ASCII.GetBytes(msg);
		Socket workerSocket = (Socket)m_workerSocketList[clientNumber - 1];
		workerSocket.Send(byData);
	}

	private void btnClear_Click(object sender, System.EventArgs e)
	{
		richTextBoxReceivedMsg.Clear();
	}

        private void SocketServer_Load(object sender, EventArgs e)
        {

        }

        private void textBoxPort_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBoxSendMsg_KeyDown(object sender, KeyEventArgs e)
        {
               
        }
	}
}
