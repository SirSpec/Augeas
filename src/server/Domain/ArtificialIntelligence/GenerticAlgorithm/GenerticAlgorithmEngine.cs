using System;
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
        private readonly IMutationOperator mutationOperator;
        private readonly ITerminationAlgorithm terminationAlgorithm;

        public GenerticAlgorithmEngine(
            IFitnessFunction fitnessFunction,
            ISelectionAlgorithm selectionAlgorithm,
            ICrossoverOperator crossoverOperator,
            IMutationOperator mutationOperator,
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
        public Phenotype<T> Evolve(Population<T> population2)
        {
            //initialize population
            var population = population2; //new Population<T>(0, new Phenotype<T>(fitnessFunction, new Genotype<T>(), 0));

            //find fitness of population
            // fitnessFunction.GetFitness(genotype);
            while (terminationAlgorithm.TerminationConditionReached(population) is false)
            {
                var parents = selectionAlgorithm.Select(population.Phenotypes);
                var offspring = crossoverOperator.Crossover(parents);
                offspring = offspring.Select(mutationOperator.Mutate);

                //decode and fitness calculation
                // fitnessFunction.GetFitness(genotype);

                var survivors = selectionAlgorithm.Select(parents).Concat(selectionAlgorithm.Select(offspring));
                population = population.CreateNextGeneration(survivors.ToArray());
            }

            return population.Fittest;
        }
    }
}
