using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;

namespace Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm
{
	// https://en.wikipedia.org/wiki/Fitness_function
	public interface IFitnessFunction
	{
		double GetFitness<TAllele>(Phenotype<TAllele> phenotype);
	}
}