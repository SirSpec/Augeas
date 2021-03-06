using System.Collections.Generic;

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
    }
}