using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Termination
{
	public interface ITerminationAlgorithm
	{
		bool TerminationConditionReached<TAllele>(Population<TAllele> population);
	}
}