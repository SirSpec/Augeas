using System.Collections.Generic;

namespace Augeas.Domain.ArtificialIntelligence.NeuralNetworks.Propagations
{
	public interface IPropagationFunction
	{
		double CalculateInput(IEnumerable<Connection> inputs);
	}
}