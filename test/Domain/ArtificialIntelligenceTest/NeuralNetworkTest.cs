using System.Collections.Generic;
using System.Linq;
using Hermes.Domain.ArtificialIntelligence;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks;
using Xunit;

namespace ArtificialIntelligenceTest
{
    public class NeuralNetworkTest
    {
        public InputNeuron InputNeuron()
        {
            var func = new BinaryStepFunction(0.0);
            var sum = new WeightedSumFunction();
            var neuron = new InputNeuron();
            return neuron;
        }

        public Neuron GetNeuron(INeuron[] connected, double[] weight )
        {
            var func = new BinaryStepFunction(0.0);
            var sum = new WeightedSumFunction();

            var conec = connected.Zip(weight);

            var consss = conec.Select(s => new Connection(s.First, s.Second)).ToList();

            return new Neuron(sum, func, consss, 0.0);
        }

        [Fact]
        public void Test()
        {            
            var w1 = new[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

            var a = new InputLayer(
                        new InputNeuron[6]
                        {
                            InputNeuron(),
                            InputNeuron(),
                            InputNeuron(),
                            InputNeuron(),
                            InputNeuron(),
                            InputNeuron(),
                        }
                    );


            var b = new NeuralLayer(
                        new Neuron[3]
                        {
                            GetNeuron(a.Neurons, w1),
                            GetNeuron(a.Neurons, w1),
                            GetNeuron(a.Neurons, w1),
                        }
                    );

            var c = new OutputLayer(
                        new Neuron[1]
                        {
                            GetNeuron(b.Neurons, new double[] { 1.0, 1.0, 1.0 }),
                        }
                    );

            var neural = new NeuralNetwork(
                a, new[] {b}, c
            );

            var output = neural.Comput(new double[6] { 1.0, 0.7, 0.5, 0.3, 1.0, 1.0 });
        }
    }
}