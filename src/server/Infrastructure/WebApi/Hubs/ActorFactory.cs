using System.Collections.Generic;
using System.Linq;
using Hermes.Domain.ArtificialIntelligence;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks;

namespace Hermes.Infrastructure.WebApi
{
    public class ActorFactory
    {
        public static Actor GetActor()
        {
            var inputLayer = new InputLayer(
                new InputNeuron(),
                new InputNeuron(),
                new InputNeuron(),
                new InputNeuron(),
                new InputNeuron(),
                new InputNeuron()
            );

            var hiddenLayer1 = new NeuralLayer(
                GetNeuron(inputLayer.Neurons, RandomWeight(6)),
                GetNeuron(inputLayer.Neurons, RandomWeight(6)),
                GetNeuron(inputLayer.Neurons, RandomWeight(6)),
                GetNeuron(inputLayer.Neurons, RandomWeight(6))
            );

            var hiddenLayer2 = new NeuralLayer(
                GetNeuron(hiddenLayer1.Neurons, RandomWeight(4)),
                GetNeuron(hiddenLayer1.Neurons, RandomWeight(4)),
                GetNeuron(hiddenLayer1.Neurons, RandomWeight(4))
            );

            var outputLayer = new OutputLayer(
                GetNeuron(hiddenLayer2.Neurons, RandomWeight(3))
            );

            return new Actor(
                new NeuralNetwork(
                    inputLayer,
                    new NeuralLayer[2]
                    {
                        hiddenLayer1,
                        hiddenLayer2
                    },
                    outputLayer
                )
            );
        }

        private static IEnumerable<double> RandomWeight(int size)
        {
            for (int i = 0; i < size; i++)
            {
                yield return Randomizer.RandomDouble(-10, 10);
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
