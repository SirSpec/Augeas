using System.Collections.Generic;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Crossover
{
    // https://en.wikipedia.org/wiki/Crossover_(genetic_algorithm)
    public interface ICrossoverOperator
    {
        IEnumerable<Phenotype<TAllele>> Crossover<TAllele>(IEnumerable<Phenotype<TAllele>> parents);
    }
}