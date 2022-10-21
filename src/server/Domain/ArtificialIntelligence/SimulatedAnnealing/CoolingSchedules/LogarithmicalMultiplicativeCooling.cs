using System;

namespace Augeas.Domain.ArtificialIntelligence.SimulatedAnnealing.CoolingSchedules
{
	// T_k = T_0 / (1 + aLog(1 + k))
	public class LogarithmicalMultiplicativeCooling : ICoolingSchedule
	{
		private readonly double initialTemperature;
		private readonly double decreaseFactor;

		public LogarithmicalMultiplicativeCooling(double initialTemperature, double decreaseFactor)
		{
			if (initialTemperature > 0 && decreaseFactor > 0)
			{
				this.initialTemperature = initialTemperature;
				this.decreaseFactor = decreaseFactor;
			}
			else throw new AggregateException($"Inputs have to be greater than zero.");
		}

		public double Temperature(int iteration) =>
			iteration >= 0
				? initialTemperature / (1 + decreaseFactor * Math.Log(1 + iteration))
				: throw new AggregateException($"Input {nameof(iteration)} has to be greater or equal zero.");
	}
}
