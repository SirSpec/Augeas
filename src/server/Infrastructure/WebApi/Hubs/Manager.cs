using System.Linq;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm;

namespace Hermes.Infrastructure.WebApi
{
    public class FitnessFun : IFitnessFunction
    {
        public double GetFitness<T>(Phenotype<T> phenotype)
        {
            return phenotype.Fitness;
        }
    }

    public class Manager
    {
        private readonly GenerticAlgorithmEngine<double> engine = new GenerticAlgorithmEngine<double>
        (
            new FitnessFun(),
            new ElitismSelection(6),
            new UniformCrossover(0.6),
            new UniformMutation<double>(new DoubleGene(-1, 1), 0.3),
            new FitnessValueTermination(1d)
        );

        private Population<double> population;

        private readonly Actor[] actors;

        public Manager()
        {
            actors = new[]
            {
                ActorFactory.GetActor(),
                ActorFactory.GetActor(),
                ActorFactory.GetActor(),
                ActorFactory.GetActor(),
                ActorFactory.GetActor(),
                ActorFactory.GetActor(),
                ActorFactory.GetActor(),
                ActorFactory.GetActor(),
                ActorFactory.GetActor(),
                ActorFactory.GetActor(),
            };

            var phenotypes = actors.Select(actor => actor.NeuralNetwork.GetWeights())
                .Select(ws => new Phenotype<double>
                    (
                        new Genotype<double>(
                            new[] {
                                new Chromosome<double>
                                (
                                    ws.Select(w => new Gene<double>(w)).ToArray()
                                )
                            }
                        )
                    )
                )
                .ToArray();
            population = new Population<double>(0, phenotypes);
        }

        public double GetAngle(int index, double[] signals) =>
            actors[index].GetAngle(signals);

        public double SetFitness(int index, double fitness) =>
            population[index].Fitness = fitness;

        public void GenerateNewPopulation()
        {
            population = engine.Generate(population);

            for (int i = 0; i < actors.Length; i++)
            {
                actors[i].SetWeights(population[i].Genotype);
            }
        }
    }
}