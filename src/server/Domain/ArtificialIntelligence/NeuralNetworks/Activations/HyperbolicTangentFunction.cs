using System;

namespace Augeas.Domain.ArtificialIntelligence.NeuralNetworks.Activations
{
	public class HyperbolicTangentFunction : IActivationFunction
	{
		public double CalculateOutput(double input) =>
			Math.Tanh(input);
	}
}