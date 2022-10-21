using System.Collections.Generic;
using System.Linq;

namespace Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures
{
	public class Phenotype<TAllele>
	{
		public Phenotype(Genotype<TAllele> genotype) =>
			Genotype = genotype;

		public Genotype<TAllele> Genotype { get; set; }
		public double Fitness { get; set; }

		public IEnumerable<Gene<TAllele>> FlattenGenes =>
			Genotype.Chromosomes.SelectMany(chromosome => chromosome.Genes);
	}
}