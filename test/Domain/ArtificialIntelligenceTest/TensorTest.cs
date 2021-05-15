using System;
using Hermes.Domain.ArtificialIntelligence.NeuralNetworks;
using Xunit;

namespace ArtificialIntelligenceTest
{
	public class TensorTest
	{
		[Fact]
		public void Elements_SingleDimentionalTensorWithMultiDimentionalShape_SingleElement()
		{
			//Arrange
			var dimention = 1;
			var sut = new Tensor(dimention, dimention, dimention);

			//Act
			var result = sut.values.DopeVector.Length;

			//Assert
			Assert.Equal(dimention * dimention * dimention, result);
		}

		[Fact]
		public void Elemets_MultiDimentionalShape_MultipliedDimentions()
		{
			//Arrange
			var dimention1 = 3;
			var dimention2 = 4;
			var dimention3 = 2;
			var sut = new Tensor(dimention1, dimention2, dimention3);

			//Act
			var result = sut.values.DopeVector.Length;

			//Assert
			Assert.Equal(dimention1 * dimention2 * dimention3, result);
		}

		[Fact]
		public void SumOperator_EqualDimention_CorrectSum()
		{
			//Arrange
			var dimention1 = 6;
			var dimention2 = 4;
			var dimention3 = 2;
			var sut1 = new Tensor(dimention1, dimention2, dimention3);
			var sut2 = new Tensor(dimention1, dimention2, dimention3);

			//Act
			var result = sut1 + sut2;

			//Assert
			// Assert.Equal(dimention1 * dimention2 * dimention3, result);
		}

		[Fact]
		public void GetIndexer_EqualDimention_CorrectSum()
		{
			//Arrange
			var dimention1 = 6;
			var dimention2 = 4;
			var dimention3 = 2;
			var sut = new Tensor(dimention1, dimention2, dimention3);

			//Act
			var result = sut[2, 0, 1];

			//Assert
			// Assert.Equal(dimention1 * dimention2 * dimention3, result);
		}
	}
}
