using System.Collections.Generic;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Selection
{
    // https://en.wikipedia.org/wiki/Selection_(genetic_algorithm)
    public interface ISelectionAlgorithm
    {
        IEnumerable<Phenotype<TAllele>> Select<TAllele>(IEnumerable<Phenotype<TAllele>> phenotypes);
    }
}