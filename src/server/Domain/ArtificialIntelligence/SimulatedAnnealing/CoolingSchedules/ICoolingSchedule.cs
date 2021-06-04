namespace Hermes.Domain.ArtificialIntelligence.SimulatedAnnealing.CoolingSchedules
{
	// https://en.wikipedia.org/wiki/Simulated_annealing
	// Temperature should be monotonically decreasing function
	public interface ICoolingSchedule
	{
		double Temperature(int iteration);
	}
}
