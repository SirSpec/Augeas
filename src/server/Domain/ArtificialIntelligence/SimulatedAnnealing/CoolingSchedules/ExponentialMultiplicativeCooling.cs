using System;

namespace Augeas.Domain.ArtificialIntelligence.SimulatedAnnealing.CoolingSchedules
{
	// T_k = T_0 * a^k
	public class ExponentialMultiplicativeCooling : ICoolingSchedule
	{
		private readonly double initialTemperature;
		private readonly double decreaseFactor;

		public ExponentialMultiplicativeCooling(double initialTemperature, double decreaseFactor)
		{
			if (initialTemperature > 0 && (decreaseFactor is > 0 and < 1))
			{
				this.initialTemperature = initialTemperature;
				this.decreaseFactor = decreaseFactor;
			}
			else throw new AggregateException($"Input {nameof(initialTemperature)} has to be greater than zero.");
		}

		public double Temperature(int iteration) =>
			iteration >= 0
				? initialTemperature * CoolingFactor(iteration)
				: throw new AggregateException($"Input {nameof(iteration)} has to be greater or equal zero.");

		private double CoolingFactor(int iteration) =>
			Math.Pow(decreaseFactor, iteration);
	}
}
