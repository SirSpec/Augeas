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

		public IEnumerable<Connection> AllConnections
		{
			get
			{
				var hiddenLayerConnections = hiddenLayers
					.SelectMany(layer => layer.Neurons)
					.SelectMany(neuron => neuron.Inputs);

				var outputLayerConnections = outputLayer
					.Neurons
					.SelectMany(neuron => neuron.Inputs);

				return hiddenLayerConnections.Concat(outputLayerConnections);
			}
		}

		public Connection GetConnection(string id) =>
			AllConnections.Single(connection => connection.NeuronId == id);

		public IEnumerable<double> Comput(double[] inputs)
		{
			inputLayer.PushInputs(inputs);
			return outputLayer.Neurons.Select(neuron => neuron.Output);
		}
	}
}