namespace Hermes.Domain.ArtificialIntelligence
{
    // https://en.wikipedia.org/wiki/Chromosome_(genetic_algorithm)
    public class Chromosome<TAllele>
    {
        private readonly Gene<TAllele>[] genes;
        public Chromosome(params Gene<TAllele>[] genes) =>
            this.genes = genes;

        public Chromosome(int size) : this(new Gene<TAllele>[size])
        {
        }

        public Gene<TAllele> this[int index] =>
            genes[index];

        public int Length =>
            genes.Length;
    }
}