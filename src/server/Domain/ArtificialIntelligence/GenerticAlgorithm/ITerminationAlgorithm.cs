namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm
{
    public interface ITerminationAlgorithm
    {
        bool TerminationConditionReached<T>(Population<T> population);
    }
}