namespace Hermes.Domain.ArtificialIntelligence.SimulatedAnnealing
{
	public interface IEnergyFunction
	{
		// Cost/Loss/Goal function
		double Energy(object state);
	}
}
