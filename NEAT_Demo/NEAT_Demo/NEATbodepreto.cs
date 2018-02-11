using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NEATbodepreto
{
    class RandomBLACKBODE
    {
        public static Random random;

    }

    class Neuron
    {
        public double value;
        public int id;
        public int activationType;
        public static int SIGMOID_ACTIVATION = 0;
        public static int BIPOLAR_SIGMOID_ACTIVATION = 1;
        public static int HYPERBOLIC_TANGENT_ACTIVATION = 2;
        public static int THRESHOLD_ACTIVATION = 3;

        public Neuron()
        {

        }

        public Neuron(int _id, int _activationType)
        {
            id = _id;
            activationType = _activationType;
            value = 0;
        }

        public void ComputeOut(double sum)
        {
            if (activationType == SIGMOID_ACTIVATION)
                value = LogSigmoid(sum);
            else if (activationType == BIPOLAR_SIGMOID_ACTIVATION)
                value = BipolarSigmoid(sum);
            else if (activationType == THRESHOLD_ACTIVATION)
                value = Linear(sum);
            else if (activationType == HYPERBOLIC_TANGENT_ACTIVATION)
                value = HyperbolicTangent(sum);
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

        public double HyperbolicTangent(double x)
        {
            return ((1.0 - Math.Exp(-2 * x)) / (1.0 + Math.Exp(-2 * x)));
        }
        public double Linear(double x)
        {
            return x;
        }

    }
    ////////////////////////////////////////////////////////////////////////////////////
    class Synapse
    {
        public int input_id;
        public int output_id;
        public double weight;
        public bool enabled;

        public Synapse()
        {

        }

        public Synapse(int _input_id, int _output_id, bool _enabled = true)
        {
            input_id = _input_id;
            output_id = _output_id;
            enabled = _enabled;
            weight = RandomBLACKBODE.random.Next(-240, 240) / 100.0;
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////
    class ANN
    {
        public List<Neuron> neurons;
        public List<Synapse> synapses;

        public int Ni;
        public int No;
        public int Nh;

        public double error;

        public ANN()
        {

        }

        public ANN(int _Ni, int _No, int SynapsesInitializationMethod = 1)
        {
            error = 0;

            neurons = new List<Neuron>();
            synapses = new List<Synapse>();

            Ni = _Ni;
            No = _No;
            Nh = 0;

            for(int i = 0; i < Ni + No; ++i)
            {
                //MessageBox.Show(i.ToString());
                neurons.Add(new Neuron(i, Neuron.SIGMOID_ACTIVATION));
            }

            if(SynapsesInitializationMethod == 1)
            {
                for (int j = Ni; j < Ni + No; ++j)
                {
                    //MessageBox.Show("HUEHUEHE");
                    for (int i = 0; i < Ni; ++i)
                    {
                        synapses.Add(new Synapse(i, j));
                    }
                }

                //String outS = "SYNAPSES:\n";
                //for (int j = 0; j < synapses.Count; ++j)
                //{
                //    outS += "synapses[" + j.ToString() + "](" + synapses.ElementAt(j).input_id.ToString() + ", " + synapses.ElementAt(j).output_id.ToString() + ") = " + synapses.ElementAt(j).weight.ToString("0.000") + "\n";
                //}
                //MessageBox.Show(outS);
            }   
        }

        public double[] NetworkOutput(double[] inputs, int ind = 0)
        {
            double[] outs = new double[No];



            if (inputs.GetLength(0) != Ni)
            {
                MessageBox.Show("WRONG SIZE!");
                return outs;
            }
            //INPUT NODES
            for(int i = 0; i < inputs.GetLength(0); ++i)
            {
                neurons.ElementAt(i).value = inputs[i];
            }
            //COMPUTE OUTPUT OF THE NETWORK
            for (int i = Ni + No; i < neurons.Count; ++i)
            {
                double sum = 0;
                for (int j = 0; j < synapses.Count; ++j)
                {
                    if(synapses.ElementAt(j).output_id == i)
                    {
                        double val = 0;
                        if (synapses.ElementAt(j).input_id == -1)//then this is a bias, and the input is -1!
                            val = -1;
                        else//a regular neuron/node
                            val = GetNeuronById(synapses.ElementAt(j).input_id).value;

                        sum += val * synapses.ElementAt(j).weight;
                    }
                }
                //MessageBox.Show("sum = " + sum.ToString());
                neurons.ElementAt(i).ComputeOut(sum);
            }

            for (int i = Ni; i < Ni + No; ++i)
            {
                double sum = 0;
                for (int j = 0; j < synapses.Count; ++j)
                {
                    //MessageBox.Show("i = " + i.ToString() + "\noutput_id = " + synapses.ElementAt(j).output_id.ToString());
                    if (synapses.ElementAt(j).output_id == i)
                    {
                        double val = 0;
                        if (synapses.ElementAt(j).input_id == -1)//then this is a bias, and the input is -1!
                            val = -1;
                        else//a regular neuron/node
                            val = GetNeuronById(synapses.ElementAt(j).input_id).value;

                        //MessageBox.Show("input_id = " + synapses.ElementAt(j).input_id.ToString());

                        sum += val * synapses.ElementAt(j).weight;
                    }
                }
                //MessageBox.Show("sum2 = " + sum.ToString());
                neurons.ElementAt(i).ComputeOut(sum);
            }

            for (int i = Ni; i < Ni + No; ++i)
            {
                outs[i - Ni] = neurons.ElementAt(i).value;
            }

            return outs;
        }

        public void ComputeNetworkError(double[] inputs, double[] desiredOutput, int ind = 0, bool InitErrorZero = false)
        {
            if (InitErrorZero)
                error = 0;

            double[] res = NetworkOutput(inputs, ind);
            for (int i = 0; i < res.GetLength(0); ++i)
            {
                error += Math.Pow(res[i] - desiredOutput[i], 2);
            }
            error /= res.GetLength(0);
        }

        public bool ContainsSynapse(Synapse syn)
        {
            for(int i = 0; i < synapses.Count; ++i)
            {
                if (syn.input_id == synapses.ElementAt(i).input_id && syn.output_id == synapses.ElementAt(i).output_id)
                    return true;
            }
            return false;
        }

        public Neuron GetNeuronById(int neuronID)
        {
            for(int i = 0; i < neurons.Count; ++i)
            {
                if (neurons.ElementAt(i).id == neuronID)
                    return neurons.ElementAt(i);
            }

            return null;
        }

        public bool ContainsNeuron(int neuronId)
        {
            for (int i = 0; i < neurons.Count; ++i)
            {
                if (neurons.ElementAt(i).id == neuronId)
                    return true;
            }
            return false;
        }

        public bool ContainsEnabledSynapses()
        {
            for (int i = 0; i < synapses.Count; ++i)
            {
                if (synapses.ElementAt(i).enabled)
                    return true;
            }
            return false;
        }

        public bool ContainsDisabledSynapses()
        {
            for (int i = 0; i < synapses.Count; ++i)
            {
                if (!synapses.ElementAt(i).enabled)
                    return true;
            }
            return false;
        }

        public bool isFullyConnected(bool isBias)
        {
            if(isBias)
            {
                for (int j = 0; j < neurons.Count; ++j)
                {
                    Synapse newSynapse = new Synapse(-1, j);
                    if (!ContainsSynapse(newSynapse))
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < neurons.Count; ++i)
                {
                    for (int j = 0; j < neurons.Count; ++j)
                    {
                        if (i != j)
                        {
                            Synapse newSynapse = new Synapse(i, j);
                            if (!ContainsSynapse(newSynapse))
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            

            return true;
        }
    
	}

    class NEAT
    {
        public List<ANN> population;

        public int Ni;
        public int No;

        public double MutationRateWeight;
        public double MutationRateLink;
        public double MutationRateLinkToBias;
        public double MutationRateNode;
        public double MutationRateEnable;
        public double MutationRateDisable;

        public double MutationAmplitude;

        public int genCounter;

        public NEAT()
        {

        }

        public NEAT(int pop_size, int _Ni, int _No, double _MutationRateWeight, double _MutationRateLink, double _MutationRateLinkToBias, double _MutationRateNode, double _MutationRateEnable, double _MutationRateDisable)
        {
            RandomBLACKBODE.random = new Random();

            Ni = _Ni;
            No = _No;

            MutationRateWeight = _MutationRateWeight;
            MutationRateLink = _MutationRateLink;
            MutationRateLinkToBias = _MutationRateLinkToBias;
            MutationRateNode = _MutationRateNode;
            MutationRateEnable = _MutationRateEnable;
            MutationRateDisable = _MutationRateDisable;

            MutationAmplitude = 0.3;

            population = new List<ANN>();

            for (int i = 0; i < pop_size; ++i)
            {
                population.Add(new ANN(Ni, No));
            }

            genCounter = 0;
        }

        public void ComputeFitnes(int index, double[] netInput, double[] desiredOut)
        {
            this.population.ElementAt(index).ComputeNetworkError(netInput, desiredOut, index);
        }

        public void ComputeOverallFitness()
        {
            double W0 = 1, W1 = 0.1;
            for (int i = 0; i < population.Count; ++i)
            {
                this.population.ElementAt(i).error = (1.0 / (this.population.ElementAt(i).error)) * W0;//Math.Pow(2, (1.0 / (this.population.ElementAt(i).error)) * W0) + 0.1;
            }
        }

        public void Mutation()//WHOLE POPULATION MUTATION
        {
            MutationWeights();
            MutationLink(false);
            MutationLink(true);
            MutationNode();
            MutationEnabled();
            MutationDisabled();
        }

        public void MutationWeights()//MUTATE THE WEIGHTS OFF ALL POPULATION
        {
            for (int i = 0; i < population.Count; ++i)
            {
                for (int j = 0; j < population.ElementAt(i).synapses.Count; ++j)
                {
                    double chance = (double)RandomBLACKBODE.random.Next(100) / 100.0;
                    if (chance < MutationRateWeight)
                    {
                        double perturbance = RandomBLACKBODE.random.Next(-(int)(MutationAmplitude * 1000), (int)(MutationAmplitude * 1000)) / 1000.0;
                        population.ElementAt(i).synapses[j].weight += perturbance;
                    }
                }
            }   
        }

        public void MutationLink(bool isBias)//CREATES AN LINK AT EXISTING NODES (COULD BE A BIAS)
        {
            int c = 0;
            for (int i = 0; i < population.Count; ++i)
            {
                double chance = (double)RandomBLACKBODE.random.Next(100) / 100.0;

                if (isBias && !population.ElementAt(i).isFullyConnected(isBias) && chance < MutationRateLinkToBias)//THIS IS A LINK BETWEEN BIAS AND A NEURON
                {
                    int ramdomNeuronId0 = -1;
                    while (true)
                    {
                        int ramdomNeuronId1 = RandomBLACKBODE.random.Next(population.ElementAt(i).neurons.Count);

                        Synapse newSynapse = new Synapse(ramdomNeuronId0, ramdomNeuronId1);
                        if (!population.ElementAt(i).ContainsSynapse(newSynapse))
                        {
                            population.ElementAt(i).synapses.Add(newSynapse);
                            break;
                        }
                        ++c;
                        if(c >= 10000)
                        {
                            break;
                            //MessageBox.Show("STUCK ON BIAS!");
                        }
                    }
                }
                else if (!isBias && !population.ElementAt(i).isFullyConnected(isBias) && chance < MutationRateLink)//NEURON TO NEURON LINK
                {
                    int ramdomNeuronId0 = RandomBLACKBODE.random.Next(population.ElementAt(i).neurons.Count);
                    while (true)
                    {
                        int ramdomNeuronId1 = RandomBLACKBODE.random.Next(population.ElementAt(i).neurons.Count);
                        if (ramdomNeuronId0 == ramdomNeuronId1)
                            continue;

                        Synapse newSynapse = new Synapse(ramdomNeuronId0, ramdomNeuronId1);
                        if (!population.ElementAt(i).ContainsSynapse(newSynapse))
                        {
                            //MessageBox.Show("Ind " + i.ToString() + "\nNeuronsSize = " + population.ElementAt(i).neurons.Count.ToString() + "\nramdomNeuronId0 = " + ramdomNeuronId0.ToString() + "\nramdomNeuronId1 = " + ramdomNeuronId1.ToString());

                            population.ElementAt(i).synapses.Add(newSynapse);
                            break;
                        }

                        ++c;
                        if (c >= 10000)
                        {
                            break;
                            //MessageBox.Show("STUCK ON NEURON!");
                        }
                    }
                }
            }
        }

        public void MutationNode()//CREATES A NODE FROM AN ALLREADY EXISTING SYNAPSE
        {
            for (int i = 0; i < population.Count; ++i)
            {
                double chance = (double)RandomBLACKBODE.random.Next(100) / 100.0;
                if(chance < MutationRateNode)
                {
                    while(true)
                    {
                        int randomSynapse = RandomBLACKBODE.random.Next(population.ElementAt(i).synapses.Count);

                        if (population.ElementAt(i).synapses.ElementAt(randomSynapse).input_id == -1)
                            continue;

                        Synapse newSynapseA = new Synapse(population.ElementAt(i).synapses.ElementAt(randomSynapse).input_id, population.ElementAt(i).neurons.ElementAt(population.ElementAt(i).neurons.Count - 1).id + 1);
                        Synapse newSynapseB = new Synapse(population.ElementAt(i).neurons.ElementAt(population.ElementAt(i).neurons.Count - 1).id + 1, population.ElementAt(i).synapses.ElementAt(randomSynapse).output_id);

                        if(!population.ElementAt(i).ContainsSynapse(newSynapseA) && !population.ElementAt(i).ContainsSynapse(newSynapseB))
                        {
                            Neuron newNeuron = new Neuron(newSynapseA.output_id, population.ElementAt(i).neurons.ElementAt(0).activationType);
                            population.ElementAt(i).neurons.Add(newNeuron);
                            population.ElementAt(i).synapses.Add(newSynapseA);
                            population.ElementAt(i).synapses.Add(newSynapseB);

                            //String outS = "Individual " + i.ToString() + "\n\nnewNeuron = " + newNeuron.id.ToString() + "\nnewSynapseA = (" + newSynapseA.input_id.ToString() + ", " + newSynapseA.output_id.ToString() + ")\nnewSynapseB = (" + newSynapseB.input_id.ToString() + ", " + newSynapseB.output_id.ToString() + ")";
                            //outS += "\nTotalNeuronsCount = " + population.ElementAt(i).neurons.Count.ToString();
                            //MessageBox.Show(outS);
                            break;
                        }
                    }
                }
            }
        }

        public void MutationEnabled()//DISABLES AN ENABLED NEURON
        {
            for (int i = 0; i < population.Count; ++i)
            {
                double chance = (double)RandomBLACKBODE.random.Next(100) / 100.0;
                if (chance < MutationRateEnable && population.ElementAt(i).ContainsEnabledSynapses())
                {
                    while (true)
                    {
                        int randomSynapse = RandomBLACKBODE.random.Next(population.ElementAt(i).synapses.Count);

                        if (population.ElementAt(i).synapses.ElementAt(randomSynapse).enabled == false)
                            continue;

                        population.ElementAt(i).synapses.ElementAt(randomSynapse).enabled = false;

                        break;
                    }
                }
            }
        }

        public void MutationDisabled()//ENABLED AN DISABLED NEURON
        {
            for (int i = 0; i < population.Count; ++i)
            {
                double chance = (double)RandomBLACKBODE.random.Next(100) / 100.0;
                if (chance < MutationRateEnable && population.ElementAt(i).ContainsDisabledSynapses())
                {
                    while (true)
                    {
                        int randomSynapse = RandomBLACKBODE.random.Next(population.ElementAt(i).synapses.Count);

                        if (population.ElementAt(i).synapses.ElementAt(randomSynapse).enabled == true)
                            continue;

                        population.ElementAt(i).synapses.ElementAt(randomSynapse).enabled = true;

                        break;
                    }
                }
            }
        }

        public void Crossover()
        {

        }

        public ANN[] Crossover2(ANN partnerA, ANN partnerB)
        {
            ANN[] childs = new ANN[] { new ANN(partnerA.Ni, partnerA.No), new ANN(partnerA.Ni, partnerA.No) };
            childs[0].synapses.Clear();
            childs[1].synapses.Clear();

            int mid = 0;
            if (partnerA.synapses.Count < partnerB.synapses.Count)
            {
                mid = RandomBLACKBODE.random.Next(partnerA.synapses.Count);

                for (int i = 0; i < partnerB.synapses.Count; ++i)
                {
                    if (i <= mid)
                    {
                        childs[0].synapses.Add(partnerA.synapses[i]);
                        childs[1].synapses.Add(partnerB.synapses[i]);
                    }
                    else
                    {
                        childs[0].synapses.Add(partnerB.synapses[i]);

                        if (i < partnerA.synapses.Count)
                            childs[1].synapses.Add(partnerA.synapses[i]);
                    }

                }
            }
            else
            {
                mid = RandomBLACKBODE.random.Next(partnerB.synapses.Count);

                for (int i = 0; i < partnerA.synapses.Count; ++i)
                {
                    if (i <= mid)
                    {
                        childs[0].synapses.Add(partnerA.synapses[i]);
                        childs[1].synapses.Add(partnerB.synapses[i]);
                    }
                    else
                    {
                        childs[0].synapses.Add(partnerA.synapses[i]);

                        if (i < partnerB.synapses.Count)
                            childs[1].synapses.Add(partnerB.synapses[i]);
                    }

                }
            }

            int HighestID = -1;

            for (int i = 0; i < partnerA.synapses.Count; ++i)
            {
                if (partnerA.synapses.ElementAt(i).input_id > HighestID)
                    HighestID = partnerA.synapses.ElementAt(i).input_id;
                else if (partnerA.synapses.ElementAt(i).output_id > HighestID)
                    HighestID = partnerA.synapses.ElementAt(i).output_id;
            }

            for (int i = 0; i < partnerB.synapses.Count; ++i)
            {
                if (partnerB.synapses.ElementAt(i).input_id > HighestID)
                    HighestID = partnerB.synapses.ElementAt(i).input_id;
                else if (partnerB.synapses.ElementAt(i).output_id > HighestID)
                    HighestID = partnerB.synapses.ElementAt(i).output_id;
            }

            for (int i = partnerA.Ni + partnerA.No; i <= HighestID; ++i)
            {
                childs[0].neurons.Add(new Neuron(i, partnerA.neurons[0].activationType));
                childs[1].neurons.Add(new Neuron(i, partnerA.neurons[0].activationType));
            }


            //for (int m = 0; m < 2; ++m)
            //{
            //    for (int i = 0; i < childs[m].synapses.Count; ++i)
            //    {
            //        if (childs[m].synapses.ElementAt(i).input_id != -1 && !childs[m].ContainsNeuron(childs[m].synapses.ElementAt(i).input_id))
            //        {
            //            childs[m].neurons.Add(new Neuron(childs[m].synapses.ElementAt(i).input_id, partnerA.neurons[0].activationType));
            //        }

            //        if (!childs[m].ContainsNeuron(childs[m].synapses.ElementAt(i).output_id))
            //        {
            //            childs[m].neurons.Add(new Neuron(childs[m].synapses.ElementAt(i).output_id, partnerA.neurons[0].activationType));
            //        }
            //    }
            //}



            return childs;
        }

        public ANN HighestFitnessIndividual
        {
            get
            {
                double highest = -1;
                int highestIndex = 0;

                for (int i = 0; i < population.Count; ++i)
                {
                    if(this.population.ElementAt(i).error > highest || highest == -1)
                    {
                        highest = this.population.ElementAt(i).error;
                        highestIndex = i;
                    }
                }
                return this.population.ElementAt(highestIndex);

            }
        }

        public int[] SortPopulationByFitnes()
        {
            int[] res = new int[population.Count];
            List<ANN> sortedPopulation = new List<ANN>();

            for (int i = 0; i < res.GetLength(0); ++i)
            {
                res[i] = i;
                sortedPopulation.Add(population.ElementAt(i));
            }




            for (int i = 0; i < res.GetLength(0); ++i)
            {
                ANN prev;
                int indPrev;
                int beginIndex = -1;

                for (int j = 0; j < i; ++j)
                {
                    if (sortedPopulation[i].error > sortedPopulation[j].error)
                    {
                        beginIndex = j;
                        break;
                    }
                }

                if (beginIndex != -1)
                {
                    prev = sortedPopulation[beginIndex];
                    sortedPopulation[beginIndex] = sortedPopulation[i];
                    indPrev = res[beginIndex];
                    res[beginIndex] = i;

                    for (int j = beginIndex + 1; j <= i; ++j)
                    {
                        ANN p = sortedPopulation[j];
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
            //    debugs += "res[" + i.ToString() + "] = " + res[i].ToString() + "        sorted[" + i.ToString() + "] = " + sortedPopulation[i].error.ToString(".00") + "\n";
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
            double HighesFitness = HighestFitnessIndividual.error;

            //Random r = new Random(DateTime.Now.Millisecond);
            List<ANN> newPopulation = new List<ANN>();
            //int[] usedParents = new int[popSize];

            //for (int i = 0; i < popSize; ++i)
            //{
            //    usedParents[i] = -1;
            //}

            /////////////////////////////////////////////////////////////////////////////////////////////
            double Po = 0.8;
            double Pe = 0.1;
            int[] sortedIndexes = SortPopulationByFitnes();
            int eliteNumber = (int)(population.Count * Pe);//10%
            List<ANN> elite = new List<ANN>();
            int eliteOffspringSize = (int)(population.Count * Po);//80%

            for (int i = 0; i < eliteNumber; ++i)
            {
                elite.Add(population[sortedIndexes[i]]);
            }

            HighesFitness = population[sortedIndexes[eliteNumber]].error;

            int count = 0;
            for (int i = 0; i < elite.Count; ++i)
            {
                for (int j = 0; j < (int)(Po / Pe) / 2; ++j)
                {
                    while (true)
                    {
                        int randomParent = RandomBLACKBODE.random.Next(elite.Count, eliteOffspringSize);
                        //MessageBox.Show(randomParent.ToString());

                        double r1 = (RandomBLACKBODE.random.NextDouble() * HighesFitness);
                        //int r1 = DnaAnn.rnd.Next((int)(HighesFitness * 100));

                        if ((double)r1 < population[randomParent].error)
                        {
                            ANN[] childs = Crossover2(elite[i], population[sortedIndexes[randomParent]]);
                            newPopulation.Add(childs[0]);
                            newPopulation.Add(childs[1]);
                            count += 2;
                            break;
                        }
                        else
                            continue;

                    }
                }
            }

            int imigrationSize = population.Count - eliteOffspringSize;

            for (int i = 0; i < imigrationSize; ++i)
            {
                newPopulation.Add(new ANN(elite[0].Ni, elite[0].No));
                ++count;
            }

            //////////////////////////////////////////////////////////////////////////////////////////////
            //MessageBox.Show("popSize = " + population.Count.ToString());
            population.Clear();
            for (int i = 0; i < newPopulation.Count; ++i)
            {
                population.Add(newPopulation.ElementAt(i));
            }
            //MessageBox.Show("popSize = " + population.Count.ToString());


            //Mutation
            Mutation();


            ++genCounter;
        }
    }

}
