using System;
using System.Collections.Generic;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm
{
    public class UniformCrossover : ICrossoverOperator
    {
        private readonly double probabilityOfGeneSwapping;

        public UniformCrossover(double probabilityOfGeneSwapping) =>
            this.probabilityOfGeneSwapping = probabilityOfGeneSwapping;

        public IEnumerable<Gene<T>> GetGenes<T>(int num)
        {
            for (int i = 0; i < num; i++)
            {
                yield return new Gene<T>(default);
            }
        }

        public IEnumerable<Phenotype<T>> Crossover<T>(IEnumerable<Phenotype<T>> parents)
        {
            var (firstParent, secondParent) = (parents.ElementAt(0), parents.ElementAt(1));

            var firstOffspring = new Genotype<T>(new Chromosome<T>(GetGenes<T>(firstParent.Genotype.Length * firstParent.Genotype[0].Length).ToArray()));
            var secondOffspring = new Genotype<T>(new Chromosome<T>(GetGenes<T>(firstParent.Genotype.Length * firstParent.Genotype[0].Length).ToArray()));

            for (int chromosomeIndex = 0; chromosomeIndex < firstParent.Genotype.Length; chromosomeIndex++)
            {
                for (int geneIndex = 0; geneIndex < firstParent.Genotype[chromosomeIndex].Length; geneIndex++)
                {
                    if (Randomizer.RandomDouble(0d, 1d) >= probabilityOfGeneSwapping)
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
                new Phenotype<T>(firstOffspring),
                new Phenotype<T>(secondOffspring),
            };
        }
    }
}