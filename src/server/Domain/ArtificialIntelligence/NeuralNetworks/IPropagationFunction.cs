using System.Collections.Generic;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
    public interface IPropagationFunction
    {
        double CalculateInput(IEnumerable<Connection> inputs);
    }
}