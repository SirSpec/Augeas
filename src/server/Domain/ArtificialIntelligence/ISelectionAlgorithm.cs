using System.Collections.Generic;

namespace Hermes.Domain.ArtificialIntelligence
{
    // https://en.wikipedia.org/wiki/Selection_(genetic_algorithm)
    public interface ISelectionAlgorithm
    {
        IEnumerable<Phenotype<T>> Select<T>(IEnumerable<Phenotype<T>> phenotypes);
    }
}