using System.Collections.Generic;
using System.Linq;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks.Activations;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks.Propagations;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
    public class NeuralNetworkBuilder
    {
        public NeuralNetwork Build(int inputs, int outputs, int layers)
        {
            var inputLayer = BuildInputLayer(inputs);
            var hiddenLayers = BuildHiddrenLayer(layers, inputLayer);
            var outputLayer = BuildOutputLayer(outputs, hiddenLayers.Last().Neurons, RandomWeight(hiddenLayers.Last().Neurons.Count()));

            return new NeuralNetwork(inputLayer, hiddenLayers.ToArray(), outputLayer);
        }

        private OutputLayer BuildOutputLayer(int outputs, IEnumerable<INeuron> connections, IEnumerable<double> weights)
        {
            return new OutputLayer(BuildNeurons(outputs, connections, weights).ToArray());
        }

        private static IEnumerable<double> RandomWeight(int size)
        {
            for (int i = 0; i < size; i++)
            {
                yield return Randomizer.RandomDouble(-1, 1);
            }
        }

        private IEnumerable<Neuron> BuildNeurons(int neurons, IEnumerable<INeuron> connections, IEnumerable<double> weights)
        {
            var zipped = connections.Zip(weights);

            for (int i = 0; i < neurons; i++)
                yield return new Neuron(
                new WeightedSumFunction(),
                new HyperbolicTangentFunction(),
                zipped
                    .Select(connection => new Connection(connection.First, connection.Second))
                    .ToList(),
                0
            );
        }

        private InputLayer BuildInputLayer(int inputs)
        {
            return new InputLayer(BuildInputNeurons(inputs).ToArray());
        }

        private IEnumerable<InputNeuron> BuildInputNeurons(int inputs)
        {
            for (int i = 0; i < inputs; i++)
                yield return new InputNeuron();
        }

        private IEnumerable<NeuralLayer> BuildHiddrenLayer(int layers, InputLayer inputLayer)
        {
            var current = new NeuralLayer(
                BuildNeurons(4, inputLayer.Neurons, RandomWeight(inputLayer.Neurons.Count())).ToArray()
            );

            for (int i = 0; i < layers; i++)
            {
                yield return current;

                current = new NeuralLayer(
                    BuildNeurons(3, current.Neurons, RandomWeight(current.Neurons.Count())).ToArray()
                );
            }
        }
    }
}