namespace Augeas.Domain.ArtificialIntelligence.SimulatedAnnealing
{
	public interface IStateGenerator
	{
		// candidate generator
		object PickRandomNeighbour();
	}
}
