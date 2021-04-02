using System.Collections.Generic;
using System.Linq;
using Hermes.Domain.ArtificialIntelligence;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks.Activations;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks.Propagations;
using Xunit;

namespace ArtificialIntelligenceTest
{
	public class NeuralNetworkTest
	{
		[Fact]
		public void Test()
		{
			var w1 = new NeuralNetworkBuilder();

			var n = w1.Build(6, 1, 2);
		}
	}
}