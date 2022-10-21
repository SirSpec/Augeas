using System;

namespace Augeas.Domain.ArtificialIntelligence.SimulatedAnnealing.CoolingSchedules
{
	// T_k = T_n + (T_0 - T_n) * ((n - k) / n)^2
	public class QuadraticAdditiveCooling : ICoolingSchedule
	{
		private readonly double initialTemperature;
		private readonly double finalTemperature;
		private readonly double maximumIterationIndex;

		public QuadraticAdditiveCooling(double initialTemperature, double finalTemperature, double maximumIterationIndex)
		{
			if (initialTemperature > 0 && finalTemperature < initialTemperature && maximumIterationIndex > 0)
			{
				this.initialTemperature = initialTemperature;
				this.finalTemperature = finalTemperature;
				this.maximumIterationIndex = maximumIterationIndex;
			}
			else throw new AggregateException(
				$"Inputs {nameof(initialTemperature)} and {nameof(finalTemperature)} has to be greater than zero.{Environment.NewLine}");
		}

		public double Temperature(int iteration) =>
			iteration >= 0 && iteration <= maximumIterationIndex
				? finalTemperature + (initialTemperature - finalTemperature) * Math.Pow(((maximumIterationIndex - iteration) / maximumIterationIndex), 2)
				: throw new AggregateException($"Input {nameof(iteration)} has to be greater or equal zero.");
	}
}
