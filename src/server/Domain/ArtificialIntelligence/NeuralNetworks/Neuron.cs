using System;
using System.Collections.Generic;
using System.Linq;
using Augeas.Domain.ArtificialIntelligence.NeuralNetworks.Activations;
using Augeas.Domain.ArtificialIntelligence.NeuralNetworks.Propagations;

namespace Augeas.Domain.ArtificialIntelligence.NeuralNetworks
{
	public class Neuron : INeuron
	{
		private readonly IPropagationFunction propagationFunction;
		private readonly IActivationFunction activationFunction;
		private readonly IList<Connection> inputs;
		private readonly double bias;

		public string Id { get; }

		public Neuron(
			string id,
			IPropagationFunction propagationFunction,
			IActivationFunction activationFunction,
			IList<Connection> inputs,
			double bias)
		{
			Id = id;
			this.propagationFunction = propagationFunction;
			this.activationFunction = activationFunction;
			this.inputs = inputs;
			this.bias = bias;
		}

		public IEnumerable<Connection> Inputs =>
			inputs;

		public double Output =>
			activationFunction.CalculateOutput(Activation + bias);

		public void AddInput(Connection connection)
		{
			if (Inputs.All(input => input.NeuronId != connection.NeuronId))
				inputs.Add(connection);
			else throw new ArgumentException(
				$"{nameof(inputs)} set cannot contain duplicated connection id:{connection.NeuronId}.");
		}

		private double Activation =>
			propagationFunction.CalculateInput(inputs);
	}
}