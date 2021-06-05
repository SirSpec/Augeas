using System;

namespace Hermes.Domain.ArtificialIntelligence.SimulatedAnnealing
{
	// https://en.wikipedia.org/wiki/Metropolis%E2%80%93Hastings_algorithm
	public class MetropolisAcceptanceCriterion : IAcceptanceProbabilityFunction
	{
		private readonly double boltzmannConstant;

		public MetropolisAcceptanceCriterion(double boltzmannConstant) =>
			this.boltzmannConstant = boltzmannConstant;

		public bool IsAccepted(double currentStateEnergy, double newStateEnergy, double temperature)
		{
			var deltaEnergy = newStateEnergy - currentStateEnergy;
			return Math.Exp(-deltaEnergy / (boltzmannConstant * temperature)) >= Randomizer.RandomDouble(0, 1);
		}
	}
}
