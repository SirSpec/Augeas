using System.Linq;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks;

namespace Hermes.Domain.ArtificialIntelligence
{
    public class Actor
    {
        public static Neuron GetNeuron(INeuron[] connected, double[] weight)
        {
            var func = new HyperbolicTangentFunction();
            var sum = new WeightedSumFunction();

            var connectecNeurons = connected.Zip(weight);

            var connections = connectecNeurons.Select(c => new Connection(c.First, c.Second)).ToList();

            return new Neuron(sum, func, connections, 0.0);
        }

        public NeuralNetwork NeuralNetwork { get; }
        public double Evaluation { get; set; }

        public Actor()
        {
            var inputLayer = new InputLayer(
                new InputNeuron[6]
                {
                    new InputNeuron(),
                    new InputNeuron(),
                    new InputNeuron(),
                    new InputNeuron(),
                    new InputNeuron(),
                    new InputNeuron(),
                }
            );

            var hiddenLayer = new NeuralLayer(
                new Neuron[3]
                {
                    GetNeuron(inputLayer.Neurons, new[] { 6.3, 8.9, -10.4, 20.1, -10.7, 20.5 }),
                    GetNeuron(inputLayer.Neurons, new[] { 6.3, 8.9, -10.4, 20.1, -10.7, 20.5 }),
                    GetNeuron(inputLayer.Neurons, new[] { 6.3, 8.9, -10.4, 20.1, -10.7, 20.5 }),
                }
            );

            var outputLayer = new OutputLayer(
                new Neuron[1]
                {
                    GetNeuron(hiddenLayer.Neurons, new double[] { 20.4, -10.2, 20.9 }),
                }
            );

            NeuralNetwork = new NeuralNetwork(inputLayer, new[] { hiddenLayer }, outputLayer);
        }

        public double GetAngle(double[] inputs)
        {
            return NeuralNetwork.Comput(inputs).Single();
        }
    }
}