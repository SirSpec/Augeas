using System;
using System.Collections.Generic;
using System.Linq;

namespace Hermes.Domain.ArtificialIntelligence.NeuralNetworks
{
    // Neuron Input
    public class Dendrite
    {
        public double Weight { get; set; }
        public double Value { get; set; }
    }

    public class Neuron
    {
        private readonly IPropagationFunction propagationFunction;
        private readonly IActivationFunction activationFunction;
        private readonly double bias;
        private double[] weights;

        public Neuron(IActivationFunction activationFunction, double bias, double[] weights)
        {
            this.activationFunction = activationFunction;
            this.bias = bias;
            this.weights = weights;
        }

        private readonly Dendrite[] dendrites;

        public double Output => activationFunction.CalculateOutput(Activation + bias);

        private double Activation => dendrites.Sum(dendrite => dendrite.Weight * dendrite.Value);

        public double GetOutput(double[] inputs)
        {
            var activation = 0.0;

            for (int i = 0; i < weights.Length; i++)
            {
                activation += weights[i] * inputs[i];
            }

            return activationFunction.CalculateOutput(activation);
        }

        public Dendrite this[int index]
        {
            get => dendrites[index];
            set => dendrites[index] = value;
        }
    }

    public class NeuralLayer
    {
        public Neuron[] Neurons;

        public NeuralLayer(Neuron[] neurons)
        {
            Neurons = neurons;
        }

        public double[] GetOutputs(double[] inputs)
        {
            return Neurons.Select(neuron => neuron.GetOutput(inputs)).ToArray();
        }
    }

    public class NeuralNetwork
    {
        public NeuralLayer[] Layers;

        public NeuralNetwork(NeuralLayer[] Layers)
        {
            this.Layers = Layers;
        }

        public double[] Comput(double[] inputs)
        {
            var output = Layers.First().GetOutputs(inputs);
            foreach (var item in Layers)
            {
                output = item.GetOutputs(output);
            }

            return output;
        }
    }
}