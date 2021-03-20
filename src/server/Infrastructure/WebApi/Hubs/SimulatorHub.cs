using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Hermes.Infrastructure.WebApi
{
    public class SimulatorHub : Hub
    {
        private static Simulator simulator = new Simulator();

        public async Task GetAngle(int index, double[] sensors)
        {
            var angle = simulator.GetAngle(index, sensors);
            await Clients.Caller.SendAsync("ReceiveAngle", index, angle);
        }

        public async Task SetFitness(int index, double fitness)
        {
            simulator.SetFitness(index, fitness);
            await Clients.Caller.SendAsync("ReceiveConfirmation", true);
        }

        public async Task GenerateNewPopulation()
        {
            simulator.GenerateNewPopulation();
            await Clients.Caller.SendAsync("ReceiveNewPopulation", true);
        }
    }
}