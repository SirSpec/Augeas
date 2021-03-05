namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
    public interface IActivationFunction
    {
        double CalculateOutput(double input);
    }
}