using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;

namespace Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Termination
{
	public interface ITerminationAlgorithm
	{
		bool TerminationConditionReached<TAllele>(Population<TAllele> population);
	}
}