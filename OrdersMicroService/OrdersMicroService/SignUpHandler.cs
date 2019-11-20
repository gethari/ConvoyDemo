using System;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using Microsoft.Extensions.Logging;

namespace OrdersMicroService
{
	public class SignUpHandler : ICommandHandler<SignUp>
	{
		private readonly ILogger<SignUpHandler> _logger;
		private readonly IBusPublisher _publisher;

		public SignUpHandler(ILogger<SignUpHandler> logger, IBusPublisher publisher)
		{
			_logger = logger;
			_publisher = publisher;
		}
		public async Task HandleAsync(SignUp command)
		{
			await _publisher.PublishAsync(new SignUpComplete(Guid.NewGuid()));
		}
	}
}
