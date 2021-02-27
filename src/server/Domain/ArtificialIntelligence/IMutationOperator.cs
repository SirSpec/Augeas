namespace Hermes.Domain.ArtificialIntelligence
{
    // https://en.wikipedia.org/wiki/Mutation_(genetic_algorithm)
    public interface IMutationOperator
    {
        Genotype<T> Mutate<T>(Genotype<T> genotype);
    }
}