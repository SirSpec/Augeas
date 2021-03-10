using System.Linq;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks;

namespace Hermes.Infrastructure.WebApi
{
    public class Actor
    {
        public Actor(NeuralNetwork neuralNetwork) =>
            NeuralNetwork = neuralNetwork;

        public NeuralNetwork NeuralNetwork { get; }

        public double GetAngle(double[] inputs)
        {
            return NeuralNetwork.Comput(inputs).Single();
        }
    }
}