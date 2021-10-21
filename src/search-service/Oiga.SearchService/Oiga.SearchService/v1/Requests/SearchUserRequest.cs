using MediatR;
using Oiga.SearchService.Models;
using System.Threading;
using System.Threading.Tasks;

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
        public Task<PaginatedResult<UserDto>> Handle(SearchUserRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
