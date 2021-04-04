using System;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks;
using Xunit;

namespace ArtificialIntelligenceTest
{
	public class InputLayerTest
	{
		private const string TestId1 = "tesdId1";
		private const string TestId2 = "tesdId2";

		[Fact]
		public void Constructor_DuplicatedNeuronIds_ThrowsArgumentException()
		{
			//Arrange
			var sut = new[] { new InputNeuron(TestId1), new InputNeuron(TestId2), new InputNeuron(TestId2) };

			//Act
			Action action = () => new InputLayer(sut);

			//Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Indexer_Id_CorrectNeuron()
		{
			//Arrange
			var sut = new InputLayer(new InputNeuron(TestId1), new InputNeuron(TestId2));

			//Act
			var result = sut[TestId2];

			//Assert
			Assert.Equal(TestId2, result.Id);
		}

		[Fact]
		public void PushInputs_EmptyInputLayerOneInput_ThrowsArgumentException()
		{
			//Arrange
			var sut = new InputLayer();

			//Act
			Action action = () => sut.PushInputs(double.MinValue);

			//Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void PushInputs_EqualNumberOfInputsAndNeurons_NeuronsOutputSet()
		{
			//Arrange
			const double FirstInput = 1d;
			const double SecondInput = 2d;
			var sut = new InputLayer(new InputNeuron(TestId1), new InputNeuron(TestId2));

			//Act
			sut.PushInputs(FirstInput, SecondInput);

			//Assert
			Assert.Equal(sut[0].Output, FirstInput);
			Assert.Equal(sut[1].Output, SecondInput);
			Assert.Equal(sut[TestId1].Output, FirstInput);
			Assert.Equal(sut[TestId2].Output, SecondInput);
		}
	}
}