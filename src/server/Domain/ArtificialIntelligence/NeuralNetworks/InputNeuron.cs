namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
    public class InputNeuron : INeuron
    {
        public double Input { get; set; }

        public double Output => Input;
    }
}