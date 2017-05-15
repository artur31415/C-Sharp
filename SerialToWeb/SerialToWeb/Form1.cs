using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using System.IO.Ports;


namespace SerialToWeb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte[] buf;
        int length;

        String ConString = "datasource=127.0.0.1;port=3306;username=root;password=;";
        String[] ColNames = new String[] { "machine", "type", "time", "toDo", "assignedWorkerId", "complete" };

        String RawString = String.Empty;

        public String[] GetWorker(String[] colNames, String[] colValues, int Limit)
        {
            String[] Workers = new String[]{};

            ConString = "datasource=" + HOST_DB.Text + ";" +
                        "port=" + PORT_DB.Text + ";" +
                        "username=" + USERNAME_DB.Text + ";" +
                        "password=" + PASSWORD_DB.Text + ";";
            String Query = String.Empty;

            Query = "SELECT * FROM sigmasolutions.workers WHERE `" + colNames[0] + "`='" + colValues[0] + "'";
            for (int i = 1; i < colNames.GetLength(0); ++i)
            {
                Query += " AND `" + colNames[i] + "`='" + colValues[i] + "'";
            }
            if (Limit != -1)
                Query += " LIMIT " + Limit.ToString();

            MySqlConnection conDB = new MySqlConnection(ConString);
            MySqlCommand cmdDB = new MySqlCommand(Query, conDB);
            MySqlDataReader myReader;

            try
            {
                //richTextBox1.Clear();
                conDB.Open();
                myReader = cmdDB.ExecuteReader();
                //MessageBox.Show("SAVED");

                int Counter = 0;
                String allWorkers = String.Empty;
                while (myReader.Read())
                {
                    //myReader.getv
                    richTextBox1.Text += Counter.ToString("00") + ":\n";
                    for (int i = 0; i < ColNames.GetLength(0); ++i)
                    {
                        richTextBox1.Text += ColNames[i] + ": " + myReader.GetString(ColNames[i]) + "\n";
                    }
                    //richTextBox1.Text += myReader.GetString("machine") + "\n";
                    ++Counter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return Workers;
        }

        public void AddOrder(String[] orderData)
        {
            ConString = "datasource=" + HOST_DB.Text + ";" +
                        "port=" + PORT_DB.Text + ";" +
                        "username=" + USERNAME_DB.Text + ";" +
                        "password=" + PASSWORD_DB.Text + ";";

            String Query = "INSERT INTO sigmasolutions.orders(machine, type, time, toDo, assignedWorkerId, complete) " +
                                        "VALUES('" + orderData[0] + "', '" + orderData[1] + "', '" + orderData[2] + "', '" + orderData[3] + "', '" + orderData[4] + "', '" + orderData[5] + "') ;";
            MySqlConnection conDB = new MySqlConnection(ConString);
            MySqlCommand cmdDB = new MySqlCommand(Query, conDB);
            MySqlDataReader myReader;

            try
            {
                conDB.Open();
                myReader = cmdDB.ExecuteReader();
                //MessageBox.Show("SAVED");
                richTextBox1.Text += "Saved to the DB!\n";
                while (myReader.Read())
                {
                    //richTextBox1.Text = myReader.GetString("machine");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdatePorts()
        {
            string[] myPort;
            PortName.Items.Clear();


            if (System.IO.Ports.SerialPort.GetPortNames().GetLength(0) != 0)
            {
                myPort = System.IO.Ports.SerialPort.GetPortNames();
                for (int i = 0; i < myPort.Length; ++i)
                {
                    PortName.Items.Add(myPort[i]);
                    if (i == 0)
                        PortName.SelectedIndex = 0;
                }
            }
            else
            {
                PortName.Items.Add("Sem Portas");
                PortName.SelectedIndex = 0;
            }
            //MessageBox.Show("teste1");
        }

        public void UpdateImg(object sender, EventArgs e)
        {
            //for (int i = 0; i < length; ++i)
            //{
            //    richTextBox1.Text += buf[i].ToString();
            //}
            RawString += System.Text.Encoding.UTF8.GetString(buf);
            if (RawString.IndexOf('#') > 0)
            {
                //MessageBox.Show("Received!\n" + richTextBox1.Text);
                
                String[] OrderData = new String[6] { "", "", "", "", "", "" };
                OrderData[0] = RawString.Split(';')[0].Split(':')[1];//machine
                OrderData[1] = RawString.Split(';')[1].Split(':')[1];//type
                OrderData[2] = System.DateTime.Now.ToString() + ";";
                OrderData[3] = RawString.Split(';')[2].Split(':')[1].Split('#')[0];//toDo
                OrderData[4] = "2";//worker
                OrderData[5] = "0";

                richTextBox1.Text += "Order[" + OrderData[0] + ";" + OrderData[1] + ";" + OrderData[2] + ";" + OrderData[3] + ";" + OrderData[4] + ";" + OrderData[5] + "]\n";

                //Get the best worker for this order and assign this job for him(her)!

                //Add the order to the system!
                AddOrder(OrderData);

                RawString = String.Empty;
            }
            //SendHex(richTextBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //String ConString = "datasource=127.0.0.1;port=3306;username=root;password=;";
            //String Query = "INSERT INTO sigmasolutions.orders(machine, type, time, toDo, assignedWorkerId, complete) VALUES('Arduino', 'Mechanical', '16:10;', 'Press a Button', '2', '0') ;";
            //MySqlConnection conDB = new MySqlConnection(ConString);
            //MySqlCommand cmdDB = new MySqlCommand(Query, conDB);
            //MySqlDataReader myReader;

            //try
            //{
            //    conDB.Open();
            //    myReader = cmdDB.ExecuteReader();
            //    MessageBox.Show("SAVED");
            //    while (myReader.Read())
            //    {
            //        richTextBox1.Text = myReader.GetString("machine");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            UpdatePorts();
            //MessageBox.Show(System.DateTime.Now.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //String ConString = "datasource=127.0.0.1;port=3306;username=root;password=;";

            ConString = "datasource=" + HOST_DB.Text + ";" +
                        "port=" + PORT_DB.Text + ";" +
                        "username=" + USERNAME_DB.Text + ";" +
                        "password=" + PASSWORD_DB.Text + ";";

            String OrderTypeString = String.Empty;
            if (checkBox1.Checked)
                OrderTypeString = "0";
            else
                OrderTypeString = "1";

            String Query = "SELECT * FROM sigmasolutions.orders WHERE `complete`='" + OrderTypeString + "'";
            MySqlConnection conDB = new MySqlConnection(ConString);
            MySqlCommand cmdDB = new MySqlCommand(Query, conDB);
            MySqlDataReader myReader;

            try
            {
                richTextBox1.Clear();
                conDB.Open();
                myReader = cmdDB.ExecuteReader();
                //MessageBox.Show("SAVED");
                
                int Counter = 0;
                while (myReader.Read())
                {
                    richTextBox1.Text += Counter.ToString("00") + ":\n";
                    for (int i = 0; i < ColNames.GetLength(0); ++i)
                    {
                        richTextBox1.Text += ColNames[i] + ": " + myReader.GetString(ColNames[i]) + "\n";
                    }
                    //richTextBox1.Text += myReader.GetString("machine") + "\n";
                    ++Counter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Connect_Click(object sender, EventArgs e)
        {
            try
            {
                if (PortName.SelectedItem.ToString() != "No ports available")
                {
                    if (Connect.Text == "Conect")//to connect
                    {
                        if (PICSerial.IsOpen)
                            PICSerial.Close();
                        PICSerial.BaudRate = int.Parse(baud.Text);
                        PICSerial.PortName = PortName.SelectedItem.ToString();
                        PICSerial.Open();
                        status_label.Text = "Conected: " + PICSerial.PortName + ", " + PICSerial.BaudRate;
                        status_label.ForeColor = Color.Green;
                        Connect.Text = "Disconect";

                    }
                    else//to disconnect
                    {
                        if (PICSerial.IsOpen)
                            PICSerial.Close();
                        status_label.Text = "Disconected";
                        status_label.ForeColor = Color.Red;
                        Connect.Text = "Conect";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            UpdatePorts();
        }

        private void PICSerial_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                length = sp.BytesToRead;
                buf = new byte[length];

                sp.Read(buf, 0, length);
                this.Invoke(new EventHandler(UpdateImg));
            }
            catch (Exception)
            {
            }
        }
    }
}
