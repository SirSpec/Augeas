using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm;

namespace Hermes.Infrastructure.WebApi
{
    public class Manager
    {
        private readonly GenerticAlgorithmEngine<double> engine;
        private readonly Actor[] actors;

        public Manager()
        {
            actors = new[]
            {
                new Actor(null)
            };
        }

        public double GetAngle(int index, double[] signals) =>
            actors[index].GetAngle(signals);

        public double SetFitness(int index, double fitness) =>
            engine.SetFitness(index);

        public void GenerateNewPopulation()
        {
            
        }
    }
}
