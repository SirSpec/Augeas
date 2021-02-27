namespace Hermes.Domain.ArtificialIntelligence
{
    public class Phenotype<T>
    {
        private readonly IFitnessFunction fitnessFunction;

        public Phenotype(IFitnessFunction fitnessFunction, Genotype<T> genotype, int generation)
        {
            this.fitnessFunction = fitnessFunction;
            Genotype = genotype;
            Generation = generation;
        }

        public int Generation { get; }
        public Genotype<T> Genotype { get; }
        public double Fitness => fitnessFunction.GetFitness(Genotype);
    }
}