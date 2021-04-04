namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
	public class Connection
	{
		private readonly INeuron connectedNeuron;

		public double Weight { get; set; }

		public Connection(INeuron connectedNeuron, double weight)
		{
			this.connectedNeuron = connectedNeuron;
			Weight = weight;
		}

		public string Id =>
			connectedNeuron.Id;

		public double Value =>
			connectedNeuron.Output;
	}
}