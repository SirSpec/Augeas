using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Mutation
{
    // https://en.wikipedia.org/wiki/Mutation_(genetic_algorithm)
    public interface IMutationOperator<TAllele>
    {
        Genotype<TAllele> GetMutation(Genotype<TAllele> genotype);
    }
}