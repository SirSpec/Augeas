using System;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
	public class Tensor
	{
		private readonly double[] values;

		public Tensor(params int[] shape)
		{
			DopeVector = new DopeVector(shape);
			values = new double[DopeVector.Length];
		}

		public DopeVector DopeVector { get; }
		public bool IsMatrix => DopeVector.Rank == 2;

		public double this[params int[] indices]
		{
			get => indices.Length == DopeVector.Rank
				? values[GetOffset(indices)]
				: throw new ArgumentException($"Rank of {nameof(indices)} is not equal: {DopeVector.Rank}.");
			set => values[GetOffset(indices)] = indices.Length == DopeVector.Rank
				? value
				: throw new ArgumentException($"Rank of {nameof(indices)} is not equal: {DopeVector.Rank}.");
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
}
