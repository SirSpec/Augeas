using System;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks.Propagations;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks.Activations;
using Xunit;
using System.Linq;

namespace ArtificialIntelligenceTest
{
	public class NeuralLayerTest
	{
		private const string TestId1 = "tesdId1";
		private const string TestId2 = "tesdId2";

		private Neuron GetNeuron(string id) =>
			new Neuron(id, new WeightedSumFunction(), new BinaryStepFunction(0), Enumerable.Empty<Connection>().ToList(), 0);

		[Fact]
		public void Constructor_DuplicatedNeuronIds_ThrowsArgumentException()
		{
			//Arrange
			var sut = new[] { GetNeuron(TestId1), GetNeuron(TestId2), GetNeuron(TestId2) };

			//Act
			Action action = () => new NeuralLayer(sut);

			//Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Indexer_Id_CorrectNeuron()
		{
			//Arrange
			var sut = new NeuralLayer(GetNeuron(TestId1), GetNeuron(TestId2));

			//Act
			var result = sut[TestId2];

			//Assert
			Assert.Equal(TestId2, result.Id);
		}
	}
}