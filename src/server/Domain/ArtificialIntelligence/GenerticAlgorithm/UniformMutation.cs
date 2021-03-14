namespace Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm
{
    public interface IRandomGeneProvider<T>
    {
        T RandomGene();
    }

    public class DoubleGene : IRandomGeneProvider<double>
    {
        private readonly double min;
        private readonly double max;

        public DoubleGene(double min, double max)
        {
            this.min = min;
            this.max = max;
        }

        public double RandomGene()
        {
            return Randomizer.RandomDouble(min, max);
        }
    }

    public class UniformMutation<T> : IMutationOperator<T>
    {
        private readonly IRandomGeneProvider<T> randomGeneProvider;
        private readonly double mutationProbability;

        public UniformMutation(
            IRandomGeneProvider<T> randomGeneProvider,
            double mutationProbability)
        {
            this.randomGeneProvider = randomGeneProvider;
            this.mutationProbability = mutationProbability;
        }

        public Phenotype<T> Mutate(Phenotype<T> phenotype)
        {
            foreach (var chromosome in phenotype.Genotype.Genes)
            {
                foreach (var gene in chromosome)
                {
                    if (Randomizer.RandomDouble(0, 1) >= mutationProbability)
                    {
                        gene.Allele = randomGeneProvider.RandomGene();
                    }
                }
            }

            return phenotype;
        }
    }
}