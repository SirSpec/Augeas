using System.Linq;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm;
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

        public void SetWeights(Genotype<double> genotype)
        {
            var g = genotype.Genes.SelectMany(ge => ge).ToArray();
            for (int i = 0; i < g.Length; i++)
            {
                NeuralNetwork.SetWeight(i, g[i].Allele);
            }
        }
    }
}