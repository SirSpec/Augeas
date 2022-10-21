using System;
using System.Collections.Generic;
using System.Linq;

namespace Augeas.Domain.ArtificialIntelligence.NeuralNetworks
{
	public class NeuralLayer
	{
		private readonly Neuron[] neurons;

		public NeuralLayer(params Neuron[] neurons) =>
			this.neurons = ContainsUniqueIds(neurons)
				? neurons
				: throw new ArgumentException($"{nameof(neurons)} set cannot contain duplicated Ids.");

		public IEnumerable<Neuron> Neurons =>
			neurons;

		public Neuron this[int index] =>
			neurons[index];

		public Neuron this[string id] =>
			Neurons.Single(neuron => neuron.Id == id);

		private static bool ContainsUniqueIds(INeuron[] neurons) =>
			neurons.GroupBy(neuron => neuron.Id).All(group => group.Count() == 1);
	}
}