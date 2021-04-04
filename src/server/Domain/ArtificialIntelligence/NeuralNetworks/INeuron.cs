namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
	public interface INeuron
	{
		string Id { get; }
		double Output { get; }
	}
}