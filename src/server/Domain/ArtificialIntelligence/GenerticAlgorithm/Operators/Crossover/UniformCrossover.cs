using System;
using System.Collections.Generic;
using System.Linq;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;

namespace Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Crossover
{
	public class UniformCrossover : ICrossoverOperator
	{
		private readonly ConstraintRange<double> randomizerConstraints;
		private readonly double probabilityOfGeneSwapping;

		public UniformCrossover(
			ConstraintRange<double> randomizerConstraints,
			double probabilityOfGeneSwapping)
		{
			if (probabilityOfGeneSwapping >= randomizerConstraints.Min && probabilityOfGeneSwapping <= randomizerConstraints.Max)
			{
				this.randomizerConstraints = randomizerConstraints;
				this.probabilityOfGeneSwapping = probabilityOfGeneSwapping;
			}
			else throw new ArgumentException(
			  $"{nameof(probabilityOfGeneSwapping)}:{probabilityOfGeneSwapping} is outside the " +
			  $"{nameof(randomizerConstraints)} boundaries:{randomizerConstraints.Min},{randomizerConstraints.Min}.");
		}

		public IEnumerable<Phenotype<TAllele>> Crossover<TAllele>(IEnumerable<Phenotype<TAllele>> parents)
		{
			var (firstParent, secondParent) = (parents.ElementAt(0), parents.ElementAt(1));

			var firstOffspring = firstParent.Genotype.DeepCopy();
			var secondOffspring = secondParent.Genotype.DeepCopy();

			for (int chromosomeIndex = 0; chromosomeIndex < firstParent.Genotype.Length; chromosomeIndex++)
			{
				for (int geneIndex = 0; geneIndex < firstParent.Genotype[chromosomeIndex].Length; geneIndex++)
				{
					if (CanSwap())
					{
						firstOffspring[chromosomeIndex][geneIndex] = secondParent.Genotype[chromosomeIndex][geneIndex];
						secondOffspring[chromosomeIndex][geneIndex] = firstParent.Genotype[chromosomeIndex][geneIndex];
					}
					else
					{
						firstOffspring[chromosomeIndex][geneIndex] = firstParent.Genotype[chromosomeIndex][geneIndex];
						secondOffspring[chromosomeIndex][geneIndex] = secondParent.Genotype[chromosomeIndex][geneIndex];
					}
				}
			}

			return new[]
			{
				new Phenotype<TAllele>(firstOffspring),
				new Phenotype<TAllele>(secondOffspring),
			};
		}

		private bool CanSwap() =>
			Randomizer.RandomDouble(randomizerConstraints.Min, randomizerConstraints.Max) >= probabilityOfGeneSwapping;
	}
}