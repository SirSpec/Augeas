namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
	public class NeuralLayer
	{
		public Neuron[] Neurons { get; }

		public NeuralLayer(params Neuron[] neurons)
		{
			Neurons = neurons;
		}
	}
}