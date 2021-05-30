using System;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
	public static class MatrixExtensions
	{
		private const int RowDimention = 0;
		private const int ColumnDimention = 1;

		public static Tensor MatrixMultiplicationBy(this Tensor first, Tensor second)
		{
			if (first.IsMatrix && second.IsMatrix && first.Shape[ColumnDimention] == second.Shape[RowDimention])
			{
				var resultRows = first.Shape[RowDimention];
				var resultColumns = second.Shape[ColumnDimention];

				var result = new Tensor(resultRows, resultColumns);

				for (int row = 0; row < resultRows; row++)
					for (int column = 0; column < resultColumns; column++)
						for (int i = 0; i < first.Shape[ColumnDimention]; i++)
							result[row, column] += first[row, i] * second[i, column];

				return result;
			}
			else throw new ArgumentException(
				"The number of columns in the first matrix must be equal to the number of rows in the second matrix.");
		}

		public static Tensor TransposeMatrix(this Tensor tensor)
		{
			if (tensor.IsMatrix)
			{
				var result = new Tensor(tensor.Shape[ColumnDimention], tensor.Shape[RowDimention]);

				for (int i = 0; i < tensor.Shape[ColumnDimention]; i++)
					for (int j = 0; j < tensor.Shape[RowDimention]; j++)
						result[i, j] = tensor[j, i];

				return result;
			}
			else throw new ArgumentException($"Input {nameof(tensor)} is not a matrix.");
		}
	}
}
