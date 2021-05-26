using System;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
	public static class TensorExtensions
	{
		private const int RowDimention = 0;
		private const int ColumnDimention = 1;

		public static Tensor MatrixMultiplicationBy(this Tensor first, Tensor second)
		{
			if (first.IsMatrix && second.IsMatrix && first.DopeVector.Shape[ColumnDimention] == second.DopeVector.Shape[RowDimention])
			{
				var resultRows = first.DopeVector.Shape[RowDimention];
				var resultColumns = second.DopeVector.Shape[ColumnDimention];

				var result = new Tensor(resultRows, resultColumns);

				for (int row = 0; row < resultRows; row++)
					for (int column = 0; column < resultColumns; column++)
						for (int i = 0; i < first.DopeVector.Shape[ColumnDimention]; i++)
							result[row, column] += first[row, i] * second[i, column];

				return result;
			}
			else throw new ArgumentException();
		}

		public static Tensor TransposeMatrtix(this Tensor tensor)
		{
			if (tensor.IsMatrix)
			{
				var result = new Tensor(tensor.DopeVector.Shape[ColumnDimention], tensor.DopeVector.Shape[RowDimention]);

				for (int i = 0; i < tensor.DopeVector.Shape[ColumnDimention]; i++)
					for (int j = 0; j < tensor.DopeVector.Shape[RowDimention]; j++)
						result[i, j] = tensor[j, i];

				return result;
			}
			else throw new ArgumentException();
		}
	}
}
