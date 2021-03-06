using System.Collections.Generic;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm
{
    // https://en.wikipedia.org/wiki/Crossover_(genetic_algorithm)
    public interface ICrossoverOperator
    {
        IEnumerable<Phenotype<T>> Crossover<T>(IEnumerable<Phenotype<T>> parents);
    }
}