using System.Collections.Generic;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
    public class Neuron : INeuron
    {
        private readonly IPropagationFunction propagationFunction;
        private readonly IActivationFunction activationFunction;
        public readonly IList<Connection> inputs;
        private readonly double bias;

        public Neuron(
            IPropagationFunction propagationFunction,
            IActivationFunction activationFunction,
            IList<Connection> inputs,
            double bias)
        {
            this.propagationFunction = propagationFunction;
            this.activationFunction = activationFunction;
            this.bias = bias;
            this.inputs = inputs;
        }

        public Neuron(
            IPropagationFunction propagationFunction,
            IActivationFunction activationFunction,
            double bias)
            : this(propagationFunction, activationFunction, new List<Connection>(), bias)
        {
        }

        public double Output => activationFunction.CalculateOutput(Activation + bias);

        public void AddInput(Connection connection)
        {
            inputs.Add(connection);
        }

        private double Activation => propagationFunction.CalculateInput(inputs);
    }
}