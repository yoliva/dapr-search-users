using MediatR;
using Microsoft.Extensions.Logging;
using Oiga.UserService.Data.Entities;
using Oiga.UserService.v1.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Oiga.Common.Exceptions;
using Oiga.Common;

namespace Oiga.UserService.v1.Requests
{
    public class GetUserProfileRequest : IRequest<UserProfileDto>
    {
        public string Username { get; set; }
    }

    public class GetUserProfileRequestHanlder : IRequestHandler<GetUserProfileRequest, UserProfileDto>
    {
        private readonly UsersServiceDbContext context;
        private readonly ILogger<GetUserProfileRequest> logger;

        public GetUserProfileRequestHanlder(UsersServiceDbContext context, ILogger<GetUserProfileRequest> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Task<UserProfileDto> Handle(GetUserProfileRequest request, CancellationToken cancellationToken)
        {
            Validate(request);

            var usr = context.Users.SingleOrDefault(x => x.Username == request.Username);

            if (usr is null)
                throw new NotFoundException((int)ErrorCode.InvalidRequestReceived, "usr not found");

            return Task.FromResult(UserProfileDto.FromUser(usr));
        }

        private void Validate(GetUserProfileRequest request)
        {
            if (string.IsNullOrEmpty(request.Username))
                throw new BadRequestExcpetion((int)ErrorCode.InvalidRequestReceived, nameof(request.Username));
        }
    }
}
