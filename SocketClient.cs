using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace DefaultNamespace
{
	/// <summary>
	/// Description of SocketClient.	
	/// </summary>
	public class SocketClient : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonDisconnect;
		private System.Windows.Forms.TextBox textBoxIP;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.RichTextBox richTextRxMessage;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxConnectStatus;
		private System.Windows.Forms.RichTextBox richTextTxMessage;
		private System.Windows.Forms.Button buttonSendMessage;
		private System.Windows.Forms.Button buttonClose;
		
		byte[] m_dataBuffer = new byte [10];
		IAsyncResult m_result;
		public AsyncCallback m_pfnCallBack ;
		private System.Windows.Forms.Button btnClear;
        	private TextBox TxtBoxMsgTo;
		public Socket m_clientSocket;
             	public string Client_number = null;
             	public byte[] byData = null;
             	private Label label6;
             	private Button Request;
             	private Label label7;
             	private TextBox TxtBoxMyNumber;
             	int check_First_Msg = 0;

		public SocketClient()
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
			Application.Run(new SocketClient());
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
		this.buttonClose = new System.Windows.Forms.Button();
		this.buttonSendMessage = new System.Windows.Forms.Button();
		this.richTextTxMessage = new System.Windows.Forms.RichTextBox();
		this.textBoxConnectStatus = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.richTextRxMessage = new System.Windows.Forms.RichTextBox();
		this.textBoxPort = new System.Windows.Forms.TextBox();
		this.buttonConnect = new System.Windows.Forms.Button();
		this.label5 = new System.Windows.Forms.Label();
		this.textBoxIP = new System.Windows.Forms.TextBox();
		this.buttonDisconnect = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.btnClear = new System.Windows.Forms.Button();
		this.TxtBoxMsgTo = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.Request = new System.Windows.Forms.Button();
		this.label7 = new System.Windows.Forms.Label();
		this.TxtBoxMyNumber = new System.Windows.Forms.TextBox();
		this.SuspendLayout();
		// 
		// buttonClose
		// 
		this.buttonClose.BackColor = System.Drawing.Color.Red;
		this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
		this.buttonClose.ForeColor = System.Drawing.Color.Yellow;
		this.buttonClose.Location = new System.Drawing.Point(450, 279);
		this.buttonClose.Name = "buttonClose";
		this.buttonClose.Size = new System.Drawing.Size(64, 24);
		this.buttonClose.TabIndex = 11;
		this.buttonClose.Text = "Close";
		this.buttonClose.UseVisualStyleBackColor = false;
		this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
		// 
		// buttonSendMessage
		// 
		this.buttonSendMessage.BackColor = System.Drawing.SystemColors.HotTrack;
		this.buttonSendMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
		this.buttonSendMessage.ForeColor = System.Drawing.Color.Yellow;
		this.buttonSendMessage.Location = new System.Drawing.Point(18, 247);
		this.buttonSendMessage.Name = "buttonSendMessage";
		this.buttonSendMessage.Size = new System.Drawing.Size(240, 24);
		this.buttonSendMessage.TabIndex = 1;
		this.buttonSendMessage.Text = "Send Message";
		this.buttonSendMessage.UseVisualStyleBackColor = false;
		this.buttonSendMessage.Click += new System.EventHandler(this.ButtonSendMessageClick);
		// 
		// richTextTxMessage
		// 
		this.richTextTxMessage.BackColor = System.Drawing.Color.White;
		this.richTextTxMessage.Location = new System.Drawing.Point(18, 143);
		this.richTextTxMessage.Name = "richTextTxMessage";
		this.richTextTxMessage.Size = new System.Drawing.Size(240, 96);
		this.richTextTxMessage.TabIndex = 2;
		this.richTextTxMessage.Text = "";
		// 
		// textBoxConnectStatus
		// 
		this.textBoxConnectStatus.BackColor = System.Drawing.Color.SkyBlue;
		this.textBoxConnectStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.textBoxConnectStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.textBoxConnectStatus.Location = new System.Drawing.Point(138, 287);
		this.textBoxConnectStatus.Name = "textBoxConnectStatus";
		this.textBoxConnectStatus.ReadOnly = true;
		this.textBoxConnectStatus.Size = new System.Drawing.Size(240, 13);
		this.textBoxConnectStatus.TabIndex = 10;
		this.textBoxConnectStatus.Text = "Not Connected";
		// 
		// label4
		// 
		this.label4.Location = new System.Drawing.Point(18, 127);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(120, 16);
		this.label4.TabIndex = 9;
		this.label4.Text = "Message To Server";
		// 
		// richTextRxMessage
		// 
		this.richTextRxMessage.BackColor = System.Drawing.Color.White;
		this.richTextRxMessage.Location = new System.Drawing.Point(266, 143);
		this.richTextRxMessage.Name = "richTextRxMessage";
		this.richTextRxMessage.ReadOnly = true;
		this.richTextRxMessage.Size = new System.Drawing.Size(248, 128);
		this.richTextRxMessage.TabIndex = 1;
		this.richTextRxMessage.Text = "";
		// 
		// textBoxPort
		// 
		this.textBoxPort.Location = new System.Drawing.Point(352, 58);
		this.textBoxPort.Name = "textBoxPort";
		this.textBoxPort.Size = new System.Drawing.Size(116, 20);
		this.textBoxPort.TabIndex = 6;
		this.textBoxPort.Text = "10000";
		// 
		// buttonConnect
		// 
		this.buttonConnect.BackColor = System.Drawing.SystemColors.HotTrack;
		this.buttonConnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.buttonConnect.ForeColor = System.Drawing.Color.Yellow;
		this.buttonConnect.Location = new System.Drawing.Point(95, 12);
		this.buttonConnect.Name = "buttonConnect";
		this.buttonConnect.Size = new System.Drawing.Size(165, 26);
		this.buttonConnect.TabIndex = 7;
		this.buttonConnect.Text = "Connect To Server";
		this.buttonConnect.UseVisualStyleBackColor = false;
		this.buttonConnect.Click += new System.EventHandler(this.ButtonConnectClick);
		// 
		// label5
		// 
		this.label5.Location = new System.Drawing.Point(10, 287);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(104, 16);
		this.label5.TabIndex = 13;
		this.label5.Text = "Connection Status";
		// 
		// textBoxIP
		// 
		this.textBoxIP.Location = new System.Drawing.Point(147, 58);
		this.textBoxIP.Name = "textBoxIP";
		this.textBoxIP.Size = new System.Drawing.Size(116, 20);
		this.textBoxIP.TabIndex = 3;
		// 
		// buttonDisconnect
		// 
		this.buttonDisconnect.BackColor = System.Drawing.Color.Red;
		this.buttonDisconnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.buttonDisconnect.ForeColor = System.Drawing.Color.Yellow;
		this.buttonDisconnect.Location = new System.Drawing.Point(266, 12);
		this.buttonDisconnect.Name = "buttonDisconnect";
		this.buttonDisconnect.Size = new System.Drawing.Size(165, 26);
		this.buttonDisconnect.TabIndex = 15;
		this.buttonDisconnect.Text = "Disconnet From Server";
		this.buttonDisconnect.UseVisualStyleBackColor = false;
		this.buttonDisconnect.Click += new System.EventHandler(this.ButtonDisconnectClick);
		// 
		// label1
		// 
		this.label1.Location = new System.Drawing.Point(42, 61);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(96, 16);
		this.label1.TabIndex = 4;
		this.label1.Text = "Server IP Address";
		// 
		// label2
		// 
		this.label2.Location = new System.Drawing.Point(276, 61);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(64, 16);
		this.label2.TabIndex = 5;
		this.label2.Text = "Server Port";
		// 
		// label3
		// 
		this.label3.Location = new System.Drawing.Point(266, 127);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(192, 16);
		this.label3.TabIndex = 8;
		this.label3.Text = "Message From Server";
		// 
		// btnClear
		// 
		this.btnClear.BackColor = System.Drawing.Color.Red;
		this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
		this.btnClear.ForeColor = System.Drawing.Color.Yellow;
		this.btnClear.Location = new System.Drawing.Point(384, 279);
		this.btnClear.Name = "btnClear";
		this.btnClear.Size = new System.Drawing.Size(64, 24);
		this.btnClear.TabIndex = 16;
		this.btnClear.Text = "Clear";
		this.btnClear.UseVisualStyleBackColor = false;
		this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
		// 
		// TxtBoxMsgTo
		// 
		this.TxtBoxMsgTo.Location = new System.Drawing.Point(352, 90);
		this.TxtBoxMsgTo.Name = "TxtBoxMsgTo";
		this.TxtBoxMsgTo.Size = new System.Drawing.Size(116, 20);
		this.TxtBoxMsgTo.TabIndex = 17;
		this.TxtBoxMsgTo.Text = "s";
		this.TxtBoxMsgTo.TextChanged += new System.EventHandler(this.TxtBoxMsgTo_TextChanged);
		// 
		// label6
		// 
		this.label6.Location = new System.Drawing.Point(276, 93);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(64, 16);
		this.label6.TabIndex = 18;
		this.label6.Text = "Message to";
		// 
		// Request
		// 
		this.Request.BackColor = System.Drawing.Color.Red;
		this.Request.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
		this.Request.ForeColor = System.Drawing.Color.Yellow;
		this.Request.Location = new System.Drawing.Point(314, 279);
		this.Request.Name = "Request";
		this.Request.Size = new System.Drawing.Size(64, 23);
		this.Request.TabIndex = 20;
		this.Request.Text = "Request";
		this.Request.UseVisualStyleBackColor = false;
		this.Request.Click += new System.EventHandler(this.button1_Click);
		// 
		// label7
		// 
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(48, 93);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(90, 13);
		this.label7.TabIndex = 21;
		this.label7.Text = "My Client Number";
		// 
		// TxtBoxMyNumber
		// 
		this.TxtBoxMyNumber.Enabled = false;
		this.TxtBoxMyNumber.Location = new System.Drawing.Point(147, 90);
		this.TxtBoxMyNumber.Name = "TxtBoxMyNumber";
		this.TxtBoxMyNumber.Size = new System.Drawing.Size(116, 20);
		this.TxtBoxMyNumber.TabIndex = 22;
		// 
		// SocketClient
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		this.BackColor = System.Drawing.Color.SkyBlue;
		this.ClientSize = new System.Drawing.Size(529, 320);
		this.Controls.Add(this.TxtBoxMyNumber);
		this.Controls.Add(this.label7);
		this.Controls.Add(this.Request);
		this.Controls.Add(this.label6);
		this.Controls.Add(this.TxtBoxMsgTo);
		this.Controls.Add(this.btnClear);
		this.Controls.Add(this.buttonDisconnect);
		this.Controls.Add(this.buttonSendMessage);
		this.Controls.Add(this.label5);
		this.Controls.Add(this.buttonClose);
		this.Controls.Add(this.textBoxConnectStatus);
		this.Controls.Add(this.label4);
		this.Controls.Add(this.label3);
		this.Controls.Add(this.buttonConnect);
		this.Controls.Add(this.textBoxPort);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.label1);
		this.Controls.Add(this.textBoxIP);
		this.Controls.Add(this.richTextTxMessage);
		this.Controls.Add(this.richTextRxMessage);
		this.Name = "SocketClient";
		this.Text = "Socket Client";
		this.Load += new System.EventHandler(this.SocketClient_Load);
		this.ResumeLayout(false);
		this.PerformLayout();

		}
		#endregion
		void ButtonCloseClick(object sender, System.EventArgs e)
		{
			if ( m_clientSocket != null )
			{
				m_clientSocket.Close ();
				m_clientSocket = null;
			}		
			Close();
		}
		
		void ButtonConnectClick(object sender, System.EventArgs e)
		{
			if(textBoxIP.Text == "" || textBoxPort.Text == "")
			{
				MessageBox.Show("IP Address and Port Number are required to connect to the Server\n");
				return;
			}
			try
			{
				UpdateControls(false);
				m_clientSocket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
				IPAddress ip = IPAddress.Parse (textBoxIP.Text);
				int iPortNo = System.Convert.ToInt16 ( textBoxPort.Text);
				IPEndPoint ipEnd = new IPEndPoint (ip,iPortNo);
				m_clientSocket.Connect ( ipEnd );

				if(m_clientSocket.Connected) 
                		{
					UpdateControls(true);
					WaitForData();
				}
			}
			catch(SocketException se)
			{
				string str;
				str = "\nConnection failed, is the server running?\n" + se.Message;
				MessageBox.Show (str);
				UpdateControls(false);
			}		
		}			
		void ButtonSendMessageClick(object sender, System.EventArgs e)
		{
			try
			{
				if (TxtBoxMsgTo.Text == "s" || TxtBoxMsgTo.Text == "1" || TxtBoxMsgTo.Text == "2" ||
				    TxtBoxMsgTo.Text == "3" || TxtBoxMsgTo.Text == "4" || TxtBoxMsgTo.Text == "5" ||
				    TxtBoxMsgTo.Text == "6" || TxtBoxMsgTo.Text == "7" || TxtBoxMsgTo.Text == "8" ||
				    TxtBoxMsgTo.Text == "9")
				{

					string msg = richTextTxMessage.Text;
					msg = msg + Client_number + TxtBoxMsgTo.Text;

					richTextRxMessage.Text = richTextRxMessage.Text + '\n' +
					Client_number.Substring(2, 1) + ":" + TxtBoxMsgTo.Text + ":" +
					msg.Substring(0, msg.Length - 4);

					byData = System.Text.Encoding.ASCII.GetBytes(msg);
					if (m_clientSocket != null)
					{
						m_clientSocket.Send(byData);
					}
				}
				else 
				{
					MessageBox.Show("   Destination Host code in incorrect   ", "Error");
				}
			}
			catch (SocketException se)
			{
				MessageBox.Show(se.Message);
			}
			finally 
			{
				richTextTxMessage.Text = "";
			}
		}
		public void WaitForData()
		{
			try
			{
				if  ( m_pfnCallBack == null ) 
				{
					m_pfnCallBack = new AsyncCallback (OnDataReceived);
				}

				SocketPacket theSocPkt = new SocketPacket ();
				theSocPkt.thisSocket = m_clientSocket;
				m_result = m_clientSocket.BeginReceive (theSocPkt.dataBuffer,
				                                        0, theSocPkt.dataBuffer.Length,
				                                        SocketFlags.None, 
				                                        m_pfnCallBack, 
				                                        theSocPkt);
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}

		}
		public class SocketPacket
		{
			public System.Net.Sockets.Socket thisSocket;
			public byte[] dataBuffer = new byte[1024];
		}

		public  void OnDataReceived(IAsyncResult asyn)
		{
			try
			{
                		SocketPacket theSockId = (SocketPacket)asyn.AsyncState;
				int iRx  = theSockId.thisSocket.EndReceive (asyn);
				char[] chars = new char[iRx +  1];
				System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
				int charLen = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
				System.String szData = new System.String(chars); 
                		string First_Word = szData.Substring(0, 6);

				if (First_Word != "%%^^@@")  //reuest the number of clients from client to server
				{
					if (First_Word != "$D$##@")  //disconnected client 
					{
						if (First_Word != "$$##@@")  //message from server
						{
							int length_of_msg = szData.Length;
							string client_source = szData.Substring(length_of_msg - 4, 1);
							szData = szData.Substring(0, length_of_msg - 2);

							if (check_First_Msg == 1)
							{
								szData = szData.Substring(0, length_of_msg - 6);
								richTextRxMessage.Text = richTextRxMessage.Text + '\n' + client_source + ": " + szData;
							}

							else
							{
								richTextRxMessage.Text = richTextRxMessage.Text + '\n' + szData;
							}

							if (check_First_Msg == 0)
							{
								Client_number = null;
								Client_number = richTextRxMessage.Text.Substring(14);
								check_First_Msg = 1;
								TxtBoxMyNumber.Text = null;
								TxtBoxMyNumber.Text = Client_number.Substring(2, 1); 
							}
						}
					}
				}

				if (First_Word == "%%^^@@") 
				{
					szData = szData.Substring(6);
					char[] Data = szData.ToCharArray();
					string Recived_data = null;
					for (int i = 0; i < Data.Length; i++) 
					{
						if (i == 0)
						{
							Recived_data = Recived_data + Data[i].ToString();
						}
						else if(i < Data.Length -1  && i > 0 )
						{
						    Recived_data = Recived_data + " - " + Data[i].ToString();
						}

					}
					Recived_data = "S:" + Recived_data; 
					richTextRxMessage.Text = richTextRxMessage.Text + "\n" + Recived_data;
				}

				if (First_Word == "$D$##@")  // disconnected client
				{
				    richTextRxMessage.Text = richTextRxMessage.Text + '\n' + "S:" + szData.Substring(6);
				}


				if (First_Word == "$$##@@")   //message from server
				{
				    richTextRxMessage.Text = richTextRxMessage.Text + '\n' + "S:" + szData.Substring(6, szData.Length - 7); 
				}
				WaitForData();
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}
		}	
		private void UpdateControls( bool connected ) 
		{
			buttonConnect.Enabled = !connected;
			buttonDisconnect.Enabled = connected;
			string connectStatus = connected? "Connected" : "Not Connected";
			textBoxConnectStatus.Text = connectStatus;
		}
		void ButtonDisconnectClick(object sender, System.EventArgs e)
		{
			if ( m_clientSocket != null )
			{
				m_clientSocket.Close();
				m_clientSocket = null;
				UpdateControls(false);
                check_First_Msg = 0;
			}
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			richTextRxMessage.Clear();
		}

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = "%%^^@@" + Client_number;
                byData = System.Text.Encoding.ASCII.GetBytes(msg);
                if (m_clientSocket != null)
                {
                    m_clientSocket.Send(byData);
                }
            }
            catch (Exception err) 
            {
                MessageBox.Show(err.Message.ToString(), "Error");
            }
        }

        private void TxtBoxMsgTo_TextChanged(object sender, EventArgs e)
        {

        }

        private void SocketClient_Load(object sender, EventArgs e)
        {

        }		
	}
}
