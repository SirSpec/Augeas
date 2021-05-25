using System;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
	public static class TensorExtensions
	{
		public static Tensor MatrixMultiplicationBy(this Tensor first, Tensor second)
		{
			if (first.IsMatrix && second.IsMatrix && first.DopeVector.Shape[1] == second.DopeVector.Shape[0])
			{
				var resultRows = first.DopeVector.Shape[0];
				var resultColumns = second.DopeVector.Shape[1];

				var result = new Tensor(resultRows, resultColumns);

				for (int row = 0; row < resultRows; row++)
					for (int column = 0; column < resultColumns; column++)
						for (int i = 0; i < first.DopeVector.Shape[1]; i++)
							result[row, column] += first[row, i] * second[i, column];

				return result;
			}
			else throw new ArgumentException();
		}

		public static Tensor TransposeMatrtix(this Tensor tensor)
		{
			if (tensor.IsMatrix)
			{
				var result = new Tensor(tensor.DopeVector.Shape[1], tensor.DopeVector.Shape[0]);

				for (int i = 0; i < tensor.DopeVector.Shape[1]; i++)
					for (int j = 0; j < tensor.DopeVector.Shape[0]; j++)
						result[i, j] = tensor[j, i];

				return result;
			}
			else throw new ArgumentException();
		}
	}
}
