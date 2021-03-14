using System.Collections.Generic;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm
{
    // https://en.wikipedia.org/wiki/Genetic_algorithm
    public class GenerticAlgorithmEngine<T>
    {
        private readonly IFitnessFunction fitnessFunction;
        private readonly ISelectionAlgorithm selectionAlgorithm;
        private readonly ICrossoverOperator crossoverOperator; //Alterer operators
        private readonly IMutationOperator<T> mutationOperator;
        private readonly ITerminationAlgorithm terminationAlgorithm;

        public GenerticAlgorithmEngine(
            IFitnessFunction fitnessFunction,
            ISelectionAlgorithm selectionAlgorithm,
            ICrossoverOperator crossoverOperator,
            IMutationOperator<T> mutationOperator,
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
        // public Phenotype<T> Evolve(Population<T> population2)
        // {
        //     //initialize population
        //     var population = population2; //new Population<T>(0, new Phenotype<T>(fitnessFunction, new Genotype<T>(), 0));

        //     //find fitness of population
        //     foreach (var phenotype in population.Phenotypes)
        //     {
        //         phenotype.Fitness = fitnessFunction.GetFitness(phenotype);
        //     }

        //     while (terminationAlgorithm.TerminationConditionReached(population) is false)
        //     {
        //         var parents = selectionAlgorithm.Select(population.Phenotypes);

        //         var offsprings = new List<Phenotype<T>>();

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

        public IEnumerable<(Phenotype<T> f, Phenotype<T> s)> GetPair(Phenotype<T>[] parents)
        {
            for (int i = 0; i < parents.Length - 1; i += 2)
            {
                yield return (parents[i], parents[i + 1]);
            }
        }

        public Population<T> Generate(Population<T> population)
        {
            var newPopulation = population;

            while (terminationAlgorithm.TerminationConditionReached(newPopulation) is false)
            {
                var parents = selectionAlgorithm.Select(newPopulation.Phenotypes);

                var newOffspring = new List<Phenotype<T>>();

                foreach (var pair in GetPair(parents.ToArray()))
                {
                    var offspring = crossoverOperator.Crossover(new[] { pair.f, pair.s });
                    offspring = offspring.Select(mutationOperator.Mutate);
                    newOffspring.AddRange(offspring);
                }

                var survivors = parents.Concat(newOffspring)
                    .Take(10)
                    .ToArray();

                return new Population<T>(
                    population.Generation + 1,
                    survivors
                );
            }

            return population;
        }
    }
}
