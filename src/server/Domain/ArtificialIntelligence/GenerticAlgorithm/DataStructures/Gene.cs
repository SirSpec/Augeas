namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures
{
    public class Gene<TAllele>
    {
        public Gene(TAllele allele) =>
            Allele = allele;

        public TAllele Allele { get; set; }
    }
}