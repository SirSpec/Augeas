using System.Linq;
using Hermes.Domain.ArtificialIntelligence;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.Builders;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Crossover;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Mutation;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Selection;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Termination;

namespace Hermes.Infrastructure.WebApi
{
	public class Simulator
	{
		private static readonly ConstraintRange<double> range = new ConstraintRange<double>(0, 1);

		private readonly GenerticAlgorithmEngine<double> engine = new GenerticAlgorithmEngine<double>
		(
			null!,
			new ElitismSelection(6),
			new UniformCrossover(range, 0.6),
			new UniformMutation<double>(new RandomDoubleAlleleFactory(new ConstraintRange<double>(-1, 1)), range, 0.7),
			new FitnessValueTermination(1d)
		);

		private Population<double> population;

		private readonly Actor[] actors;

		public Simulator()
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

			var phenotypes = actors.Select(actor => actor.NeuralNetwork.AllConnections.Select(c => c.Weight))
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
			population = new Population<double>(phenotypes);
		}

		public double GetAngle(int index, double[] signals) =>
			actors[index].GetAngle(signals);

		public double SetFitness(int index, double fitness) =>
			population[index].Fitness = fitness;

		public void GenerateNewPopulation()
		{
			population = engine.GenerateNewPopulation(population);

			for (int i = 0; i < actors.Length; i++)
			{
				actors[i].SetWeights(population[i]);
			}
		}
	}
}