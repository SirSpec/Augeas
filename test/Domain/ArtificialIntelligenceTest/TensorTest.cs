using System;
using Hermes.Domain.ArtificialIntelligence;
using Xunit;

namespace ArtificialIntelligenceTest
{
	public class TensorTest
	{
		[Fact]
		public void IsMatrix_Rank2Tensor_True()
		{
			//Arrange
			var dimention = 1;
			var sut = new Tensor(dimention, dimention);

			//Act
			var result = sut.IsMatrix;

			//Assert
			Assert.True(result);
		}

		[Fact]
		public void IsMatrix_Rank1Tensor_False()
		{
			//Arrange
			var dimention = 1;
			var sut = new Tensor(dimention);

			//Act
			var result = sut.IsMatrix;

			//Assert
			Assert.False(result);
		}

		[Fact]
		public void IsMatrix_Rank3Tensor_False()
		{
			//Arrange
			var dimention = 1;
			var sut = new Tensor(dimention, dimention, dimention);

			//Act
			var result = sut.IsMatrix;

			//Assert
			Assert.False(result);
		}
	}
}
