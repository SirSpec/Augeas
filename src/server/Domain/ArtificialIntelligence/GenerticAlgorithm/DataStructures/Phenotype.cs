using System.Collections.Generic;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures
{
    public class Phenotype<TAllele>
    {
        public Phenotype(Genotype<TAllele> genotype) =>
            Genotype = genotype;

        public Genotype<TAllele> Genotype { get; }
        public double Fitness { get; set; }

        public IEnumerable<Gene<TAllele>> FlattenGenes =>
            Genotype.Chromosomes.SelectMany(chromosome => chromosome.Genes);
    }
}