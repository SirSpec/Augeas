using System.Collections.Generic;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;

namespace Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Builders
{
	public class PhenotypeFactory<TAllele>
	{
		private readonly IAlleleFactory<TAllele> alleleFactory;

		public PhenotypeFactory(IAlleleFactory<TAllele> alleleFactory)
		{
			this.alleleFactory = alleleFactory;
		}

		public Phenotype<TAllele> Build(int chromosomeSize, int genesSize)
		{
			return new Phenotype<TAllele>(
				new Genotype<TAllele>(
					new List<Chromosome<TAllele>>(BuildChromosomes(chromosomeSize, genesSize))
				)
			);
		}

		private IEnumerable<Chromosome<TAllele>> BuildChromosomes(int chromosomeSize, int genesSize)
		{
			for (int i = 0; i < chromosomeSize; i++)
			{
				yield return new Chromosome<TAllele>(BuildGenes(genesSize));
			}
		}

		private IEnumerable<Gene<TAllele>> BuildGenes(int genesSize)
		{
			for (int i = 0; i < genesSize; i++)
			{
				yield return new Gene<TAllele>(alleleFactory.GetValue());
			}
		}
	}
}