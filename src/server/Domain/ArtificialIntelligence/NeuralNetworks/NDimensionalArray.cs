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
					? values[GetIndex(indices)]
					: throw new ArgumentOutOfRangeException();
			}
			set
			{
				values[GetIndex(indices)] = indices.Length == DopeVector.Rank
					? value
					: throw new ArgumentOutOfRangeException();
			}
		}

		private int GetIndex(params int[] indices)
		{
			var index = 0;

			for (int i = 0; i < indices.Length; i++)
				index += indices[i] * DopeVector.Strides[i];

			return index;
		}
	}
}
