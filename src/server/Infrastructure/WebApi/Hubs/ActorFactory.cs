using Hermes.Domain.ArtificialIntelligence.NeuralNetworks;

namespace Hermes.Infrastructure.WebApi
{
    public class ActorFactory
    {
        public static Actor GetActor()
        {
            var build = new NeuralNetworkBuilder();

            return new Actor(build.Build(6, 1, 2));
        }
    }
}