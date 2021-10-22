using Microsoft.Extensions.Logging;
using Moq;
using Oiga.Common.Tests;
using Oiga.SearchService.Data;
using Oiga.SearchService.v1.Requests;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Oiga.Common.Exceptions;
using System.Linq;
using System.Dynamic;

namespace Oiga.SearchService.UnitTests.v1.Requests
{
    public class ProcessPubsubEventRequestHandlerTests
    {
        private ProcessPubsubEventRequestHandler handler;
        private SearchServiceDbContext context;

        public ProcessPubsubEventRequestHandlerTests()
        {
            context = DbContextHelpers.GetInMemoryDbContext<SearchServiceDbContext>();
            var loggerMock = new Mock<ILogger<ProcessPubsubEventRequestHandler>>();

            handler = new ProcessPubsubEventRequestHandler(context, loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WithNullPayload_ShouldThrowBadRequest()
        {
            var request = new ProcessPubsubEventRequest();

            await Assert.ThrowsAsync<BadRequestExcpetion>(async () => await handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithValidPayload_ShouldStoreEventAndUserData()
        {
            dynamic payload = new ExpandoObject();
            payload.FullName = "Peter Parker";
            payload.Username = "peter.parker";
            payload.FirstName = "Peter";
            payload.LastName = "Parker";

            var request = new ProcessPubsubEventRequest
            {
                Data = payload
            };

            await handler.Handle(request, CancellationToken.None);

            Assert.Single(context.UserEvents);
            Assert.Single(context.UsersData);

            Assert.Equal("peter.parker", context.UsersData.Single().Username);
            Assert.Equal("Peter Parker", context.UsersData.Single().FullName);
        }
    }
}
