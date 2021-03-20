namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.Builders
{
    public interface IAlleleFactory<TAllele>
    {
        TAllele GetValue();
    }
}