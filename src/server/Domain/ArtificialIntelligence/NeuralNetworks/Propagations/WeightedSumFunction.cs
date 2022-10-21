using System.Collections.Generic;
using System.Linq;

namespace Augeas.Domain.ArtificialIntelligence.NeuralNetworks.Propagations
{
	public class WeightedSumFunction : IPropagationFunction
	{
		public double CalculateInput(IEnumerable<Connection> inputs) =>
			inputs.Sum(input => input.Weight * input.Value);
	}
}