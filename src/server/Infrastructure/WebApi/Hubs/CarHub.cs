using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Hermes.Infrastructure.WebApi
{
    public class CarHub : Hub
    {
        private static Manager manager = new Manager();

        public async Task GetAngle(int index, double[] sensors)
        {
            var angle = manager.GetAngle(index, sensors);
            await Clients.Caller.SendAsync("ReceiveAngle", index, angle);
        }

        public async Task SetFitness(int index, double fitness)
        {
            manager.SetFitness(index, fitness);
            await Clients.Caller.SendAsync("ReceiveConfirmation", true);
        }

        public async Task GenerateNewPopulation()
        {
            manager.GenerateNewPopulation();
            await Clients.Caller.SendAsync("ReceiveConfirmation", true);
        }
    }
}