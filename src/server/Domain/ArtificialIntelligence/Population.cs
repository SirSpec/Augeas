namespace Hermes.Domain.ArtificialIntelligence
{
    public class Population<T>
    {
        private readonly Phenotype<T>[] phenotypes;

        public Population(int generation, params Phenotype<T>[] phenotypes)
        {
            Generation = generation;
            this.phenotypes = phenotypes;
        }

        public int Generation { get; }

        public Phenotype<T> this[int index] =>
            phenotypes[index];
    }
}