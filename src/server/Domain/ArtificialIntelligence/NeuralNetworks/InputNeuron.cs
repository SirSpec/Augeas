namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
	public class InputNeuron : INeuron
	{
		public string Id { get; }
		public double Input { get; set; }

		public InputNeuron(string id) =>
			Id = id;

		public double Output => Input;
	}
}