namespace Hermes.Domain.ArtificialIntelligence.SimulatedAnnealing
{
	public interface IAcceptanceProbabilityFunction
	{
		bool IsAccepted(double currentStateEnergy, double newStateEnergy, double temperature);
	}
}
