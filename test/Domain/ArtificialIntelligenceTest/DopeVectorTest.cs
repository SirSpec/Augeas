using System;
using System.Linq;
using Augeas.Domain.ArtificialIntelligence;
using Xunit;

namespace ArtificialIntelligenceTest
{
	public class DopeVectorTest
	{
		[Fact]
		public void Contructor_EmptyShape_ArgumentException()
		{
			//Arrange Act
			Action action = () => new DopeVector(Enumerable.Empty<int>().ToArray());

			//Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Contructor_ZeroDimention_ArgumentException()
		{
			//Arrange Act
			Action action = () => new DopeVector(new int[] { 0 });

			//Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Rank_1DShape_1()
		{
			//Arrange
			var sut = new DopeVector(new int[] { 5 });

			//Act
			var result = sut.Rank;

			//Assert
			Assert.Equal(1, result);
		}

		[Fact]
		public void Rank_3DShape_3()
		{
			//Arrange
			var sut = new DopeVector(new int[] { 2, 3, 4 });

			//Act
			var result = sut.Rank;

			//Assert
			Assert.Equal(3, result);
		}

		[Fact]
		public void Length_3DShape_MultipliedDimentions()
		{
			//Arrange
			var dimention1 = 2;
			var dimention2 = 3;
			var dimention3 = 4;
			var sut = new DopeVector(new int[] { dimention1, dimention2, dimention3 });

			//Act
			var result = sut.Length;

			//Assert
			Assert.Equal(dimention1 * dimention2 * dimention3, result);
		}

		[Fact]
		public void Strides_4DShape_ContiguousStrides()
		{
			//Arrange
			var dimention1 = 2;
			var dimention2 = 3;
			var dimention3 = 4;
			var dimention4 = 5;
			var sut = new DopeVector(new int[] { dimention1, dimention2, dimention3, dimention4 });
			int[] expected = new[] { dimention4 * dimention3 * dimention2, dimention4 * dimention3, dimention4, 1 };

			//Act
			var result = sut.Strides;

			//Assert
			Assert.Equal(expected, result);
		}
	}
}
