using System.Collections.Generic;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm
{
    // https://en.wikipedia.org/wiki/Chromosome_(genetic_algorithm)
    public class Genotype<T>
    {
        private readonly Chromosome<T>[] chromosomes;

        public Genotype(params Chromosome<T>[] chromosomes) =>
            this.chromosomes = chromosomes;

        public int Length =>
            chromosomes.Length;

        public IEnumerable<IEnumerable<Gene<T>>> Genes =>
            chromosomes.Select(chromosome => chromosome.Genes);

        public Chromosome<T> this[int index] =>
            chromosomes[index];

        public T this[int chromosomeIndex, int geneIndex]
        {
            get => chromosomes[chromosomeIndex][geneIndex];
            set => chromosomes[chromosomeIndex][geneIndex] = value;
        }
    }
}