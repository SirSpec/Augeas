using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;

namespace Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Mutation
{
	// https://en.wikipedia.org/wiki/Mutation_(genetic_algorithm)
	public interface IMutationOperator<TAllele>
	{
		Genotype<TAllele> GetMutation(Genotype<TAllele> genotype);
	}
}