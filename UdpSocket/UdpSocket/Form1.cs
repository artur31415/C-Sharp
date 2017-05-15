using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UdpSocket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int receiverPort;
        UdpClient receiver;

        public String Data = "";
        public String msg = "";

        double lastTime = 0, actualTime = 0, deltaTime = 0;

        public void GetSocketText(object sender, EventArgs e)
        {
            //actualTime = Double.Parse(msg);
            //deltaTime = actualTime - lastTime;
            //lastTime = actualTime;

            RT_DEBUG.Text = "Received: " + Data;// + "\nDeltaTime = " + deltaTime.ToString();
            Data = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BT_SEND.Enabled = false;
            BT_TIMER.Enabled = false;
        }

        private void DataReceived(IAsyncResult ar)
        {
            UdpClient c = (UdpClient)ar.AsyncState;
            IPEndPoint receivedIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Byte[] receivedBytes = c.EndReceive(ar, ref receivedIpEndPoint);

            // Convert data to ASCII and print in console
            string receivedText = ASCIIEncoding.ASCII.GetString(receivedBytes);
            msg = receivedText;
            Data = "(" + receivedIpEndPoint + ") > " + receivedText;

            // Restart listening for udp data packages
            c.BeginReceive(DataReceived, ar.AsyncState);
            this.Invoke(new EventHandler(GetSocketText));
        }

        private void BT_CONNECT_Click(object sender, EventArgs e)
        {
            receiverPort = int.Parse(TB_PORT.Text);
            receiver = new UdpClient(receiverPort);
            

            // Start async receiving
            receiver.BeginReceive(DataReceived, receiver);

            BT_CONNECT.Enabled = false;
            BT_SEND.Enabled = true;
            BT_TIMER.Enabled = true;

        }

        private void BT_SEND_Click(object sender, EventArgs e)
        {
            receiver.Send(Encoding.ASCII.GetBytes(TB_SEND.Text), Encoding.ASCII.GetBytes(TB_SEND.Text).Length, TB_HOSTNAME.Text, receiverPort);
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            receiver.Send(Encoding.ASCII.GetBytes("T"), Encoding.ASCII.GetBytes("T").Length, TB_HOSTNAME.Text, receiverPort);
        }

        private void BT_TIMER_Click(object sender, EventArgs e)
        {
            UpdateTimer.Start();
        }
    }
}
