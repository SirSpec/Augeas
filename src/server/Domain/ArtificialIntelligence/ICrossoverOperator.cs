using System.Collections.Generic;

namespace Hermes.Domain.ArtificialIntelligence
{
    // https://en.wikipedia.org/wiki/Crossover_(genetic_algorithm)
    public interface ICrossoverOperator
    {
        IEnumerable<Genotype<T>> Crossover<T>(IEnumerable<Genotype<T>> parents);
    }
}