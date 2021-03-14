using System.Collections.Generic;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm
{
    // https://en.wikipedia.org/wiki/Chromosome_(genetic_algorithm)
    public class Chromosome<TAllele>
    {
        private readonly Gene<TAllele>[] genes;

        public Chromosome(params Gene<TAllele>[] genes) =>
            this.genes = genes;

        public int Length =>
            genes.Length;

        public TAllele this[int index]
        {
            get => genes[index].Allele;
            set => genes[index].Allele = value;
        }

        public IEnumerable<Gene<TAllele>> Genes => genes;
    }
}