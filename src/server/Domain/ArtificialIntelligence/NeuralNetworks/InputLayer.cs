using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
    public class InputLayer
    {
        public InputNeuron[] Neurons { get; }

        public InputLayer(InputNeuron[] neurons)
        {
            this.Neurons = neurons;
        }

        public void PushInputs(double[] inputs)
        {
            foreach (var pair in Neurons.Zip(inputs))
            {
                pair.First.Input = pair.Second;
            }
        }
    }
}