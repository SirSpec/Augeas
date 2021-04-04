using System;
using System.Collections.Generic;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
	public class InputLayer
	{
		private readonly InputNeuron[] neurons;

		public InputLayer(params InputNeuron[] neurons) =>
			this.neurons = ContainsUniqueIds(neurons)
				? neurons
				: throw new ArgumentException($"{nameof(neurons)} set cannot contain duplicated Ids.");

		public IEnumerable<InputNeuron> Neurons =>
			neurons;

		public InputNeuron this[int index] =>
			neurons[index];

		public InputNeuron this[string id] =>
			Neurons.Single(neuron => neuron.Id == id);

		public void PushInputs(params double[] inputs)
		{
			if (AreEquivalent(neurons, inputs))
			{
				foreach (var (neuron, input) in Neurons.Zip(inputs))
					neuron.Input = input;
			}
			else throw new ArgumentException(
				$"{nameof(inputs)} set in not equivalent of {nameof(neurons)} set:{inputs.Length} - {neurons.Length}");
		}

		private bool AreEquivalent(InputNeuron[] neurons, double[] inputs) =>
			neurons.Length == inputs.Length;

		private static bool ContainsUniqueIds(InputNeuron[] neurons) =>
			neurons.GroupBy(neuron => neuron.Id).All(group => group.Count() == 1);
	}
}