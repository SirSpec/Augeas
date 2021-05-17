using System;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
	public class NDimensionalArray
	{
		private double[] values;
		public DopeVector DopeVector { get; }

		public NDimensionalArray(params int[] shape)
		{
			DopeVector = new DopeVector(shape);
			values = new double[DopeVector.Length];
		}

		public double this[params int[] indices]
		{
			get
			{
				return indices.Length == DopeVector.Rank
					? values[GetOffset(indices)]
					: throw new ArgumentOutOfRangeException($"Rank of {nameof(indices)} is not equal: {DopeVector.Rank}.");
			}
			set
			{
				values[GetOffset(indices)] = indices.Length == DopeVector.Rank
					? value
					: throw new ArgumentOutOfRangeException($"Rank of {nameof(indices)} is not equal: {DopeVector.Rank}.");
			}
		}

		private int GetOffset(params int[] indices)
		{
			var index = 0;

			for (int i = 0; i < indices.Length; i++)
				index += indices[i] * DopeVector.Strides[i];

			return index;
		}
	}
}
