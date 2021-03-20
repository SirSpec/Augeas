using System;
using System.Collections.Generic;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures
{
    public class Population<TAllele>
    {
        private const int InitialGeneration = 0;
        private readonly Phenotype<TAllele>[] phenotypes;

        public Population(IEnumerable<Phenotype<TAllele>> phenotypes, int generation)
        {
            this.phenotypes = phenotypes.Any()
                ? phenotypes.ToArray()
                : throw new ArgumentException($"{nameof(phenotypes)} cannot be empty.");

            Generation = generation;
        }

        public Population(IEnumerable<Phenotype<TAllele>> phenotypes) : this(phenotypes, InitialGeneration)
        {
        }

        public int Generation { get; }

        public IEnumerable<Phenotype<TAllele>> Phenotypes =>
            phenotypes;

        public Phenotype<TAllele> Fittest =>
            phenotypes.OrderByDescending(phenotype => phenotype.Fitness).First();

        public Phenotype<TAllele> this[int index] =>
            phenotypes[index];
    }
}