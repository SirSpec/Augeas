using System.Collections.Generic;
using System.Linq;
using Hermes.Domain.ArtificialIntelligence;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks.Activations;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks.Propagations;

namespace Hermes.Infrastructure.WebApi
{
    public class ActorFactory
    {
        public static Actor GetActor()
        {
            var build = new NeuralNetworkBuilder();

            return new Actor(build.Build(6, 1, 2));
        }

        private static IEnumerable<double> RandomWeight(int size)
        {
            for (int i = 0; i < size; i++)
            {
                yield return Randomizer.RandomDouble(-1, 1);
            }
        }

        private static Neuron GetNeuron(IEnumerable<INeuron> neurons, IEnumerable<double> weights)
        {
            var zipped = neurons.Zip(weights);

            return new Neuron(
                new WeightedSumFunction(),
                new HyperbolicTangentFunction(),
                zipped
                    .Select(connection => new Connection(connection.First, connection.Second))
                    .ToList(),
                0
            );
        }
    }
}
