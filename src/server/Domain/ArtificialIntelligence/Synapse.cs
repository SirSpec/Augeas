namespace Hermes.Domain.ArtificialIntelligence
{
    public class Synapse
    {
        private (INeuron From, INeuron To) neurons;

        public double Weight { get; set; }
    }
}