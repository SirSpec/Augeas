using System.Collections.Generic;
using System.Linq;

namespace Augeas.Domain.ArtificialIntelligence.NeuralNetworks
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

		public IEnumerable<Neuron> Neurons =>
			hiddenLayers
				.SelectMany(layer => layer.Neurons)
				.Concat(outputLayer.Neurons);

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

		public IEnumerable<double> Comput(double[] inputs)
		{
			inputLayer.PushInputs(inputs);
			return outputLayer.Neurons.Select(neuron => neuron.Output);
		}
	}
}
