using System;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
    public class HyperbolicTangentFunction : IActivationFunction
    {
        public double CalculateOutput(double input) =>
            Math.Tanh(input);
    }
}