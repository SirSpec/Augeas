namespace Hermes.Domain.ArtificialIntelligence
{
    public interface ITerminationAlgorithm
    {
        bool TerminationConditionReached<T>(Population<T> population);
    }
}