using Hermes.Domain.ArtificialIntelligence;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks;
using Xunit;

namespace ArtificialIntelligenceTest
{
    public class NeuralNetworkTest
    {
        [Fact]
        public void Test()
        {
            var func = new BinaryStepFunction(0.7);
            var neural = new NeuralNetwork(
                new NeuralLayer(
                        new Neuron[6]
                        {
                            new Neuron(func, 0, new double[1] { 1.0 }),
                            new Neuron(func, 0, new double[1] { 1.0 }),
                            new Neuron(func, 0, new double[1] { 1.0 }),
                            new Neuron(func, 0, new double[1] { 1.0 }),
                            new Neuron(func, 0, new double[1] { 1.0 }),
                            new Neuron(func, 0, new double[1] { 1.0 }),
                        }
                    ),
                new NeuralLayer[1]
                {
                    new NeuralLayer(new Neuron[3]
                        {
                            new Neuron(func, 0, new double[6] { 1.0, 0.7, 0.5, -0.3, 0.7, -0.2 }),
                            new Neuron(func, 0, new double[6] { 1.0, 0.7, 0.5, -0.3, 0.7, -0.2 }),
                            new Neuron(func, 0, new double[6] { 1.0, 0.7, 0.5, -0.3, 0.7, -0.2 }),
                        }),
                },
                new NeuralLayer(new Neuron[1]
                        {
                            new Neuron(func, 0, new double[3] { -1.0, 0.7, 0.5 }),
                        })
            );

            var output = neural.Comput(new double[6] { 1.0, 0.7, 0.5, 0.3, 1.0, 1.0 });
        }
    }
}