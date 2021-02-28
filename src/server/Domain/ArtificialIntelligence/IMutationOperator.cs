namespace Hermes.Domain.ArtificialIntelligence
{
    // https://en.wikipedia.org/wiki/Mutation_(genetic_algorithm)
    public interface IMutationOperator
    {
        Phenotype<T> Mutate<T>(Phenotype<T> phenotype);
    }
}