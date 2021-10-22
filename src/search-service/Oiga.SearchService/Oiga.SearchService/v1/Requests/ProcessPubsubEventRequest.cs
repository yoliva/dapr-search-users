using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Oiga.Common;
using Oiga.Common.Exceptions;
using Oiga.Events.Enums;
using Oiga.SearchService.Data;
using Oiga.SearchService.Data.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oiga.SearchService.v1.Requests
{
    public class ProcessPubsubEventRequest:IRequest<Unit>
    {
        public dynamic Data { get; set; }
    }

    public class ProcessPubsubEventRequestHandler : IRequestHandler<ProcessPubsubEventRequest, Unit>
    {
        private readonly SearchServiceDbContext context;
        private readonly ILogger<ProcessPubsubEventRequestHandler> logger;

        public ProcessPubsubEventRequestHandler(SearchServiceDbContext context, ILogger<ProcessPubsubEventRequestHandler> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<Unit> Handle(ProcessPubsubEventRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Event received!!!!!");

            Validate(request);

            await context.UserEvents.AddAsync(new UserEvent
            {
                CreatedDateUtc = DateTime.UtcNow,
                EvtTopic = Topics.UserCreated,
                ID = Guid.NewGuid().ToString("N"),
                Data = JsonConvert.SerializeObject(request.Data)
            }, cancellationToken);

            await context.UsersData.AddAsync(new UserData
            {
                ID = Guid.NewGuid().ToString("N"),
                FullName = request.Data.FullName,
                Username = request.Data.Username
            }, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Event data persisted");
            
            return Unit.Value;
        }

        private void Validate(ProcessPubsubEventRequest request)
        {
            if (request.Data is null)
                throw new BadRequestExcpetion((int)ErrorCode.InvalidRequestReceived, "Event payload can not be null");
        }
    }
}
