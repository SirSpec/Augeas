namespace Hermes.Domain.ArtificialIntelligence
{
    // https://en.wikipedia.org/wiki/Chromosome_(genetic_algorithm)
    public class Genotype<T>
    {
        private readonly Chromosome<T>[] chromosomes;

        public Genotype(params Chromosome<T>[] chromosomes) =>
            this.chromosomes = chromosomes;

        public Genotype(int size) : this(new Chromosome<T>[size])
        {
        }

        public Chromosome<T> this[int index] =>
            chromosomes[index];
    }
}