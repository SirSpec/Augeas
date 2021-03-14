using System.Collections.Generic;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm
{
    public class ElitismSelection : ISelectionAlgorithm
    {
        private readonly int numberOfSelectedPhenotypes;

        public ElitismSelection(int numberOfSelectedPhenotypes) =>
            this.numberOfSelectedPhenotypes = numberOfSelectedPhenotypes;

        public IEnumerable<Phenotype<T>> Select<T>(IEnumerable<Phenotype<T>> phenotypes) =>
            phenotypes
                .OrderByDescending(phenotype => phenotype.Fitness)
                .Take(numberOfSelectedPhenotypes);
    }
}