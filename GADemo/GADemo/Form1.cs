using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EP;
using System.Threading;
using System.Diagnostics;

namespace GADemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static int MAX_TIME = 40;
        //--------------------------------------------------------------------------------------
        public static int numberOfSensors = 128;
        int ENNNi = 2 * numberOfSensors + 1;
        int ENNNh = 10;
        int ENNNo = 2;

        int ENNPopSize = 20;
        double ENNMutationRate = 0.1;
        ENN enn;
        //--------------------------------------------------------------------------------------

        List<double[]> AllTheHighFitnessIndividuals = new List<double[]>();
        


        GA ga;

        Thread T;

        long c = 0;
        

        NoFlickPanel P_ENVI;
        NoFlickPanel P_IMG;
        Robot robot;

        Random r;

        Robot[] robots;

        Double MaxWheelSpeed = 12;//7

        Point TopLeft = new Point(150, 150);

        public static Point pc0 = new Point(150 + 100, 150 + 100);
        public static Point pc1 = new Point(150 + 100 + 300, 150 + 100);
        public static Point pc2 = new Point(150 + 100 + 300, 150 + 100 + 100);
        public static Point pc3 = new Point(150 + 100, 150 + 100 + 100);

        public static int MAX_X = 700;
        public static int MIN_X = 100;

        public static int MAX_Y = 500;
        public static int MIN_Y = 100;


        public static string debugStr = "";

        public static Color TrackBackColor = Color.Yellow;
        Pen TrackPen = new Pen(Color.Black, 5);
        Pen TrackPen2 = new Pen(TrackBackColor, 10);

        double[] t = new double[] { 0, 0 };

        Stopwatch globalTimer;

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.RT_FEEDBACK0.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.RT_FEEDBACK0.Text = text;
            }
        }

        private void SetText2(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.RT_FEEDBACK1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText2);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.RT_FEEDBACK1.Text = text;
            }
        }

        double Map(double x, double x1, double x2, double y1, double y2)
        {
            return (y1 + (x - x1) * (y2 - y1) / (x2 - x1));
        }

        public void DrawTrack(Graphics g)
        {
            //Point TopLeft = new Point(150, 150);

            

            DrawCurve(new Point(TopLeft.X, TopLeft.Y), g);
            DrawStraight(new Point(TopLeft.X + 100, TopLeft.Y), g, 1);
            DrawStraight(new Point(TopLeft.X + 200, TopLeft.Y), g, 1);
            DrawStraight(new Point(TopLeft.X + 300, TopLeft.Y), g, 1);
            DrawCurve(new Point(TopLeft.X + 400, TopLeft.Y), g, 1);

            DrawStraight(new Point(TopLeft.X + 400, TopLeft.Y + 100), g, 2);

            DrawCurve(new Point(TopLeft.X + 400, TopLeft.Y + 200), g, 2);
            DrawStraight(new Point(TopLeft.X + 300, TopLeft.Y + 200), g, 3);
            DrawStraight(new Point(TopLeft.X + 200, TopLeft.Y + 200), g, 3);
            DrawStraight(new Point(TopLeft.X + 100, TopLeft.Y + 200), g, 3);
            DrawCurve(new Point(TopLeft.X, TopLeft.Y + 200), g, 3);

            DrawStraight(new Point(TopLeft.X, TopLeft.Y + 100), g, 0);

            

            //g.DrawEllipse(Pens.Black, new RectangleF(150 - 5 / 2 + 100, 150 - 5 / 2 + 100, 5, 5));
            //g.DrawEllipse(Pens.Black, new RectangleF(150 - 5 / 2 + 100 + 300, 150 - 5 / 2 + 100, 5, 5));
            //g.DrawEllipse(Pens.Black, new RectangleF(150 - 5 / 2 + 100 + 300, 150 - 5 / 2 + 100 + 100, 5, 5));
            //g.DrawEllipse(Pens.Black, new RectangleF(150 - 5 / 2 + 100, 150 - 5 / 2 + 100 + 100, 5, 5));

            //g.DrawLine(Pens.Orange, new Point(150 - 5 / 2 + 100, 150 - 5 / 2 + 100 - 20), new Point(150 - 5 / 2 + 100, 150 - 5 / 2 + 100 - 20 - 60));
        }
        public void DrawTrackCheckPoints(Graphics g)
        {
            Point TopLeft = new Point(150, 150);

            DrawCurveCheckPoints(new Point(TopLeft.X, TopLeft.Y), g);
            DrawStraightCheckPoints(new Point(TopLeft.X + 100, TopLeft.Y), g, 1);
            DrawStraightCheckPoints(new Point(TopLeft.X + 200, TopLeft.Y), g, 1);
            DrawStraightCheckPoints(new Point(TopLeft.X + 300, TopLeft.Y), g, 1);
            DrawCurveCheckPoints(new Point(TopLeft.X + 400, TopLeft.Y), g, 1);

            DrawStraightCheckPoints(new Point(TopLeft.X + 400, TopLeft.Y + 100), g, 2);

            DrawCurveCheckPoints(new Point(TopLeft.X + 400, TopLeft.Y + 200), g, 2);
            DrawStraightCheckPoints(new Point(TopLeft.X + 300, TopLeft.Y + 200), g, 3);
            DrawStraightCheckPoints(new Point(TopLeft.X + 200, TopLeft.Y + 200), g, 3);
            DrawStraightCheckPoints(new Point(TopLeft.X + 100, TopLeft.Y + 200), g, 3);
            DrawCurveCheckPoints(new Point(TopLeft.X, TopLeft.Y + 200), g, 3);

            DrawStraightCheckPoints(new Point(TopLeft.X, TopLeft.Y + 100), g, 0);
        }


        public void ThreadMethod()
        {
            while(true)
            {

                ga.doStep();
                if(ga.HighestFitnessIndividual.GenesToString == ga.Target.ToString())
                {
                    break;
                }
                ++c;
            }
        }

        public void ThreadAnimation()
        {
            float v = 2.2f;
            double ZeroAngle = 270;
            double theda = ZeroAngle;

            
            bool[] deadMask = new bool[robots.GetLength(0)];
            double[] trackPenalties = new double[robots.GetLength(0)];

            for (int i = 0; i < deadMask.GetLength(0); ++i)
            {
                deadMask[i] = false;
            }

            //Thread.Sleep(5000);

            globalTimer = Stopwatch.StartNew();
            while (true)
            {
                Bitmap b = new Bitmap(P_ENVI.Width, P_ENVI.Height);
                double trackPenalty = 0.0;

                int HighAliveIndex = 0;
                int highEver = 0;


                this.Invoke((MethodInvoker)delegate
                {
                    Graphics g = Graphics.FromImage(b);
                    g.FillRectangle(Brushes.Yellow, new RectangleF(0, 0, b.Width, b.Height));


                    DrawTrack(g);
                    

                    trackPenalties = new double[robots.GetLength(0)];
                    //robots[0].ApplyVelocity(t);
                    for (int i = 0; i < robots.GetLength(0); ++i)
                    {
                        if(!deadMask[i])
                        {
                            enn.SetBrain(i);
                            robots[i].ReadSensors(b);

                            double[] res = enn.UseNetwork(robots[i].GetSensorInput);
                            res[0] = Map(res[0], 0, 1, - MaxWheelSpeed, MaxWheelSpeed);
                            res[1] = Map(res[1], 0, 1, - MaxWheelSpeed, MaxWheelSpeed);

                            robots[i].ApplyVelocity(res);
                        }
                    }

                    //robot.ReadSensors(b);

                    //double[] res = enn.UseNetwork(robot.GetSensorInput);
                    //res[0] *= 3;
                    //res[1] = Map(res[1], 0, 1, -45, 45);

                    //robot.ApplyVelocity(res);


                    //trackPenalty = robot.isOnTrackBorder(b);

                    for (int i = 0; i < robots.GetLength(0); ++i)
                    {
                        if (!deadMask[i])
                        {
                            trackPenalties[i] = robots[i].isOnTrackBorder(b);
                        }
                    }

                    DrawTrackCheckPoints(g);

                    for (int i = 0; i < robots.GetLength(0); ++i)
                    {
                        if(!deadMask[i])
                        {
                            robots[i].CheckPoint(b);
                        }    
                    }


                    HighAliveIndex = 0;

                    double highAlive = -1;
                    for (int i = 0; i < robots.GetLength(0); ++i)
                    {
                        if (!deadMask[i] && (robots[i].InsideTrackCounter > highAlive || highAlive == -1))
                        {
                            highAlive = robots[i].InsideTrackCounter;
                            HighAliveIndex = i;
                        }
                    }


                    robots[HighAliveIndex].Show(g);
                    for (int i = 0; i < robots.GetLength(0); ++i)
                    {
                        if (!deadMask[i] && i != HighAliveIndex)
                        {
                            if(robots[i].isWithingTheTrack)
                                robots[i].Show(g, 100);
                            else
                                robots[i].Show(g, 50);
                            //robots[i].CheckOrientation(g, b);
                        }   
                    }


                    //robot.CheckPoint(b);
                    //robot.Show(g);

                    ///////////////////////////////////////////////////////////////////////////////

                    ///////////////////////////////////////////////////////////////////////////////

                    for (int i = 0; i < robots.GetLength(0); ++i)
                    {
                        if (deadMask[i] == false && (trackPenalties[i] == Robot.PENALTY_BORDER || trackPenalties[i] == Robot.PENALTY_OUTSIDE_BORDER || trackPenalties[i] == Robot.PENALTY_OUTSIDE_TRACK || globalTimer.ElapsedMilliseconds / 1000.0 >= MAX_TIME))//THEN THE INDIVIDUAL IS DEAD!
                        //if (deadMask[i] == false && (globalTimer.ElapsedMilliseconds / 1000.0 >= 40) || !robots[i].isWithingTheTrack)//THEN THE INDIVIDUAL IS DEAD!
                        {
                            if(globalTimer.ElapsedMilliseconds / 1000.0 >= 40)
                                globalTimer.Stop();

                            double TrackMin = 8 * 100 + 4 * (Math.PI * 23 / 2.0);
                            //double t = robots[i].GetAliveTimeSeconds();
                            //if (t >= 100)
                            //    t = 99.5;
                            //MessageBox.Show(TrackMin.ToString());
                            deadMask[i] = true;
                            enn.EvaluateIndividual(i, robots[i].GetTraveledDistance, globalTimer.ElapsedMilliseconds / 1000.0, trackPenalties[i], robots[i].CheckPointCounter);
                            
                            if (AllTheHighFitnessIndividuals.Count == 0 || AllTheHighFitnessIndividuals.Count - 1 != enn.GetActualGeneration)
                            {
                                AllTheHighFitnessIndividuals.Add(new double[] { (double)robots[i].GetPosition.X, (double)robots[i].GetPosition.Y, enn.HighestFitnessIndividual.Fitness});
                            }
                            else if (enn.HighestFitnessIndividual.Fitness > AllTheHighFitnessIndividuals.ElementAt<double[]>(AllTheHighFitnessIndividuals.Count - 1)[2])
                            {
                                AllTheHighFitnessIndividuals.ElementAt<double[]>(AllTheHighFitnessIndividuals.Count - 1)[0] = (double)robots[i].GetPosition.X;
                                AllTheHighFitnessIndividuals.ElementAt<double[]>(AllTheHighFitnessIndividuals.Count - 1)[1] = (double)robots[i].GetPosition.Y;
                                AllTheHighFitnessIndividuals.ElementAt<double[]>(AllTheHighFitnessIndividuals.Count - 1)[2] = enn.HighestFitnessIndividual.Fitness;
                            }

                            robots[i].Reset();
                        }
                    }
                    int c = 0;
                    for (int i = 0; i < deadMask.GetLength(0); ++i)
                    {
                        if (deadMask[i] == false)
                            ++c;
                    }

                    if(c == 0)//all robots are dead!
                    {
                        enn.doStep();
                        for (int i = 0; i < deadMask.GetLength(0); ++i)
                        {
                            deadMask[i] = false;
                        }
                        globalTimer = Stopwatch.StartNew();
                    }

                    //if (trackPenalty == 0.4 || trackPenalty == 0 || trackPenalty == 0.1 || robot.GetAliveTimeSeconds() >= 100)//THEN THE INDIVIDUAL IS DEAD!
                    //{
                    //    enn.EvaluateActualIndividual(robot.GetTraveledDistance, 100 - robot.GetAliveTimeSeconds(), trackPenalty, robot.CheckPointCounter);
                    //    if (AllTheHighFitnessIndividuals.Count == 0 || AllTheHighFitnessIndividuals.Count - 1 != enn.GetActualGeneration)
                    //    {
                    //        AllTheHighFitnessIndividuals.Add(new double[] { (double)robot.GetPosition.X, (double)robot.GetPosition.Y, enn.HighestFitnessIndividual.Fitness });
                    //    }
                    //    else if (enn.HighestFitnessIndividual.Fitness > AllTheHighFitnessIndividuals.ElementAt<double[]>(AllTheHighFitnessIndividuals.Count - 1)[2])
                    //    {
                    //        AllTheHighFitnessIndividuals.ElementAt<double[]>(AllTheHighFitnessIndividuals.Count - 1)[0] = (double)robot.GetPosition.X;
                    //        AllTheHighFitnessIndividuals.ElementAt<double[]>(AllTheHighFitnessIndividuals.Count - 1)[1] = (double)robot.GetPosition.Y;
                    //        AllTheHighFitnessIndividuals.ElementAt<double[]>(AllTheHighFitnessIndividuals.Count - 1)[2] = enn.HighestFitnessIndividual.Fitness;
                    //    }

                    //    robot.Reset();
                    //    enn.SetBrain();

                    //}

                    double high = -1;
                    highEver = 0;
                    for (int i = 0; i < AllTheHighFitnessIndividuals.Count; ++i)
                    {
                        if (AllTheHighFitnessIndividuals.ElementAt<double[]>(i)[2] > high || high == -1)
                        {
                            high = AllTheHighFitnessIndividuals.ElementAt<double[]>(i)[2];
                            highEver = i;
                        }
                    }

                    float radius = 5.0f;
                    for(int i = 0; i < AllTheHighFitnessIndividuals.Count; ++i)
                    {
                        if (i  == highEver)
                            g.FillEllipse(Brushes.DarkOrange, new RectangleF((float)AllTheHighFitnessIndividuals.ElementAt<double[]>(i)[0] - radius / 2, (float)AllTheHighFitnessIndividuals.ElementAt<double[]>(i)[1] - radius / 2, radius, radius));
                        else if (i < AllTheHighFitnessIndividuals.Count - 1)
                            g.FillEllipse(Brushes.LightSteelBlue, new RectangleF((float)AllTheHighFitnessIndividuals.ElementAt<double[]>(i)[0] - radius / 2, (float)AllTheHighFitnessIndividuals.ElementAt<double[]>(i)[1] - radius / 2, radius, radius));
                        else
                            g.FillEllipse(Brushes.Red, new RectangleF((float)AllTheHighFitnessIndividuals.ElementAt<double[]>(i)[0] - radius / 2, (float)AllTheHighFitnessIndividuals.ElementAt<double[]>(i)[1] - radius / 2, radius, radius));
                    }
                    g.DrawRectangle(Pens.DarkGreen, new Rectangle(MIN_X, MIN_Y, MAX_X - MIN_X, MAX_Y - MIN_Y));

                    g.Dispose();


                    //HighAliveIndex = 0;
                    //if (!deadMask[enn.HighestFitnessIndividualIndex])
                    //    HighAliveIndex = enn.HighestFitnessIndividualIndex;
                    //else
                    //{
                    //    double highAlive = -1;
                    //    for (int i = 0; i < robots.GetLength(0); ++i)
                    //    {
                    //        if (!deadMask[i] && (enn.Population[i].Fitness > highAlive || highAlive == -1))
                    //        {
                    //            highAlive = enn.Population[i].Fitness;
                    //            HighAliveIndex = i;
                    //        }
                    //    }
                    //}

                    P_IMG.BackgroundImage = robots[HighAliveIndex].GetSensorAsImage(P_IMG.Width, P_IMG.Height);

                    P_ENVI.BackgroundImage = b;

                });

                

                String outS = "Vel = (" + robots[HighAliveIndex].Velocity[0].ToString(".0000") + ", " + robots[HighAliveIndex].Velocity[1].ToString(".0000") + ")\nOri = " + robots[HighAliveIndex].Orientation.ToString(".0000") + "\n\ntrackPenalty = " + trackPenalties[HighAliveIndex].ToString() + "\nGetTraveledDistance = " + robots[HighAliveIndex].GetTraveledDistance.ToString(".0000") + "\nInsideTrackCounter = " + robots[HighAliveIndex].InsideTrackCounter.ToString()  + "\nGlobalTimer = " + (globalTimer.ElapsedMilliseconds / 1000.0).ToString(".0000");

                outS += "\n\nGetActualIndividual = " + HighAliveIndex.ToString() + "\nGetActualGeneration = " + enn.GetActualGeneration.ToString() + "\n\nHighestFitnessIndividual = " + enn.HighestFitnessIndividual.Fitness.ToString() + "\n";

                outS += "\nCheckPointCounter = " + robots[HighAliveIndex].CheckPointCounter.ToString() + "\n";

                for (int i = 0; i < enn.Population.GetLength(0); ++i)
                {
                    
                    outS += "Fitnes[" + i.ToString() + "] = " + enn.Population[i].Fitness.ToString("000.000") + "; f = " + robots[i].factor.ToString() + "\n";
                }

                this.SetText(outS);

                
                if(AllTheHighFitnessIndividuals.Count > 0)
                {
                    debugStr = "HighestFitnessIndividualsPerEpoch[" + highEver + "] = " + AllTheHighFitnessIndividuals.ElementAt<double[]>(highEver)[2].ToString() + "\n\n";
                    for (int i = 0; i < AllTheHighFitnessIndividuals.Count; ++i)
                    {
                        debugStr += "HighestFitnessIndividualsPerEpoch[" + i.ToString() + "] = " + AllTheHighFitnessIndividuals.ElementAt<double[]>(i)[2].ToString(".000") + "\n";
                    }
                }
                

                this.SetText2(debugStr);

                Thread.Sleep(20);

                if (P_ENVI.BackgroundImage != null)
                    P_ENVI.BackgroundImage.Dispose();



            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void DrawCurve(Point pos, Graphics g, int dir = 0)
        {
            Bitmap bitm = new Bitmap(100, 100);
            Graphics gra = Graphics.FromImage(bitm);
            PointF internalPoint = new PointF(0, 0);


            float angle = 0;

            if (dir == 1)
                angle = 90;
            else if (dir == 2)
                angle = 180;
            else if (dir == 3)
                angle = 270;
            else if (dir == 4)
                angle = 0;

            //gra.DrawRectangle(Pens.Olive, new Rectangle(0, 0, bitm.Width - 1, bitm.Height - 1));

            gra.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gra.TranslateTransform((float)bitm.Width / 2, (float)bitm.Height / 2);
            //Rotate.        
            gra.RotateTransform(angle);
            //Move image back.
            gra.TranslateTransform(-(float)bitm.Width / 2, -(float)bitm.Height / 2);

            //g.DrawRectangle(Pens.Black, new Rectangle(pos.X, pos.Y, 100, 100));
            //g.FillRectangle(Brushes.White, new Rectangle(pos.X, pos.Y, 100, 100));
            //g.DrawArc(TrackPen, new Rectangle(pos.X + 75, pos.Y + 75, 50, 50), 180, 90);
            //g.DrawArc(TrackPen, new Rectangle(pos.X + 20, pos.Y + 20, 160, 160), 180, 90);

            Point nPos = new Point(0, 0);

            gra.FillRectangle(Brushes.White, new Rectangle(0, 0, bitm.Width, bitm.Height));
            
            
            



            for(int i = 0; i < 5; ++i)
                gra.DrawArc(TrackPen2, new Rectangle(75 + (3 * (i + 1)), 75 + (3 * (i + 1)), 50, 50), 180 - 1, 90 + 2);
            gra.DrawArc(TrackPen, new Rectangle(75, 75, 50, 50), 180 - 1, 90 + 2);

            for (int i = 0; i < 14; ++i)
                gra.DrawArc(TrackPen2, new RectangleF(20 - (5 * (i + 1)), 20 - (5 * (i + 1)), 160 + (9.2f * (i + 1)), 160 + (9.2f * (i + 1))), 180 - 1, 90 + 2);

            gra.DrawArc(TrackPen, new Rectangle(20, 20, 160, 160), 180 - 1, 90 + 2);



            gra.Dispose();
            g.DrawImage(bitm, new PointF(pos.X, pos.Y));
        }
        public void DrawStraight(Point pos, Graphics g, int dir = 0)
        {
            Bitmap bitm = new Bitmap(100, 100);
            Graphics gra = Graphics.FromImage(bitm);
            PointF internalPoint = new PointF(0, 0);

            float angle = 0;

            if (dir == 1)
                angle = 90;
            else if (dir == 2)
                angle = 180;
            else if (dir == 3)
                angle = 270;
            else if (dir == 4)
                angle = 0;

            //gra.DrawRectangle(Pens.Olive, new Rectangle(0, 0, bitm.Width - 1, bitm.Height - 1));

            gra.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gra.TranslateTransform((float)bitm.Width / 2, (float)bitm.Height / 2);
            //Rotate.        
            gra.RotateTransform(angle);
            //Move image back.
            gra.TranslateTransform(-(float)bitm.Width / 2, -(float)bitm.Height / 2);

            //g.DrawRectangle(Pens.Black, new Rectangle(pos.X, pos.Y, 100, 100));
            //g.FillRectangle(Brushes.White, new Rectangle(pos.X, pos.Y, 100, 100));
            //g.DrawLine(TrackPen, new Point(pos.X + 20, pos.Y), new Point(pos.X + 20, pos.Y + 100));
            //g.DrawLine(TrackPen, new Point(pos.X + 75, pos.Y), new Point(pos.X + 75, pos.Y + 100));

            Point nPos = new Point(0, 0);
            gra.FillRectangle(Brushes.White, new Rectangle(nPos.X + 20, nPos.Y, 55, bitm.Height));
            gra.DrawLine(TrackPen, new Point(nPos.X + 20, nPos.Y), new Point(nPos.X + 20, nPos.Y + bitm.Height));
            gra.DrawLine(TrackPen, new Point(nPos.X + 75, nPos.Y), new Point(nPos.X + 75, nPos.Y + bitm.Height));

            //gra.DrawRectangle(Pens.Black, new Rectangle(0, 0, bitm.Width - 1, bitm.Height - 1));


            gra.Dispose();
            g.DrawImage(bitm, new PointF(pos.X, pos.Y));
        }

        public void DrawCurveCheckPoints(Point pos, Graphics g, int dir = 0)
        {

            Bitmap bitm = new Bitmap(100, 100);
            Graphics gra = Graphics.FromImage(bitm);
            PointF internalPoint = new PointF(0, 0);


            float angle = 0;

            if (dir == 1)
                angle = 90;
            else if (dir == 2)
                angle = 180;
            else if (dir == 3)
                angle = 270;
            else if (dir == 4)
                angle = 0;

            //gra.DrawRectangle(Pens.Olive, new Rectangle(0, 0, bitm.Width - 1, bitm.Height - 1));

            gra.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gra.TranslateTransform((float)bitm.Width / 2, (float)bitm.Height / 2);
            //Rotate.        
            gra.RotateTransform(angle);
            //Move image back.
            gra.TranslateTransform(-(float)bitm.Width / 2, -(float)bitm.Height / 2);

            //g.DrawRectangle(Pens.Black, new Rectangle(pos.X, pos.Y, 100, 100));
            //g.FillRectangle(Brushes.White, new Rectangle(pos.X, pos.Y, 100, 100));
            //g.DrawArc(TrackPen, new Rectangle(pos.X + 75, pos.Y + 75, 50, 50), 180, 90);
            //g.DrawArc(TrackPen, new Rectangle(pos.X + 20, pos.Y + 20, 160, 160), 180, 90);


            //gra.DrawArc(TrackPen, new Rectangle(75, 75, 50, 50), 180 - 1, 90 + 2);
            //gra.DrawArc(TrackPen, new Rectangle(20, 20, 160, 160), 180 - 1, 90 + 2);
            //gra.DrawEllipse(Pens.DarkBlue, new RectangleF(100 - 5, 100 - 5, 10, 10));

            //gra.DrawArc(TrackPen2, new Rectangle(20, 20, 160, 160), 180 - 1, 70);

            int n = 5;
            for (int i = 0; i <= n; ++i)
            {
                double fi = ((Math.PI / 2) / n) * i;
                PointF pA = new PointF(100 - (float)(23 * Math.Cos(fi)), 100 - (float)(23 * Math.Sin(fi)));
                PointF pB = new PointF(100 - (float)(80 * Math.Cos(fi)), 100 - (float)(80 * Math.Sin(fi)));
                gra.DrawLine(Pens.LightBlue, pA, pB);
            }

            gra.Dispose();
            g.DrawImage(bitm, new PointF(pos.X, pos.Y));
        }
        public void DrawStraightCheckPoints(Point pos, Graphics g, int dir = 0)
        {
            Bitmap bitm = new Bitmap(100, 100);
            Graphics gra = Graphics.FromImage(bitm);
            PointF internalPoint = new PointF(0, 0);

            float angle = 0;

            if (dir == 1)
                angle = 90;
            else if (dir == 2)
                angle = 180;
            else if (dir == 3)
                angle = 270;
            else if (dir == 4)
                angle = 0;

            //gra.DrawRectangle(Pens.Olive, new Rectangle(0, 0, bitm.Width - 1, bitm.Height - 1));

            gra.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gra.TranslateTransform((float)bitm.Width / 2, (float)bitm.Height / 2);
            //Rotate.        
            gra.RotateTransform(angle);
            //Move image back.
            gra.TranslateTransform(-(float)bitm.Width / 2, -(float)bitm.Height / 2);

            Point nPos = new Point(0, 0);


            int n = 5;
            for (int i = 0; i <= n; ++i)
            {
                gra.DrawLine(Pens.LightBlue, new Point(nPos.X + 20, nPos.Y + (100 / n) * i), new Point(nPos.X + 75, nPos.Y + (100 / n) * i));
            }



            //gra.DrawRectangle(Pens.Black, new Rectangle(0, 0, bitm.Width - 1, bitm.Height - 1));


            gra.Dispose();
            g.DrawImage(bitm, new PointF(pos.X, pos.Y));
        }

        public void DrawRobot(Point pos, Graphics g)
        {
            int xInc = 33;
            int RobotWidth = 30;
            int RobotHeight = 50;
            int SensorLength = 120;
            double FOV = 30;
            int n = 5;

            Point CentralSensorPoint = new Point(pos.X + xInc + RobotWidth / 2, pos.Y + 130 + RobotHeight / 2);

            g.FillRectangle(Brushes.DeepSkyBlue, new Rectangle(pos.X + xInc, pos.Y + 130, RobotWidth, RobotHeight));
            g.FillRectangle(Brushes.DarkSlateBlue, new RectangleF(pos.X + xInc + 5 * 2.5f, pos.Y + 130, 5, 15));
            g.FillRectangle(Brushes.DarkGray, new RectangleF(pos.X + xInc, pos.Y + 130 + 30, 5, 15));
            g.FillRectangle(Brushes.DarkGray, new RectangleF(pos.X + xInc + RobotWidth - 5, pos.Y + 130 + 30, 5, 15));

            for(int i = 0; i <= n*2; ++i)
            {
                double a = (FOV / (n)) * (i) + 180.0 + (90 - FOV);
                double l = SensorLength + SensorLength * (1 / Math.Cos((270 - a) * Math.PI / 180.0f) - 1);
                PointF endP = new PointF((float)(l * Math.Cos(a * Math.PI / 180.0f)) + CentralSensorPoint.X, (float)(l * (float)Math.Sin(a * Math.PI / 180.0f)) + CentralSensorPoint.Y);
                g.DrawLine(Pens.Red, CentralSensorPoint, endP);
                //MessageBox.Show("i = " + i.ToString() + "\na = " + a.ToString() + "\nl = " + l.ToString());
            }
            //g.DrawLine(Pens.Red, CentralSensorPoint, new PointF(pos.X + xInc + RobotWidth / 2, pos.Y + 130 + RobotHeight / 2 - SensorLength));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //double dou = 1.7;
            //MessageBox.Show(Math.Round(dou).ToString());
            enn = new ENN(ENNNi, ENNNh, ENNNo, ENNPopSize, ENNMutationRate);
            robots = new Robot[ENNPopSize];
            for(int i = 0; i < ENNPopSize; ++i)
            {
                robots[i] = new Robot(new PointF(500, 200), numberOfSensors, 50, 45);
            }

            r = new Random();
            P_ENVI = new NoFlickPanel();
            P_ENVI.Location = new Point(538, 10);
            P_ENVI.Size = new Size(800, 700);
            P_ENVI.Name = "P_ENVI";
            P_ENVI.BackColor = Color.Red;
            this.Controls.Add(P_ENVI);

            P_IMG= new NoFlickPanel();
            P_IMG.Location = new Point(15, 610);
            P_IMG.Size = new Size(517, 36);
            P_IMG.Name = "P_IMG";
            P_IMG.BackColor = Color.Red;
            this.Controls.Add(P_IMG);

            //robot = new Robot(new PointF(300, 200), 10, 70, 30);

            PB_ENVIROMENT.Visible = false;
            panel1.Visible = false;


            this.WindowState = FormWindowState.Maximized;


            //Bitmap b = new Bitmap(P_ENVI.Width, P_ENVI.Height);
            //Graphics g = Graphics.FromImage(b);

            ////g.FillRectangle(Brushes.LightGreen, new RectangleF(0, 0, b.Width, b.Height));

            ////Pen p = new Pen(Color.Black, 5);
            ////g.DrawRectangle(Pens.Black, new Rectangle(0, 0, 100, 100));
            ////g.DrawArc(p, new Rectangle(75, 75, 50, 50), 180, 90);
            ////g.DrawArc(p, new Rectangle(20, 20, 160, 160), 180, 90);
            //DrawCurve(0, new Point(0, 0), g);
            //DrawStraight(new Point(0, 100), g);
            //DrawCurve(0, new Point(100, 100), g);
            //DrawStraight(new Point(100, 200), g);
            //DrawCurve(0, new Point(200, 200), g);
            //DrawStraight(new Point(200, 300), g);
            //DrawCurve(0, new Point(300, 300), g);
            //DrawStraight(new Point(300, 400), g);

            ////g.DrawRectangle(Pens.Black, new Rectangle(30, 130, 30, 50));
            //g.FillRectangle(Brushes.DeepSkyBlue, new Rectangle(30, 130, 30, 50));
            //g.FillRectangle(Brushes.DarkSlateBlue, new RectangleF(30 + 5 * 2.5f, 130, 5, 15));
            //g.FillRectangle(Brushes.DarkGray, new RectangleF(30, 130 + 30, 5, 15));
            //g.FillRectangle(Brushes.DarkGray, new RectangleF(30 + 30 - 5, 130 + 30, 5, 15));


            //g.DrawLine(Pens.Red, new Point(30 + 15, 130 + 25), new Point(30 + 15, 130 + 25 - 100));

            ////DrawRobot(new Point(200, 200), g);


            ////g.DrawRectangle(Pens.Black, new Rectangle(0, 100, 100, 100));
            ////g.DrawLine(p, new Point(20, 100), new Point(20, 200));
            ////g.DrawLine(p, new Point(75, 100), new Point(75, 200));

            //g.Dispose();

            //P_ENVI.BackgroundImage = b;


            T = new Thread(ThreadAnimation);
            T.Start();


        }

        private void BT_EVOLVE_Click(object sender, EventArgs e)
        {
            try
            {
                int PopSize = int.Parse(TB_POP_SIZE.Text);
                double MutationRate = double.Parse(TB_MUT_RATE.Text);

                string Target = TB_TARGET.Text;
                int geneSize = Target.Length;

                ga = new GA(Target, PopSize, geneSize, MutationRate);

                T = new Thread(ThreadMethod);
                T.Start();
                T_UPDATE.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void T_UPDATE_Tick(object sender, EventArgs e)
        {
            
            DNA HighFitnessIndividual = ga.HighestFitnessIndividual;

            if (HighFitnessIndividual.GenesToString == ga.Target.ToString())
            {
                T.Abort();
                
                T_UPDATE.Stop();
                
            }
            L_HIGH.Text = "(" + HighFitnessIndividual.GenesToString + ")";

            RT_FEEDBACK0.Text = "Population(0, " + (ga.PopulationSize / 2).ToString() + "):\n";
            for (int i = 0; i < ga.PopulationSize / 2; ++i)
            {
                RT_FEEDBACK0.Text += ga.Population[i].GenesToString + "   Fitness = " + ga.Population[i].Fitness.ToString() + "\n";
            }

            RT_FEEDBACK1.Text = "Generation = " + ga.GetActualGeneration.ToString() + "\nc = " + c.ToString() + "\nHighFitness = " + HighFitnessIndividual.Fitness.ToString();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                T.Abort();
            }
            catch
            {

            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            RT_FEEDBACK1.Text = e.KeyChar.ToString();
            

            if (e.KeyChar.ToString() == "d")
                t[1] += 2;
            else if (e.KeyChar.ToString() == "a")
                t[1] += -2;

            if (e.KeyChar.ToString() == "w")
                t[0] += 0.4f;
            else if (e.KeyChar.ToString() == "s")
                t[0] -= 0.4f;

            
            //MessageBox.Show(t[0].ToString() + t[1].ToString());

            if (e.KeyChar.ToString() == "q")
            {
                t = new double[] { 0, 0 };
                robots[0].Reset();
            }
        }
    }

    class Robot
    {
        private PointF pos;
        private PointF PrevPos;
        private PointF initPos;
        private PointF vel;
        private PointF oriPoint;
        private int N;
        private int SensorLength;
        private double FOV;
        public double angularVel;

        private double[] SensorReadings;
        private PointF[] SensorPoints;
        private Color[] SensorPixels;

        private double orientation;
        private double initOrientation;
        private float[] wheelsVelocities;

        private double traveledDistance;

        private Stopwatch watch;
        private double lastAliveSeconds;

        private int checkPointCounter;

        private Point lastCheckPoint;

        public static double PENALTY_OUTSIDE_BORDER = 0.2;
        public static double PENALTY_BORDER = 0.4;
        public static double PENALTY_TIME = 0.5;
        public static double PENALTY_OUTSIDE_TRACK = 0.1;

        private long insideTrackCounter;
        public bool isWithingTheTrack;

        public static int RobotWidth = 30;
        public static int RobotHeight = 50;

        public int factor;
        



        public Robot(PointF _pos, int _N, int _SensorLength, double _FOV, double _initOrientation = 0)
        {
            initPos = _pos;
            pos = _pos;
            PrevPos = _pos;
            N = _N;
            SensorLength = _SensorLength;
            FOV = _FOV;

            wheelsVelocities = new float[2] { 0, 0};
            initOrientation = _initOrientation;
            orientation = _initOrientation;

            SensorReadings = new double[2 * N + 1];
            SensorPoints = new PointF[2 * N + 1];
            SensorPixels = new Color[2 * N + 1];

            traveledDistance = 0;
            watch = Stopwatch.StartNew();

            checkPointCounter = 0;
            lastCheckPoint = new Point((int)_pos.X, (int)_pos.Y);

            insideTrackCounter = 0;
            isWithingTheTrack = true;

            angularVel = 0;
        }

        public void StopStopWatch()
        {
            watch.Stop();
        }

        public long GetAliveTimeMillisencods()
        {
            return watch.ElapsedMilliseconds;
        }

        public double GetAliveTimeSeconds()
        {
            return (watch.ElapsedMilliseconds / 1000.0);
        }

        public void Reset()
        {
            pos = new PointF(initPos.X, initPos.Y);
            PrevPos = new PointF(initPos.X, initPos.Y);
            orientation = initOrientation;
            wheelsVelocities = new float[2] { 0, 0 };
            traveledDistance = 0;

            StopStopWatch();
            lastAliveSeconds = GetAliveTimeSeconds();
            watch = Stopwatch.StartNew();

            checkPointCounter = 0;
            lastCheckPoint = new Point((int)initPos.X, (int)initPos.Y);

            insideTrackCounter = 0;
            isWithingTheTrack = true;

            angularVel = 0;
        }

        public void CheckPoint(Bitmap bitm)
        {

            //173 216 230

            

            if(Math.Sqrt(Math.Pow(lastCheckPoint.X - pos.X, 2) + Math.Pow(lastCheckPoint.Y - pos.Y, 2)) >= 3)
            {
                lastCheckPoint.X = (int)pos.X;
                lastCheckPoint.Y = (int)pos.Y;


                //Form1.debugStr = "";
                int n = 5;
                int rayLength = 2;
                for (int i = 0; i <= n; ++i)
                {
                    double fi = (2 * Math.PI / n) * i;
                    Point A = new Point((int)(rayLength * Math.Cos(fi) + pos.X), (int)(rayLength * Math.Sin(fi) + pos.Y));
                    Color c = bitm.GetPixel(A.X, A.Y);
                    //Form1.debugStr += "c = (" + c.R.ToString() + ", " + c.G.ToString() + ", " + c.B.ToString() + ")\n";
                    if (c.R == 173 && c.G == 216 && c.B == 230)
                    {
                        ++checkPointCounter;
                        break;
                    }
                }

            }

            

            
        }

        public void ReadSensors(Bitmap bitm)
        {
            int xInc = (int)(SensorLength * (3.0f / 2.0f)) - 15;
            int yInc = (int)(SensorLength * (3.0f / 2.0f)) - 50 / 2;//130
            int RobotWidth = 30;
            int RobotHeight = 50;

            float ori = (float)(orientation * 180.0 / Math.PI) + 90;
            
            PointF CentralSensorPoint = new PointF(xInc + RobotWidth / 2, yInc + RobotHeight / 2);
            
            float radi = 5;
            PointF origin = new PointF(CentralSensorPoint.X + pos.X - (SensorLength * (3.0f / 2.0f)) - radi / 2, CentralSensorPoint.Y + pos.Y - (SensorLength * (3.0f / 2.0f)) - radi / 2 + 2.5f);
            for (int i = 0; i <= N * 2; ++i)
            {
                double a2 = (FOV / (N)) * (i) + ori + 180 + (90 - FOV);
                double l2 = SensorLength;// + SensorLength * (1 / Math.Cos((a2) * Math.PI / 180.0f) - 1);
                double a = (FOV / (N)) * (i) + 180.0 + (90 - FOV);
                double l = SensorLength + SensorLength * (1 / Math.Cos((270 - a) * Math.PI / 180.0f) - 1);

                PointF endP2 = new PointF((float)(l * Math.Cos(a2 * Math.PI / 180.0f)) + origin.X, (float)(l * (float)Math.Sin(a2 * Math.PI / 180.0f)) + origin.Y);

                if (endP2.X >= bitm.Width)
                    endP2.X = bitm.Width - 1;
                if (endP2.Y >= bitm.Height)
                    endP2.Y = bitm.Height - 1;

                SensorPoints[i] = endP2;
            }

            for (int i = 0; i < SensorPoints.GetLength(0); ++i)
            {
                SensorPixels[i] = bitm.GetPixel((int)SensorPoints[i].X, (int)SensorPoints[i].Y);
                
                SensorReadings[i] = (SensorPixels[i].R + SensorPixels[i].G + SensorPixels[i].B) / 3.0;
                SensorReadings[i] /= 255;

                //MessageBox.Show(SensorPixels[i].ToArgb().ToString());
                //MessageBox.Show("SensorPixels[" + i.ToString() + "] = (" + SensorPixels[i].R.ToString() + ", " + SensorPixels[i].G.ToString() + ", " + SensorPixels[i].B.ToString() + ")\nSensorReadings[" + i.ToString() + "] = " + SensorReadings[i].ToString());
            }
        }

        public void CheckOrientation(Graphics g, Bitmap bitm)
        {
            int m = 2;
            oriPoint = new PointF((float)((SensorPoints[N].X + pos.X * (Math.Pow(2, m) - 1)) / Math.Pow(2, m)), (float)((SensorPoints[N].Y + pos.Y * (Math.Pow(2, m) - 1)) / Math.Pow(2, m)));
            

            float len = 50;
            PointF[] beginPoint = new PointF[2] { new PointF(-1, 0), new PointF(-1, 0) };
            PointF[] endPoint = new PointF[2];
            double[] angle = new double[2];

            double[] borderCenter = new double[2] { 0, 0 };
            double[] borderOri = new double[2] { 0, 0 };

            if (pos.X > Form1.pc1.X && pos.Y < Form1.pc1.Y)
            {
                beginPoint[0] = new PointF(Form1.pc1.X, Form1.pc1.Y);
            }
            else if (pos.X > Form1.pc2.X && pos.Y > Form1.pc2.Y)
            {
                beginPoint[0] = new PointF(Form1.pc2.X, Form1.pc2.Y);
            }
            else if (pos.X < Form1.pc3.X && pos.Y > Form1.pc3.Y)
            {
                beginPoint[0] = new PointF(Form1.pc3.X, Form1.pc3.Y);
            }
            else if (pos.X < Form1.pc0.X && pos.Y < Form1.pc0.Y)
            {
                beginPoint[0] = new PointF(Form1.pc0.X, Form1.pc0.Y);
            }
            ///////////////////////////////
            if (oriPoint.X > Form1.pc1.X && oriPoint.Y < Form1.pc1.Y)
            {
                beginPoint[1] = new PointF(Form1.pc1.X, Form1.pc1.Y);
            }
            else if (oriPoint.X > Form1.pc2.X && oriPoint.Y > Form1.pc2.Y)
            {
                beginPoint[1] = new PointF(Form1.pc2.X, Form1.pc2.Y);
            }
            else if (oriPoint.X < Form1.pc3.X && oriPoint.Y > Form1.pc3.Y)
            {
                beginPoint[1] = new PointF(Form1.pc3.X, Form1.pc3.Y);
            }
            else if (oriPoint.X < Form1.pc0.X && oriPoint.Y < Form1.pc0.Y)
            {
                beginPoint[1] = new PointF(Form1.pc0.X, Form1.pc0.Y);
            }


            if (beginPoint[0].X != -1)
            {
                angle[0] = Math.Atan2(beginPoint[0].Y - pos.Y, beginPoint[0].X - pos.X) - Math.PI;
                endPoint[0] = new PointF(beginPoint[0].X + 2 * len * (float)Math.Cos(angle[0]), beginPoint[0].Y + 2 * len * (float)Math.Sin(angle[0]));
            }
            else
            {
                angle[0] = 0;
                beginPoint[0] = new PointF(pos.X, pos.Y + len);
                endPoint[0] = new PointF(pos.X, pos.Y - len);
            }

            if (beginPoint[1].X != -1)
            {
                angle[1] = Math.Atan2(beginPoint[1].Y - oriPoint.Y, beginPoint[1].X - oriPoint.X) - Math.PI;
                endPoint[1] = new PointF(beginPoint[1].X + 2 * len * (float)Math.Cos(angle[1]), beginPoint[1].Y + 2 * len * (float)Math.Sin(angle[1]));
            }
            else
            {
                angle[1] = 0;
                beginPoint[1] = new PointF(oriPoint.X, oriPoint.Y + len);
                endPoint[1] = new PointF(oriPoint.X, oriPoint.Y - len);
            }
            ////////////////////////////////

            float stepSize = 0.1f;
            float maxBorder = 100;
            borderCenter[0] = stepSize;
            borderCenter[1] = stepSize;
            borderOri[0] = stepSize;
            borderOri[1] = stepSize;

            PointF[] centerBorders = new PointF[2];
            PointF[] oriBorders = new PointF[2];

            for (int i = 0; i < borderCenter.GetLength(0); ++i)
            {
                PointF v = new PointF(beginPoint[0].X - endPoint[0].X, beginPoint[0].Y - endPoint[0].Y);
                PointF u = new PointF(v.X / (float)Math.Sqrt(v.X * v.X + v.Y * v.Y), v.Y / (float)Math.Sqrt(v.X * v.X + v.Y * v.Y));

                while (true)
                {
                    PointF borderP;

                    borderP = new PointF(pos.X + (float)borderCenter[i] * u.X * (1 - 2 * i), pos.Y + (float)borderCenter[i] * u.Y * (1 - 2 * i));
                    Color c = bitm.GetPixel((int)Math.Round(borderP.X), (int)Math.Round(borderP.Y));
                    if (((c.R + c.G + c.B) / 3) < 20)//BORDER!
                    {
                        centerBorders[i] = borderP;
                        break;
                    }
                    borderCenter[i] += stepSize;
                    if (borderCenter[i] > maxBorder)
                    {
                        centerBorders[i] = borderP;
                        break;
                    }
                        
                }
            }

            for (int i = 0; i < borderCenter.GetLength(0); ++i)
            {
                PointF v = new PointF(beginPoint[1].X - endPoint[1].X, beginPoint[1].Y - endPoint[1].Y);
                PointF u = new PointF(v.X / (float)Math.Sqrt(v.X * v.X + v.Y * v.Y), v.Y / (float)Math.Sqrt(v.X * v.X + v.Y * v.Y));

                while (true)
                {
                    PointF borderP;

                    borderP = new PointF(oriPoint.X + (float)borderOri[i] * u.X * (1 - 2 * i), oriPoint.Y + (float)borderOri[i] * u.Y * (1 - 2 * i));
                    Color c = bitm.GetPixel((int)Math.Round(borderP.X), (int)Math.Round(borderP.Y));
                    if (((c.R + c.G + c.B) / 3) < 20)//BORDER!
                    {
                        oriBorders[i] = borderP;
                        break;
                    }
                    borderOri[i] += stepSize;
                    if (borderOri[i] > maxBorder)
                    {
                        oriBorders[i] = borderP;
                        break;
                    }

                }
            }

            float radi = 5;

            g.FillEllipse(Brushes.Red, new RectangleF(centerBorders[0].X - radi / 2, centerBorders[0].Y - radi / 2, radi, radi));
            g.FillEllipse(Brushes.Red, new RectangleF(centerBorders[1].X - radi / 2, centerBorders[1].Y - radi / 2, radi, radi));

            g.FillEllipse(Brushes.Red, new RectangleF(oriBorders[0].X - radi / 2, oriBorders[0].Y - radi / 2, radi, radi));
            g.FillEllipse(Brushes.Red, new RectangleF(oriBorders[1].X - radi / 2, oriBorders[1].Y - radi / 2, radi, radi));


            g.FillEllipse(Brushes.Black, new RectangleF(oriPoint.X - 5 / 2, oriPoint.Y - 5 / 2, 5, 5));
            g.DrawLine(Pens.Black, beginPoint[0], endPoint[0]);
            g.DrawLine(Pens.Black, beginPoint[1], endPoint[1]);
        }

        public Bitmap GetSensorAsImage(int w, int h)
        {
            Bitmap bitm = new Bitmap(w, h);

            for(int j = 0; j < h; ++j)
            {
                int c = 0;
                float t = (float)w / SensorReadings.GetLength(0);
                for (int i = 0; i < w; ++i)
                {
                    
                    if (i > t * (c + 1))
                    {
                        ++c;
                    }

                    int pix = (int)(SensorReadings[c] * 255);
                    bitm.SetPixel(i, j, Color.FromArgb(pix, pix, pix));
                    //bitm.SetPixel(i, j, SensorPixels[c]);
                    
                }
            }

            return bitm;
        }

        public double isOnTrackBorder(Bitmap bitm)
        {
            if (pos.Y >= bitm.Width)
                return 0;

            Color c = bitm.GetPixel((int)pos.X, (int)pos.Y);
            if((c.R > Color.Green.R - 10 && c.R < Color.Green.R + 10) && (c.G > Color.Green.G - 10 && c.G < Color.Green.G + 10) && (c.B > Color.Green.B - 10 && c.B < Color.Green.B + 10))
            {
                MessageBox.Show("HAHAHA");
                ++insideTrackCounter;
                isWithingTheTrack = true;
                return PENALTY_OUTSIDE_BORDER;//0.2
            }
            else
            {
                int gray = (c.R + c.G + c.B) / 3;
                if (gray >= 220)//white
                {
                    ++insideTrackCounter;
                    isWithingTheTrack = true;

                    return PENALTY_TIME;//0.5
                }
                else if (gray >= 50)
                {
                    isWithingTheTrack = false;
                    return PENALTY_OUTSIDE_TRACK;//0.1
                }
                else
                {
                    isWithingTheTrack = false;
                    return PENALTY_BORDER;//0.4
                }
            }
            
        }
        
        public void Show(Graphics g, int alpha = 255)
        {

            Bitmap bitm = new Bitmap(SensorLength * 3, SensorLength * 3);
            Graphics gra = Graphics.FromImage(bitm);
            PointF internalPoint = new PointF(0, 0);

            int xInc = bitm.Width / 2 - 15;
            int yInc = bitm.Height / 2 - 50 / 2;//130
            
            float x0 = internalPoint.X + xInc, y0 = internalPoint.Y + yInc;
            float x1 = internalPoint.X + xInc + 5 * 2.5f, y1 = internalPoint.Y + yInc;
            float x2 = internalPoint.X + xInc, y2 = internalPoint.Y + yInc + 30;
            float x3 = internalPoint.X + xInc + RobotWidth - 5, y3 = internalPoint.Y + yInc + 30;

            float ori = (float)(orientation * 180.0 / Math.PI) + 90;


            //gra.DrawRectangle(Pens.Olive, new Rectangle(0, 0, bitm.Width - 1, bitm.Height - 1));
            //gra.TranslateTransform(-bitm.Width / 2, -bitm.Height / 2);
            //gra.RotateTransform((float)orientation);

            gra.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            gra.TranslateTransform(((float)bitm.Width / 2), ((float)bitm.Height / 2));
            //Rotate.        
            gra.RotateTransform(ori);
            //Move image back.
            gra.TranslateTransform(-((float)bitm.Width / 2), -((float)bitm.Height / 2));

            //gra.DrawRectangle(Pens.DarkViolet, new Rectangle(0, 0, bitm.Width - 1, bitm.Height - 1));
            PointF CentralSensorPoint = new PointF(internalPoint.X + xInc + RobotWidth / 2, internalPoint.Y + yInc + RobotHeight / 2);

            SolidBrush b0 = new SolidBrush(Color.FromArgb(alpha, Color.DeepSkyBlue));
            SolidBrush b1 = new SolidBrush(Color.FromArgb(alpha, Color.DarkSlateBlue));
            SolidBrush b2 = new SolidBrush(Color.FromArgb(alpha, Color.DarkGray));

            gra.FillRectangle(b0, new RectangleF(x0, y0, RobotWidth, RobotHeight));
            gra.FillRectangle(b1, new RectangleF(x1, y1, 5, 15));
            gra.FillRectangle(b2, new RectangleF(x2, y2, 5, 15));
            gra.FillRectangle(b2, new RectangleF(x3, y3, 5, 15));

            Pen p0 = new Pen(Color.FromArgb(alpha, Color.Red));


            float radi = 5;
            PointF origin = new PointF(CentralSensorPoint.X + pos.X - bitm.Width / 2.0f - radi / 2, CentralSensorPoint.Y + pos.Y - bitm.Height / 2.0f - radi / 2 + 2.5f);
            for (int i = 0; i <= N * 2; ++i)
            {
                double a = (FOV / (N)) * (i) + 180.0 + (90 - FOV);
                double l = SensorLength + SensorLength * (1 / Math.Cos((270 - a) * Math.PI / 180.0f) - 1);
                PointF endP = new PointF((float)(l * Math.Cos(a * Math.PI / 180.0f)) + CentralSensorPoint.X, (float)(l * (float)Math.Sin(a * Math.PI / 180.0f)) + CentralSensorPoint.Y);

                if (i == 0 || i == N * 2)
                    gra.DrawLine(p0, CentralSensorPoint, endP);

                if(i == N)
                {
                    int m = 2;
                    PointF oriPoint = new PointF((float)((endP.X + CentralSensorPoint.X * (Math.Pow(2, m) - 1)) / Math.Pow(2, m)), (float)((endP.Y + CentralSensorPoint.Y * (Math.Pow(2, m) - 1)) / Math.Pow(2, m)));
                    gra.DrawLine(p0, CentralSensorPoint, oriPoint);
                }

                double a2 = (FOV / (N)) * (i) + ori + 180 + (90 - FOV);
                double l2 = SensorLength + SensorLength * (1 / Math.Cos((a2) * Math.PI / 180.0f) - 1);
                PointF endP2 = new PointF((float)(l * Math.Cos(a2 * Math.PI / 180.0f)) + origin.X, (float)(l * (float)Math.Sin(a2 * Math.PI / 180.0f)) + origin.Y);
                //SensorPoints[i] = endP2;
                //g.DrawLine(Pens.Black, origin, endP2);
                //g.FillEllipse(Brushes.Blue, endP2.Y * 2 + pos.X - radi / 2, endP2.X + pos.Y - bitm.Height / 2 - radi / 2, radi, radi);
                //g.DrawEllipse(Pens.DarkOrange, endP.X, endP.Y, 10, 10);
            }


            //gra.FillEllipse(Brushes.Black, bitm.Width / 2 - radi / 2, bitm.Height / 2 - radi / 2, radi, radi);
            gra.Dispose();
            g.DrawImage(bitm, new PointF(pos.X - bitm.Width / 2, pos.Y - bitm.Height / 2));
            g.FillEllipse(new SolidBrush(Color.FromArgb(alpha, Color.Black)), pos.X - radi / 2, pos.Y - radi / 2, radi, radi);
            //g.FillEllipse(Brushes.Blue, origin.X, origin.Y, radi, radi);

            //g.RotateTransform((float)orientation);

        }

        public void AddVelocity(PointF _vel)
        {
            vel = _vel;
        }

        public void ApplyVelocity(PointF _vel)
        {
            //vel = _vel;
            //pos.X += vel.X;
            //pos.Y += vel.Y;
        }

        public void ApplyVelocity(float wV = -1, double _orientation = -1)
        {
            //if(wV != -1)
            //    wheelsVelocities = wV;

            //if(_orientation != -1)
            //    orientation = _orientation;
            //angularVel += (wheelsVelocities / RobotHeight) * Math.Tan(orientation * Math.PI / 180.0);
            //vel.X = wheelsVelocities * (float)Math.Cos(Math.PI / 2 - angularVel);
            //vel.Y = wheelsVelocities * (float)Math.Sin(Math.PI / 2 - angularVel);

            //pos.X += vel.X;
            //pos.Y += vel.Y;

            //if (pos.X > Form1.MAX_X)
            //    pos.X = Form1.MAX_X;
            //else if (pos.X < Form1.MIN_X)
            //    pos.X = Form1.MIN_X;

            //if (pos.Y > Form1.MAX_Y)
            //    pos.Y = Form1.MAX_Y;
            //else if (pos.Y < Form1.MIN_Y)
            //    pos.Y = Form1.MIN_Y;

            //traveledDistance += Math.Sqrt(Math.Pow(pos.X - PrevPos.X, 2) + Math.Pow(pos.Y - PrevPos.Y, 2));
            //PrevPos.X = pos.X;
            //PrevPos.Y = pos.Y;
        }

        public void ApplyVelocity(double[] RobotControls)
        {
            wheelsVelocities[0] = (float)RobotControls[0];
            wheelsVelocities[1] = (float)RobotControls[1];

            //orientation = (float)RobotControls[1];

            orientation += (wheelsVelocities[1] - wheelsVelocities[0]) / RobotWidth;
            if (orientation >= 2 * Math.PI)
                orientation = orientation - 2 * Math.PI;
            else if (orientation <= - 2 * Math.PI)
                orientation = orientation + 2 * Math.PI;


            vel.X = ((wheelsVelocities[0] + wheelsVelocities[1]) / 2) * (float)Math.Cos(orientation);
            vel.Y = ((wheelsVelocities[0] + wheelsVelocities[1]) / 2) * (float)Math.Sin(orientation);

            //orientation *= 180.0 / Math.PI;

            //vel.X = wheelsVelocities * (float)Math.Cos(orientation * Math.PI / 180.0);
            //vel.Y = wheelsVelocities * (float)Math.Sin(orientation * Math.PI / 180.0);

            pos.X += vel.X;
            pos.Y += vel.Y;

            if (pos.X > Form1.MAX_X)
                pos.X = Form1.MAX_X;
            else if (pos.X < Form1.MIN_X)
                pos.X = Form1.MIN_X;

            if (pos.Y > Form1.MAX_Y)
                pos.Y = Form1.MAX_Y;
            else if (pos.Y < Form1.MIN_Y)
                pos.Y = Form1.MIN_Y;

            double[] v = new double[] { pos.X - PrevPos.X, pos.Y - PrevPos.Y };
            double[] u = new double[] { v[0] / Math.Sqrt(v[0] * v[0] + v[1] * v[1]), v[1] / Math.Sqrt(v[0] * v[0] + v[1] * v[1]) };

            factor = 1;
            if ((u[0] < 0 && pos.Y < Form1.pc1.Y))// || u[0] > 0 && pos.Y > Form1.pc1.Y))
            {
                factor = -1;
            }
            if ((u[1] < 0 && pos.Y < Form1.pc1.Y - 50))// || u[0] > 0 && pos.Y > Form1.pc1.Y))
            {
                factor = -1;
            }


            traveledDistance += factor * Math.Sqrt(Math.Pow(pos.X - PrevPos.X, 2) + Math.Pow(pos.Y - PrevPos.Y, 2));
            PrevPos.X = pos.X;
            PrevPos.Y = pos.Y;
        }

        public PointF GetPosition
        {
            get
            {
                return pos;
            }
        }

        public PointF GetVelcity
        {
            get
            {
                return vel;
            }
        }

        public double Orientation
        {
            get
            {
                return orientation * 180.0 / Math.PI;
            }
            set
            {
                orientation = value;
            }
        }

        public float[] Velocity
        {
            get
            {
                return wheelsVelocities;
            }
            set
            {
                wheelsVelocities = value;
            }
        }

        public double GetTraveledDistance
        {
            get
            {
                return traveledDistance;
            }
        }

        public double GetLastAliveSeconds
        {
            get
            {
                return lastAliveSeconds;
            }
        }

        public double[] GetSensorInput
        {
            get
            {
                double[] s = new double[SensorReadings.GetLength(0)];
                for(int i = 0; i < s.GetLength(0); ++i)
                {
                    s[i] = SensorReadings[i];
                }
                return s;
            }
        }

        public int CheckPointCounter
        {
            get
            {
                return checkPointCounter;
            }
        }

        public long InsideTrackCounter
        {
            get
            {
                return insideTrackCounter;
            }
        }
    }

    class NoFlickPanel : Panel
    {
        public NoFlickPanel()
        {
            this.DoubleBuffered = true;
        }
    }
}
