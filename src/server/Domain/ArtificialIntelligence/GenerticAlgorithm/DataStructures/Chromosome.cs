using System;
using System.Collections.Generic;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures
{
	// https://en.wikipedia.org/wiki/Chromosome_(genetic_algorithm)
	public class Chromosome<TAllele>
	{
		private readonly Gene<TAllele>[] genes;

		public Chromosome(IEnumerable<Gene<TAllele>> genes) =>
			this.genes = genes.Any()
				? genes.ToArray()
				: throw new ArgumentException($"{nameof(genes)} cannot be empty.");

		public int Length =>
			genes.Length;

		public IEnumerable<Gene<TAllele>> Genes =>
			genes;

		public TAllele this[int index]
		{
			get => genes[index].Allele;
			set => genes[index].Allele = value;
		}

		public Chromosome<TAllele> DeepCopy() =>
			new Chromosome<TAllele>(
				genes.Select(gene => new Gene<TAllele>(gene.Allele))
			);
	}
}