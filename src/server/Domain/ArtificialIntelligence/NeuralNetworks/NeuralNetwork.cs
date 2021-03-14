using System.Collections.Generic;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
    public class NeuralNetwork
    {
        private readonly InputLayer inputLayer;
        private readonly NeuralLayer[] hiddenLayers;
        private readonly OutputLayer outputLayer;

        public NeuralNetwork(InputLayer inputLayer, NeuralLayer[] hiddenLayers, OutputLayer outputLayer)
        {
            this.inputLayer = inputLayer;
            this.hiddenLayers = hiddenLayers;
            this.outputLayer = outputLayer;
        }

        public IEnumerable<double> Comput(double[] inputs)
        {
            inputLayer.PushInputs(inputs);
            return outputLayer.Outputs;
        }

        public IEnumerable<double> GetWeights()
        {
            var connections = hiddenLayers.SelectMany(h => h.Neurons).SelectMany(n => n.inputs).Concat(outputLayer.neurons.SelectMany(n => n.inputs)).ToArray();

            return connections.Select(c => c.Weight);
        }

        public void SetWeight(int i, double g)
        {
            var connections = hiddenLayers.SelectMany(h => h.Neurons).SelectMany(n => n.inputs).Concat(outputLayer.neurons.SelectMany(n => n.inputs)).ToArray();

            connections[i].Weight = g;
        }
    }
}