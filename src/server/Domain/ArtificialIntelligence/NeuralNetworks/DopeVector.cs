using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
	public record DopeVector
	{
		public int[] Shape { get; }
		public int Length { get; }
		public int Rank { get; }
		public int[] Stride { get; }

		public DopeVector(params int[] shape)
		{
			Shape = shape;
			Rank = Shape.Length;
			Length = Shape.Aggregate((accumulator, next) => accumulator * next);
			Stride = CalculateContiguousStride(Shape);
		}

		private static int[] CalculateContiguousStride(int[] shape)
		{
			var acc = 1;
			var stride = new int[shape.Length];

			for (var i = shape.Length - 1; i >= 0; --i)
			{
				stride[i] = acc;
				acc *= shape[i];
			}

			return stride;
		}
	}
}
