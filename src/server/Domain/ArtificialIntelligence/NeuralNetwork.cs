using System.Collections.Generic;

namespace Hermes.Domain.ArtificialIntelligence
{
    // https://en.wikipedia.org/wiki/Neural_network
    public class NeuralNetwork
    {
        public IEnumerable<NeuronLayer> Layers { get; }
    }
}