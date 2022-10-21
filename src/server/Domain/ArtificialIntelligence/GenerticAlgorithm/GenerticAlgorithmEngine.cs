using System.Collections.Generic;
using System.Linq;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Crossover;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Mutation;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Selection;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Termination;

namespace Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm
{
	// https://en.wikipedia.org/wiki/Genetic_algorithm
	public class GenerticAlgorithmEngine<TAllele>
	{
		private readonly IFitnessFunction fitnessFunction;
		private readonly ISelectionAlgorithm selectionAlgorithm;
		private readonly ICrossoverOperator crossoverOperator;
		private readonly IMutationOperator<TAllele> mutationOperator;
		private readonly ITerminationAlgorithm terminationAlgorithm;

		public GenerticAlgorithmEngine(
			IFitnessFunction fitnessFunction,
			ISelectionAlgorithm selectionAlgorithm,
			ICrossoverOperator crossoverOperator,
			IMutationOperator<TAllele> mutationOperator,
			ITerminationAlgorithm terminationAlgorithm)
		{
			this.fitnessFunction = fitnessFunction;
			this.selectionAlgorithm = selectionAlgorithm;
			this.crossoverOperator = crossoverOperator;
			this.mutationOperator = mutationOperator;
			this.terminationAlgorithm = terminationAlgorithm;
		}

		public Population<TAllele> GenerateNewPopulation(Population<TAllele> population)
		{
			var parents = selectionAlgorithm.Select(population.Phenotypes);
			var offspring = GenerateOffspring(parents);
			var survivors = SelectSurvivors(parents, offspring);

			return new Population<TAllele>(
				survivors,
				population.Generation + 1
			);
		}

		private IEnumerable<Phenotype<TAllele>> GenerateOffspring(IEnumerable<Phenotype<TAllele>> parents)
		{
			var newOffspring = new List<Phenotype<TAllele>>();

			foreach (var pairedParents in GetPair(parents))
			{
				var offspring = crossoverOperator.Crossover(pairedParents);

				foreach (var child in offspring)
					child.Genotype = mutationOperator.GetMutation(child.Genotype);

				newOffspring.AddRange(offspring);
			}

			return newOffspring;
		}

		public IEnumerable<IEnumerable<Phenotype<TAllele>>> GetPair(IEnumerable<Phenotype<TAllele>> parents)
		{
			for (int i = 0; i < parents.Count() - 1; i += 2)
			{
				yield return new[] {
					parents.ElementAt(i), parents.ElementAt(i + 1)
				};
			}
		}

		private IEnumerable<Phenotype<TAllele>> SelectSurvivors(IEnumerable<Phenotype<TAllele>> parents, IEnumerable<Phenotype<TAllele>> newOffspring)
		{
			return parents
				.Take(4)
				.Concat(newOffspring.Take(6))
				.ToArray();
		}
	}
}
