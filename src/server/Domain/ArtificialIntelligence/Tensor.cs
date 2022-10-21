using System;
using System.Collections.Generic;

namespace Augeas.Domain.ArtificialIntelligence
{
	public class Tensor
	{
		private readonly double[] values;
		private readonly DopeVector dopeVector;

		public Tensor(params int[] shape)
		{
			dopeVector = new DopeVector(shape);
			values = new double[dopeVector.Length];
		}

		public IReadOnlyList<int> Shape => dopeVector.Shape;
		public bool IsMatrix => dopeVector.Rank == 2;

		public double this[params int[] indices]
		{
			get => indices.Length == dopeVector.Rank
				? values[GetOffset(indices)]
				: throw new ArgumentException($"Rank of {nameof(indices)} is not equal: {dopeVector.Rank}.");
			set => values[GetOffset(indices)] = indices.Length == dopeVector.Rank
				? value
				: throw new ArgumentException($"Rank of {nameof(indices)} is not equal: {dopeVector.Rank}.");
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
			if (first.dopeVector.Rank == second.dopeVector.Rank)
			{
				var result = new Tensor(first.dopeVector.Shape);

				for (int i = 0; i < result.dopeVector.Length; i++)
					result.values[i] = operation(first.values[i], second.values[i]);

				return result;
			}
			else throw new ArgumentException(
				$"Inputs have different Ranks:{first.dopeVector.Rank}|{second.dopeVector.Rank}.");
		}

		private int GetOffset(params int[] indices)
		{
			var index = 0;

			for (int i = 0; i < indices.Length; i++)
				index += indices[i] * dopeVector.Strides[i];

			return index;
		}
	}
}
