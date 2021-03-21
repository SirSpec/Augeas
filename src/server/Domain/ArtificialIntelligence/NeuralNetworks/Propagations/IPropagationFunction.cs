using System.Collections.Generic;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks.Propagations
{
    public interface IPropagationFunction
    {
        double CalculateInput(IEnumerable<Connection> inputs);
    }
}