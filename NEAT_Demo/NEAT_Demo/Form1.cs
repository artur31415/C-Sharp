using NEATbodepreto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NEAT_Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ANN annBB;

        NEAT neatBB;

        NoFlickPanel P_IMG;

        private void Form1_Load(object sender, EventArgs e)
        {
            //annBB = new ANN(3, 2);
            //double[] res = annBB.NetworkOutput(new double[] { 1, 0.5, 0});
            //MessageBox.Show("res[0] = " + res[0].ToString() + "\nres[1] = " + res[1].ToString());

            this.WindowState = FormWindowState.Maximized;

            P_IMG = new NoFlickPanel();
            P_IMG.Location = new Point(414, 12);
            P_IMG.Size = new Size(700, 500);
            P_IMG.Name = "P_IMG";
            P_IMG.BackColor = Color.FromArgb(0, 200, 0);
            this.Controls.Add(P_IMG);
            panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Ni = 2, No = 2;
            double[,] dataSet = new double[,]    {
                                                    { 0, 0, 1, 1},
                                                    { 0, 1, 1, 0},
                                                    { 1, 0, 0, 1},
                                                    { 1, 1, 0, 0}
                                                };

            double _MutationRateWeight = 0.2;
            double _MutationRateLink = 0.08;
            double _MutationRateLinkToBias = 0.05;
            double _MutationRateNode = 0.05;
            double _MutationRateEnable = 0.2;
            double _MutationRateDisable = 0.01;

            int PopulationSize = 50;

            neatBB = new NEAT(PopulationSize, Ni, No, _MutationRateWeight, _MutationRateLink, _MutationRateLinkToBias, _MutationRateNode, _MutationRateEnable, _MutationRateDisable);


            ANN High = new ANN();

            while (neatBB.genCounter < 1000000)
            {
                for (int m = 0; m < dataSet.GetLength(0); ++m)
                {
                    for (int i = 0; i < PopulationSize; ++i)
                    {
                        double[] inputs = new double[Ni];
                        double[] desiredOutputs = new double[No];

                        for (int x = 0; x < dataSet.GetLength(1); ++x)
                        {
                            if (x < Ni)
                                inputs[x] = dataSet[m, x];
                            else
                                desiredOutputs[x - Ni] = dataSet[m, x];
                        }
                        neatBB.ComputeFitnes(i, inputs, desiredOutputs);
                    }
                }


                neatBB.ComputeOverallFitness();

                High = neatBB.HighestFitnessIndividual;
                if ((1.0 / High.error) <= 0.001)
                    break;

                neatBB.doStep();
                

            }

            String outS = "Generation = " + neatBB.genCounter.ToString() + "\n\nHighesFitnessIndividual:\n";
            
            for (int j = 0; j < High.synapses.Count; ++j)
            {
                outS += "synapses[" + j.ToString() + "](" + High.synapses.ElementAt(j).input_id.ToString() + ", " + High.synapses.ElementAt(j).output_id.ToString() + ") = " + High.synapses.ElementAt(j).weight.ToString("0.000") + "\n";
            }
            
            outS += "\nError = " + (1.0 / High.error).ToString();


            richTextBox1.Text = outS;

            richTextBox1.Text += "Neurons\n";
            for (int j = 0; j < High.neurons.Count; ++j)
            {
                richTextBox1.Text += "neurons[" + j.ToString() + "](" + High.neurons.ElementAt(j).id.ToString() + ") = " + High.neurons.ElementAt(j).value.ToString() + "\n";
            }

            richTextBox1.Text = "Using the Net:\n";
            for (int m = 0; m < dataSet.GetLength(0); ++m)
            {
                for (int i = 0; i < PopulationSize; ++i)
                {
                    double[] inputs = new double[Ni];
                    double[] outs = new double[No];

                    for (int x = 0; x < inputs.GetLength(0); ++x)
                    {
                        inputs[x] = dataSet[m, x];
                    }

                    outs = neatBB.population.ElementAt(i).NetworkOutput(inputs);

                    richTextBox1.Text += "res(";
                    for (int x = 0; x < inputs.GetLength(0); ++x)
                    {
                        richTextBox1.Text += inputs[x];
                        if (x < inputs.GetLength(0) - 1)
                            richTextBox1.Text += ", ";
                    }
                    richTextBox1.Text += ") = [";
                    for (int x = 0; x < outs.GetLength(0); ++x)
                    {
                        richTextBox1.Text += outs[x];
                        if (x < outs.GetLength(0) - 1)
                            richTextBox1.Text += ", ";
                    }
                    richTextBox1.Text += "]\n";
                }
                richTextBox1.Text += "\n========================\n";
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
