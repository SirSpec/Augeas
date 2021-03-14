namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm
{
    public class Phenotype<T>
    {
        public Phenotype(Genotype<T> genotype)
        {
            Genotype = genotype;
        }

        public Genotype<T> Genotype { get; }
        public double Fitness { get; set; }
    }
}