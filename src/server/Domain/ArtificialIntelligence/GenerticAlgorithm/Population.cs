using System.Collections.Generic;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm
{
    public class Population<T>
    {
        private readonly Phenotype<T>[] phenotypes;

        public Population(int generation, params Phenotype<T>[] phenotypes)
        {
            Generation = generation;
            this.phenotypes = phenotypes;
        }

        public int Generation { get; }

        public IEnumerable<Phenotype<T>> Phenotypes =>
            phenotypes;

        public Phenotype<T> this[int index] =>
            phenotypes[index];

        public Phenotype<T> Fittest =>
            phenotypes.OrderByDescending(phenotype => phenotype.Fitness).First();

        public Population<T> CreateNextGeneration(params Phenotype<T>[] phenotypes) =>
            new Population<T>(Generation + 1, phenotypes);
    }
}