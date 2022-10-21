using System;
using System.Collections.Generic;
using System.Linq;

namespace Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures
{
	// https://en.wikipedia.org/wiki/Chromosome_(genetic_algorithm)
	public class Genotype<TAllele>
	{
		private readonly Chromosome<TAllele>[] chromosomes;

		public Genotype(IEnumerable<Chromosome<TAllele>> chromosomes) =>
			this.chromosomes = chromosomes.Any()
				? chromosomes.ToArray()
				: throw new ArgumentException($"{nameof(chromosomes)} cannot be empty.");

		public int Length =>
			chromosomes.Length;

		public IEnumerable<Chromosome<TAllele>> Chromosomes =>
			chromosomes;

		public Chromosome<TAllele> this[int index] =>
			chromosomes[index];

		public Genotype<TAllele> DeepCopy() =>
			new Genotype<TAllele>(
				chromosomes.Select(chromosome => chromosome.DeepCopy())
			);
	}
}