namespace Hermes.Domain.ArtificialIntelligence
{
    // https://en.wikipedia.org/wiki/Fitness_function
    public interface IFitnessFunction
    {
        double GetFitness<T>(Genotype<T> genotype);
    }
}