using System.Collections.Generic;
using System.Linq;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Crossover;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Mutation;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Selection;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Termination;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm
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

		// START
		// Generate the initial population
		// Compute fitness
		// REPEAT
		//     Selection
		//     Crossover
		//     Mutation
		//     Compute fitness
		// UNTIL population has converged
		// STOP
		// public Phenotype<TAllele> Evolve(Population<TAllele> population2)
		// {
		//     //initialize population
		//     var population = population2; //new Population<TAllele>(0, new Phenotype<TAllele>(fitnessFunction, new Genotype<TAllele>(), 0));

		//     //find fitness of population
		//     foreach (var phenotype in population.Phenotypes)
		//     {
		//         phenotype.Fitness = fitnessFunction.GetFitness(phenotype);
		//     }

		//     while (terminationAlgorithm.TerminationConditionReached(population) is false)
		//     {
		//         var parents = selectionAlgorithm.Select(population.Phenotypes);

		//         var offsprings = new List<Phenotype<TAllele>>();

		//         foreach (var pair in parents)
		//         {
		//             var offspring = crossoverOperator.Crossover(parents);
		//             offspring = offspring.Select(mutationOperator.Mutate);
		//             offsprings.AddRange(offspring);
		//         }

		//         foreach (var phenotype in offsprings)
		//         {
		//             phenotype.Fitness = fitnessFunction.GetFitness(phenotype);
		//         }

		//         var survivors = selectionAlgorithm.Select(parents).Concat(selectionAlgorithm.Select(offsprings));
		//         population = population.CreateNextGeneration(survivors.ToArray());
		//     }

		//     return population.Fittest;
		// }

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
