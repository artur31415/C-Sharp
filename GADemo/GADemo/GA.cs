using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EP
{
    
    class DNA
    {
        public char[] genes;
        private int geneSize;

        private double fitness;

        public static Random rnd;

        public char pickRandomGene()
        {
            int s = 97, b = 122;
            
            int v = rnd.Next(s - 1, b + 1);

            if (v == s - 1)
                v = 32;
            //if (i < 97)
            //    i = 97;
            //else if (i > 122)
            //    i = 122;

            return ((char)v);
        }

        public DNA(int _geneSize)
        {
            geneSize = _geneSize;
            genes = new char[geneSize];
            for(int i = 0; i < geneSize; ++i)
            {
                genes[i] = pickRandomGene();
            }
        }
        public DNA(char[] newGenes)
        {
            genes = new char[newGenes.GetLength(0)];
            
            geneSize = genes.GetLength(0);
            for (int i = 0; i < geneSize; ++i)
            {
                genes[i] = newGenes[i];
            }
            //this.CalFitness();
        }

        ~DNA()
        {

        }

        public string GenesToString
        {
            get
            {
                string s = "";
                for(int i = 0; i < genes.GetLength(0); ++i)
                {
                    s += genes[i];
                }
                return s;
            }
        }
        public double Fitness
        {
            get
            {
                return fitness;
            }
        }

        public void CalFitness(string t)
        {
            fitness = 0;
            for(int i = 0; i < genes.GetLength(0); ++i)
            {
                if (genes[i] == t[i])
                    ++fitness;
            }
            fitness = Math.Pow(2, (int)fitness) + 0.01;
            //fitness += 0.001;
        }

        

        public DNA Crossover(DNA partner)
        {
            DNA child = new DNA(genes);
            
            int mid = DNA.rnd.Next(geneSize);

            for (int i = 0; i < geneSize; ++i)
            {
                if (i >= mid)
                    child.genes[i] = partner.genes[i];
            }
            return child;
        }

        public void Mutation(double mutRate)
        {
            int index = DNA.rnd.Next(genes.GetLength(0));
            double chance = (double)DNA.rnd.Next(100) / 100.0;
            if (chance < mutRate)
            {
                genes[index] = pickRandomGene();
            }

            //for (int i = 0; i < geneSize; ++i)
            //{
            //    double chance = (double)DNA.rnd.Next(100) / 100.0;
            //    if (chance < mutRate)
            //    {
            //        //char p = genes[i];
            //        genes[i] = pickRandomGene();
            //        //break;
            //        //MessageBox.Show("MUTATION!");
            //        //MessageBox.Show(p + "\n" + genes[i]);
            //    }
            //}
        }


    }

    class DnaAnn
    {
        public double[] genes;
        private int geneSize;

        private double fitness;

        public static Random rnd;

        private double _Dist;
        private double _SecondsAlive;
        private double _TrackPenalty;

        public double pickRandomGene()
        {
            return rnd.Next(-240, 240) / 100.0;
        }

        public DnaAnn(int _geneSize)
        {
            geneSize = _geneSize;
            genes = new double[geneSize];
            for (int i = 0; i < geneSize; ++i)
            {
                genes[i] = pickRandomGene();
            }
        }
        public DnaAnn(double[] newGenes)
        {
            genes = new double[newGenes.GetLength(0)];

            geneSize = genes.GetLength(0);
            for (int i = 0; i < geneSize; ++i)
            {
                genes[i] = newGenes[i];
            }
            //this.CalFitness();
        }

        ~DnaAnn()
        {

        }

        public string GenesToString
        {
            get
            {
                string s = "";
                for (int i = 0; i < genes.GetLength(0); ++i)
                {
                    s += genes[i];
                }
                return s;
            }
        }
        public double Fitness
        {
            get
            {
                return fitness;
            }
        }

        public void CalFitness(double Dist, double SecondsAlive, double TrackPenalty, int CheckPoints)
        {
            double W0 = 0.2, W1 = 0, W3 = 0.05, W4 = 0;
            //if (SecondsAlive == 0)
            //    SecondsAlive = 100;

            fitness = (Dist * W0 + W1 * (1 / SecondsAlive) + W4 * CheckPoints) * W3;// * TrackPenalty;

            fitness = Math.Pow(4, fitness) + 0.01;

            _Dist = Dist;
            _SecondsAlive = SecondsAlive;
            _TrackPenalty = TrackPenalty;
        }


        public DnaAnn Crossover(DnaAnn partner)
        {
            DnaAnn child = new DnaAnn(genes);

            int mid = DnaAnn.rnd.Next(geneSize);

            for (int i = 0; i < geneSize; ++i)
            {
                if (i >= mid)
                    child.genes[i] = partner.genes[i];
            }
            return child;
        }

        public DnaAnn[] Crossover2(DnaAnn partner)
        {
            DnaAnn[] childs = new DnaAnn[] { new DnaAnn(geneSize), new DnaAnn(geneSize)};

            int mid = DnaAnn.rnd.Next(geneSize);

            for (int i = 0; i < geneSize; ++i)
            {
                if (i >= mid)
                {
                    childs[0].genes[i] = partner.genes[i];
                    childs[1].genes[i] = genes[i];
                }
                else
                {
                    childs[0].genes[i] = genes[i];
                    childs[1].genes[i] = partner.genes[i];
                }
                    
            }
            return childs;
        }

        public void Mutation(double mutRate)
        {
            //int index = DnaAnn.rnd.Next(genes.GetLength(0));
            //double chance = (double)DnaAnn.rnd.Next(100) / 100.0;
            //if (chance < mutRate)
            //{
            //    genes[index] = pickRandomGene();
            //}

            for (int i = 0; i < geneSize; ++i)
            {
                double chance = (double)DnaAnn.rnd.Next(100) / 100.0;
                if (chance < mutRate)
                {
                    //char p = genes[i];
                    double perturbance = DnaAnn.rnd.Next(-700, 700) / 1000.0;
                    genes[i] += perturbance;
                    //break;
                    //MessageBox.Show("MUTATION!");
                    //MessageBox.Show(p + "\n" + genes[i]);
                }
            }
        }


    }
    class GA
    {
        private DNA[] population;
        private int popSize;

        private string target;
        private double MutRate;
        private int genCounter;

        public GA(string _target, int _popSize, int _genSize, double _MutRate)
        {
            MutRate = _MutRate;
            target = _target;
            popSize = _popSize;
            population = new DNA[popSize];

            DNA.rnd = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < popSize; ++i)
            {
                population[i] = new DNA(_genSize);
            }
        }
        ~GA()
        {

        }

        public DNA HighestFitnessIndividual
        {
            get
            {
                double highF = 0;
                int highIndex = 0;

                for(int i = 0; i < popSize; ++i)
                {
                    if(population[i].Fitness > highF)
                    {
                        highF = population[i].Fitness;
                        highIndex = i;
                    }
                }
                return population[highIndex];
            }
        }

        public int GetActualGeneration
        {
            get
            {
                return genCounter;
            }
        }

        public int PopulationSize
        {
            get
            {
                return popSize;
            }
        }

        public DNA[] Population
        {
            get
            {
                return population;
            }
        }

        public string Target
        {
            get
            {
                return target;
            }

        }
        public void doStep()
        {
            //compute teh fitness of everyone
            for (int i = 0; i < popSize; ++i)
            {
                population[i].CalFitness(target);
            }
            //Crossover of the fitest
            double HighesFitness = 0;

            for (int i = 0; i < popSize; ++i)
            {
                if (population[i].Fitness > HighesFitness)
                    HighesFitness = population[i].Fitness;
            }

            //Random r = new Random(DateTime.Now.Millisecond);
            DNA[] newPopulation = new DNA[popSize];
            //int[] usedParents = new int[popSize];

            //for (int i = 0; i < popSize; ++i)
            //{
            //    usedParents[i] = -1;
            //}

            for (int i = 0; i < popSize; ++i)
            {
                while (true)
                {
                    int r0 = DNA.rnd.Next(popSize);
                    
                    //while(true)
                    //{
                        

                    //    if(usedParents[r0] == -1)
                    //    {
                    //        break;
                    //    }
                    //    r0 = DNA.rnd.Next(popSize);
                    //}

                    int r1 = DNA.rnd.Next((int)(HighesFitness * 100));

                    if ((double)r1 / 100.0 < population[r0].Fitness)
                    {
                        //MessageBox.Show("r0 = " + r0.ToString() + "\nr1 = " + r1.ToString());
                        newPopulation[i] = population[i].Crossover(population[r0]);
                        //usedParents[r0] = 1;
                        break;
                    }
                    else
                        continue;

                }
            }

            for (int i = 0; i < popSize; ++i)
            {
                population[i] = newPopulation[i];
                population[i].CalFitness(target);
            }
                


            //Mutation
            for (int i = 0; i < popSize; ++i)
            {
                population[i].Mutation(MutRate);
            }
            ++genCounter;
        }
    }

    class ENN : MLPbodepreto
    {
        private DnaAnn[] population;
        private int popSize;
        
        private double MutRate;
        private int genCounter;

        private int actualIndividual;

        public ENN(int _Ni, int _Nh, int _No, int _popSize, double _MutRate)
        {
            //INITIALIZE ANN VARIABLES
            this.num_input = _Ni;
            this.num_hidden = _Nh;
            this.num_output = _No;
            this.activation_function = "sigmoid";
            this.output_activation_function = "sigmoid";

            this.ih_w = new double[num_input, num_hidden];
            this.h_bias = new double[num_hidden];

            this.ho_w = new double[num_hidden, num_output];
            this.o_bias = new double[num_output];

            this.inputs = new double[num_input];
            this.outputs = new double[num_output];

            this.h_out = new double[num_hidden];
            this.o_out = new double[num_output];
            //INITIALIZE GA VARIABLES
            MutRate = _MutRate;
            popSize = _popSize;

            actualIndividual = 0;

            population = new DnaAnn[popSize];

            DnaAnn.rnd = new Random(DateTime.Now.Millisecond);

            

            for (int i = 0; i < popSize; ++i)
            {
                population[i] = new DnaAnn(num_input * num_hidden + num_hidden + num_hidden * num_output + num_output);
            }


        }
        ~ENN()
        {

        }

        public void SetBrain()
        {
            DnaAnn brain = population[actualIndividual];
            int count = 0;
            for(int i = 0; i < ih_w.GetLength(0); ++i)
            {
                for (int j = 0; j < ih_w.GetLength(1); ++j)
                {
                    ih_w[i, j] = brain.genes[count];
                    ++count;
                }
            }

            for (int i = 0; i < h_bias.GetLength(0); ++i)
            {
                h_bias[i] = brain.genes[count];
                ++count;
            }

            //---------------------------------------------------------

            for (int i = 0; i < ho_w.GetLength(0); ++i)
            {
                for (int j = 0; j < ho_w.GetLength(1); ++j)
                {
                    ho_w[i, j] = brain.genes[count];
                    ++count;
                }
            }

            for (int i = 0; i < o_bias.GetLength(0); ++i)
            {
                o_bias[i] = brain.genes[count];
                ++count;
            }
        }

        public void SetBrain(int _index)
        {
            DnaAnn brain = population[_index];
            int count = 0;
            for (int i = 0; i < ih_w.GetLength(0); ++i)
            {
                for (int j = 0; j < ih_w.GetLength(1); ++j)
                {
                    ih_w[i, j] = brain.genes[count];
                    ++count;
                }
            }

            for (int i = 0; i < h_bias.GetLength(0); ++i)
            {
                h_bias[i] = brain.genes[count];
                ++count;
            }

            //---------------------------------------------------------

            for (int i = 0; i < ho_w.GetLength(0); ++i)
            {
                for (int j = 0; j < ho_w.GetLength(1); ++j)
                {
                    ho_w[i, j] = brain.genes[count];
                    ++count;
                }
            }

            for (int i = 0; i < o_bias.GetLength(0); ++i)
            {
                o_bias[i] = brain.genes[count];
                ++count;
            }
        }

        public void EvaluateActualIndividual(double Dist, double SecondsAlive, double TrackPenalty, int CheckPoints)
        {
            population[actualIndividual].CalFitness(Dist, SecondsAlive, TrackPenalty, CheckPoints);
            ++actualIndividual;
            if(actualIndividual >= popSize)
            {
                doStep();
                actualIndividual = 0;
            }
        }

        public void EvaluateIndividual(int _index, double Dist, double SecondsAlive, double TrackPenalty, int CheckPoints)
        {
            population[_index].CalFitness(Dist, SecondsAlive, TrackPenalty, CheckPoints);
        }

        public DnaAnn HighestFitnessIndividual
        {
            get
            {
                double highF = 0;
                int highIndex = 0;

                for (int i = 0; i < popSize; ++i)
                {
                    if (population[i].Fitness > highF)
                    {
                        highF = population[i].Fitness;
                        highIndex = i;
                    }
                }
                return population[highIndex];
            }
        }

        public int HighestFitnessIndividualIndex
        {
            get
            {
                double highF = 0;
                int highIndex = 0;

                for (int i = 0; i < popSize; ++i)
                {
                    if (population[i].Fitness > highF)
                    {
                        highF = population[i].Fitness;
                        highIndex = i;
                    }
                }
                return highIndex;
            }
        }

        public int GetActualGeneration
        {
            get
            {
                return genCounter;
            }
        }

        public int PopulationSize
        {
            get
            {
                return popSize;
            }
        }

        public int GetActualIndividual
        {
            get
            {
                return actualIndividual;
            }
        }
        public DnaAnn[] Population
        {
            get
            {
                return population;
            }
        }
        
        public int[] SortPopulationByFitnes()
        {
            int[] res = new int[population.GetLength(0)];
            DnaAnn[] sortedPopulation = new DnaAnn[population.GetLength(0)];

            for(int i = 0; i < res.GetLength(0); ++i)
            {
                res[i] = i;
                sortedPopulation[i] = population[i];
            }
            

            

            for (int i = 0; i < res.GetLength(0); ++i)
            {
                DnaAnn prev;
                int indPrev;
                int beginIndex = -1;

                for (int j = 0; j < i; ++j)
                {
                    if(sortedPopulation[i].Fitness > sortedPopulation[j].Fitness)
                    {
                        beginIndex = j;
                        break;
                    }
                }

                if(beginIndex != -1)
                {
                    prev = sortedPopulation[beginIndex];
                    sortedPopulation[beginIndex] = sortedPopulation[i];
                    indPrev = res[beginIndex];
                    res[beginIndex] = i;

                    for (int j = beginIndex + 1; j <= i; ++j)
                    {
                        DnaAnn p = sortedPopulation[j];
                        sortedPopulation[j] = prev;
                        prev = p;

                        int indP = res[j];
                        res[j] = indPrev;
                        indPrev = indP;
                    }
                }
                

            }

            //string debugs = "";
            //for (int i = 0; i < res.GetLength(0); ++i)
            //{
            //    debugs += "res[" + i.ToString() + "] = " + res[i].ToString() + "        sorted[" + i.ToString() + "] = " + sortedPopulation[i].Fitness.ToString(".00") + "\n";
            //}
            //MessageBox.Show(debugs);




            return res;
        }

        public void doStep()//ONYL WHEN ALL THE INDIVIDUALS WERE TESTED!    
        {
            //compute teh fitness of everyone
            //for (int i = 0; i < popSize; ++i)
            //{
            //    population[i].CalFitness(Dist, SecondsAlive);
            //}
            //Crossover of the fitest
            double HighesFitness = HighestFitnessIndividual.Fitness;

            //Random r = new Random(DateTime.Now.Millisecond);
            DnaAnn[] newPopulation = new DnaAnn[popSize];
            //int[] usedParents = new int[popSize];

            //for (int i = 0; i < popSize; ++i)
            //{
            //    usedParents[i] = -1;
            //}

            /////////////////////////////////////////////////////////////////////////////////////////////
            double Po = 0.8;
            double Pe = 0.1;
            int[] sortedIndexes = SortPopulationByFitnes();
            int eliteNumber = (int)(popSize * Pe);//10%
            DnaAnn[] elite = new DnaAnn[eliteNumber];
            int eliteOffspringSize = (int)(popSize * Po);//80%

            for(int i = 0; i < eliteNumber; ++i)
            {
                elite[i] = population[sortedIndexes[i]];
            }

            HighesFitness = population[sortedIndexes[eliteNumber]].Fitness;

            int count = 0;
            for (int i = 0; i < elite.GetLength(0); ++i)
            {
                for (int j = 0; j < (int)(Po / Pe) / 2; ++j)
                {
                    while (true)
                    {
                        int randomParent = DnaAnn.rnd.Next(elite.GetLength(0), eliteOffspringSize);
                        //MessageBox.Show(randomParent.ToString());

                        double  r1 = (DnaAnn.rnd.NextDouble() * HighesFitness);
                        //int r1 = DnaAnn.rnd.Next((int)(HighesFitness * 100));

                        if ((double)r1 < population[randomParent].Fitness)
                        {
                            DnaAnn[] childs = elite[i].Crossover2(population[sortedIndexes[randomParent]]);
                            newPopulation[count] = childs[0];
                            newPopulation[count + 1] = childs[1];
                            count += 2;
                            break;
                        }
                        else
                            continue;

                    }
                }
            }

            int imigrationSize = popSize - eliteOffspringSize;

            for(int i = 0; i < imigrationSize; ++i)
            {
                newPopulation[count] = new DnaAnn(elite[0].genes.GetLength(0));
                ++count;
            }

            //MessageBox.Show("popSize = " + popSize.ToString() + "\neliteNumber = " + eliteNumber.ToString() + "\neliteOffspringSize = " + eliteOffspringSize.ToString() + "\ncount = " + count.ToString() + "\nimigrationSize = " + imigrationSize.ToString());

            //////////////////////////////////////////////////////////////////////////////////////////////
            //for (int i = 0; i < popSize; ++i)
            //{
            //    while (true)
            //    {
            //        int r0 = DnaAnn.rnd.Next(popSize);
            //        if (r0 == i)
            //            continue;

            //        //while(true)
            //        //{


            //        //    if(usedParents[r0] == -1)
            //        //    {
            //        //        break;
            //        //    }
            //        //    r0 = DNA.rnd.Next(popSize);
            //        //}

            //        int r1 = DnaAnn.rnd.Next((int)(HighesFitness * 100));

            //        if ((double)r1 / 100.0 < population[r0].Fitness)
            //        {
            //            //MessageBox.Show("r0 = " + r0.ToString() + "\nr1 = " + r1.ToString());
            //            newPopulation[i] = population[i].Crossover(population[r0]);
            //            //usedParents[r0] = 1;
            //            break;
            //        }
            //        else
            //            continue;

            //    }
            //}
            //////////////////////////////////////////////////////////////////////////////////////////////

            for (int i = 0; i < popSize; ++i)
            {
                population[i] = newPopulation[i];
                //population[i].CalFitness(Dist, SecondsAlive);
            }



            //Mutation
            for (int i = 0; i < popSize; ++i)
            {
                population[i].Mutation(MutRate);
            }
            ++genCounter;
        }
    }

    //---------------------------------------------------------------------------------------//
    //                              MULTILAYER PERCEPTRON                                    //
    //---------------------------------------------------------------------------------------//
    class MLPbodepreto
    {


        /// <summary>
        /// Variables
        /// </summary>
        protected int num_input, num_hidden, num_output, num_datasets, num_Epoch;
        protected int Epoch, Data_indexer;
        protected int[] Data_used;
        protected int Actual_data;
        protected double eta, alpha, aError;
        protected double min_Error;
        public double[,] ih_w, ho_w, //weights
                           delta_ih_w, delta_ho_w,//delta of hidden and output layer weights
                           prev_delta_ih_w, prev_delta_ho_w,//previous deltas of the weights
                           ih_local_input, ho_local_input;
        public double[] h_bias, o_bias,//bias
                           h_out, o_out, //output of each neuron
                           oGrad, hGrad, //gradient of each layer
                           delta_h_bias, delta_o_bias, //delta of hidden and output layer biases
                           prev_delta_h_bias, prev_delta_o_bias; //previous deltas of the biases

        protected double[] inputs, outputs;
        protected double[,] lError;
        protected double[,] NetOutputs;


        protected const string ANN_ACTIVATION_SIGMOID = "sigmoid",
                      ANN_ACTIVATION_LINEAR = "linear",
                      ANN_ACTIVATION_BIPOLAR_SIGMOID = "bipolar sigmoid";
        protected string activation_function,
                output_activation_function;
        protected double[,] Data;
        public bool finished, MinErrorAchieved, ToUseTheNet, ToDebug;
        public int NumberOfTrainings;
        public Random rnd;

        public MLPbodepreto()
        {
        }
        ~MLPbodepreto()
        {
        }
        public MLPbodepreto(double[,] TrainingData, int Ni, int Nh, int No, int Nd, double MinimalError, int MaxEpoch, double LearningRate, double Momentum, string hiddenActivationFunction = ANN_ACTIVATION_SIGMOID, string outputActivationFunction = ANN_ACTIVATION_SIGMOID)
        {
            num_input = Ni;
            num_hidden = Nh;
            num_output = No;
            num_datasets = Nd;
            num_Epoch = MaxEpoch;
            eta = LearningRate;
            alpha = Momentum;
            min_Error = MinimalError;
            activation_function = hiddenActivationFunction;
            output_activation_function = outputActivationFunction;

            Epoch = 0;
            Data_indexer = 0;
            Actual_data = 0;
            finished = false;
            MinErrorAchieved = false;
            ToUseTheNet = false;
            ToDebug = false;
            NumberOfTrainings = 0;
            aError = 0;
            //inisialize the vectors
            Data_used = new int[num_datasets];
            lError = new double[num_datasets, num_output];
            NetOutputs = new double[num_datasets, num_output];

            inputs = new double[num_input];
            outputs = new double[num_output];
            Data = new double[num_datasets, num_input + num_output];

            //Data = TrainingData;
            for (int i = 0; i < num_datasets; ++i)
            {
                for (int j = 0; j < num_input + num_output; ++j)
                {
                    Data[i, j] = TrainingData[i, j];
                }
            }
            ih_w = new double[num_input, num_hidden];
            ho_w = new double[num_hidden, num_output];
            delta_ih_w = new double[num_input, num_hidden];
            delta_ho_w = new double[num_hidden, num_output];
            prev_delta_ih_w = new double[num_input, num_hidden];
            prev_delta_ho_w = new double[num_hidden, num_output];
            ih_local_input = new double[num_input, num_hidden];
            ho_local_input = new double[num_hidden, num_output];

            h_bias = new double[num_hidden];
            o_bias = new double[num_output];
            h_out = new double[num_hidden];
            o_out = new double[num_output];
            oGrad = new double[num_output];
            hGrad = new double[num_hidden];
            delta_h_bias = new double[num_hidden];
            delta_o_bias = new double[num_output];
            prev_delta_h_bias = new double[num_hidden];
            prev_delta_o_bias = new double[num_output];

            for (int i = 0; i < num_datasets; ++i)
                Data_used[i] = -1;
            //Randomly initialize weights
            rnd = new Random();

            for (int i = 0; i < num_input; ++i)
            {
                for (int j = 0; j < num_hidden; ++j)
                {
                    ih_w[i, j] = ((double)(rnd.Next(-240, 240)) / num_input) / 100.0;
                    if (i == 0)
                    {
                        h_bias[j] = ((double)(rnd.Next(-240, 240)) / num_input) / 100.0;
                    }
                }
            }
            for (int i = 0; i < num_hidden; ++i)
            {
                for (int j = 0; j < num_output; ++j)
                {
                    ho_w[i, j] = ((double)(rnd.Next(-240, 240)) / num_hidden) / 100.0;
                    if (i == 0)
                    {
                        o_bias[j] = ((double)(rnd.Next(-240, 240)) / num_hidden) / 100.0;
                    }
                }
            }

        }


        //Get and Set functions

        public double[,] InputHiddenWeights
        {
            get
            {
                double[,] res = new double[ih_w.GetLongLength(0), ih_w.GetLongLength(1)];
                for(int i = 0; i < res.GetLongLength(0); ++i)
                {
                    for (int j= 0; j < res.GetLongLength(1); ++j)
                    {
                        res[i, j] = ih_w[i, j];
                    }
                }
                return res;
            }
            set
            {
                ih_w = new double[value.GetLongLength(0), value.GetLongLength(1)];
                for (int i = 0; i < ih_w.GetLongLength(0); ++i)
                {
                    for (int j = 0; j < ih_w.GetLongLength(1); ++j)
                    {
                        ih_w[i, j] = value[i, j];
                    }
                }
            }
        }

        public double[] HiddenBiases
        {
            get
            {
                double[] res = new double[h_bias.GetLongLength(0)];
                for (int i = 0; i < h_bias.GetLongLength(0); ++i)
                {
                    res[i] = h_bias[i];
                }
                return res;
            }
            set
            {
                h_bias = new double[value.GetLongLength(0)];
                for (int i = 0; i < h_bias.GetLongLength(0); ++i)
                {
                    h_bias[i] = value[i];
                }
            }
        }

        public double[,] HiddenOutputWeights
        {
            get
            {
                double[,] res = new double[ho_w.GetLongLength(0), ho_w.GetLongLength(1)];
                for (int i = 0; i < res.GetLongLength(0); ++i)
                {
                    for (int j = 0; j < res.GetLongLength(1); ++j)
                    {
                        res[i, j] = ho_w[i, j];
                    }
                }
                return res;
            }
            set
            {
                ho_w = new double[value.GetLongLength(0), value.GetLongLength(1)];
                for (int i = 0; i < ho_w.GetLongLength(0); ++i)
                {
                    for (int j = 0; j < ho_w.GetLongLength(1); ++j)
                    {
                        ho_w[i, j] = value[i, j];
                    }
                }
            }
        }

        public double[] OutputBiases
        {
            get
            {
                double[] res = new double[o_bias.GetLongLength(0)];
                for (int i = 0; i < o_bias.GetLongLength(0); ++i)
                {
                    res[i] = o_bias[i];
                }
                return res;
            }
            set
            {
                o_bias = new double[value.GetLongLength(0)];
                for (int i = 0; i < o_bias.GetLongLength(0); ++i)
                {
                    o_bias[i] = value[i];
                }
            }
        }

        public void GetWeightsAndBiases(char layer, double[] outputW)//returns a 2D array with the weights 0>ih and 1>ho
        {
            int cont = 0;
            //double[] temp;
            if (layer == 'h')
            {
                //temp = new double[num_input * num_hidden + num_hidden];
                for (int i = 0; i < num_hidden; ++i)
                {
                    for (int j = 0; j < num_input; ++j)
                    {
                        outputW[cont] = ih_w[j, i];
                        ++cont;
                    }
                    outputW[cont] = h_bias[i];
                    ++cont;
                }

            }
            else
            {
                //temp = new double[num_hidden * num_output + num_output];
                for (int i = 0; i < num_output; ++i)
                {
                    for (int j = 0; j < num_hidden; ++j)
                    {
                        outputW[cont] = ho_w[j, i];
                        ++cont;
                    }
                    outputW[cont] = o_bias[i];
                    ++cont;
                }
            }
        }
        public void SetWeightsAndBiases(double[] setW, char layer)
        {
            int c = 0;
            if (layer == 'h')
            {
                for (int i = 0; i < num_hidden; ++i)
                {
                    for (int j = 0; j < num_input; ++j)
                    {
                        ih_w[j, i] = setW[c];
                        ++c;
                    }
                    h_bias[i] = setW[c];
                    ++c;
                }
            }
            else
            {
                for (int i = 0; i < num_output; ++i)
                {
                    for (int j = 0; j < num_hidden; ++j)
                    {
                        ho_w[j, i] = setW[c];
                        ++c;
                    }
                    o_bias[i] = setW[c];
                    ++c;
                }
            }
        }
        public bool IsMinError
        {
            get
            {
                if (aError >= 0 && aError <= min_Error)
                    return true;
                else
                    return false;
            }
        }
        public double SetMinError
        {
            set
            {
                min_Error = value;
            }
        }
        public int SetMaxEpoch
        {
            set
            {
                num_Epoch = value;
            }
        }
        public int GetEpochCounter
        {
            get
            {
                return Epoch;
            }
        }
        public int GetNumberOfInputs
        {
            get
            {
                return num_input;
            }
        }
        public int GetNumberOfOutputs
        {
            get
            {
                return num_output;
            }
        }
        public bool IsFinished
        {
            get
            {
                return finished;
            }
        }
        public double GetError
        {
            get
            {
                return aError;
            }
        }
        public string GetWeights(char layer, InputLanguage language)
        {
            string output = "";
            if (layer == 'h')
            {
                for (int i = 0; i < num_hidden; ++i)
                {
                    for (int j = 0; j < num_input; ++j)
                    {
                        output += "ih_w[" + j.ToString(language.Culture) + "][" + i.ToString(language.Culture) + "] = " + ih_w[j, i].ToString(language.Culture) + "\n";
                    }
                    output += "h_bias[" + i.ToString(language.Culture) + "] = " + h_bias[i].ToString(language.Culture) + "\n\n";
                }
            }
            else
            {
                for (int i = 0; i < num_output; ++i)
                {
                    for (int j = 0; j < num_hidden; ++j)
                    {
                        output += "ho_w[" + j.ToString(language.Culture) + "][" + i.ToString(language.Culture) + "] = " + ho_w[j, i].ToString(language.Culture) + "\n";
                    }
                    output += "o_bias[" + i.ToString(language.Culture) + "] = " + o_bias[i].ToString(language.Culture) + "\n\n";
                }
            }
            return output;
        }
        public string GetNetOutputs(int DataIndexer)
        {
            string outs = "";
            for (int j = 0; j < num_output; ++j)
            {
                outs += "NetOut[" + DataIndexer.ToString() + "][" + j.ToString() + "] = " + NetOutputs[DataIndexer, j].ToString() + "\n";
            }
            outs += "-----------------------------------\n";
            return outs;
        }

        //Main Functions
        public void InitializeWeights(int method = 1)
        {
            int temp = 0;
            if (method == 0)//everithing begins at zero
            {
                for (int i = 0; i < num_input; ++i)
                {
                    for (int j = 0; j < num_hidden; ++j)
                    {
                        ih_w[i, j] = 0;
                        if (temp == 0)
                            h_bias[j] = 0;
                    }
                    ++temp;
                }
                temp = 0;
                for (int i = 0; i < num_hidden; ++i)
                {
                    for (int j = 0; j < num_output; ++j)
                    {
                        ho_w[i, j] = 0;
                        if (temp == 0)
                            o_bias[j] = 0;
                    }
                    ++temp;
                }
            }
            else if (method == 1)//everything begins randonly at a certain range
            {
                for (int i = 0; i < num_input; ++i)
                {
                    for (int j = 0; j < num_hidden; ++j)
                    {
                        ih_w[i, j] = ((double)(rnd.Next(-240, 240)) / num_input) / 100.0;
                        if (temp == 0)
                            h_bias[j] = ((double)(rnd.Next(-240, 240)) / num_input) / 100.0;
                    }
                    ++temp;
                }
                temp = 0;
                for (int i = 0; i < num_hidden; ++i)
                {
                    for (int j = 0; j < num_output; ++j)
                    {
                        ho_w[i, j] = ((double)(rnd.Next(-240, 240)) / num_hidden) / 100.0;
                        if (temp == 0)
                            o_bias[j] = ((double)(rnd.Next(-240, 240)) / num_hidden) / 100.0;
                    }
                    ++temp;
                }
            }
            else if (method == 2)//everything begins fully randonly
            {
                for (int i = 0; i < num_input; ++i)
                {
                    for (int j = 0; j < num_hidden; ++j)
                    {
                        ih_w[i, j] = ((double)(rnd.Next(-300, 300))) / 100.0;
                        if (temp == 0)
                            h_bias[j] = ((double)(rnd.Next(-300, 300))) / 100.0;
                    }
                    ++temp;
                }
                temp = 0;
                for (int i = 0; i < num_hidden; ++i)
                {
                    for (int j = 0; j < num_output; ++j)
                    {
                        ho_w[i, j] = ((double)(rnd.Next(-300, 300))) / 100.0;
                        if (temp == 0)
                            o_bias[j] = ((double)(rnd.Next(-300, 300))) / 100.0;
                    }
                    ++temp;
                }
            }
        }
        public void InitializeVaraiables(int WeightsMethod = 1)
        {
            InitializeWeights(WeightsMethod);
            for (int i = 0; i < num_datasets; ++i)
                Data_used[i] = -1;

            Epoch = 0;
            Data_indexer = 0;
            finished = false;
            MinErrorAchieved = false;
            ToUseTheNet = false;
            ToDebug = false;
        }
        public double BipolarSigmoid(double x)//bipolar sigmoid function
        {
            return ((1.0 - Math.Exp(-x)) / (1.0 + Math.Exp(-x)));
        }
        public double LogSigmoid(double x)//sigmoid function
        {
            if (x >= 45)
                return 1;
            else if (x <= -45)
                return 0;
            else
                return (1.0 / (1.0 + Math.Exp(-x)));
        }
        public double Linear(double x)
        {
            return x;
        }
        public double Activation(double val, char layer)//Uses the predefined activation functions
        {
            double output = 0.0;
            if (layer == 'h')
            {
                if (activation_function == ANN_ACTIVATION_SIGMOID)
                {
                    output = LogSigmoid(val);
                }
                else if (activation_function == ANN_ACTIVATION_LINEAR)
                {
                    output = Linear(val);
                }
                else// if (activation_function == ANN_ACTIVATION_BIPOLAR_SIGMOID)
                {
                    output = BipolarSigmoid(val);
                }
            }
            else
            {
                if (output_activation_function == ANN_ACTIVATION_SIGMOID)
                {
                    output = LogSigmoid(val);
                }
                else if (output_activation_function == ANN_ACTIVATION_LINEAR)
                {
                    output = Linear(val);
                }
                else// if (output_activation_function == ANN_ACTIVATION_BIPOLAR_SIGMOID)
                {
                    output = BipolarSigmoid(val);
                }
            }
            return output;
        }
        public void ComputeAverageError()
        {
            aError = 0;
            for (int i = 0; i < num_datasets; ++i)
            {
                for (int j = 0; j < num_output; ++j)
                {
                    aError += Math.Pow(lError[i, j], 2) / (2.0 * ((double)num_output));
                }
            }
        }
        public void ComputeOutputs()//computes the output of each neuron
        {
            double temp = 0.0;

            for (int i = 0; i < num_hidden; ++i)
            {
                temp = 0.0;
                for (int j = 0; j < num_input; ++j)
                {
                    if (!ToUseTheNet)
                    {
                        temp += Data[Actual_data, j] * ih_w[j, i];
                        ih_local_input[j, i] = Data[Actual_data, j];
                    }
                    else
                    {
                        temp += inputs[j] * ih_w[j, i];
                        //ih_local_input[j, i] = inputs[j];
                    }
                }
                temp += h_bias[i];

                h_out[i] = Activation(temp, 'h');
            }

            for (int i = 0; i < num_output; ++i)
            {
                temp = 0.0;
                for (int j = 0; j < num_hidden; ++j)
                {
                    temp += h_out[j] * ho_w[j, i];
                    if (!ToUseTheNet)
                        ho_local_input[j, i] = h_out[j];
                }
                temp += o_bias[i];
                o_out[i] = Activation(temp, 'o');
                
                if (ToUseTheNet)
                    outputs[i] = o_out[i];
                else
                    NetOutputs[Actual_data, i] = o_out[i];
            }
        }
        public void BackPropagation()//uses the algorithm to compute the gradients and the deltas
        {
            double temp = 0.0;
            for (int i = 0; i < num_output; ++i)
            {
                lError[Actual_data, i] = Data[Actual_data, i + num_input] - o_out[i];
                if (output_activation_function == ANN_ACTIVATION_SIGMOID)
                    oGrad[i] = o_out[i] * (1 - o_out[i]) * (lError[Actual_data, i]);
                else if (output_activation_function == ANN_ACTIVATION_LINEAR)
                    oGrad[i] = o_out[i] * (lError[Actual_data, i]);
                else if (output_activation_function == ANN_ACTIVATION_BIPOLAR_SIGMOID)
                    oGrad[i] = (1.0 / 2.0) * (1 + o_out[i]) * (1 - o_out[i]) * (lError[Actual_data, i]);
            }
            for (int i = 0; i < num_hidden; ++i)
            {
                temp = 0.0;
                for (int j = 0; j < num_output; ++j)
                {
                    temp += oGrad[j] * ho_w[i, j];
                }
                if (activation_function == ANN_ACTIVATION_SIGMOID)
                    hGrad[i] = h_out[i] * (1 - h_out[i]) * (temp);
                else if (activation_function == ANN_ACTIVATION_LINEAR)
                    hGrad[i] = h_out[i] * (temp);
                else if (activation_function == ANN_ACTIVATION_BIPOLAR_SIGMOID)
                    hGrad[i] = (1.0 / 2.0) * (1 + h_out[i]) * (1 - h_out[i]) * (temp);
            }

            //compute the deltas
            for (int i = 0; i < num_hidden; ++i)
            {
                for (int j = 0; j < num_input; ++j)
                {
                    delta_ih_w[j, i] = eta * hGrad[i] * ih_local_input[j, i];
                    ih_w[j, i] += delta_ih_w[j, i] + alpha * (prev_delta_ih_w[j, i]);
                    prev_delta_ih_w[j, i] = delta_ih_w[j, i];
                }
                delta_h_bias[i] = eta * hGrad[i] * 1.0;
                h_bias[i] += delta_h_bias[i] + alpha * prev_delta_h_bias[i];
                prev_delta_h_bias[i] = delta_h_bias[i];
            }
            for (int i = 0; i < num_output; ++i)
            {
                for (int j = 0; j < num_hidden; ++j)
                {
                    delta_ho_w[j, i] = eta * oGrad[i] * ho_local_input[j, i];
                    ho_w[j, i] += delta_ho_w[j, i] + alpha * (prev_delta_ho_w[j, i]);
                    prev_delta_ho_w[j, i] = delta_ho_w[j, i];
                }
                delta_o_bias[i] = eta * oGrad[i] * 1.0;
                o_bias[i] += delta_o_bias[i] + alpha * prev_delta_o_bias[i];
                prev_delta_o_bias[i] = delta_o_bias[i];
            }
        }

        public void TrainNetworkPerEpoch()//Train the network Per Epoch
        {
            while (Data_indexer < num_datasets)
            {
                while (true)
                {

                    Actual_data = rnd.Next(0, num_datasets);
                    int teste = 0;
                    for (int i = 0; i < num_datasets; ++i)
                    {
                        if (Data_used[i] == Actual_data)
                            ++teste;
                    }
                    if (teste == 0)
                    {
                        Data_used[Data_indexer] = Actual_data;
                        break;
                    }
                }
                ComputeOutputs();
                BackPropagation();
                ++Data_indexer;
            }
            ComputeAverageError();

            if (num_Epoch != -1)
            {
                if (Epoch == num_Epoch)
                {
                    finished = true;
                }
                else
                {
                    Data_indexer = 0;
                    for (int i = 0; i < num_datasets; ++i)
                        Data_used[i] = -1;
                    ++Epoch;
                }
            }
            else
            {
                Data_indexer = 0;
                for (int i = 0; i < num_datasets; ++i)
                    Data_used[i] = -1;
                ++Epoch;
            }
        }
        public void TrainNetwork()//TRain the network until max_epoch or min_error achieved
        {
            while (true)
            {
                TrainNetworkPerEpoch();
                if (IsFinished || IsMinError)
                    break;
            }
        }
        public double[] UseNetwork(double[] NetInput)//use the trained network for predictions
        {
            double[] NetOut = new double[num_output];
            for (int i = 0; i < num_input; ++i)
                inputs[i] = NetInput[i];

            ToUseTheNet = true;
            ComputeOutputs();
            ToUseTheNet = false;

            for (int i = 0; i < num_output; ++i)
                NetOut[i] = outputs[i];
            return NetOut;
        }

    }

}

