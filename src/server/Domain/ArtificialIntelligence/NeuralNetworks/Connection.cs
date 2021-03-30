namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
	public class Connection
	{
		private readonly INeuron connectedNeuron;

		public Connection(INeuron connectedNeuron, double weight)
		{
			this.connectedNeuron = connectedNeuron;
			Weight = weight;
		}

		public double Value => connectedNeuron.Output;
		public double Weight { get; set; }
	}
}