using System;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence
{
	// https://en.wikipedia.org/wiki/Dope_vector
	public record DopeVector
	{
		public int[] Shape { get; }
		public int Length { get; }
		public int Rank { get; }
		public int[] Strides { get; }

		public DopeVector(int[] shape)
		{
			if (shape.Length > 0 && shape.All(dimention => dimention > 0))
			{
				Shape = shape;
				Rank = Shape.Length;
				Length = Shape.Aggregate((accumulator, next) => accumulator * next);
				Strides = CalculateContiguousStrides(Shape);
			}
			else throw new ArgumentException(
				$"{nameof(shape)} is empty or contains non-positive dimension.");
		}

		private static int[] CalculateContiguousStrides(int[] shape)
		{
			var currentStride = 1;
			var strides = new int[shape.Length];

			for (var i = shape.Length - 1; i >= 0; i--)
			{
				strides[i] = currentStride;
				currentStride *= shape[i];
			}

			return strides;
		}
	}
}
