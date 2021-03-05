using System.Collections.Generic;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
    public interface IPropagationFunction
    {
        double CalculateInput(IEnumerable<(double Weight, double Value)> inputs);
    }
}