using System;

namespace Hermes.Domain.ArtificialIntelligence.Algorithms.SimulatedAnnealing.CoolingSchedules
{
	// all must be monotonic
	public class ExponentialMultiplicativeCooling : ICoolingSchedule
	{
		// Hyperparameters
		public readonly double InitialTemperature;

		// Suggestion 0.8 <= Constant <= 0.9
		public readonly double DecreaseFactor;

		public double Temperature(int iteration)
		{
			return InitialTemperature * CoolingFactor(iteration);
		}

		private double CoolingFactor(int iteration)
		{
			return Math.Pow(DecreaseFactor, iteration);
		}
	}

	// T_k+1 = a * T_k
	public class RecurrentCooling : ICoolingSchedule
	{
		private readonly double initialTemperature;
		private double currentTemperature;

		// Suggestion 0.8 <= Constant <= 0.9
		private readonly double coolingRate;

		public RecurrentCooling(double initialTemperature, double coolingRate)
		{
			this.initialTemperature = initialTemperature;
			currentTemperature = initialTemperature;
			this.coolingRate = coolingRate;
		}

		public double Temperature(int _)
		{
			currentTemperature = coolingRate * currentTemperature;
			return currentTemperature;
		}
	}

	// non-monotonic
	// T_k = T_n + (T_0 - T_n) * ((n - k) / n)^2
	public class QuadraticAdditiveCooling : ICoolingSchedule
	{
		private readonly double initialTemperature;
		private readonly double finalTemperature;
		private readonly double maxIteration;

		public double Temperature(int iteration)
		{
			return finalTemperature + (initialTemperature - finalTemperature) * Math.Pow(((maxIteration - iteration) / maxIteration), 2);
		}
	}
}
