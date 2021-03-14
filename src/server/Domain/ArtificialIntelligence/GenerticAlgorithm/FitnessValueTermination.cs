using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm
{
    public class FitnessValueTermination : ITerminationAlgorithm
    {
        private readonly double fitnessTerminationValue;

        public FitnessValueTermination(double fitnessTerminationValue) =>
            this.fitnessTerminationValue = fitnessTerminationValue;

        public bool TerminationConditionReached<T>(Population<T> population) =>
            population.Phenotypes.Any(phenotype => phenotype.Fitness == fitnessTerminationValue);
    }
}