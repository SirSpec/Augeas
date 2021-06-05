namespace Hermes.Domain.ArtificialIntelligence.SimulatedAnnealing
{
	public interface IStateGenerator
	{
		// candidate generator
		object PickRandomNeighbour();
	}
}
