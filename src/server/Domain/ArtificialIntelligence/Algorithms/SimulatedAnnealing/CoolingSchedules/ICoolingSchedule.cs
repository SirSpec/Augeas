namespace Hermes.Domain.ArtificialIntelligence.Algorithms.SimulatedAnnealing.CoolingSchedules
{
	public interface ICoolingSchedule
	{
		double Temperature(int iteration);
	}
}
