using System.Collections.Generic;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
    public class WeightedSumFunction : IPropagationFunction
    {
        public double CalculateInput(IEnumerable<(double Weight, double Value)> inputs) =>
            inputs.Sum(input => input.Weight * input.Value);
    }
}