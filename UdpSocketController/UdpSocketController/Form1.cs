using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UdpSocketController
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

        public ComboBox[] ComandKeys;
        String[] customKeys = new String[] { "Enter", "Space", "Esc", "M Left", "M Right", "M Up", "M Down", "M L Click", "M R Click" };


        enum SystemMetric
        {
            SM_CXSCREEN = 0,
            SM_CYSCREEN = 1,
        }

        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(SystemMetric smIndex);

        int CalculateAbsoluteCoordinateX(int x)
        {
            return (x * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        }

        int CalculateAbsoluteCoordinateY(int y)
        {
            return (y * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
        }


        /// <summary>
        /// ////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="nInputs"></param>
        /// <param name="pInputs"></param>
        /// <param name="cbSize"></param>
        /// <returns></returns>
        // P/Invoke function for controlling the mouse
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);

        /// <summary>
        /// structure for mouse data
        /// </summary>
        struct MouseInput
        {
            public int X; // X coordinate
            public int Y; // Y coordinate
            public uint MouseData; // mouse data, e.g. for mouse wheel
            public uint DwFlags; // further mouse data, e.g. for mouse buttons
            public uint Time; // time of the event
            public IntPtr DwExtraInfo; // further information
        }

        /// <summary>
        /// super structure for input data of the function SendInput
        /// </summary>
        struct Input
        {
            public int Type; // type of the input, 0 for mouse  
            public MouseInput Data; // mouse data
        }

        // constants for mouse flags
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002; // press left mouse button
        const uint MOUSEEVENTF_LEFTUP = 0x0004; // release left mouse button
        const uint MOUSEEVENTF_ABSOLUTE = 0x8000; // whole screen, not just application window
        const uint MOUSEEVENTF_MOVE = 0x0001; // move mouse

        private MouseInput CreateMouseInput(int x, int y, uint data, uint time, uint flag)
        {
            // create from the given data an object of the type MouseInput, which then can be send
            MouseInput Result = new MouseInput();
            Result.X = x;
            Result.Y = y;
            Result.MouseData = data;
            Result.Time = time;
            Result.DwFlags = flag;
            return Result;
        }

        private void SimulateMouseClick()
        {
            // Linksklick simulieren: Maustaste drücken und loslassen
            Input[] MouseEvent = new Input[2];
            MouseEvent[0].Type = 0;
            MouseEvent[0].Data = CreateMouseInput(0, 0, 0, 0, MOUSEEVENTF_LEFTDOWN);

            MouseEvent[1].Type = 0; // INPUT_MOUSE; 
            MouseEvent[1].Data = CreateMouseInput(0, 0, 0, 0, MOUSEEVENTF_LEFTUP);

            SendInput((uint)MouseEvent.Length, MouseEvent, Marshal.SizeOf(MouseEvent[0].GetType()));
        }

        private void SimulateMouseMove(int x, int y)
        {
            Input[] MouseEvent = new Input[1];
            MouseEvent[0].Type = 0;
            // move mouse: Flags ABSOLUTE (whole screen) and MOVE (move)
            MouseEvent[0].Data = CreateMouseInput(x, y, 0, 0, MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE);
            //MouseEvent[0].Data = CreateMouseInput(x, y, 0, 0, MOUSEEVENTF_MOVE);
            SendInput((uint)MouseEvent.Length, MouseEvent, Marshal.SizeOf(MouseEvent[0].GetType()));
        }


        public void GetSocketText(object sender, EventArgs e)
        {
            RT_VERBOSE.Text = "Received:\n" + Data + "\n\n" + Cursor.Position;

            String[] ControlData = msg.Split('{')[1].Split('}')[0].Split(',');

            //double Roll = Double.Parse(ControlData[0]);//Left and Right
            //double Pitch = Double.Parse(ControlData[1]);//Forward and Backwards

            //String Control0 = "E", Control1 = "E";
            String[] ControlFromData = new String[ControlData.GetLength(0)];
            double[] DataValues = new double[ControlData.GetLength(0)];
            int Tolerance = TB_TOL.Value;

            for (int i = 0; i < ControlFromData.GetLength(0); ++i)
                ControlFromData[i] = "E";

            for (int i = 0; i < ControlData.GetLength(0); ++i)
            {
                DataValues[i] = Double.Parse(ControlData[i]);
            }

            //ACC CONTROL CMDS
            if (DataValues[0] <= (360 - Tolerance) && DataValues[0] >= 270)
                ControlFromData[0] = "L";
            else if ((DataValues[0] >= 0 && DataValues[0] <= Tolerance) || (DataValues[0] <= 360 && DataValues[0] >= (360 - Tolerance)))
                ControlFromData[0] = "NA";
            else
                ControlFromData[0] = "R";


            if (DataValues[1] <= (360 - Tolerance) && DataValues[1] >= 270)
                ControlFromData[1] = "F";
            else if ((DataValues[1] >= 0 && DataValues[1] <= Tolerance) || (DataValues[1] <= 360 && DataValues[1] >= (360 - Tolerance)))
                ControlFromData[1] = "NA";
            else
                ControlFromData[1] = "B";


            Boolean[] pressMask = new Boolean[ComandKeys.GetLength(0)];

            for (int i = 0; i < pressMask.GetLength(0); ++i)
                pressMask[i] = false;

            L_KEY_0.BackColor = Color.Transparent;
            L_KEY_1.BackColor = Color.Transparent;
            L_KEY_2.BackColor = Color.Transparent;
            L_KEY_3.BackColor = Color.Transparent;


            ////////////////////////////////////////
            if (ControlFromData[0] == "L")
            {
                L_KEY_0.BackColor = Color.Green;
                pressMask[0] = true;
            }
            else if (ControlFromData[0] == "R")
            {
                L_KEY_1.BackColor = Color.Green;
                pressMask[1] = true;
            }
            ///////////////////////////////////////////

            if (ControlFromData[1] == "F")
            {
                L_KEY_2.BackColor = Color.Green;
                pressMask[2] = true;
            }
            else if (ControlFromData[1] == "B")
            {
                L_KEY_3.BackColor = Color.Green;
                pressMask[3] = true;
            }
            //////////////////////
            int DPos = TB_MOV.Value;

            for(int i = 0; i < pressMask.GetLength(0); ++i)
            {
                if (pressMask[i] == true)
                {
                    if (ComandKeys[i].SelectedIndex <= 38)
                        SendKeys.Send("{" + ComandKeys[i].SelectedItem.ToString() + "}");
                    else
                    {
                        if (ComandKeys[i].SelectedIndex == 39)
                        {
                            //this.Cursor = new Cursor(Cursor.Current.Handle);
                            //Cursor.Position = new Point(Cursor.Position.X - 50, Cursor.Position.Y);
                            SimulateMouseMove(CalculateAbsoluteCoordinateX(Cursor.Position.X - DPos), CalculateAbsoluteCoordinateY(Cursor.Position.Y));
                            //MouseOperations.SetCursorPosition(MouseOperations.GetCursorPosition().X - 10, MouseOperations.GetCursorPosition().Y);
                        }
                        else if (ComandKeys[i].SelectedIndex == 40)
                        {
                            SimulateMouseMove(CalculateAbsoluteCoordinateX(Cursor.Position.X + DPos), CalculateAbsoluteCoordinateY(Cursor.Position.Y));
                            //MouseOperations.SetCursorPosition(MouseOperations.GetCursorPosition().X + 10, MouseOperations.GetCursorPosition().Y);
                        }
                        else if (ComandKeys[i].SelectedIndex == 41)
                        {
                            SimulateMouseMove(CalculateAbsoluteCoordinateX(Cursor.Position.X), CalculateAbsoluteCoordinateY(Cursor.Position.Y - DPos));
                            //MouseOperations.SetCursorPosition(MouseOperations.GetCursorPosition().X, MouseOperations.GetCursorPosition().Y - 10);
                        }
                        else if (ComandKeys[i].SelectedIndex == 42)
                        {
                            SimulateMouseMove(CalculateAbsoluteCoordinateX(Cursor.Position.X), CalculateAbsoluteCoordinateY(Cursor.Position.Y + DPos));
                            //MouseOperations.SetCursorPosition(MouseOperations.GetCursorPosition().X, MouseOperations.GetCursorPosition().Y + 10);
                        }
                        ////////////////////////
                        else if (ComandKeys[i].SelectedIndex == 43)
                        {
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                        }
                        else if (ComandKeys[i].SelectedIndex == 44)
                        {
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightDown);
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp);
                        }
                    }
                }
            }
            

            //SendKeys.Send("{ENTER}");

            //this.Cursor = new Cursor(Cursor.Current.Handle);
            //Cursor.Position = new Point(Cursor.Position.X - 50, Cursor.Position.Y - 50);

            //MouseOperations.SetCursorPosition(MouseOperations.GetCursorPosition().X - 10, MouseOperations.GetCursorPosition().Y - 10);
            //MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            //MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);

            //RT_OUT.Text += "Acc = (" + data[0] + ", " + data[1] + ");\n";
        }

        public void KeysInit()
        {

            ComandKeys = new ComboBox[4];

            ComandKeys[0] = CB_KEY_0;
            ComandKeys[1] = CB_KEY_1;
            ComandKeys[2] = CB_KEY_2;
            ComandKeys[3] = CB_KEY_3;

            for (int i = 48; i <= 57; ++i)
            {
                for(int k = 0; k < ComandKeys.GetLength(0); ++k)
                    ComandKeys[k].Items.Add((char)(i));
            }

            for (int i = 65; i <= 90; ++i)
            {
                for (int k = 0; k < ComandKeys.GetLength(0); ++k)
                    ComandKeys[k].Items.Add((char)(i));
            }



            for (int i = 0; i < customKeys.GetLength(0); ++i)
            {
                for (int k = 0; k < ComandKeys.GetLength(0); ++k)
                {
                    ComandKeys[k].Items.Add(customKeys[i]);
                }
            }

            ComandKeys[0].SelectedIndex = 39;
            ComandKeys[1].SelectedIndex = 40;
            ComandKeys[2].SelectedIndex = 41;
            ComandKeys[3].SelectedIndex = 42;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KeysInit();

            L_KEY_0.BackColor = Color.Transparent;
            L_KEY_1.BackColor = Color.Transparent;
            L_KEY_2.BackColor = Color.Transparent;
            L_KEY_3.BackColor = Color.Transparent;

            
            TB_TOL.Value = 20;
            TB_MOV.Value = 1;

            L_TOL.Text = "Tol = " + TB_TOL.Value.ToString("00");
            L_MOV.Text = "Mov = " + TB_MOV.Value.ToString("00");

            //SendKeys.Send("{ENTER}");
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

        private void DemoTimer_Tick(object sender, EventArgs e)
        {
            //SendKeys.Send("{ENTER}");

            //this.Cursor = new Cursor(Cursor.Current.Handle);
            //Cursor.Position = new Point(Cursor.Position.X - 50, Cursor.Position.Y - 50);

            //MouseOperations.SetCursorPosition(MouseOperations.GetCursorPosition().X - 10, MouseOperations.GetCursorPosition().Y - 10);
            //MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            //MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);

            receiver.Send(Encoding.ASCII.GetBytes("T"), Encoding.ASCII.GetBytes("T").Length, TB_HOSTNAME.Text, receiverPort);
        }

        private void BT_CONNECT_Click(object sender, EventArgs e)
        {
            receiverPort = int.Parse(TB_PORT.Text);
            receiver = new UdpClient(receiverPort);


            // Start async receiving
            receiver.BeginReceive(DataReceived, receiver);

            BT_CONNECT.Enabled = false;
            DemoTimer.Start();

        }

        private void BT_SEARCH_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void TB_TOL_Scroll(object sender, EventArgs e)
        {
            L_TOL.Text = "Tol = " + TB_TOL.Value.ToString("00");
        }

        private void TB_MOV_Scroll(object sender, EventArgs e)
        {
            L_MOV.Text = "Mov = " + TB_MOV.Value.ToString("00");
        }
    }


    public class MouseOperations
    {
        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void SetCursorPosition(int X, int Y)
        {
            SetCursorPos(X, Y);
        }

        public static void SetCursorPosition(MousePoint point)
        {
            SetCursorPos(point.X, point.Y);
        }

        public static MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
            return currentMousePoint;
        }

        public static void MouseEvent(MouseEventFlags value)
        {
            MousePoint position = GetCursorPosition();

            mouse_event
                ((int)value,
                 position.X,
                 position.Y,
                 0,
                 0)
                ;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MousePoint
        {
            public int X;
            public int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }

        }

    }
}
