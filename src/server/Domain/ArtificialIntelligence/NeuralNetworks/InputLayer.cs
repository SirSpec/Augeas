using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
    public class InputLayer
    {
        private readonly InputNeuron[] neurons;

        public InputLayer(InputNeuron[] neurons)
        {
            this.neurons = neurons;
        }

        public void PushInputs(double[] inputs)
        {
            foreach (var pair in neurons.Zip(inputs))
            {
                pair.First.Input = pair.Second;
            }
        }
    }
}