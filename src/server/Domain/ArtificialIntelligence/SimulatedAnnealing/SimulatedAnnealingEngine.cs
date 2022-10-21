using Augeas.Domain.ArtificialIntelligence.SimulatedAnnealing.CoolingSchedules;

namespace Augeas.Domain.ArtificialIntelligence.SimulatedAnnealing
{
	// https://en.wikipedia.org/wiki/Simulated_annealing
	public class SimulatedAnnealingEngine
	{
		// Hyperparameters
		private readonly ICoolingSchedule coolingSchedule;
		private readonly IAcceptanceProbabilityFunction acceptanceProbabilityFunction;
		private readonly IStateGenerator stateGenerator;
		private readonly IEnergyFunction energyFunction;

		public SimulatedAnnealingEngine(
			IStateGenerator stateGenerator,
			IEnergyFunction energyFunction,
			ICoolingSchedule coolingSchedule,
			IAcceptanceProbabilityFunction acceptanceProbabilityFunction)
		{
			this.coolingSchedule = coolingSchedule;
			this.acceptanceProbabilityFunction = acceptanceProbabilityFunction;
			this.stateGenerator = stateGenerator;
			this.energyFunction = energyFunction;
		}

		public object Fun()
		{
			var state = stateGenerator.PickRandomNeighbour();

			for (int i = 0; i < 1000; i++)
			{
				var temperature = coolingSchedule.Temperature(i);
				var newRandomNeighbourState = stateGenerator.PickRandomNeighbour();

				var currentStateEnergy = energyFunction.Energy(state);
				var newStateEnergy = energyFunction.Energy(newRandomNeighbourState);

				if (acceptanceProbabilityFunction.IsAccepted(currentStateEnergy, newStateEnergy, temperature))
					state = newRandomNeighbourState;
			}

			return state;
		}
	}
}
