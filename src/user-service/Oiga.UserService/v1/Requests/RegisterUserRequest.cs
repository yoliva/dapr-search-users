using Dapr.Client;
using MediatR;
using Microsoft.Extensions.Logging;
using Oiga.Events.Enums;
using Oiga.UserService.Data.Entities;
using Oiga.UserService.v1.Models;
using Oiga.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using Oiga.Common;
using Oiga.UserService.Configurations;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace Oiga.UserService.v1.Requests
{
    public class RegisterUserRequest : IRequest<UserDataDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
    }

    public class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest, UserDataDto>
    {
        private readonly UsersServiceDbContext context;
        private readonly IHttpClientFactory factory;
        private readonly DaprClient client;
        private readonly IOptions<MessageBrokerConfig> brokerConfig;
        private readonly ILogger<RegisterUserRequest> logger;

        public RegisterUserRequestHandler(UsersServiceDbContext context, IHttpClientFactory factory, IOptions<MessageBrokerConfig> brokerConfig, ILogger<RegisterUserRequest> logger)
        {
            this.context = context;
            this.factory = factory;
            this.brokerConfig = brokerConfig;
            this.logger = logger;
        }

        public async Task<UserDataDto> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"{nameof(RegisterUserRequest)} received");

            Validate(request);

            var usr = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username
            };

            logger.LogInformation($"Attempt to persist user to DB. Id:{usr.ID}");
            await context.AddAsync(usr, cancellationToken);
            await context.SaveChangesAsync();

            var client = factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(usr), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{brokerConfig.Value.Uri}/{brokerConfig.Value.Topic}", content);
            
            logger.LogInformation($"Event published with {response.StatusCode} status response");

            return UserDataDto.FromUser(usr);
        }

        private void Validate(RegisterUserRequest request)
        {
            logger.LogInformation("Validating input request");

            if (string.IsNullOrWhiteSpace(request.FirstName))
                throw new BadRequestExcpetion((int)ErrorCode.InvalidRequestReceived, $"{nameof(request.FirstName)} must be provided");

            if (string.IsNullOrWhiteSpace(request.LastName))
                throw new BadRequestExcpetion((int)ErrorCode.InvalidRequestReceived, $"{nameof(request.LastName)} must be provided");

            if (string.IsNullOrWhiteSpace(request.Username))
                throw new BadRequestExcpetion((int)ErrorCode.InvalidRequestReceived, $"{nameof(request.FirstName)} must be provided");

            logger.LogInformation("Validation succeed");
        }
    }
}
