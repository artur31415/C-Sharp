using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace SocketCommunication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        IPAddress[] localIP;
        IPAddress Server;
        private TcpClient client;
        public StreamReader STR;
        public StreamWriter STW;
        string Text2SEnd = String.Empty;
        string receive = String.Empty;


        TcpListener listener;

        private void button1_Click(object sender, EventArgs e) //Send button
        {
            if (textBox1.Text != "")
            {
                Text2SEnd = textBox1.Text;
                SENDER.RunWorkerAsync();
            }
            textBox1.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IPAddress[] localIP = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress addres in localIP)
            {
                if (addres.AddressFamily == AddressFamily.InterNetwork)
                {
                    Server = addres;
                    textBox2.Text = addres.ToString();
                }
            }
            textBox3.Text = 80.ToString();
            textBox4.Text = 80.ToString(); //192.168.1.100
            //textBox5.Text = "192.168.1.100"; //192.168.1.100
            //MessageBox.Show(textBox2.Text, "LOL");
        }

        private void button2_Click(object sender, EventArgs e)  //start server
        {
            Server = IPAddress.Parse(textBox2.Text);
            listener = new TcpListener(Server, int.Parse(textBox3.Text));//IPAddress.Any
            
            listener.Start();
            client = listener.AcceptTcpClient();
            STR = new StreamReader(client.GetStream());
            STW = new StreamWriter(client.GetStream());
            STW.AutoFlush = true;
            RECEIVER.RunWorkerAsync();
            SENDER.WorkerSupportsCancellation = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) //RECEIVER
        {
            while(client.Connected)
            {
                try
                {
                    receive = STR.ReadLine();
                    this.richTextBox1.Invoke(new MethodInvoker(delegate() 
                        {richTextBox1.AppendText("Received : "+receive + "\n");}));
                    receive = String.Empty;
                }
                catch(Exception X)
                {
                    MessageBox.Show(X.Message.ToString());
                }
            }
        }

        private void SENDER_DoWork(object sender, DoWorkEventArgs e)  //SENDER
        {
            if (client.Connected)
            {
                STW.WriteLine(Text2SEnd);
                this.richTextBox1.Invoke(new MethodInvoker(delegate()
                { richTextBox1.AppendText("Sended : " + Text2SEnd + "\n"); }));

            }
            else
            {
                MessageBox.Show("send failed");
            }
            SENDER.CancelAsync();
        }

        private void button3_Click(object sender, EventArgs e)  //conect to server
        {
            client = new TcpClient();
            IPEndPoint IP_End = new IPEndPoint(IPAddress.Parse(textBox5.Text),int.Parse(textBox4.Text));

            try
            {
                client.Connect(IP_End);
                if (client.Connected)
                {
                    richTextBox1.AppendText("Connected!"+"\n");
                    STR = new StreamReader(client.GetStream());
                    STW = new StreamWriter(client.GetStream());
                    STW.AutoFlush = true;
                    RECEIVER.RunWorkerAsync();
                    SENDER.WorkerSupportsCancellation = true;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message.ToString());
            }
        }
    }
}
