using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Oiga.Common;
using Oiga.Common.Exceptions;
using Oiga.SearchService.Configurations;
using Oiga.SearchService.Data;
using Oiga.SearchService.Models;
using Oiga.SearchService.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Oiga.Common.Pagination.Helpers;

namespace Oiga.SearchService.v1.Requests
{
    public class SearchUserRequest : IRequest<PaginatedResult<UserDto>>
    {
        public string ContinuationToken { get; internal set; }
        public int Limit { get; internal set; }
        public string SearchExpression { get; internal set; }
    }

    public class SearchUserRequestHandler : IRequestHandler<SearchUserRequest, PaginatedResult<UserDto>>
    {
        private readonly SearchServiceDbContext context;
        private readonly IInputTokenizerService tokenizer;
        private readonly ILogger<SearchUserRequestHandler> logger;

        public SearchUserRequestHandler(SearchServiceDbContext context, IInputTokenizerService tokenizer, ILogger<SearchUserRequestHandler> logger)
        {
            this.context = context;
            this.tokenizer = tokenizer;
            this.logger = logger;
        }

        public async Task<PaginatedResult<UserDto>> Handle(SearchUserRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Request {nameof(SearchUserRequest)} received");

            Validate(request);

            var tokens = tokenizer.Tokenize(request.SearchExpression);

            var query = context.UsersData.AsQueryable();

            //foreach (var token in tokens)
            //{
            //    query = query.Where(ud => tokens.Any(token => ud.FullName.Contains(token) || ud.Username.Contains(token)));
            //}

            query = query.OrderBy(ud => ud.FullName).ThenBy(ud => ud.Username);

            var continuationToken = request.ContinuationToken is null ? null : Decode(request.ContinuationToken);
            var response = continuationToken is null
                ? await query.Take(request.Limit).ToListAsync(cancellationToken)
                : await query.Skip(continuationToken.SkipCount).Take(continuationToken.Limit).ToListAsync(cancellationToken);

            logger.LogInformation($"Request {nameof(SearchUserRequest)} completed");

            return new PaginatedResult<UserDto>
            {
                ContinuationToken = Encode(request.Limit, continuationToken is null ? response.Count : continuationToken.SkipCount + response.Count),
                Data = response.Select(ud => new UserDto
                {
                    FullName = ud.FullName,
                    Username = ud.Username
                })
            };
        }

        private void Validate(SearchUserRequest request)
        {
            if (request.Limit <= 0 || request.Limit > SearchConstants.MAX_USERS_RESULT)
                throw new BadRequestExcpetion((int)ErrorCode.InvalidRequestReceived, $"Invalid {nameof(request.Limit)}");
        }
    }
}
