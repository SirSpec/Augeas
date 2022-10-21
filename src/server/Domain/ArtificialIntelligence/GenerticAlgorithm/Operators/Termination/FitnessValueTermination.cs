using System.Linq;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;

namespace Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Termination
{
	public class FitnessValueTermination : ITerminationAlgorithm
	{
		private readonly double fitnessTerminationValue;

		public FitnessValueTermination(double fitnessTerminationValue) =>
			this.fitnessTerminationValue = fitnessTerminationValue;

		public bool TerminationConditionReached<TAllele>(Population<TAllele> population) =>
			population.Phenotypes.Any(phenotype => phenotype.Fitness == fitnessTerminationValue);
	}
}