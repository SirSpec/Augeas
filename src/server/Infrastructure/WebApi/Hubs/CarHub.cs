using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hermes.Domain.ArtificialIntelligence;
using Hermes.Domain.ArtificialIntelligence.GenerticAlgorithm;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hermes.Infrastructure.WebApi
{
    public class CarHub : Hub
    {
        private static IEnumerable<(string Id, Actor actor)> actors = new List<(string Id, Actor actor)>
        {
            ("1", new Actor()),
        };

        public async Task GetAngle(string id, double[] sensors)
        {
            var actor = actors.First(p => p.Id == id).actor;

            var angle = actor.GetAngle(sensors);

            await Clients.Caller.SendAsync("ReceiveAngle", id, angle);
        }
    }
}
