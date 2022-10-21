using System;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Builders;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;

namespace Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Mutation
{
	public class UniformMutation<TAllele> : IMutationOperator<TAllele>
	{
		private readonly IAlleleFactory<TAllele> alleleFactory;
		private readonly ConstraintRange<double> randomizerConstraints;
		private readonly double mutationProbability;

		public UniformMutation(
			IAlleleFactory<TAllele> alleleFactory,
			ConstraintRange<double> randomizerConstraints,
			double mutationProbability)
		{
			if (mutationProbability >= randomizerConstraints.Min && mutationProbability <= randomizerConstraints.Max)
			{
				this.alleleFactory = alleleFactory;
				this.randomizerConstraints = randomizerConstraints;
				this.mutationProbability = mutationProbability;
			}
			else throw new ArgumentException(
				$"{nameof(mutationProbability)}:{mutationProbability} is outside the " +
				$"{nameof(randomizerConstraints)} boundaries:{randomizerConstraints.Min},{randomizerConstraints.Min}.");
		}

		public Genotype<TAllele> GetMutation(Genotype<TAllele> genotype)
		{
			var newGenotype = genotype.DeepCopy();

			foreach (var chromosome in newGenotype.Chromosomes)
				foreach (var gene in chromosome.Genes)
					if (CanMutate())
						gene.Allele = alleleFactory.GetValue();

			return newGenotype;
		}

		private bool CanMutate() =>
			Randomizer.RandomDouble(randomizerConstraints.Min, randomizerConstraints.Max) >= mutationProbability;
	}
}