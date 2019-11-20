using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.HTTP;
using Convey.Logging;
using Convey.MessageBrokers.Outbox;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace OrdersMicroService
{
	public class Program
	{
		public static async Task Main(string[] args)
			=> await WebHost.CreateDefaultBuilder(args)
				.ConfigureServices(services => services
					.AddConvey()
					.AddServices()
					.AddHttpClient()
					.AddWebApi()
					.AddCommandHandlers()
					.AddEventHandlers()
					.AddQueryHandlers()
					.AddInMemoryCommandDispatcher()
					.AddInMemoryEventDispatcher()
					.AddInMemoryQueryDispatcher()
					.Build())
				.Configure(app =>
				{
					app
						.UseErrorHandler()
						.UseInitializers()
						.UseRouting()
						.UseEndpoints(r => r.MapControllers())
						.UseDispatcherEndpoints(endpoints => endpoints
							.Get("", ctx => ctx.Response.WriteAsync("Orders Service"))
							.Get("ping", ctx => ctx.Response.WriteAsync("pong"))
							.Post<SignUp>("signup", (cmd, ctx) => ctx.Response.Created($"signup/{cmd.Id}")));
				})
				.UseLogging()
				.Build()
				.RunAsync();
	}
}
