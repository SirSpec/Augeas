namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm
{
    // https://en.wikipedia.org/wiki/Mutation_(genetic_algorithm)
    public interface IMutationOperator<T>
    {
        Phenotype<T> Mutate(Phenotype<T> phenotype);
    }
}