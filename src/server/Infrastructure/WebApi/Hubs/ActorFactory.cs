using Augeas.Domain.ArtificialIntelligence.NeuralNetworks.Builders;

namespace Augeas.Infrastructure.WebApi
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