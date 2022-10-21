using System.Linq;
using Augeas.Domain.ArtificialIntelligence;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Builders;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Crossover;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Mutation;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Selection;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Termination;

namespace Augeas.Infrastructure.WebApi
{
	public class Simulator
	{
		private static readonly ConstraintRange<double> range = new ConstraintRange<double>(0, 1);

		private readonly GenerticAlgorithmEngine<double> engine = new GenerticAlgorithmEngine<double>
		(
			null!,
			new ElitismSelection(6),
			new UniformCrossover(range, 0.3),
			new UniformMutation<double>(new RandomDoubleAlleleFactory(new ConstraintRange<double>(-1, 1)), range, 0.3),
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

			population = new Population<double>(
				actors.Select(actor =>
					new Phenotype<double>(
						new Genotype<double>(
							actor.NeuralNetwork.Neurons.Select(neuron => new Chromosome<double>(
								neuron.Inputs.Select(input => new Gene<double>(input.Weight))
							))
						)
					)
				)
			);
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
