namespace Augeas.Domain.ArtificialIntelligence.NeuralNetworks.Activations
{
	public class BinaryStepFunction : IActivationFunction
	{
		private readonly double threshold;

		public BinaryStepFunction(double threshold) =>
			this.threshold = threshold;

		public double CalculateOutput(double input) =>
			input >= threshold
				? 1.0
				: 0.0;
	}
}