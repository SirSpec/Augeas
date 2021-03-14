using System.Collections.Generic;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
    public class OutputLayer
    {
        public readonly Neuron[] neurons;

        public OutputLayer(params Neuron[] neurons)
        {
            this.neurons = neurons;
        }

        public IEnumerable<double> Outputs =>
            neurons.Select(neuron => neuron.Output);
    }
}