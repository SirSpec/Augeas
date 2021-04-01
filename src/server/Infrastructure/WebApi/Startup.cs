using Hermes.Infrastructure.WebApi.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Hermes.Infrastructure.WebApi
{
	public class Startup
	{
		private readonly IConfiguration configuration;

		public Startup(IConfiguration configuration) =>
			this.configuration = configuration;

		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<SimulatorHubOptions>(
				configuration.GetSection(SimulatorHubOptions.SimulatorHub));

			services.AddCors();
			services.AddSignalR();
		}

		public void Configure(
			IApplicationBuilder app,
			IWebHostEnvironment env,
			IOptions<SimulatorHubOptions> simulatorHubOptions)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(builder =>
				builder.WithOrigins(simulatorHubOptions.Value.Url)
					.AllowAnyHeader()
					.WithMethods("GET", "POST")
					.AllowCredentials()
			);

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("Hermes");
				});

				endpoints.MapHub<SimulatorHub>("/hub");
			});
		}
	}
}
