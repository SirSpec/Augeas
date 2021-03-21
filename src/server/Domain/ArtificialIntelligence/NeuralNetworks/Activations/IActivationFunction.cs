namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks.Activations
{
    public interface IActivationFunction
    {
        double CalculateOutput(double input);
    }
}