using System.Collections.Generic;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks.Activations;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks.Propagations;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
	public class Neuron : INeuron
	{
		private readonly IPropagationFunction propagationFunction;
		private readonly IActivationFunction activationFunction;
		public readonly IList<Connection> inputs;
		private readonly double bias;

		public Neuron(
			IPropagationFunction propagationFunction,
			IActivationFunction activationFunction,
			IList<Connection> inputs,
			double bias)
		{
			this.propagationFunction = propagationFunction;
			this.activationFunction = activationFunction;
			this.bias = bias;
			this.inputs = inputs;
		}

		public double Output => activationFunction.CalculateOutput(Activation + bias);

		public void AddInput(Connection connection)
		{
			inputs.Add(connection);
		}

		private double Activation => propagationFunction.CalculateInput(inputs);
	}
}