using System;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
	public class Tensor
	{
		//NDimensionalArray
		private double[] values;
		public DopeVector DopeVector { get; }

		public Tensor(params int[] shape)
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
					: throw new ArgumentException($"Rank of {nameof(indices)} is not equal: {DopeVector.Rank}.");
			}
			set
			{
				values[GetOffset(indices)] = indices.Length == DopeVector.Rank
					? value
					: throw new ArgumentException($"Rank of {nameof(indices)} is not equal: {DopeVector.Rank}.");
			}
		}

		public static Tensor operator +(Tensor first, Tensor second) =>
			PerformElementWiseOperation(first, second, (firstValue, secondValue) => firstValue + secondValue);

		public static Tensor operator -(Tensor first, Tensor second) =>
			PerformElementWiseOperation(first, second, (firstValue, secondValue) => firstValue - secondValue);

		public static Tensor operator *(Tensor first, Tensor second) =>
			PerformElementWiseOperation(first, second, (firstValue, secondValue) => firstValue * secondValue);

		public static Tensor operator /(Tensor first, Tensor second) =>
			PerformElementWiseOperation(first, second, (firstValue, secondValue) => firstValue / secondValue);

		private static Tensor PerformElementWiseOperation(Tensor first, Tensor second, Func<double, double, double> operation)
		{
			if (first.DopeVector.Rank == second.DopeVector.Rank)
			{
				var result = new Tensor(first.DopeVector.Shape);

				for (int i = 0; i < result.DopeVector.Length; i++)
					result.values[i] = operation(first.values[i], second.values[i]);

				return result;
			}
			else throw new ArgumentException(
				$"Inputs have different Ranks:{first.DopeVector.Rank}|{second.DopeVector.Rank}.");
		}

		private int GetOffset(params int[] indices)
		{
			var index = 0;

			for (int i = 0; i < indices.Length; i++)
				index += indices[i] * DopeVector.Strides[i];

			return index;
		}
	}

	// public class Operations
	// {
	// 	//not dot product
	// 	public Tensor Dot(Tensor first, Tensor second)
	// 	{
	// 		if (first.values.DopeVector.Shape[1] != second.values.DopeVector.Shape[0])
	// 		{
	// 			throw new Exception("first->Cols must be equal to second->Rows");
	// 		}

	// 		var m = first.values.DopeVector.Shape[0];
	// 		var q = second.values.DopeVector.Shape[1];
	// 		var n = first.values.DopeVector.Shape[1];
	// 		var result = new Tensor(m, q);
	// 		for (var i = 0; i < m; i++)
	// 		{
	// 			for (var j = 0; j < q; j++)
	// 			{
	// 				result[i, j] = 0;
	// 				for (var k = 0; k < n; k++)
	// 				{
	// 					result[i, j] += first[i, k] * second[k, j];
	// 				}
	// 			}
	// 		}

	// 		return result;
	// 	}

	// 	public Tensor Exp(Tensor x)
	// 	{
	// 		var result = new Tensor(x.values.DopeVector.Shape);
	// 		for (int i = 0; i < x.values.DopeVector.Length; i++)
	// 		{
	// 			result[i] = Math.Exp(x[i]);
	// 		}

	// 		return result;
	// 	}

	// 	public Tensor Log(Tensor x)
	// 	{
	// 		var result = new Tensor(x.values.DopeVector.Shape);
	// 		for (int i = 0; i < x.values.DopeVector.Length; i++)
	// 		{
	// 			result[i] = Math.Log(x[i]);
	// 		}

	// 		return result;
	// 	}

	// 	public Tensor Sqrt(Tensor x)
	// 	{
	// 		var result = new Tensor(x.values.DopeVector.Shape);
	// 		for (int i = 0; i < x.values.DopeVector.Length; i++)
	// 		{
	// 			result[i] = Math.Sqrt(x[i]);
	// 		}

	// 		return result;
	// 	}

	// 	public Tensor Square(Tensor x)
	// 	{
	// 		var result = new Tensor(x.values.DopeVector.Shape);
	// 		for (int i = 0; i < x.values.DopeVector.Length; i++)
	// 		{
	// 			result[i] = Math.Pow(x[i], 2);
	// 		}

	// 		return result;
	// 	}

	// 	/// Transpose the matrix which is formed by turning all the rows of first given matrix into columns and vice-versa.
	// 	public Tensor Transpose(Tensor x)
	// 	{
	// 		Tensor result = new Tensor(x.values.DopeVector.Shape[1], x.values.DopeVector.Shape[0]);

	// 		for (int i = 0; i < x.values.DopeVector.Shape[1]; i++)
	// 		{
	// 			for (int j = 0; j < x.values.DopeVector.Shape[0]; j++)
	// 			{
	// 				result[i, j] = x[j, i];
	// 			}
	// 		}

	// 		return result;
	// 	}
	// }
}
