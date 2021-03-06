namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
    public class NeuralLayer
    {
        public Neuron[] Neurons { get; }

        public NeuralLayer(Neuron[] neurons)
        {
            Neurons = neurons;
        }
    }
}