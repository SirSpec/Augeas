using System.Linq;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;
using Augeas.Domain.ArtificialIntelligence.NeuralNetworks;

namespace Augeas.Infrastructure.WebApi
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

		public void SetWeights(Phenotype<double> phenotype)
		{
			var genes = phenotype.FlattenGenes.ToArray();
			var connections = NeuralNetwork.AllConnections.ToArray();

			for (int i = 0; i < genes.Length; i++)
			{
				connections[i].Weight = genes[i].Allele;
			}
		}
	}
}